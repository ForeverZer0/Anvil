using System.Net.Sockets;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents a network client.
/// </summary>
[PublicAPI]
public interface IClient
{
    /// <summary>
    /// Occurs when a connection to the server is established.
    /// </summary>
    event EventHandler? Connected;
    
    /// <summary>
    /// Occurs when the client is disconnected from the server.
    /// </summary>
    event EventHandler<DisconnectEventArgs>? Disconnected;

    /// <summary>
    /// Gets the network protocol type used by the <see cref="IClient"/>.
    /// </summary>
    ProtocolType ProtocolType { get; }
    
    /// <summary>
    /// Gets the underlying <see cref="Socket"/> for this <see cref="IClient"/>.
    /// </summary>
    Socket Socket { get; }
 
    /// <summary>
    /// Gets the current state of the client.
    /// </summary>
    ClientState State { get; }
    
    /// <summary>
    /// Gets a flag indicating if the client is currently connected to the server.
    /// </summary>
    bool IsConnected { get; }
}