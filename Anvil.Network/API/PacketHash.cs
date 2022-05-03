using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// A hash code suitable for differentiating network packets in a dictionary-like container.
/// </summary>
/// <remarks>
/// The first 2 MSB contain the <see cref="Direction"/> information, the following 3 MSB contain the
/// <see cref="ClientState"/>, and remaining 27 bits contain the packet ID.
/// <para/>
/// For this reason, packet IDs are limited to a maximum value of <c>0x7FFFFFF</c>, so though they are represented as
/// 32-bit values in the API, they are actually only valid as 27-bit values.
/// </remarks>
[StructLayout(LayoutKind.Explicit, Size = sizeof(int), Pack = 0), PublicAPI]
public readonly struct PacketHash : IEquatable<PacketHash>
{
    /// <summary>
    /// Gets the minimum possible value for a packet identifier.
    /// </summary>
    public const int MinPacketId = -0x8000000;
    
    /// <summary>
    /// Gets the maximum possible value for a packet identifier.
    /// </summary>
    public const int MaxPacketId =  0x7FFFFFF;

    private const int PACKET_MASK                     = 0b00000111111111111111111111111111;
    private const int CLIENT_STATE_MASK               = 0b00111000000000000000000000000000;
    private const int DIRECTION_MASK  = unchecked((int) 0b11000000000000000000000000000000);
    
    private const int DIRECTION_SHIFT    = 29;
    private const int CLIENT_STATE_SHIFT = 27;

    /// <summary>
    /// Gets the ID for the packet represented by this <see cref="PacketHash"/>.
    /// </summary>
    public int Packet => hashCode & PACKET_MASK;

    /// <summary>
    /// Gets the network direction for the packet represented by this <see cref="PacketHash"/>.
    /// </summary>
    public NetworkDirection Direction
    {
        get
        {
            var value = (hashCode & DIRECTION_MASK) >> DIRECTION_SHIFT;
            return Unsafe.As<int, NetworkDirection>(ref value);
        }
    }

    /// <summary>
    /// Gets the required client state for the packet represented by this <see cref="PacketHash"/>.
    /// </summary>
    public ClientState State
    {
        get
        {
            var value = (hashCode & CLIENT_STATE_MASK) >> CLIENT_STATE_SHIFT;
            return Unsafe.As<int, ClientState>(ref value);
        }
    }

    /// <summary>
    /// The hash code value.
    /// </summary>
    [FieldOffset(0)]
    private readonly int hashCode;
    
    private static void AssertPacketRange(int id)
    {
        if (id is >= MinPacketId and <= MaxPacketId)
            return;

        var min = Math.Abs(MinPacketId).ToString("X");
        var message = $"Packet ID must be in the range of -0x{min} and 0x{MaxPacketId:X}.";
        throw new ArgumentOutOfRangeException(nameof(id), message);
    }

    /// <summary>
    /// Initializes a new <see cref="PacketHash"/>.
    /// </summary>
    /// <param name="direction">The network direction of the packet.</param>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">A unique ID for the packet.</param>
    public PacketHash(NetworkDirection direction, ClientState state, int id)
    {
        AssertPacketRange(id);
        unchecked
        {
            hashCode = (Unsafe.As<NetworkDirection, int>(ref direction) << DIRECTION_SHIFT) |
                       (Unsafe.As<ClientState, int>(ref state) << CLIENT_STATE_SHIFT) |
                       (id & PACKET_MASK);
        }
    }
    
    /// <summary>
    /// Initializes a new <see cref="PacketHash"/>.
    /// </summary>
    /// <param name="direction">The network direction of the packet.</param>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">A unique ID for the packet.</param>
    public PacketHash(NetworkDirection direction, ClientState state, Enum id)
    {
        var i = Convert.ToInt32(id);
        AssertPacketRange(i);
        
        unchecked
        {
            hashCode = (Unsafe.As<NetworkDirection, int>(ref direction) << DIRECTION_SHIFT) |
                       (Unsafe.As<ClientState, int>(ref state) << CLIENT_STATE_SHIFT) |
                       (i & PACKET_MASK);
        }
    }

    /// <inheritdoc />
    public bool Equals(PacketHash other) => hashCode == other.hashCode;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is PacketHash other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => hashCode;

    /// <inheritdoc />
    public override string ToString() => hashCode.ToString();

    /// <summary>
    /// Determines whether two specified hash codes have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="PacketHash"/> to compare.</param>
    /// <param name="right">The second <see cref="PacketHash"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(PacketHash left, PacketHash right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified hash codes have different values.
    /// </summary>
    /// <param name="left">The first <see cref="PacketHash"/> to compare.</param>
    /// <param name="right">The second <see cref="PacketHash"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(PacketHash left, PacketHash right) => !left.Equals(right);

    /// <summary>
    /// Implicit conversion of a <see cref="PacketHash"/> to a <see cref="int"/>.
    /// </summary>
    /// <param name="hash">The <see cref="PacketHash"/> to convert.</param>
    /// <returns>The integral value of the <see cref="PacketHash"/>.</returns>
    public static implicit operator int(PacketHash hash) => hash.hashCode;
}