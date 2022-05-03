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

internal class TcpConnection : IConnection, IEquatable<IConnection>
{
    /// <inheritdoc />
    public DateTime LastSeen { get; set; }
    
    /// <inheritdoc />
    public Socket Socket => Tcp.Client;

    /// <inheritdoc />
    public ClientState State { get; set; }

    /// <inheritdoc />
    public ProtocolType ProtocolType => ProtocolType.Tcp;

    public IPacketReader Reader { get; }
    
    public TcpClient Tcp { get; }

    public TcpConnection(TcpClient client, ClientState state = ClientState.Initial)
    {
        Tcp = client;
        State = state;
        LastSeen = DateTime.UtcNow;
        Reader = new PacketReader(client.GetStream(), null, true);
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
    
    private readonly CancellationTokenSource cancelSource;
    private readonly HashSet<TcpConnection> clients;
    private readonly TcpListener listener;
    private readonly PacketManager<TClientBound, TServerBound> pacman;

    // protected IReadOnlyCollection<IConnection> Clients => clients;

    /// <inheritdoc />
    public int CompressionThreshold => pacman.CompressionThreshold;
    
    /// <inheritdoc />
    public event ServerEventHandler<ConnectionEventArgs>? ClientConnected;

    /// <inheritdoc />
    public event ServerEventHandler<DisconnectEventArgs>? ClientDisconnected;

    /// <summary>
    /// Occurs when a packet is received from a client.
    /// </summary>
    public event ServerEventHandler<ClientPacketEventArgs<TServerBound>>? PacketReceived; 

    /// <inheritdoc />
    public int MaxClients { get; }

    /// <inheritdoc />
    public int ConnectedClients => clients.Count;

    /// <inheritdoc />
    public async Task StartAsync()
    {
        listener.Start(MaxClients);
        Log.Info($"Listening on {listener.Server.LocalEndPoint}...");
 
        while (!cancelSource.IsCancellationRequested)
        {
            if (listener.Pending())
            {
                await Task.Run(Listen, cancelSource.Token);
            }
 
        }
        

    }

    private async Task Listen()
    {
        try
        {
            var client = await listener.AcceptTcpClientAsync(cancelSource.Token);
            Log.Info($"Connecting to {client.Client.RemoteEndPoint}...");
            OnClientConnecting(client);
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to connect to client.");
        }
    }

    /// <summary>
    /// Sends a <paramref name="packet"/> to the specified client <paramref name="connection"/>.
    /// </summary>
    /// <param name="connection">The connection for the client to send the <paramref name="packet"/> to.</param>
    /// <param name="packet">The <see cref="IPacket"/> to send.</param>
    protected abstract void SendPacket(IConnection connection, IPacket packet);

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
        if (cancelSource.IsCancellationRequested)
            cancelSource.Cancel();
        
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
    
    
    protected Server(ServerConfig? config = null)
    {
        config ??= new ServerConfig();
        cancelSource = new CancellationTokenSource();
        
        pacman = new PacketManager<TClientBound, TServerBound>(Log, config.CompressionThreshold);

        clients = new HashSet<TcpConnection>();
        MaxClients = config.MaxClients;

        Host = config.Host;
        Port = config.Port;
        listener = new TcpListener(Host, Port);
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



}