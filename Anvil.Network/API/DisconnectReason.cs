using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Describes the reason for the disconnection of a client/server.
/// </summary>
[PublicAPI]
public enum DisconnectReason
{
    /// <summary>
    /// Not specified or unknown.
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Timeout.
    /// </summary>
    Timeout,
    
    /// <summary>
    /// The server initiated a disconnection from the client.
    /// </summary>
    ServerDisconnect,
    
    /// <summary>
    /// The client initiated a disconnection from the server.
    /// </summary>
    ClientDisconnect
}