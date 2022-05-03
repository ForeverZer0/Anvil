using JetBrains.Annotations;

namespace Anvil.Network.API;


/// <summary>
/// Represents a binary packet of data that can be encoded and decoded to be sent across a network.
/// </summary>
[PublicAPI]
public interface IPacket
{
    /// <summary>
    /// Gets the maximum size of this packet, in bytes.
    /// </summary>
    /// <remarks>
    /// While a memory pool is used to avoid costly allocations and GC pressure, this hint allows the system to supply
    /// the most efficient sized buffer for the underlying storage when writing packets, and also ensure there will be
    /// no overflow.
    /// </remarks>
    int MaximumSize { get; }

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
