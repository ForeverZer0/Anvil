using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Anvil.Network.API;
using JetBrains.Annotations;

[assembly: CLSCompliant(true)]

namespace Anvil.Network;

///
/// <summary>
/// Concrete implementation of a <see cref="IServer{TIncoming,TOutgoing}"/>, providing functionality for accepting
/// client connections and asynchronously sending/emitting packets.
/// </summary>
/// <typeparam name="TIncoming">The 16-bit enum type used for incoming packet identifiers.</typeparam>
/// <typeparam name="TOutgoing">The 16-bit enum type used for outgoing packet identifiers.</typeparam>
[PublicAPI]
public class Server<TIncoming, TOutgoing> : NetworkEndPoint<TIncoming, TOutgoing>, IServer<TIncoming, TOutgoing>
    where TIncoming : unmanaged, Enum
    where TOutgoing : unmanaged, Enum
{
    /// <inheritdoc />
    public int MaxClients => config.MaxClients;

    /// <inheritdoc />
    public bool IsRunning { get; private set; }
    
    /// <summary>
    /// Gets an enumerator for clients that have initiated connection with the server, but are awaiting completion
    /// of the connection protocol and validation.
    /// </summary>
    /// <remarks>
    /// This enumerator is thread-safe.
    /// </remarks>
    protected IEnumerable<IConnection> PendingConnections
    {
        get
        {
            lock (pendingMutex)
            {
                foreach (var connection in pendingConnections)
                    yield return connection;
            }
        }
    }
    
    /// <inheritdoc />
    /// <remarks>This collection is thread-safe and read-only.</remarks>
    public ICollection<IConnection> Clients => connectedClients.Values;
    
    /// <inheritdoc />
    public int Port => config.Port;

    /// <inheritdoc />
    public IPAddress Host => config.Host;

    /// <summary>
    /// Creates a new instance of the <see cref="Server{TIncoming,TOutgoing}"/> class with the default configuration.
    /// </summary>
    public Server() : this(new ServerConfiguration())
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Server{TIncoming,TOutgoing}"/> class with the specified
    /// <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">
    /// A <see cref="ServerConfiguration"/> containing properties defining the desired behavior of the server.
    /// </param>
    public Server(ServerConfiguration configuration) : base(configuration)
    {
        config = configuration.Clone();

        connectedClients = new ConcurrentDictionary<int, IConnection>();
        pendingConnections = new HashSet<IConnection>();
        Socket.Bind(new IPEndPoint(Host, Port));
        pendingMutex = new object();
    }
    
    public Task StartAsync() => StartAsync(CancelSource.Token);
    
    /// <inheritdoc />
    public Task StartAsync(CancellationToken token)
    {
        var timeoutTask = MonitorTimeoutsAsync(token);
        var networkTask = ProcessNetworkAsync(new IPEndPoint(IPAddress.Any, Port), token);
        IsRunning = true;
        return Task.WhenAll(networkTask, timeoutTask);
    }

    /// <inheritdoc />
    protected override void ProcessPacket(EndPoint endPoint, PacketHeader header, IBinaryReader payload, int payloadSize)
    {
        var hash = HashCode.Combine(endPoint, header.Salt);
        if (connectedClients.TryGetValue(hash, out var connection))
        {
            if (header.Flags == PacketFlags.KeepAlive)
            {
                connection.LastReceive = DateTime.UtcNow;
                return;
            }
            if (header.Flags == PacketFlags.Disconnect)
            {
                ProcessDisconnect(connection, payload, payloadSize);
                return;
            }
            
            var packet = PacketManager.Factory(Direction.ServerBound, header.PacketType);
            packet.Deserialize(payload);

            var id = Unsafe.As<short, TIncoming>(ref header.PacketType);
            var data = new PacketData<TIncoming>(connection, id, packet);
            if (RealTimeEvents)
                EmitPacket(data);
            else
                PacketQueue.Enqueue(data);
        }

        switch (header.Flags)
        {
            case PacketFlags.ConnectionRequest:
                ProcessConnectionRequest(endPoint, header, payload, payloadSize);
                break;
            case PacketFlags.ChallengeReply:
                ProcessChallengeResponse(endPoint, header, payload, payloadSize);
                break;
            
        }
    }


    private void ProcessConnectionRequest(EndPoint endPoint, PacketHeader header, IBinaryReader reader, int length)
    {
        if (length != CONNECTION_PADDING)
            return;

        int serverSalt, clientSalt;
        lock (pendingMutex)
        {
            if (pendingConnections.Any(c => c.EndPoint.Equals(endPoint)))
                return;
            if (header.Salt != 0 || endPoint is not IPEndPoint ip)
                return;

            var protocol = reader.ReadInt32();
            serverSalt = Rand.Next();
            clientSalt = reader.ReadInt32();
            var connection = new Connection(ip, serverSalt, clientSalt);
            
            if (!IsProtocolSupported(protocol))
            {
                Disconnect(connection, DisconnectReason.UnsupportedProtocol);
                return;
            }
            
            pendingConnections.Add(connection);
        }

        SendChallengePacket(endPoint, serverSalt, clientSalt);
    }

    private void SendChallengePacket(EndPoint endPoint, int serverSalt, int clientSalt)
    {
        Span<byte> buffer = stackalloc byte[HEADER_SIZE + sizeof(int)];
        var challenge = new PacketHeader(0, PacketFlags.Challenge, serverSalt);
        MemoryMarshal.Write(buffer, ref challenge);
        BinaryPrimitives.WriteInt32LittleEndian(buffer[HEADER_SIZE..], clientSalt);
        Socket.SendTo(buffer, SocketFlags, endPoint);
    }

    private void ProcessChallengeResponse(EndPoint endPoint, PacketHeader header, IBinaryReader reader, int length)
    {
        if (length != CONNECTION_PADDING)
            return;
        
        IConnection? pending;
        lock (pendingMutex)
        {
            pending = pendingConnections.FirstOrDefault(c => c.EndPoint.Equals(endPoint));
            pendingConnections.RemoveWhere(c => c.EndPoint.Equals(endPoint));
        }
        
        if (pending is null)
            return;

        if (header.Salt != pending.Salt)
        {
            Disconnect(pending, DisconnectReason.Refused);
            return;
        }
        
        if (connectedClients.Count >= MaxClients)
        {
            Disconnect(pending, DisconnectReason.ServerFull);
            return;
        }

        var hash = HashCode.Combine(endPoint, header.Salt);
        if (!connectedClients.TryAdd(hash, pending))
            return;
        
        SendConnectionConfirmation(pending);
        OnConnected(pending);
    }

    private void SendConnectionConfirmation(IConnection connection)
    {
        var header = new PacketHeader(0, PacketFlags.ConnectionConfirm, connection.Salt);
        Span<byte> buffer = stackalloc byte[HEADER_SIZE];
        MemoryMarshal.Write(buffer, ref header);
        Socket.SendTo(buffer, SocketFlags, connection.EndPoint);
    }
    
    private void ProcessDisconnect(IConnection connection, IBinaryReader reader, int length)
    {
        
        
    }



    public void Stop()
    {
        IsRunning = false;
        CancelSource.Cancel();
        // TODO
    }

    /// <summary>
    /// Checks if the specified <paramref name="protocol"/> version is compatible with the server's protocol.
    /// </summary>
    /// <param name="protocol">A protocol version to query.</param>
    /// <returns><c>true</c> if protocol is supported, otherwise <c>false</c> if client should be refused.</returns>
    /// <remarks>
    /// The default implementation simply checks for equality of the specified protocol and the server's value.
    /// </remarks>
    protected virtual bool IsProtocolSupported(int protocol) => protocol == Protocol;
    
    /// <summary>
    /// Disconnects the specified <paramref name="connection"/> and sends a disconnect packet with optional message.
    /// </summary>
    /// <param name="connection">The connection to remove.</param>
    /// <param name="reason">A strongly-typed constant describing the general reason for the disconnection.</param>
    /// <param name="message">An optional text message to send with the disconnect packets.</param>
    public new void Disconnect(IConnection connection, DisconnectReason reason, string? message = null)
    {
        if (connectedClients.TryRemove(connection.GetHashCode(), out var dummy))
            base.Disconnect(connection, reason, message);
    }
    
    /// <summary>
    /// Asynchronously monitors for both active and pending connections that have not transmitted any data for the
    /// amount of time defined by <see cref="ServerConfiguration.ClientTimeout"/>.
    /// </summary>
    /// <param name="token">A cancellation token that can be used to cancel the asynchronous task.</param>
    protected virtual async Task MonitorTimeoutsAsync(CancellationToken token)
    {
        var duration = config.ClientTimeout;
        if (duration <= TimeSpan.Zero)
            return;
        
        await Task.Yield();
        
        var timeout = (int) duration.TotalMilliseconds;
        var list = new List<IConnection>();
        
        while (!token.IsCancellationRequested && IsRunning)
        {
            await Task.Delay(timeout, token);
            
            var now = DateTime.UtcNow;
            // Cannot modify while iterating, so store in temp list
            list.AddRange(connectedClients.Values.Where(c => now - c.LastReceive > duration));
            foreach (var client in list)
                Disconnect(client, DisconnectReason.Timeout);

            lock (pendingMutex)
            {
                pendingConnections.RemoveWhere(c => now - c.LastReceive > duration);
            }
        }
    }

    /// <inheritdoc />
    public void Send(IConnection connection, TOutgoing id, IPacket packet)
    {
        using var buffer = SerializePacket(id, packet, PacketFlags.None, connection.Salt);
        Socket.SendTo(buffer.AsSpan(), SocketFlags, connection.EndPoint);
    }
    
    /// <summary>
    /// Asynchronously sends a <paramref name="packet"/> to the specified <paramref name="connection"/>.
    /// </summary>
    /// <param name="connection">The connection used for communicating with the client.</param>
    /// <param name="id">The outgoing ID for client-bound packet.</param>
    /// <param name="packet">A <see cref="IPacket"/> instance containing the payload.</param>
    /// <returns>A <see cref="Task"/> that completes when the packet completes sending.</returns>
    public async Task SendAsync(IConnection connection, TOutgoing id, IPacket packet)
    {
        using var buffer = SerializePacket(id, packet, PacketFlags.None, connection.Salt);
        await Socket.SendToAsync(buffer.AsMemory(), SocketFlags, connection.EndPoint, CancelSource.Token);
    }

    /// <inheritdoc />
    public async Task SendAsync(IConnection connection, TOutgoing id, IPacket packet, CancellationToken token)
    {
        using var buffer = SerializePacket(id, packet, PacketFlags.None, connection.Salt);
        await Socket.SendToAsync(buffer.AsMemory(), SocketFlags, connection.EndPoint, token);
    }

    /// <inheritdoc />
    public void SendAll(TOutgoing id, IPacket packet)
    {
        foreach (var connection in connectedClients.Values)
        {
            using var buffer = SerializePacket(id, packet, PacketFlags.None, connection.Salt);
            Socket.SendTo(buffer.AsSpan(), SocketFlags, connection.EndPoint);
        }
    }

    /// <inheritdoc />
    public Task SendAllAsync(TOutgoing id, IPacket packet, CancellationToken token)
    {
        return Task.WhenAll(connectedClients.Values.Select(c => SendAsync(c, id, packet, token)));
    }
    
    /// <summary>
    /// Asynchronously broadcasts a <paramref name="packet"/> to all connected clients.
    /// </summary>
    /// <param name="id">The outgoing ID for client-bound packet.</param>
    /// <param name="packet">A <see cref="IPacket"/> instance containing the payload.</param>
    /// <returns>A <see cref="Task"/> that completes when the packet is sent to all clients.</returns>
    public Task SendAllAsync(TOutgoing id, IPacket packet) => SendAllAsync(id, packet, CancelSource.Token);
    
    private readonly object pendingMutex;
    private readonly HashSet<IConnection> pendingConnections;
    private readonly ConcurrentDictionary<int, IConnection> connectedClients;
    private readonly ServerConfiguration config;
}