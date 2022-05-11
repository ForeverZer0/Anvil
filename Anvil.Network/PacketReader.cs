using System.Runtime.CompilerServices;
using System.Text;
using Anvil.Network.API;

namespace Anvil.Network;

internal class PacketReader : IBinaryReader
{
    private int cursor;
    private int length;

    private readonly int headerOffset;
    private readonly byte[] sourceBuffer;

    /// <inheritdoc />
    public Endianness Endian { get; }

    /// <inheritdoc />
    public Encoding Encoding { get; }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public int Read(Span<byte> buffer)
    {
        var size = Math.Min(buffer.Length, length - cursor);
        if (size + headerOffset > sourceBuffer.Length)
            throw new EndOfStreamException();

        var span = new ReadOnlySpan<byte>(sourceBuffer, headerOffset + cursor, size);
        span.CopyTo(buffer);
        cursor += size;
        return size;
    }
    
    public PacketReader(byte[] buffer, int offset, Encoding? encoding = null, Endianness endian = Endianness.Little)
    {
        Encoding = encoding ?? Encoding.UTF8;
        Endian = endian;
        sourceBuffer = buffer;
        headerOffset = offset;
        cursor = 0;
        length = buffer.Length;
    }

    public void SetLength(int newLength)
    {
        length = newLength;
        cursor = 0;
    }
}