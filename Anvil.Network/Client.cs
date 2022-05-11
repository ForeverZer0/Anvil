using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

/// <summary>
/// Concrete implementation of a <see cref="IClient{TIncoming,TOutgoing}"/>, providing basic functionality for
/// connecting to a server, and asynchronously sending/emitting packets.
/// </summary>
/// <typeparam name="TIncoming">The 16-bit enum type used for incoming packet identifiers.</typeparam>
/// <typeparam name="TOutgoing">The 16-bit enum type used for outgoing packet identifiers.</typeparam>
[PublicAPI]
public class Client<TIncoming, TOutgoing> : NetworkEndPoint<TIncoming, TOutgoing>, IClient<TIncoming, TOutgoing>
    where TIncoming : unmanaged, Enum
    where TOutgoing : unmanaged, Enum
{
    
    /// <inheritdoc />
    public bool IsConnected
    {
        get => isConnected && Server is not null;
        private set => isConnected = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Client{TIncoming,TOutgoing}"/> class with the specified
    /// <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">
    /// A <see cref="ClientConfiguration"/> defining properties to change behavior of the client.
    /// </param>
    public Client(ClientConfiguration configuration) : base(configuration)
    {
        config = configuration.Clone();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Client{TIncoming,TOutgoing}"/> class with the default configuration.
    /// </summary>
    public Client() : this(new ClientConfiguration())
    {
    }

    /// <inheritdoc />
    public IConnection? Server { get; private set; }

    /// <inheritdoc />
    public bool Connect(IPEndPoint endPoint, TimeSpan timeout)
    {
        Disconnect();
        var result = Task.WaitAny(new[] {ConnectAsync(endPoint, CancelSource.Token)}, timeout);
        return result != -1;
    }

    /// <inheritdoc cref="ConnectAsync(System.Net.IPEndPoint,CancellationToken)" />
    public Task ConnectAsync(IPEndPoint endPoint) => ConnectAsync(endPoint, CancellationToken.None);
    
    /// <inheritdoc />
    public async Task ConnectAsync(IPEndPoint endPoint, CancellationToken token)
    {
        if (IsConnected)
            await DisconnectAsync(token);

        CancelSource.TryReset();
        Server = new Connection(endPoint, 0, Rand.Next());

        try
        {
            await Socket.ConnectAsync(endPoint, token);
            networkTask = ProcessNetworkAsync(endPoint, token);
            SendConnectionRequest(Server.ClientSalt);

            while (!IsConnected && !token.IsCancellationRequested)
            {
                await Task.Delay(10, token);
            }
            
            if (IsConnected && !token.IsCancellationRequested)
                keepAliveTask = KeepAliveAsync(token);
        }
        catch (SocketException e)
        {
            Log.Error(e.Message);
        }
    }
    
    private void SendConnectionRequest(int clientSalt)
    {
        if (!Socket.Connected)
            throw new InvalidOperationException("Endpoint for server is not defined.");
        
        var header = new PacketHeader(0, PacketFlags.ConnectionRequest, 0);
        var buffer = new byte[CONNECTION_PADDING + HEADER_SIZE];
        var span = new Span<byte>(buffer);
        
        MemoryMarshal.Write(span, ref header);
        BinaryPrimitives.WriteInt32LittleEndian(span[HEADER_SIZE..], Protocol);
        BinaryPrimitives.WriteInt32LittleEndian(span[(HEADER_SIZE + sizeof(int))..], clientSalt);

        Socket.Send(span, SocketFlags);
    }

    private void ProcessChallenge(int serverSalt, IBinaryReader reader)
    {
        if (!Socket.Connected || Server is null || Server.ClientSalt == 0)
            throw new NotConnectedException("Endpoint for server is not defined.");

        if (reader.ReadInt32() != Server.ClientSalt)
        {
            IsConnected = false;
            Log.Warn("Invalid challenge response from server.");
            return;
        }

        Server.ServerSalt = serverSalt;
        var header = new PacketHeader(0, PacketFlags.ChallengeReply, Server.Salt);
        var buffer = new byte[CONNECTION_PADDING + HEADER_SIZE];
        var span = new Span<byte>(buffer);
        
        MemoryMarshal.Write(span, ref header);
        Socket.Send(span, SocketFlags);
    }


    /// <inheritdoc />
    protected override void ProcessPacket(EndPoint endPoint, PacketHeader header, IBinaryReader payload, int payloadSize)
    {
        if (isConnected && Server is not null && Server.Salt == header.Salt)
        {
            if (header.Flags == PacketFlags.Disconnect)
            {
                ProcessServerDisconnect(Server, payload);
                return;
            }
            
            var packet = PacketManager.Factory(Direction.ClientBound, header.PacketType);
            packet.Deserialize(payload);

            var id = Unsafe.As<short, TIncoming>(ref header.PacketType);
            var data = new PacketData<TIncoming>(Server, id, packet);
            if (RealTimeEvents)
                EmitPacket(data);
            else
                PacketQueue.Enqueue(data);

            return;
        }
        
        if (header.Flags == PacketFlags.Challenge)
        {
            ProcessChallenge(header.Salt, payload);
            return;
        }

        if (header.Flags == PacketFlags.ConnectionConfirm && Server is not null && header.Salt == Server.Salt)
        {
            IsConnected = true;
            OnConnected(Server);
        }
    }

    /// <inheritdoc />
    public void Disconnect(DisconnectReason reason = DisconnectReason.Unspecified)
    {
        IsConnected = false;
        DisconnectAsync(CancellationToken.None, reason).RunSynchronously(TaskScheduler.Default);
    }

    /// <inheritdoc cref="DisconnectAsync(CancellationToken,DisconnectReason)" />
    public Task DisconnectAsync() => DisconnectAsync(CancellationToken.None);

    /// <inheritdoc />
    public async Task DisconnectAsync(CancellationToken token, DisconnectReason reason = DisconnectReason.Unspecified)
    {
        IsConnected = false;
        if (Socket.Connected)
            await Socket.DisconnectAsync(true, token);

        if (Server is not null)
        {
            OnDisconnected(Server, reason);
            Server = null;
        }
        
        CancelSource.TryReset();
    }
    
    /// <inheritdoc />
    public void Send(TOutgoing id, IPacket packet)
    {
        if (Server is null)
            throw new NotConnectedException();
        
        using var buffer = SerializePacket(id, packet, PacketFlags.None, Server.Salt);
        Socket.Send(buffer.AsSpan(), SocketFlags);
    }

    /// <inheritdoc />
    public async Task SendAsync(TOutgoing id, IPacket packet, CancellationToken token)
    {
        if (Server is null)
            throw new NotConnectedException();
        
        await Task.Yield();
        using var buffer = SerializePacket(id, packet, PacketFlags.None, Server.Salt);
        await Socket.SendAsync(buffer.AsMemory(), SocketFlags, token);
    }

    private void ProcessServerDisconnect(IConnection connection, IBinaryReader payload)
    {
        isConnected = false;
        Server = null;
        if (!CancelSource.IsCancellationRequested)
            CancelSource.Cancel();

        var reason = payload.ReadInt8<DisconnectReason>();
        var msg = payload.ReadString();
        OnDisconnected(connection, reason, msg);
    }

    private async Task KeepAliveAsync(CancellationToken token)
    {
        if (!isConnected || Server is null || config.KeepAliveInterval <= TimeSpan.Zero)
            return;
        
        await Task.Yield();
        
        var buffer = new byte[HEADER_SIZE];
        var memory = new ReadOnlyMemory<byte>(buffer);
        var header = new PacketHeader(0, PacketFlags.KeepAlive, Server.Salt);
        MemoryMarshal.Write(buffer, ref header);

        while (IsConnected && !token.IsCancellationRequested)
        {
            await Socket.SendAsync(memory, SocketFlags, token);
            await Task.Delay(config.KeepAliveInterval, token);
        }
    }
    
    private readonly ClientConfiguration config;
    private Task? networkTask;
    private Task? keepAliveTask;
    private bool isConnected;
}