using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Describes the direction and/or receiver for network traffic.
/// </summary>
[PublicAPI, Flags]
public enum NetworkDirection : byte
{
    /// <summary>
    /// Not specified and/or invalid.
    /// </summary>
    None,
    
    /// <summary>
    /// Sent to a server from a client.
    /// </summary>
    ServerBound = 0x01,
    
    /// <summary>
    /// Sent to a client from the server.
    /// </summary>
    ClientBound = 0x02,
    
    /// <summary>
    /// Can be sent to/from clients and server.
    /// </summary>
    Both = ServerBound | ClientBound
}