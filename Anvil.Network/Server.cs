using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Anvil.Logging;
using Anvil.Network.API;

namespace Anvil.Network;

public class Server
{
    protected static ILogger Log = LogManager.GetLogger<Server>();
    
    public event EventHandler<ConnectionEventArgs>? Connected;

    protected readonly Socket Socket;
    protected readonly ConcurrentDictionary<EndPoint, IClientConnection> Connections;
    private readonly CancellationTokenSource cancelToken;

    private IConnectionFactory ConnectionFactory { get; }
    
    public Server(Socket? socket = null, IConnectionFactory? factory = null)
    {
        Socket = socket ?? new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Connections = new ConcurrentDictionary<EndPoint, IClientConnection>();
        cancelToken = new CancellationTokenSource();
        
        ConnectionFactory = factory ?? new ConnectionFactory();
        ConnectionFactory.ConnectionCreated += OnConnectionCreated;
    }

    private void OnConnectionCreated(object? sender, ConnectionEventArgs e)
    {
        Log.Info($"Connection made to client at {e.Connection.RemoteEndPoint}");
        Connected?.Invoke(this, e);
    }


    public void Start(IPEndPoint endpoint)
    {
        Socket.Bind(endpoint);
        Socket.Listen(10);
        Socket.BeginAccept(ConnectionHandler, null);
        Log.Info("Server started successfully, now awaiting client connections.");
    }

    public void Start(string address, int port)
    {
        var bytes = Encoding.UTF8.GetBytes(address);
        var ip = new IPAddress(bytes);
        Start(new IPEndPoint(ip, port));
    }
    
    public void Start(IPAddress address, int port) => Start(new IPEndPoint(address, port));

    public void Stop()
    {
        Log.Info("Server is stopping.");
        cancelToken.Cancel();
        foreach (var connection in Connections.Values)
            connection.Disconnect();
    }
    
    private void ConnectionHandler(IAsyncResult ar)
    {
        Socket socket;
        try
        {
            socket = Socket.EndAccept(ar);
        }
        catch (Exception e)
        {
            // TODO: Log
            return;
        }
        finally
        {
            Socket.BeginAccept(ConnectionHandler, null);
        }

        var connection = ConnectionFactory.Create(NetworkDirection.ClientBound, socket);
        if (connection is null)
        {
            Log.Error("Failed to create connection.");
            return;
        }

        if (socket.RemoteEndPoint is null)
        {
            Log.Error("Client has no remote end point configured.");
            return;
        }

        if (Connections.TryAdd(socket.RemoteEndPoint, connection))
        {
            connection.Closed += ConnectionOnClosed;
            connection.Initialize();
        }
        else
        {
            Log.Error($"Already connected to client at {connection.RemoteEndPoint}.");
        }
    }

    private void ConnectionOnClosed(object? sender, ConnectionEventArgs e)
    {
        if (Connections.TryRemove(e.Connection.RemoteEndPoint, out var nc))
        {
            Log.Info($"Client at {nc.RemoteEndPoint} disconnected.");
        }
    }
}