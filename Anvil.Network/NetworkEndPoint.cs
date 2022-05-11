using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Anvil.Logging;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

/// <summary>
/// Abstract base class providing functionality shared by both servers and clients.
/// </summary>
/// <typeparam name="TIncoming">The 16-bit enum type used for incoming packet identifiers.</typeparam>
/// <typeparam name="TOutgoing">The 16-bit enum type used for outgoing packet identifiers.</typeparam>
[PublicAPI]
public abstract class NetworkEndPoint<TIncoming, TOutgoing> : INetworkEndPoint<TIncoming>, IDisposable
    where TIncoming : unmanaged, Enum 
    where TOutgoing : unmanaged, Enum
{

    /// <summary>
    /// The required size that connection-related client packets must be padded to. This padding is simply there to make
    /// the server a less attractive target for DDoS amplification attacks.
    /// </summary>
    private protected const int CONNECTION_PADDING = 1024;

    /// <summary>
    /// The size of a packet header.
    /// </summary>
    protected const int HEADER_SIZE = 12;

    /// <summary>
    /// Flags that are passed to send/receive calls on the underlying <see cref="System.Net.Sockets.Socket"/>.
    /// </summary>
    protected readonly SocketFlags SocketFlags;
    
    /// <summary>
    /// The socket used for transmitting network data.
    /// </summary>
    protected readonly Socket Socket;
    
    /// <summary>
    /// A <see cref="ILogger"/> instance for outputting messages.
    /// </summary>
    protected readonly ILogger Log;
    
    /// <summary>
    /// Gets a cancellable source that is used when no token is supplied by the users for asynchronous methods.
    /// </summary>
    protected readonly CancellationTokenSource CancelSource;
    
    /// <summary>
    /// The queue of deserialized packets pending being emitted to events and/or handlers.
    /// </summary>
    protected readonly ConcurrentQueue<PacketData<TIncoming>> PacketQueue;

    /// <inheritdoc />
    public event EventHandler<PacketEventArgs<TIncoming>>? PacketReceived;

    /// <inheritdoc />
    public event EventHandler<ConnectionEventArgs>? Connected;

    /// <inheritdoc />
    public event EventHandler<DisconnectEventArgs>? Disconnected;
    
    /// <summary>
    /// Gets a number indicating the protocol version used by this implementation.
    /// </summary>
    /// <remarks>
    /// This is simply an arbitrary number whose meaning is defined by consumers.
    /// <para>The default value is <c>0</c>.</para>
    /// </remarks>
    public int Protocol { get; }
    
    /// <inheritdoc />
    public bool AlwaysInvokePacketEvents { get; }

    /// <inheritdoc />
    public bool RealTimeEvents { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkEndPoint{TIncoming,TOutgoing}"/> class.
    /// </summary>
    protected NetworkEndPoint(BaseConfiguration configuration)
    {
        Binary.AssetSize<TIncoming>(sizeof(short));
        Binary.AssetSize<TOutgoing>(sizeof(short));

        SocketFlags = configuration.SocketFlags;
        Protocol = configuration.Protocol;
        AlwaysInvokePacketEvents = configuration.AlwaysInvokePacketEvents;
        RealTimeEvents = configuration.RealTime;

        Log = LogManager.GetLogger(typeof(Client<TIncoming, TOutgoing>));
        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        PacketQueue = new ConcurrentQueue<PacketData<TIncoming>>();
        packetHandlers = new Dictionary<TIncoming, PacketHandler<TIncoming>>();
        CancelSource = new CancellationTokenSource();
        Rand = new Random();
    }

    /// <summary>
    /// Emits packet events and/or handlers with the specified packet <paramref name="data"/>.
    /// </summary>
    /// <param name="data">A <see cref="PacketData{T}"/> instance describing the packet.</param>
    protected void EmitPacket(PacketData<TIncoming> data)
    {
        var handled = false;
        if (packetHandlers.TryGetValue(data.Id, out var handler))
        {
            handled = true;
            handler.Invoke(data.Connection, data.Id, data.Packet);
            if (!AlwaysInvokePacketEvents)
                return;
        }

        var args = new PacketEventArgs<TIncoming>(data, handled);
        PacketReceived?.Invoke(this, args);
        if (!args.Handled)
            Log.Warn($"Unhandled packet (ID:{data.Id}) from {data.Connection}.");
    }

    /// <inheritdoc />
    public void Tick()
    {
        while (PacketQueue.TryDequeue(out var data))
        {
            EmitPacket(data);
        }
    }

    /// <summary>
    /// Performs binary serialization of a packet into a buffer for sending over a network.
    /// </summary>
    /// <param name="id">The unique ID of the packet.</param>
    /// <param name="packet">The <see cref="IPacket"/> instance to serialize.</param>
    /// <param name="flags">Flags to include in the packet header.</param>
    /// <param name="salt">A unique salt value for the target to validate the connection with.</param>
    /// <returns>A buffer containing the serialized packet with header.</returns>
    protected BinaryBuffer SerializePacket(TOutgoing id, IPacket packet, PacketFlags flags, int salt)
    {
        var buffer = new BinaryBuffer(packet.MaximumSize, true, HEADER_SIZE);
        packet.Serialize(buffer);

        var header = new PacketHeader(Unsafe.As<TOutgoing, short>(ref id), flags, salt);
        MemoryMarshal.Write(buffer[..HEADER_SIZE], ref header);
        return buffer;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> if called normally, otherwise <c>false</c> when called by object finalizer.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;
        
        if (!CancelSource.IsCancellationRequested)
            CancelSource.Cancel();
        
        Socket.Dispose();
        CancelSource.Dispose();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    public bool SetHandler(TIncoming id, PacketHandler<TIncoming> handler) => packetHandlers.TryAdd(id, handler);

    /// <summary>
    /// Disconnects the specified <paramref name="connection"/> and sends a disconnect packet with optional message.
    /// </summary>
    /// <param name="connection">The connection to remove.</param>
    /// <param name="reason">A strongly-typed constant describing the general reason for the disconnection.</param>
    /// <param name="message">An optional text message to send with the disconnect packets.</param>
    private protected virtual void Disconnect(IConnection connection, DisconnectReason reason, string? message = null)
    {
        const int MAX_MESSAGE_SIZE = 128;

        var size = HEADER_SIZE + 2;
        if (string.IsNullOrWhiteSpace(message) || message.Length > MAX_MESSAGE_SIZE)
            message = null;
        else
            size += 4 + Encoding.UTF8.GetByteCount(message);

        using var writer = new BinaryBuffer(size, true, HEADER_SIZE);
        var header = new PacketHeader(0, PacketFlags.Disconnect, connection.Salt);
        MemoryMarshal.Write(writer.Buffer, ref header);
        writer.WriteInt8(reason);
        writer.WriteString(message);

        for (var i = 0; i < 10; i++)
            Socket.SendAsync(writer.Buffer, SocketFlags);
    }

    /// <summary>
    /// Begins an asynchronous <see cref="Task"/> that continuously monitors for incoming datagrams. The header and
    /// payload are then passed on to <see cref="ProcessPacket"/> for further processing and validation.
    /// </summary>
    /// <param name="endPoint">The remote end point to monitor for incoming datagrams.</param>
    /// <param name="token">A cancellation token that can be used to abort the running task.</param>
    protected async Task ProcessNetworkAsync(EndPoint endPoint, CancellationToken token)
    {
        await Task.Yield();

        var buffer = new byte[ushort.MaxValue];
        var memory = new Memory<byte>(buffer);
        var reader = new PacketReader(buffer, HEADER_SIZE, Encoding.UTF8);
        
        while (!token.IsCancellationRequested)
        {
            try
            {
                var result = await Socket.ReceiveFromAsync(memory, SocketFlags, endPoint, token);
                if (token.IsCancellationRequested || result.ReceivedBytes < HEADER_SIZE)
                    continue;

                var header = MemoryMarshal.Read<PacketHeader>(buffer);
                var payloadSize = result.ReceivedBytes - HEADER_SIZE;
                reader.SetLength(payloadSize);
                ProcessPacket(result.RemoteEndPoint, header, reader, payloadSize);
            }
            catch (ObjectDisposedException e)
            {
                Log.Error(e.Message);
                if (e.ObjectName == nameof(Socket))
                    break;
            }
            catch (SocketException e)
            {
                Log.Error($"Socket error: {e.Message}");
                break;
            }
            catch (Exception e)
            {
                Log.Error($"{e.GetType()}: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Process raw input from the network.
    /// </summary>
    /// <param name="endPoint">The remote end point the data was sent from.</param>
    /// <param name="header">The packet header, which may or may not be valid.</param>
    /// <param name="payload">A <see cref="IBinaryReader"/> for reading the payload.</param>
    /// <param name="payloadSize">The number of bytes in the payload.</param>
    /// <remarks>
    /// The data may or not even be valid, or from a known connection. The responsibility of this method is to filter
    /// raw input and determine if the data should be discarded, or sent on for further processing.
    /// <para/>
    /// The <paramref name="payload"/> object and the underlying memory store is not valid once this method exits, and
    /// a reference to it should not be stored or used beyond this scope.
    /// </remarks>
    protected abstract void ProcessPacket(EndPoint endPoint, PacketHeader header, IBinaryReader payload, int payloadSize);

    /// <summary>
    /// Invokes the <see cref="Connected"/> event with specified <paramref name="connection"/>.
    /// </summary>
    /// <param name="connection">The <see cref="IConnection"/> that is causing the event.</param>
    protected virtual void OnConnected(IConnection connection)
    {
        Connected?.Invoke(this, new ConnectionEventArgs(connection));
    }

    /// <summary>
    /// Invokes the <see cref="Disconnected"/> event with specified <paramref name="connection"/>.
    /// </summary>
    /// <param name="connection">The <see cref="IConnection"/> that is causing the event.</param>
    /// <param name="reason">The reason for the disconnection.</param>
    /// <param name="message">Optional message for the disconnect event.</param>
    protected virtual void OnDisconnected(IConnection connection, DisconnectReason reason, string? message = null)
    {
        Disconnected?.Invoke(this, new DisconnectEventArgs(connection, reason));
    }

    private protected readonly Random Rand;
    private readonly Dictionary<TIncoming, PacketHandler<TIncoming>> packetHandlers;
}