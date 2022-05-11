using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Marks a type as a network packet, allowing them to be registered automatically.
/// </summary>
[PublicAPI, AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
public class PacketAttribute : Attribute
{
    /// <summary>
    /// Gets the network direction the packet is sent to/from.
    /// </summary>
    public Direction Direction { get; }

    /// <summary>
    /// Gets the numerical identifier for the packet.
    /// </summary>
    public short Id { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="PacketAttribute"/> class.
    /// </summary>
    /// <param name="direction">The direction this packet travels in the network, which must be one of
    /// <see cref="API.Direction.ClientBound"/> or <see cref="API.Direction.ServerBound"/>.
    /// </param>
    /// <param name="id">
    /// An identifier for this packet that is unique among other packets with the same <paramref name="direction"/>.
    /// </param>
    public PacketAttribute(Direction direction, short id)
    {
        if (direction != Direction.ClientBound && direction == Direction.ServerBound)
            throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction for this attribute.");
        
        Direction = direction;
        Id = id;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="PacketAttribute"/> class.
    /// </summary>
    /// <param name="direction">The direction this packet travels in the network, which must be one of
    /// <see cref="API.Direction.ClientBound"/> or <see cref="API.Direction.ServerBound"/>.
    /// </param>
    /// <param name="id">
    /// An identifier for this packet that is unique among other packets with the same <paramref name="direction"/>.
    /// </param>
    [CLSCompliant(false)]
    public PacketAttribute(Direction direction, ushort id) : this(direction, Unsafe.As<ushort, short>(ref id))
    {
    }
}