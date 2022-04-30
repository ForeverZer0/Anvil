using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represent an object that is capable of writing binary data to a an underlying stream in a supported format.
/// </summary>
[PublicAPI]
public interface IPacketReader
{
    /// <summary>
    /// Reads a <see cref="bool"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="bool"/> value.</returns>
    bool ReadBool();
    
    /// <summary>
    /// Reads a <see cref="byte"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="byte"/> value.</returns>
    byte ReadInt8();

    /// <summary>
    /// Reads a <see cref="short"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="short"/> value.</returns>
    short ReadInt16();
    
    /// <summary>
    /// Reads a <see cref="int"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="int"/> value.</returns>
    int ReadInt32();

    /// <summary>
    /// Reads a <see cref="long"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="long"/> value.</returns>
    long ReadInt64();

    /// <summary>
    /// Reads a <see cref="float"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="float"/> value.</returns>
    float ReadFloat();

    /// <summary>
    /// Reads a <see cref="double"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="double"/> value.</returns>
    double ReadDouble();
    
    /// <summary>
    /// Reads a <see cref="string"/> value from the underlying data store.
    /// </summary>
    /// <returns>The <see cref="string"/> value.</returns>
    /// <remarks>Implementors must return an empty string when no data is present.</remarks>
    string ReadString();

    /// <summary>
    /// Reads a an arbitrary block of bytes form the underlying data store.
    /// </summary>
    /// <param name="count">The number of bytes to read.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> over a region of the internal buffer.</returns>
    ReadOnlySpan<byte> ReadBuffer(int count);
    
    /// <summary>
    /// Reads an arbitrary block of bytes from the underlying data store into a user-supplied <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">A <see cref="Span{T}"/> to receive the data.</param>
    void ReadBuffer(Span<byte> buffer);

    /// <summary>
    /// Reads an arbitrary block of bytes from the underlying data store into a user-supplied <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">An array to receive the data.</param>
    /// <param name="start">The index into the <paramref name="buffer"/> to begin writing.</param>
    /// <param name="length">The number of bytes to read into the <paramref name="buffer"/>.</param>
    void ReadBuffer(byte[] buffer, int start, int length);
    
    /// <summary>
    /// Reads an arbitrary block of data from the underlying data store as the specified primitive type.
    /// </summary>
    /// <param name="count">The number of items to read.</param>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> over a region of the internal buffer.</returns>
    ReadOnlySpan<T> ReadBuffer<T>(int count) where T : unmanaged;
    
    /// <summary>
    /// Reads an arbitrary block of bytes from the underlying data store into a user-supplied <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">A <see cref="Span{T}"/> to receive the data.</param>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    void ReadBuffer<T>(Span<T> buffer) where T : unmanaged;
    
    /// <summary>
    /// Reads an arbitrary block of data from the underlying data store into a user-supplied <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">An array to receive the data.</param>
    /// <param name="start">The index into the <paramref name="buffer"/> to begin writing.</param>
    /// <param name="length">The number of items to read into the <paramref name="buffer"/>.</param>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    void ReadBuffer<T>(T[] buffer, int start, int length) where T : unmanaged;

    /// <summary>
    /// Reads a <see cref="float"/> value from the underlying data store.
    /// </summary>
    /// <typeparam name="TEnum">An <see cref="Enum"/> type.</typeparam>
    /// <returns>The <see cref="Enum"/> value.</returns>
    /// <remarks>Enumeration are backed by integers of different sizes, and implementor must account for this.</remarks>
    TEnum ReadEnum<TEnum>() where TEnum : struct, Enum;

    /// <summary>
    /// Reads an arbitrary value type from the stream.
    /// </summary>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    /// <returns>The structure value.</returns>
    T ReadStruct<T>() where T : unmanaged;
}