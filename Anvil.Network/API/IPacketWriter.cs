using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represent an object containing methods for writing the data of an <see cref="IPacket"/> into binary format,
/// typically for the purpose of transmission over a network.
/// </summary>
[PublicAPI]
public interface IPacketWriter
{
    /// <summary>
    /// Exposes access to the underlying <see cref="Stream"/> used by the <see cref="IPacketWriter"/>.
    /// </summary>
    public Stream BaseStream { get; }
    
    /// <inheritdoc cref="BinaryWriter.Write(byte[])" />
    void WriteBytes(byte[] buffer);
    
    /// <inheritdoc cref="BinaryWriter.Write(ReadOnlySpan{byte})" />
    void WriteBytes(ReadOnlySpan<byte> buffer);

    /// <inheritdoc cref="BinaryWriter.Write(byte)" />
    /// <typeparam name="TEnum8">An <see cref="Enum"/> type backed by a 8-bit integer.</typeparam>
    void WriteByte<TEnum8>(TEnum8 value) where TEnum8 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryWriter.Write(byte)" />
    void WriteByte(byte value);

    /// <inheritdoc cref="BinaryWriter.Write(short)" />
    /// <typeparam name="TEnum16">An <see cref="Enum"/> type backed by a 16-bit integer.</typeparam>
    void WriteInt16<TEnum16>(TEnum16 value) where TEnum16 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryWriter.Write(short)" />
    void WriteInt16(short value);

    /// <inheritdoc cref="BinaryWriter.Write(int)" />
    /// <typeparam name="TEnum32">An <see cref="Enum"/> type backed by a 32-bit integer.</typeparam>
    void WriteInt32<TEnum32>(TEnum32 value) where TEnum32 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryWriter.Write(int)" />
    void WriteInt32(int value);

    /// <inheritdoc cref="BinaryWriter.Write(long)" />
    /// <typeparam name="TEnum64">An <see cref="Enum"/> type backed by a 64-bit integer.</typeparam>
    void WriteInt64<TEnum64>(TEnum64 value) where TEnum64 : unmanaged, Enum;

    /// <inheritdoc cref="BinaryWriter.Write(long)" />
    void WriteInt64(long value);

    /// <inheritdoc cref="BinaryWriter.Write7BitEncodedInt(int)" />
    /// <typeparam name="TEnum32">An <see cref="Enum"/> type backed by a 32-bit integer.</typeparam>
    void WriteVarInt<TEnum32>(TEnum32 value);

    /// <inheritdoc cref="BinaryWriter.Write7BitEncodedInt(int)" />
    void WriteVarInt(int value);

    /// <inheritdoc cref="BinaryWriter.Write7BitEncodedInt64(long)" />
    /// <typeparam name="TEnum64">An <see cref="Enum"/> type backed by a 64-bit integer.</typeparam>
    void WriteVarLong<TEnum64>(TEnum64 value);

    /// <inheritdoc cref="BinaryWriter.Write7BitEncodedInt64(long)" />
    void WriteVarLong(long value);

    /// <inheritdoc cref="BinaryWriter.Write(bool)" />
    void WriteBool(bool value);
    
    /// <inheritdoc cref="BinaryWriter.Write(double)" />
    void WriteDouble(double value);
    
    /// <inheritdoc cref="BinaryWriter.Write(Half)" />
    void WriteHalf(Half value);
    
    /// <inheritdoc cref="BinaryWriter.Write(float)" />
    void WriteFloat(float value);
    
    /// <inheritdoc cref="BinaryWriter.Write(string)" />
    void WriteString(string value);

    /// <summary>
    /// Writes an array to the input stream. The array is prefixed with the length, encoded with an integer seven bits
    /// at a time.
    /// </summary>
    /// <param name="array">The array instance to write.</param>
    /// <typeparam name="T">An unmanaged value type.</typeparam>
    void WriteArray<T>(T[] array) where T : unmanaged;
    
    /// <summary>
    /// Writes a 8-byte <see cref="DateTime"/> structure to the current stream and advances the stream by eight bytes. 
    /// </summary>
    /// <param name="time">The <see cref="DateTime"/> structure to write.</param>
    void WriteTime(DateTime time);
    
    /// <summary>
    /// Writes the 8-byte <see cref="DateTime.UtcNow"/> value to the current stream and advanced the stream by eight
    /// bytes.
    /// </summary>
    void WriteTimeNow() => WriteTime(DateTime.UtcNow);
}