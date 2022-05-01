using System.Net;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents a network connection between a client and a server.
/// </summary>
[PublicAPI]
public interface IClientConnection
{
    /// <summary>
    /// Occurs when the connection is closed.
    /// </summary>
    event EventHandler<DisconnectEventArgs> Closed;

    /// <summary>
    /// Gets the endpoint for this connection.
    /// </summary>
    EndPoint RemoteEndPoint { get; }

    /// <summary>
    /// Gets the state of the connection.
    /// </summary>
    ClientState State { get; }

    /// <summary>
    /// Gets a flag indicating if the connection is currently open or closed.
    /// </summary>
    bool IsConnected { get; }
    
    /// <summary>
    /// Closes the connection.
    /// </summary>
    void Disconnect();

    /// <summary>
    /// Initializes a new connection.
    /// </summary>
    /// <remarks>Is called after a successful connection has been made, but before data transfer begins.</remarks>
    void Initialize();
}