using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Describes the target receiver for a <see cref="Packet"/>.
/// </summary>
[PublicAPI, Flags]
public enum PacketDirection : byte
{
    /// <summary>
    /// Not specified and/or invalid.
    /// </summary>
    Unspecified,
    
    /// <summary>
    /// Sent to a server.
    /// </summary>
    ServerBound = 0x01,
    
    /// <summary>
    /// Sent to a client.
    /// </summary>
    ClientBound = 0x02,
    
    /// <summary>
    /// Can be sent to any receiver.
    /// </summary>
    Any = 0xFF
}