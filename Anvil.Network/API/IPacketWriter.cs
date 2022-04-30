using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represent an object that is capable of writing binary data to a an underlying stream in a supported format.
/// </summary>
[PublicAPI]
public interface IPacketWriter
{
    /// <summary>
    /// Writes a <see cref="bool"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="bool"/> value to write.</param>
    void WriteBool(bool value);
    
    /// <summary>
    /// Writes a <see cref="byte"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="byte"/> value to write.</param>
    void WriteInt8(byte value);

    /// <summary>
    /// Writes a <see cref="short"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="short"/> value to write.</param>
    void WriteInt16(short value);
    
    /// <summary>
    /// Writes a <see cref="int"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="int"/> value to write.</param>
    void WriteInt32(int value);

    /// <summary>
    /// Writes a <see cref="long"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="long"/> value to write.</param>
    void WriteInt64(long value);

    /// <summary>
    /// Writes a <see cref="float"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value to write.</param>
    void WriteFloat(float value);

    /// <summary>
    /// Writes a <see cref="double"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value to write.</param>
    void WriteDouble(double value);
    
    /// <summary>
    /// Writes a <see cref="string"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to write.</param>
    void WriteString(string? value);

    /// <summary>
    /// Writes a <see cref="Enum"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The <see cref="Enum"/> value to write.</param>
    /// <typeparam name="TEnum">An <see cref="Enum"/> type.</typeparam>
    /// <remarks>Enumeration are backed by integers of different sizes, and implementor must account for this.</remarks>
    void WriteEnum<TEnum>(TEnum value) where TEnum : struct, Enum;

    /// <summary>
    /// Writes a <see cref="bool"/> value to the underlying data store.
    /// </summary>
    /// <param name="value">The structure value to write.</param>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    void WriteStruct<T>(T value) where T : unmanaged;

    /// <summary>
    /// Writes an arbitrary buffer of bytes to the underlying data store.
    /// </summary>
    /// <param name="value">A <see cref="ReadOnlySpan{T}"/> containing the data to write.</param>
    void WriteBuffer(ReadOnlySpan<byte> value);
    
    /// <summary>
    /// Writes an arbitrary buffer of bytes to the underlying data store.
    /// </summary>
    /// <param name="buffer">A array containing the data to write.</param>
    /// <param name="start">The index into the <paramref name="buffer"/> to begin reading.</param>
    /// <param name="length">The number of byte to write from the <paramref name="buffer"/>.</param>
    void WriteBuffer(byte[] buffer, int start, int length);
    
    /// <summary>
    /// Writes an arbitrary buffer of value types to the underlying data store.
    /// </summary>
    /// <param name="value">A <see cref="ReadOnlySpan{T}"/> containing the data to write.</param>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    void WriteBuffer<T>(ReadOnlySpan<T> value) where T : unmanaged;
    
    /// <summary>
    /// Writes an arbitrary buffer of value types to the underlying data store.
    /// </summary>
    /// <param name="buffer">A array containing the data to write.</param>
    /// <param name="start">The index into the <paramref name="buffer"/> to begin reading.</param>
    /// <param name="length">The number of items to write from the <paramref name="buffer"/>.</param>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    void WriteBuffer<T>(T[] buffer, int start, int length) where T : unmanaged;
}