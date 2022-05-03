using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

/// <summary>
/// Contains methods for reading binary packet data from an arbitrary buffer.
/// </summary>
[PublicAPI]
public class BinaryPacketReader : IPacketReader
{
    private const int SIZEOF_BYTE   = sizeof(byte);
    private const int SIZEOF_SHORT  = sizeof(short);
    private const int SIZEOF_INT    = sizeof(int);
    private const int SIZEOF_LONG   = sizeof(long);
    private const int SIZEOF_FLOAT  = sizeof(float);
    private const int SIZEOF_DOUBLE = sizeof(double);
    
    private int startPos;
    private int cursorPos;
    private int cursorEnd;
    private readonly bool swapEndian;
    
    /// <summary>
    /// Gets the encoding used for reading strings.
    /// </summary>
    public Encoding Encoding { get; }
    
    /// <summary>
    /// Gets the raw buffer representing the data payload of this <see cref="BinaryPacketReader"/>.
    /// </summary>
    public byte[] Buffer { get; private set; }
    
    /// <inheritdoc />
    public int Position => cursorPos - startPos;

    /// <inheritdoc />
    public int Length => cursorEnd - startPos;

    /// <summary>
    /// Creates a new <see cref="BinaryPacketReader"/> instance with the specified <paramref name="buffer"/> as its
    /// backing storage.
    /// </summary>
    /// <param name="buffer">A buffer that will be read from.</param>
    /// <param name="start">The index into the <paramref name="buffer"/> to begin reading.</param>
    /// <param name="length">The maximum number of bytes that can be read.</param>
    /// <param name="swapEndian">Flag indicating is endian-swapping is required.</param>
    /// <param name="encoding">The encoding to use for text, or <c>null</c> to use the default <see cref="UTF8Encoding"/>.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="buffer"/> is <c>null</c>.</exception>
    public BinaryPacketReader(byte[] buffer, int start, int length, bool swapEndian, Encoding? encoding = null)
    {
        Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        startPos = start;
        cursorPos = start;
        cursorEnd = start + length;
        Encoding = encoding ?? Encoding.UTF8;
        this.swapEndian = swapEndian;
    }
    
    /// <summary>
    /// Creates a new <see cref="BinaryPacketReader"/> instance with no backing storage.
    /// </summary>
    /// <param name="swapEndian">Flag indicating is endian-swapping is required.</param>
    /// <param name="encoding">The encoding to use for text, or <c>null</c> to use the default <see cref="UTF8Encoding"/>.</param>
    public BinaryPacketReader(bool swapEndian, Encoding? encoding = null)
    {
        Buffer = Array.Empty<byte>();
        startPos = 0;
        cursorPos = 0;
        cursorEnd = 0;
        Encoding = encoding ?? Encoding.UTF8;
        this.swapEndian = swapEndian;
    }

    /// <summary>
    /// Sets the underlying data store for the <see cref="BinaryPacketReader"/>.
    /// </summary>
    /// <param name="buffer">The buffer to write to.</param>
    /// <param name="start">The offset into the <paramref name="buffer"/> to begin writing.</param>
    /// <param name="length">The maximum number of bytes that can be written to the <paramref name="buffer"/>.</param>
    public void SetBuffer(byte[]? buffer, int start, int length)
    {
        Buffer = buffer ?? Array.Empty<byte>();
        startPos = start;
        cursorPos = start;
        cursorEnd = start + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AssertBounds(int count)
    {
        if (cursorPos + count > cursorEnd)
            throw new EndOfStreamException("An attempt was made to read past the end of the buffer.");
    }

    /// <inheritdoc />
    public bool ReadBool() => ReadInt8() != 0;

    /// <inheritdoc />
    public byte ReadInt8()
    {
        AssertBounds(SIZEOF_BYTE);
        return Buffer[cursorPos++];
    }

    /// <inheritdoc />
    public short ReadInt16()
    {
        AssertBounds(SIZEOF_SHORT);
        var value = BitConverter.ToInt16(Buffer, cursorPos);
        cursorPos += SIZEOF_SHORT;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public int ReadInt32()
    {
        AssertBounds(SIZEOF_INT);
        var value = BitConverter.ToInt32(Buffer, cursorPos);
        cursorPos += SIZEOF_INT;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public long ReadInt64()
    {
        AssertBounds(SIZEOF_LONG);
        var value = BitConverter.ToInt64(Buffer, cursorPos);
        cursorPos += SIZEOF_LONG;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public int ReadVarInt()
    {
        var value = VarInt.Decode(Buffer, cursorPos, cursorEnd - cursorPos, out var size);
        cursorPos += size;
        return value;
    }
    
    /// <inheritdoc />
    public TEnum32 ReadVarInt<TEnum32>()
    {
        if (Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TEnum32))) != sizeof(int))
            throw new ArgumentException("Generic enum type must be backed by a 32-bit integer.", nameof(TEnum32));
        
        var value = VarInt.Decode(Buffer, cursorPos, cursorEnd - cursorPos, out var size);
        cursorPos += size;
        return Unsafe.As<int, TEnum32>(ref value);
    }

    /// <inheritdoc />
    public long ReadVarLong()
    {
        var value = VarLong.Decode(Buffer, cursorPos, cursorEnd - cursorPos, out var size);
        cursorPos += size;
        return value;
    }

    /// <inheritdoc />
    public float ReadFloat()
    {
        AssertBounds(SIZEOF_FLOAT);
        var value = BitConverter.ToSingle(Buffer, cursorPos);
        cursorPos += SIZEOF_FLOAT;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public double ReadDouble()
    {
        AssertBounds(SIZEOF_DOUBLE);
        var value = BitConverter.ToDouble(Buffer, cursorPos);
        cursorPos += SIZEOF_DOUBLE;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public string ReadString()
    {
        var length = ReadVarInt();
        if (length == 0)
            return string.Empty;
        
        AssertBounds(length);
        var str = Encoding.GetString(Buffer, cursorPos, length);
        cursorPos += length;
        return str;
    }

    /// <inheritdoc />
    public Span<byte> ReadBuffer(int count)
    {
        AssertBounds(count);
        var span = new Span<byte>(Buffer, cursorPos, count);
        cursorPos += count;
        return span;
    }

    /// <inheritdoc />
    public Span<T> ReadBuffer<T>(int count) where T : unmanaged
    {
        count = Unsafe.SizeOf<T>() * count;
        var span = ReadBuffer(count);
        return MemoryMarshal.Cast<byte, T>(span);
    }

    /// <inheritdoc />
    public TEnum ReadEnum<TEnum>() where TEnum : struct, Enum
    {
        var size = Marshal.SizeOf(typeof(TEnum).GetEnumUnderlyingType());
        switch (size)
        {
            case 1:
            {
                var b = ReadInt8();
                return Unsafe.As<byte, TEnum>(ref b);
            }
            case 2:
            {
                var s = ReadInt16();
                return Unsafe.As<short, TEnum>(ref s);
            }
            case 4:
            {
                var i = ReadInt32();
                return Unsafe.As<int, TEnum>(ref i);
            }
            case 8:
            {
                var l = ReadInt64();
                return Unsafe.As<long, TEnum>(ref l);
            }
            default:
                throw new InvalidDataException("Cannot read enum from stream.");
        }
    }

    /// <inheritdoc />
    public T ReadStruct<T>() where T : unmanaged
    {
        var count = Unsafe.SizeOf<T>();
        AssertBounds(count);
        var span = new ReadOnlySpan<byte>(Buffer, cursorPos, count);
        cursorPos += count;
        return MemoryMarshal.Read<T>(span);
    }
}