using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Describes the state of a connection.
/// </summary>
[PublicAPI]
public enum ConnectionState : byte
{
    /// <summary>
    /// Initial "handshake" state.
    /// </summary>
    Initial,
    
    /// <summary>
    /// Status query.
    /// </summary>
    Status,
    
    /// <summary>
    /// Authentication and/or login state.
    /// </summary>
    Authentication,
    
    /// <summary>
    /// Connected, authenticated, and now "in-game" or "play" state.
    /// </summary>
    Joined
}