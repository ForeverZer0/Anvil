using JetBrains.Annotations;

namespace Anvil.Network.API;


/// <summary>
/// Represents a binary packet of data that can be encoded and decoded to be sent across a network.
/// </summary>
[PublicAPI]
public interface IPacket
{
    /// <summary>
    /// Gets a unique identifier for this <see cref="IPacket"/>.
    /// </summary>
    int PacketId { get; }

    /// <summary>
    /// Encodes the packet data using the given <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">An object for writing data in binary format to underlying storage.</param>
    void Encode(IPacketWriter writer);
    
    /// <summary>
    /// Decodes the packet using the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An object for reading binary data into managed types.</param>
    void Decode(IPacketReader reader);
}

/// <summary>
/// Represents a binary packet of data that can be encoded and decoded to be sent across a network.
/// </summary>
/// <typeparam name="TPacketId">
/// An <see cref="Enum"/> type that is backed by a <see cref="int"/>, whose maximum <i>used</i> values does not exceed
/// <c>0x7FFFFFF</c>, as the value is sent over the network as a <see cref="VarInt"/>, as well as uses the top 5 MSB for
/// hashing algorithms.
/// </typeparam>
public interface IPacket<out TPacketId> : IPacket where TPacketId : unmanaged, Enum
{
    /// <summary>
    /// Gets a unique identifier for this <see cref="IPacket"/>.
    /// </summary>
    new TPacketId PacketId { get; }

    /// <inheritdoc />
    int IPacket.PacketId => Convert.ToInt32(PacketId);
}
