using System.Net;
using System.Net.Sockets;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents a network server.
/// </summary>
[PublicAPI]
public interface IServer
{
    /// <summary>
    /// Occurs when a new client connected to the server.
    /// </summary>
    event ServerEventHandler<ConnectionEventArgs> ClientConnected;
    
    /// <summary>
    /// Occurs when a connected client disconnects from the server.
    /// </summary>
    event ServerEventHandler<DisconnectEventArgs>? ClientDisconnected;

    /// <summary>
    /// Gets the network protocol type used by the <see cref="IServer"/>.
    /// </summary>
    ProtocolType ProtocolType { get; }

    /// <summary>
    /// Gets the IP address of the server that clients can connect to.
    /// </summary>
    IPAddress Host { get; }
    
    /// <summary>
    /// Gets the port of the server that client can connect on.
    /// </summary>
    int Port { get; }

    /// <summary>
    /// Gets a the minimum number of bytes required to enable compression of data sent over the network, or <c>-1</c>
    /// to indicate that compression is disabled.
    /// </summary>
    int CompressionThreshold { get; }
    
    /// <summary>
    /// Gets the maximum number of clients that can be connected.
    /// </summary>
    int MaxClients { get; }
    
    /// <summary>
    /// Gets the number of currently connected clients.
    /// </summary>
    int ConnectedClients { get; }
        
    /// <summary>
    /// Initializes the server and begins asynchronously listening for client connections.
    /// </summary>
    void Start();

    /// <summary>
    /// Disconnects any connected clients and closes the server.
    /// </summary>
    void Stop();
    
    /// <summary>
    /// Severs a connection to a client that is connected to the <see cref="IServer"/>.
    /// </summary>
    /// <param name="connection">The connection to the client to disconnect.</param>
    void Disconnect(IConnection connection);
}