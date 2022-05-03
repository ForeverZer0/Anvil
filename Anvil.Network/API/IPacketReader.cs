using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represent an object containing methods for reading binary formatted data, typically transmitted over a network, back
/// into managed types contained with an <see cref="IPacket"/> object.
/// </summary>
[PublicAPI]
public interface IPacketReader
{
    /// <summary>
    /// Gets the total length of the data in the <see cref="IPacketReader"/>;
    /// </summary>
    int Length { get; }
    
    /// <summary>
    /// Gets the current position of the data cursor within the <see cref="IPacketReader"/>.
    /// </summary>
    int Position { get; }
    
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
    /// Reads a variable length integer from the underlying data store.
    /// </summary>
    /// <returns>The value as a <see cref="int"/>.</returns>
    /// <remarks>A <see cref="VarInt"/> only contains the number of bytes required to fully express the value.</remarks>
    int ReadVarInt();

    /// <summary>
    /// Reads a variable length integer from the underlying data store and returns it as an <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="TEnum32">An <see cref="Enum"/> type that is backed by a 32-bit integer.</typeparam>
    /// <returns>The value as a <typeparamref name="TEnum32"/>..</returns>
    /// <remarks>A <see cref="VarInt"/> only contains the number of bytes required to fully express the value.</remarks>
    TEnum32 ReadVarInt<TEnum32>();
    
    /// <summary>
    /// Reads a variable length integer from the underlying data store.
    /// </summary>
    /// <returns>The value as a <see cref="long"/>.</returns>
    /// <remarks>A <see cref="VarLong"/> only contains the number of bytes required to fully express the value.</remarks>
    long ReadVarLong();

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
    Span<byte> ReadBuffer(int count);

    /// <summary>
    /// Reads an arbitrary block of data from the underlying data store as the specified primitive type.
    /// </summary>
    /// <param name="count">The number of items to read.</param>
    /// <typeparam name="T">A primitive/blittable value type.</typeparam>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> over a region of the internal buffer.</returns>
    Span<T> ReadBuffer<T>(int count) where T : unmanaged;

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