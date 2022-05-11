using System.Buffers;
using System.Text;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

/// <summary>
/// Provides an arbitrary block of heap-allocated memory from a memory pool, allowing for cheap allocation of
/// temporary-use memory.
/// </summary>
/// <remarks>
/// The allocated memory may or may not be zeroed out, but it should never be assumed to be so.
/// </remarks>
[PublicAPI]
public class BinaryBuffer : IBinaryReader, IBinaryWriter, IDisposable
{
    /// <summary>
    /// Gets the maximum capacity that can be allocated.
    /// </summary>
    public const int MaxCapacity = ushort.MaxValue;
    
    /// <summary>
    /// Shared pool for memory allocations.
    /// </summary>
    private static readonly ArrayPool<byte> MemoryPool;

    /// <summary>
    /// Static constructor to initialize <see cref="ArrayPool{T}"/> used for memory allocation.
    /// </summary>
    static BinaryBuffer()
    {
        MemoryPool = ArrayPool<byte>.Create(MaxCapacity + byte.MaxValue, 16);
    }

    /// <summary>
    /// Gets the current position for reading/writing.
    /// </summary>
    public int Position => (int) Stream.Position;

    /// <summary>
    /// Gets the maximum capacity of the backing memory used by the <see cref="BinaryBuffer"/>.
    /// </summary>
    public int Capacity { get; }
    
    /// <summary>
    /// Gets the capacity of the backing memory.
    /// </summary>
    /// <remarks>
    /// This property is redundant, but required for to be classified as "Countable" by the runtime and get support for
    /// range indexers. 
    /// </remarks>
    public int Length { get; }

    /// <summary>
    /// Gets the raw underlying byte array. The length of this array may differ from the requested capacity, but any
    /// extra length is not considered valid, this is merely a byproduct of the memory pool selecting the most efficient
    /// block that met the minimum requirement of the requested capacity.
    /// </summary>
    public byte[] Buffer => rentedBuffer;

    /// <summary>
    /// Gets a <see cref="MemoryStream"/> that encompasses the backing memory.
    /// </summary>
    public MemoryStream Stream { get; }

    /// <summary>
    /// Returns a sub-region of the backing memory as a <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="start">The start index of the backing memory where the span should begin.</param>
    /// <param name="length">The desired length of the span.</param>
    /// <returns>A <see cref="Span{T}"/> over the requested region.</returns>
    public Span<byte> Slice(int start, int length) => new(rentedBuffer, start, length);

    /// <summary>
    /// Gets the byte value at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The zero-based index into the buffer to retrieve.</param>
    public byte this[int index] => rentedBuffer[index];

    /// <summary>
    /// Creates a new instance of the <see cref="BinaryBuffer"/> class with the specified <paramref name="capacity"/>.
    /// </summary>
    /// <param name="capacity">The minimum required capacity needed for the buffer, up to <c>65535</c>.</param>
    /// <param name="encoding">
    /// The encoding used for reading/writing strings, or <c>null</c> to use the default <see cref="UTF8Encoding"/>.
    /// </param>
    public BinaryBuffer(int capacity, Encoding? encoding = null) : this(capacity, true, 0, encoding)
    {
    }

    internal BinaryBuffer(int capacity, bool write, int offset = 0, Encoding? encoding = null)
    {
        if (capacity is < 0 or > MaxCapacity)
            throw new ArgumentOutOfRangeException(nameof(capacity), $"Capacity must be in the range of 0..{MaxCapacity}.");
        
        rentedBuffer = MemoryPool.Rent(capacity + offset);
        Stream = new MemoryStream(rentedBuffer, offset, capacity, write);
        Capacity = capacity;
        Encoding = encoding ?? Encoding.UTF8;
        this.offset = offset;
    }
    
    /// <inheritdoc cref="IDisposable.Dispose"/>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;
        MemoryPool.Return(rentedBuffer);    
        Stream.Dispose();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets or sets the endianness of numerical primitives when reading/writing.
    /// </summary>
    public Endianness Endian { get; set; }

    /// <summary>
    /// Gets the encoding used when reading/writing strings.
    /// </summary>
    public Encoding Encoding { get; }

    /// <inheritdoc />
    public int Write(ReadOnlySpan<byte> buffer)
    {
        Stream.Write(buffer);
        return buffer.Length;
    }

    /// <inheritdoc />
    public int Read(Span<byte> buffer) => Stream.Read(buffer);

    /// <summary>
    /// Implicit conversion of a <see cref="BinaryBuffer"/> into a <see cref="Stream"/>.
    /// </summary>
    /// <param name="buffer">The <see cref="BinaryBuffer"/>.</param>
    /// <returns>The <see cref="Stream"/> representation of the <paramref name="buffer"/>.</returns>
    public static implicit operator Stream(BinaryBuffer buffer) => buffer.Stream;
    
    internal ReadOnlyMemory<byte> AsMemory() => new(rentedBuffer, 0, offset + Position);

    internal ReadOnlySpan<byte> AsSpan() => new(rentedBuffer, 0, offset + Position);

    private readonly byte[] rentedBuffer;
    private readonly int offset;
}