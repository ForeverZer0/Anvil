using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents a binary packet of data that can be encoded and decoded to be sent across a network.
/// </summary>
[PublicAPI]
public interface IPacket
{
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
/// <typeparam name="TReader">A type that implement <see cref="IPacketReader"/>.</typeparam>
/// <typeparam name="TWriter">A type that implement <see cref="IPacketWriter"/>.</typeparam>
[PublicAPI]
public interface IPacket<in TReader, in TWriter> : IPacket 
    where TReader : IPacketReader 
    where TWriter : IPacketWriter
{
    /// <summary>
    /// Encodes the packet data using the given <paramref name="writer"/>.
    /// </summary>
    /// <param name="writer">An object for writing data in binary format to underlying storage.</param>
    void Encode(TWriter writer);
    
    /// <summary>
    /// Decodes the packet using the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An object for reading binary data into managed types.</param>
    void Decode(TReader reader);

    /// <inheritdoc />
    void IPacket.Encode(IPacketWriter writer) => Encode(writer);
    
    /// <inheritdoc />
    void IPacket.Decode(IPacketReader reader) => Decode(reader);
}

