using System.Buffers;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using Anvil.Logging;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

/*

[PublicAPI]
public class ClientConnection : IClientConnection
{
    protected static readonly ILogger Log = LogManager.GetLogger<ClientConnection>();
    
    /// <inheritdoc />
    public event EventHandler<DisconnectEventArgs>? Closed;
    
    public event EventHandler<ConnectionEventArgs>? ConnectionConfirmed;

    /// <inheritdoc />
    public EndPoint RemoteEndPoint { get; }

    /// <inheritdoc />
    public ClientState State { get; private set; }
    
    /// <inheritdoc />
    public bool IsConnected { get; private set; }
    
    
    protected readonly NetworkDirection Direction;
    protected readonly Socket Socket;
    protected readonly Task ProcessingTask;
    protected readonly Task WritingTask;
    protected readonly bool CompressionEnabled;
    protected readonly int CompressionThreshold;

    private readonly CancellationTokenSource cancelToken;

    public ClientConnection(NetworkDirection direction, Socket socket)
    {
        Direction = direction;
        Socket = socket;
        RemoteEndPoint = socket.RemoteEndPoint ?? throw new ArgumentException("No endpoint defined for socket.");

        cancelToken = new CancellationTokenSource();
        State = ClientState.Initial;
        IsConnected = true;
        
        ProcessingTask = new Task(DoProcess, cancelToken.Token);
        WritingTask = new Task(DoWrite, cancelToken.Token);
    }

    private void Read(NetworkStream stream, byte[] buffer, int start, int length)
    {
        var read = 0;
        do
        {
            if (!SpinWait.SpinUntil(() => stream.DataAvailable, 5000))
            {
                OnDisconnect(false);
                break;
            }
            
            var r = stream.Read(buffer, read, length - read);
            if (r > 0)
                read += r; // TODO: -1, etc
            
        } while (read < length && !cancelToken.IsCancellationRequested);
    }
    
    private void DoWrite()
    {
        throw new NotImplementedException();
    }

    private void DoProcess()
    {
        Log.Info($"Listening on {Socket.RemoteEndPoint}");
        var memPool = ArrayPool<byte>.Create();
        try
        {
            using var ns = new NetworkStream(Socket);
            var reader = new BinaryPacketReader(BitConverter.IsLittleEndian);
            byte[] payload;
            
            while (!cancelToken.IsCancellationRequested)
            {
                var id = VarInt.Read(ns);
                var length = VarInt.Read(ns);

                if (CompressionEnabled && length > CompressionThreshold)
                {
                    var decompressedLength = VarInt.Read(ns);
                    var compressedBuffer = memPool.Rent(length);
                    payload = memPool.Rent(decompressedLength);
                    
                    Read(ns, compressedBuffer, 0, length);
                    using var ms = new MemoryStream(compressedBuffer, 0, length);
                    using var gzip = new GZipStream(ms, CompressionMode.Decompress);
                    if (gzip.Read(payload, 0, decompressedLength) != decompressedLength)
                    {
                        memPool.Return(payload);
                        memPool.Return(compressedBuffer);
                        // TODO: Corrupted/lost packet, decompression failure, etc
                        continue;
                    }
                    memPool.Return(compressedBuffer);
                    length = decompressedLength;
                }
                else
                {
                    payload = memPool.Rent(length);
                    Read(ns, payload, 0, length);
                }
                
                reader.SetBuffer(payload, 0, length);
                ProcessIncomingPacket(id, reader);
                memPool.Return(payload);
                
                SpinWait.SpinUntil(() => cancelToken.IsCancellationRequested || ns.DataAvailable);
            }
        }
        
        catch (Exception e)
        {
            switch (e)
            {
                case OperationCanceledException:
                case EndOfStreamException:
                case IOException:
                    return;
                default:
                    Log.Error(e, "Unhandled exception occurred while processing network.");
                    break;
            }
        }
        finally
        {
            OnDisconnect(false);
        }
    }

    protected virtual void ProcessIncomingPacket(int id, IPacketReader reader)
    {
        
    }
    

    /// <inheritdoc />
    public void Disconnect()
    {
        if (cancelToken.IsCancellationRequested)
            return;
        cancelToken.Cancel();
        
        // If the server is still sending data, then it is unaware that the connection closed.
        var isNoMoreData = Socket.Poll(1000, SelectMode.SelectRead) && Socket.Available == 0;
        OnDisconnect(!isNoMoreData);
    }

    protected virtual void OnDisconnect(bool notified)
    {
        if (!cancelToken.IsCancellationRequested)
            cancelToken.Cancel();
        
        Socket.Shutdown(SocketShutdown.Both);
        Socket.Close();

        IsConnected = false;
        Closed?.Invoke(this, new DisconnectEventArgs(this, notified));
    }

    /// <inheritdoc />
    public void Initialize()
    {
        Socket.Blocking = true;
        ProcessingTask.Start();
        WritingTask.Start();
    }

}

*/