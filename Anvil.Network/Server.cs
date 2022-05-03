using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Anvil.Logging;
using Anvil.Network.API;

namespace Anvil.Network;

public class ServerConfig
{
    public int MaxClients { get; set; } = 20;

    public IPAddress Host { get; set; } = IPAddress.Parse("127.0.0.1");

    public int Port { get; set; } = 1776;

    public int CompressionThreshold { get; set; } = byte.MaxValue;

}

/// <summary>
/// Represents a connection between a server and a client.
/// </summary>
public interface IConnection
{
    /// <summary>
    /// Gets the <see cref="Socket"/> for the connection.
    /// </summary>
    Socket Socket { get; }
    
    /// <summary>
    /// Gets or sets the expected state of the client connection.
    /// </summary>
    ClientState State { get; set; }
}

internal class TcpConnection : IConnection, IEquatable<IConnection>
{
    /// <inheritdoc />
    public Socket Socket => Tcp.Client;

    /// <inheritdoc />
    public ClientState State { get; set; }
    
    public TcpClient Tcp { get; }

    public TcpConnection(TcpClient client, ClientState state = ClientState.Initial)
    {
        Tcp = client;
        State = state;
    }

    public TcpConnection(Socket socket, ClientState state = ClientState.Initial)
    {
        Tcp = new TcpClient {Client = socket};
        State = state;
    }
    
    /// <inheritdoc />
    public bool Equals(IConnection? other)
    {
        if (ReferenceEquals(other, null))
            return false;
        return ReferenceEquals(this, other) || Socket.Equals(other.Socket);
    }
}

