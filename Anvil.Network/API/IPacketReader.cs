using System.Net.Sockets;
using JetBrains.Annotations;

namespace Anvil.Network.API;

// TODO: Maximum packet size

/// <summary>
/// Represent an object containing methods for reading binary formatted data, typically transmitted over a network, back
/// into managed types contained with an <see cref="IPacket"/> object.
/// </summary>
[PublicAPI]
public interface IPacketReader
{
    /// <summary>
    /// Gets a value indicating if there is pending data that can be read from the underlying <see cref="BaseStream"/>.
    /// </summary>
    public bool Available
    {
        get
        {
            if (BaseStream is NetworkStream ns)
                return ns.DataAvailable;
            if (BaseStream.CanSeek)
                return BaseStream.Position < BaseStream.Length;
            return false;
        }
    }
    
    /// <summary>
    /// Exposes access to the underlying <see cref="Stream"/> used by the <see cref="IPacketReader"/>.
    /// </summary>
    public Stream BaseStream { get; }
    
    /// <inheritdoc cref="BinaryReader.ReadBytes(int)" />
    byte[] ReadBytes(int count);

    /// <inheritdoc cref="BinaryReader.ReadByte()" />
    /// <typeparam name="TEnum8">An <see cref="Enum"/> type backed by a 8-bit integer.</typeparam>
    TEnum8 ReadByte<TEnum8>() where TEnum8 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryReader.ReadByte()" />
    byte ReadByte();

    /// <inheritdoc cref="BinaryReader.ReadInt16()" />
    /// <typeparam name="TEnum16">An <see cref="Enum"/> type backed by a 16-bit integer.</typeparam>
    TEnum16 ReadInt16<TEnum16>() where TEnum16 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryReader.ReadInt16()" />
    short ReadInt16();

    /// <inheritdoc cref="BinaryReader.ReadInt32()" />
    /// <typeparam name="TEnum32">An <see cref="Enum"/> type backed by a 32-bit integer.</typeparam>
    TEnum32 ReadInt32<TEnum32>() where TEnum32 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryReader.ReadInt32()" />
    int ReadInt32();

    /// <inheritdoc cref="BinaryReader.ReadInt64()" />
    /// <typeparam name="TEnum64">An <see cref="Enum"/> type backed by a 64-bit integer.</typeparam>
    TEnum64 ReadInt64<TEnum64>() where TEnum64 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryReader.ReadInt64()" />
    long ReadInt64();

    /// <inheritdoc cref="BinaryReader.Read7BitEncodedInt()" />
    /// <typeparam name="TEnum32">An <see cref="Enum"/> type backed by a 32-bit integer.</typeparam>
    TEnum32 ReadVarInt<TEnum32>();

    /// <inheritdoc cref="BinaryReader.Read7BitEncodedInt()" />
    int ReadVarInt();

    /// <inheritdoc cref="BinaryReader.Read7BitEncodedInt64()" />
    /// <typeparam name="TEnum64">An <see cref="Enum"/> type backed by a 64-bit integer.</typeparam>
    TEnum64 ReadVarLong<TEnum64>();

    /// <inheritdoc cref="BinaryReader.Read7BitEncodedInt64()" />
    long ReadVarLong();

    /// <inheritdoc cref="BinaryReader.ReadBoolean()" />
    bool ReadBool();
    
    /// <inheritdoc cref="BinaryReader.ReadDouble()" />
    double ReadDouble();
    
    /// <inheritdoc cref="BinaryReader.ReadHalf()" />
    Half ReadHalf();
    
    /// <inheritdoc cref="BinaryReader.ReadSingle()" />
    float ReadFloat();
    
    /// <inheritdoc cref="BinaryReader.ReadString()" />
    string ReadString();

    /// <summary>
    /// Reads an array from the input stream. The array is prefixed with the length, encoded with an integer seven bits
    /// at a time.
    /// </summary>
    /// <typeparam name="T">An unmanaged value type.</typeparam>
    /// <returns></returns>
    T[] ReadArray<T>() where T : unmanaged;
    
    /// <summary>
    /// Reads a 8-byte <see cref="DateTime"/> structure from the current stream and advances the stream by eight bytes. 
    /// </summary>
    /// <returns>A 8-byte <see cref="DateTime"/> structure value read from the current stream.</returns>
    DateTime ReadTime();
}

