using System.Buffers;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Base class for network packets.
/// <para/>
/// An internal memory pool is used as the backing store for the packet payloads, dramatically decreasing GC pressure
/// and heap allocations.
/// </summary>
[PublicAPI]
public abstract class Packet : IDisposable
{
    private bool borrowed;
    private static readonly ArrayPool<byte> memoryPool;

    /// <summary>
    /// Static constructor.
    /// </summary>
    static Packet()
    {
        memoryPool = ArrayPool<byte>.Create();
    }

    /// <summary>
    /// Gets a raw buffer containing the packet data.
    /// </summary>
    public byte[] Payload { get; }

    /// <summary>
    /// Initializes a new <see cref="Packet"/> instance with the specified <paramref name="payload"/>.
    /// </summary>
    /// <param name="payload">A buffer containing the raw packet payload.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="payload"/> is <c>null</c>.</exception>
    protected Packet(byte[] payload)
    {
        Payload = payload;
        borrowed = false;
    }
    
    /// <summary>
    /// Initializes a new <see cref="Packet"/> instance with a <see cref="Payload"/> containing at least the specified
    /// <paramref name="capacity"/>.
    /// </summary>
    /// <param name="capacity">The minimum required capacity of the <see cref="Payload"/>.</param>
    /// <remarks>
    /// The actual length of the <see cref="Payload"/> may differ depending on the requested capacity and available
    /// blocks in the pool, but will contain <i>at least</i> the desired amount. 
    /// </remarks>
    protected Packet(int capacity)
    {
        Payload = memoryPool.Rent(capacity);
        borrowed = true;
    }

    /// <summary>
    /// Initializes a new <see cref="Packet"/> instance, and reads the specified <paramref name="length"/> of bytes from
    /// the given <paramref name="stream"/> into its <see cref="Payload"/>.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> instance to read from containing the payload data.</param>
    /// <param name="length">The number of bytes to read from the <paramref name="stream"/>.</param>
    /// <exception cref="IOException">When the specified number of bytes cannot be read from the stream.</exception>
    /// <remarks>
    /// The actual length of the <see cref="Payload"/> may differ depending on the required length and available
    /// blocks in the pool, but will contain <i>at least</i> the needed amount to read <paramref name="length"/> bytes
    /// from the <paramref name="stream"/>. 
    /// </remarks>
    protected Packet(Stream stream, int length) : this(length)
    {
        if (length != stream.Read(Payload, 0, length))
            throw new IOException("Failed to read the specified number of bytes from stream");
        borrowed = true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (borrowed)
            memoryPool.Return(Payload, true);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Object finalizer.
    /// </summary>
    ~Packet()
    {
        Dispose(false);
    }
}