/// <summary>
/// Abstract base class for general purpose TCP server for sending/receiving packets.
/// </summary>
/// <typeparam name="TClientBound"></typeparam>
/// <typeparam name="TServerBound"></typeparam>
public abstract class Server<TClientBound, TServerBound> : IServer
    where TClientBound : unmanaged, Enum
    where TServerBound : unmanaged, Enum
{
    protected static ILogger Log = LogManager.GetLogger<Server<TClientBound, TServerBound>>();
    
    private readonly CancellationTokenSource cancellationToken;
    private readonly HashSet<TcpConnection> clients;
    private readonly TcpListener listener;

    // protected IReadOnlyCollection<IConnection> Clients => clients;

    /// <inheritdoc />
    public int CompressionThreshold { get; }
    
    /// <inheritdoc />
    public event ServerEventHandler<ConnectionEventArgs>? ClientConnected;

    /// <inheritdoc />
    public event ServerEventHandler<DisconnectEventArgs>? ClientDisconnected;
    
    /// <inheritdoc />
    public int MaxClients { get; }

    /// <inheritdoc />
    public int ConnectedClients => clients.Count;

    /// <inheritdoc />
    public void Start()
    {
        listener.Start(MaxClients);
        Task.Run(Listen, cancellationToken.Token);
        
    }

    private async Task Listen()
    {
        Log.Info($"Listening on {listener.Server.LocalEndPoint}...");
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var client = await listener.AcceptTcpClientAsync(cancellationToken.Token);
                Log.Info($"Connecting to {client.Client.RemoteEndPoint}...");
                OnClientConnecting(client);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to connect to client.");
            }
        }
    }

    /// <summary>
    /// Sends a <paramref name="packet"/> to the specified client <paramref name="connection"/>.
    /// </summary>
    /// <param name="connection">The connection for the client to send the <paramref name="packet"/> to.</param>
    /// <param name="packet">The <see cref="IPacket{TPacketId}"/> to send.</param>
    protected abstract void SendPacket(IConnection connection, IPacket<TClientBound> packet);

    /// <summary>
    /// Deserializes a packet and processes its input.
    /// </summary>
    /// <param name="connection">The client connection that sent the data.</param>
    /// <param name="id">The unique ID of the packet.</param>
    /// <param name="reader">A <see cref="IPacketReader"/> containing the packet payload.</param>
    /// <remarks>
    /// The packet must be completely deserialized before this method returns, as the <paramref name="reader"/> will
    /// be invalidated when control returns to the caller.
    /// </remarks>
    protected abstract void ProcessPacket(IConnection connection, TServerBound id, IPacketReader reader);

    /// <inheritdoc />
    public ProtocolType ProtocolType => ProtocolType.Tcp;

    /// <inheritdoc />
    public IPAddress Host { get; }
    
    /// <inheritdoc />
    public int Port { get; }

    /// <inheritdoc />
    public void Stop() => Stop(1000);
    
    public void Stop(int timeout)
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.Cancel();
        
        var disconnectTasks = clients.Select(DisconnectAsync).ToArray();
        Task.WaitAll(disconnectTasks, timeout);
        clients.Clear();
    }
    
    public void Disconnect(IConnection connection)
    {
        if (connection is not TcpConnection tcp || !clients.Remove(tcp))
            return;
        
        connection.Socket.Disconnect(true);
        ClientDisconnected?.Invoke(this, new DisconnectEventArgs(connection, DisconnectReason.ServerDisconnect));
    }

    public async Task DisconnectAsync(IConnection connection)
    {
        if (connection is not TcpConnection tcp || !clients.Remove(tcp))
            return;
        
        await connection.Socket.DisconnectAsync(true);
        ClientDisconnected?.Invoke(this, new DisconnectEventArgs(connection, DisconnectReason.ServerDisconnect));
    }

    public void DisconnectAll()
    {
        foreach (var connection in clients)
            Disconnect(connection);
    }

    public async void DisconnectAllAsync() => await Task.WhenAll(clients.Select(DisconnectAsync));


    public void Run()
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (var client in clients)
            {
                
                
            }
        }
    }
    
    
    protected Server(ServerConfig? config = null)
    {
        cancellationToken = new CancellationTokenSource();

        config ??= new ServerConfig();
        clients = new HashSet<TcpConnection>();
        MaxClients = config.MaxClients;

        Host = config.Host;
        Port = config.Port;
        listener = new TcpListener(Host, Port);

        CompressionThreshold = config.CompressionThreshold;
    }

    protected abstract void OnClientConnecting(TcpClient client);

    protected virtual void AddClient(TcpClient client)
    {
        var connection = new TcpConnection(client);
        if (clients.Add(connection))
        {
            Log.Info($"Client connected at {client.Client.RemoteEndPoint}");
            ClientConnected?.Invoke(this, new ConnectionEventArgs(connection));
        }
        else
        {
            var endPoint = client.Client.RemoteEndPoint;
            Log.Warn($"Client is already connected at {endPoint}");
        }
    }



    //
    //
    //
    //
    //
    // public event EventHandler<ConnectionEventArgs>? Connected;
    //
    // protected readonly Socket ListenSocket;
    // protected readonly ConcurrentDictionary<EndPoint, IClientConnection> Connections;
    // private readonly CancellationTokenSource cancelToken;
    //
    // protected IConnectionFactory ConnectionFactory { get; private set; }
    //
    // public Server(Socket? socket = null, IConnectionFactory? factory = null)
    // {
    //     ListenSocket = socket ?? new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //     Connections = new ConcurrentDictionary<EndPoint, IClientConnection>();
    //     cancelToken = new CancellationTokenSource();
    // }
    //
    // private void Init()
    // {
    //     ConnectionFactory = CreateConnectionFactory();
    //     ConnectionFactory.ConnectionCreated += OnConnectionCreated;
    // }
    //
    //
    //
    // protected virtual IConnectionFactory CreateConnectionFactory()
    // {
    //     return new ConnectionFactory();
    // }
    //
    // protected virtual void OnConnectionCreated(object? sender, ConnectionEventArgs e)
    // {
    //     Log.Info($"Connection made to client at {e.Connection.RemoteEndPoint}");
    //     Connected?.Invoke(this, e);
    // }
    //
    //
    // public void Start(IPEndPoint endpoint)
    // {
    //     ListenSocket.Bind(endpoint);
    //     ListenSocket.Listen(10);
    //     ListenSocket.BeginAccept(ConnectionHandler, null);
    //     Log.Info($"Server started successfully, waiting for connections at {ListenSocket.LocalEndPoint}...");
    // }
    //
    // public void Start(string address, int port)
    // {
    //     var bytes = Encoding.UTF8.GetBytes(address);
    //     var ip = new IPAddress(bytes);
    //     Start(new IPEndPoint(ip, port));
    // }
    //
    // public void Start(IPAddress address, int port) => Start(new IPEndPoint(address, port));
    //
    // public void Stop()
    // {
    //     Log.Info("Server is stopping.");
    //     cancelToken.Cancel();
    //     foreach (var connection in Connections.Values)
    //         connection.Disconnect();
    // }
    //
    // private void ConnectionHandler(IAsyncResult ar)
    // {
    //     Socket socket;
    //     try
    //     {
    //         socket = ListenSocket.EndAccept(ar);
    //     }
    //     catch (Exception e)
    //     {
    //         // TODO: Log
    //         return;
    //     }
    //     finally
    //     {
    //         ListenSocket.BeginAccept(ConnectionHandler, null);
    //     }
    //
    //     var connection = ConnectionFactory.Create(NetworkDirection.ClientBound, socket);
    //     if (connection is null)
    //     {
    //         Log.Error("Failed to create connection.");
    //         return;
    //     }
    //
    //     if (socket.RemoteEndPoint is null)
    //     {
    //         Log.Error("Client has no remote end point configured.");
    //         return;
    //     }
    //
    //     if (Connections.TryAdd(socket.RemoteEndPoint, connection))
    //     {
    //         connection.Closed += ConnectionOnClosed;
    //         connection.Initialize();
    //     }
    //     else
    //     {
    //         Log.Error($"Already connected to client at {connection.RemoteEndPoint}.");
    //     }
    // }
    //
    // private void ConnectionOnClosed(object? sender, ConnectionEventArgs e)
    // {
    //     if (Connections.TryRemove(e.Connection.RemoteEndPoint, out var nc))
    //     {
    //         Log.Info($"Client at {nc.RemoteEndPoint} disconnected.");
    //     }
    // }
    /// <inheritdoc />

}