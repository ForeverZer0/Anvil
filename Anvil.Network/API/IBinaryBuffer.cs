using System.Text;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represent an object that with an underlying memory storage that can be interpreted as arbitrary bytes.
/// </summary>
public interface IBinaryBuffer
{
    /// <summary>
    /// Gets a value describing the endianness of numerical data in this <see cref="IBinaryBuffer"/>.
    /// </summary>
    Endianness Endian { get; }
    
    /// <summary>
    /// Gets the encoding used for strings in this <see cref="IBinaryBuffer"/>.
    /// </summary>
    Encoding Encoding { get; }
}

/// <summary>
/// Represent an object that supports reading arbitrary data from an underlying memory storage.
/// </summary>
[PublicAPI]
public interface IBinaryReader : IBinaryBuffer
{
    /// <summary>
    /// Read bytes from the underlying storage into the specified <paramref name="buffer"/>, and advances the current
    /// read position by the length of the bytes written to the <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">A <see cref="Span{T}"/> to receive the data.</param>
    int Read(Span<byte> buffer);
}


/// <summary>
/// Represent an object that supports writing arbitrary data to an underlying memory storage.
/// </summary>
[PublicAPI]
public interface IBinaryWriter : IBinaryBuffer
{
    /// <summary>
    /// Writes bytes from the specified <paramref name="buffer"/> into the underlying storage, and advances the current
    /// write position by the length of the bytes written.
    /// </summary>
    /// <param name="buffer">A <see cref="ReadOnlySpan{T}"/> containing the data to write.</param>
    /// <remarks>The number of bytes written.</remarks>
    int Write(ReadOnlySpan<byte> buffer);
}