using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

[PublicAPI]
public class BinaryPacketWriter : IPacketWriter
{
    private const int SIZEOF_BYTE   = sizeof(byte);
    private const int SIZEOF_SHORT  = sizeof(short);
    private const int SIZEOF_INT    = sizeof(int);
    private const int SIZEOF_LONG   = sizeof(long);
    private const int SIZEOF_FLOAT  = sizeof(float);
    private const int SIZEOF_DOUBLE = sizeof(double);

    private const int SIZEOF_VARINT = 5;
    private const int SIZEOF_VARLONG = 10;
    
    /// <summary>
    /// Gets the encoding used for writing strings.
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    /// Gets the raw buffer representing the data payload of this <see cref="BinaryPacketWriter"/>.
    /// </summary>
    public byte[] Buffer { get; private set; }

    private int startPos;
    private int cursorPos;
    private int cursorEnd;

    /// <summary>
    /// Creates a new <see cref="BinaryPacketWriter"/> instance with the specified <paramref name="buffer"/> as its
    /// backing storage.
    /// </summary>
    /// <param name="buffer">A buffer that will be written to.</param>
    /// <param name="start">The index into the <paramref name="buffer"/> to begin writing.</param>
    /// <param name="length">The maximum number of bytes that can be written.</param>
    /// <param name="encoding">The encoding to use for text, or <c>null</c> to use the default <see cref="UTF8Encoding"/>.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="buffer"/> is <c>null</c>.</exception>
    public BinaryPacketWriter(byte[] buffer, int start, int length, Encoding? encoding = null)
    {
        Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        startPos = start;
        cursorPos = start;
        cursorEnd = start + length;
        Encoding = encoding ?? Encoding.UTF8;
    }

    /// <summary>
    /// Creates a new <see cref="BinaryPacketWriter"/> instance with no backing storage.
    /// </summary>
    /// <param name="encoding">The encoding to use for text, or <c>null</c> to use the default <see cref="UTF8Encoding"/>.</param>
    public BinaryPacketWriter(Encoding? encoding = null) : this(Array.Empty<byte>(), 0, 0, encoding)
    {
    }
    
    /// <summary>
    /// Sets the underlying data store for the <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="buffer">The buffer to write to.</param>
    /// <param name="start">The offset into the <paramref name="buffer"/> to begin writing.</param>
    /// <param name="length">The maximum number of bytes that can be written to the <paramref name="buffer"/>.</param>
    public void SetBuffer(byte[] buffer, int start, int length)
    {
        Buffer = buffer;
        startPos = start;
        cursorPos = start;
        cursorEnd = start + length;
    }

    /// <summary>
    /// Gets a <see cref="Span{T}"/> of bytes over the region of the <see cref="Buffer"/> that have been written to.
    /// </summary>
    /// <returns>A <see cref="Span{T}"/> containing the region written to by this <see cref="BinaryWriter"/>.</returns>
    public Span<byte> AsSpan() => new (Buffer, startPos, cursorPos - startPos);

    /// <summary>
    /// Gets the total number of bytes that have been written to the current <see cref="Buffer"/>.
    /// </summary>
    public int Length => cursorPos - startPos;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AssertBounds(int count)
    {
        if (cursorPos + count > cursorEnd)
            throw new InternalBufferOverflowException("An attempt was made to write past the end of the buffer.");
    }

    /// <inheritdoc />
    public void WriteBool(bool value) => WriteInt8((byte) (value ? 0 : 1));

    /// <inheritdoc />
    public void WriteInt8(byte value)
    {
        AssertBounds(SIZEOF_BYTE);
        Buffer[cursorPos++] = value;
    }

    /// <inheritdoc />
    public void WriteInt16(short value)
    {
        AssertBounds(SIZEOF_SHORT);
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_SHORT);
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);
        cursorPos += SIZEOF_SHORT;
    }

    /// <inheritdoc />
    public void WriteInt32(int value)
    {
        AssertBounds(SIZEOF_INT);
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_INT);
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);
        cursorPos += SIZEOF_INT;
    }

    /// <inheritdoc />
    public void WriteInt64(long value)
    {
        AssertBounds(SIZEOF_LONG);
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_LONG);
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);
        cursorPos += SIZEOF_LONG;
    }

    /// <inheritdoc />
    public void WriteVarInt(int value)
    {
        AssertBounds(SIZEOF_VARINT);
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_VARINT);
        cursorPos += VarInt.Write(span, value);
    }

    /// <inheritdoc />
    public void WriteVarInt<TEnum32>(TEnum32 value) where TEnum32 : unmanaged, Enum
    {
        AssertBounds(SIZEOF_VARINT);
        if (Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TEnum32))) != sizeof(int))
            throw new ArgumentException("Generic enum type must be backed by a 32-bit integer.", nameof(TEnum32));
        
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_VARINT);
        cursorPos += VarInt.Write(span, Unsafe.As<TEnum32, int>(ref value));
    }

    /// <inheritdoc />
    public void WriteVarLong(long value)
    {
        AssertBounds(SIZEOF_VARLONG);
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_VARLONG);
        cursorPos += VarLong.Write(span, value);
    }

    /// <inheritdoc />
    public void WriteFloat(float value)
    {
        AssertBounds(SIZEOF_FLOAT);
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_FLOAT);
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);
        cursorPos += SIZEOF_FLOAT;
    }

    /// <inheritdoc />
    public void WriteDouble(double value)
    {
        AssertBounds(SIZEOF_DOUBLE);
        var span = new Span<byte>(Buffer, cursorPos, SIZEOF_DOUBLE);
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(span), value);
        cursorPos += SIZEOF_DOUBLE;
    }

    /// <inheritdoc />
    public void WriteString(string? value)
    {
        if (value is null)
        {
            WriteVarInt(0);
            return;
        }
        
        var bytes = Encoding.GetBytes(value);
        WriteVarInt(bytes.Length);
        AssertBounds(bytes.Length);
        Array.Copy(bytes, 0, Buffer, cursorPos, bytes.Length);
        cursorPos += bytes.Length;
    }

    /// <inheritdoc />
    public void WriteEnum<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var size = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TEnum)));
        switch (size)
        {
            case SIZEOF_BYTE:
                WriteInt8(Unsafe.As<TEnum, byte>(ref value));
                break;
            case SIZEOF_SHORT:
                WriteInt16(Unsafe.As<TEnum, short>(ref value));
                break;
            case SIZEOF_INT:
                WriteInt32(Unsafe.As<TEnum, int>(ref value));
                break;
            case SIZEOF_LONG:
                WriteInt64(Unsafe.As<TEnum, long>(ref value));
                break;
            default:
                throw new InvalidDataException("Cannot write enum to the stream.");
        }
    }

    /// <inheritdoc />
    public void WriteStruct<T>(T value) where T : unmanaged
    {
        var size = Unsafe.SizeOf<T>();
        AssertBounds(size);
        var span = new Span<byte>(Buffer, cursorPos, size);
        MemoryMarshal.Write(span, ref value);
        cursorPos += size;
    }

    /// <inheritdoc />
    public void WriteBuffer(ReadOnlySpan<byte> value)
    {
        AssertBounds(value.Length);
        value.CopyTo(new Span<byte>(Buffer, cursorPos, value.Length));
        cursorPos += value.Length;
    }

    /// <inheritdoc />
    public void WriteBuffer(byte[] buffer, int start, int length)
    {
        AssertBounds(length);
        Array.Copy(buffer, start, Buffer, cursorPos, length);
        cursorPos += length;
    }

    /// <inheritdoc />
    public void WriteBuffer<T>(ReadOnlySpan<T> value) where T : unmanaged
    {
        var src = MemoryMarshal.AsBytes(value);
        AssertBounds(src.Length);
        
        var dst = new Span<byte>(Buffer, cursorPos, src.Length);
        src.CopyTo(dst);
        cursorPos += src.Length;
    }

    /// <inheritdoc />
    public void WriteBuffer<T>(T[] buffer, int start, int length) where T : unmanaged
    {
        var src = MemoryMarshal.AsBytes(new ReadOnlySpan<T>(buffer, start, length));
        WriteBuffer(src);
    }
}