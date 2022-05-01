using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Anvil.Network.API;

namespace Anvil.Network;

// /// <inheritdoc />
// protected override ReadOnlySpan<byte> Read(int count)
// {
//     if (count > DataBuffer.Length)
//         throw new InternalBufferOverflowException($"Read operation cannot exceed {DataBuffer.Length} bytes.");
//
//     var totalRead = 0;
//     do
//     {
//         SpinWait.SpinUntil(() => BaseStream.DataAvailable);
//         var read = BaseStream.Read(DataBuffer, totalRead, count - totalRead);
//         if (read < 0)
//             break;
//
//         totalRead += count;
//
//     } while (totalRead < count);
//
//     Debug.Assert(totalRead == count);
//     BytesRead += totalRead;
//     return new ReadOnlySpan<byte>(DataBuffer, 0, totalRead);
// }
//
// /// <inheritdoc />
// protected override byte ReadByte()
// {
//     SpinWait.SpinUntil(() => BaseStream.DataAvailable);
//     return (byte) BaseStream.ReadByte();
// }


public class StreamPacketReader
{
    
}


/// <summary>
/// Contains methods for reading binary packet data from an arbitrary buffer.
/// </summary>
public class BinaryPacketReader : IPacketReader
{
    private const int SIZEOF_BYTE = sizeof(byte);
    private const int SIZEOF_SHORT = sizeof(short);
    private const int SIZEOF_INT = sizeof(int);
    private const int SIZEOF_LONG = sizeof(long);
    private const int SIZEOF_FLOAT = sizeof(float);
    private const int SIZEOF_DOUBLE = sizeof(double);
    
    /// <summary>
    /// Gets the encoding used when reading strings.
    /// </summary>
    public Encoding Encoding { get; }
    
    /// <summary>
    /// Gets the raw buffer representing the data payload of this <see cref="BinaryPacketReader"/>.
    /// </summary>
    public byte[] Buffer { get; private set; }

    private int cursorPos;
    private int cursorEnd;
    private readonly bool swapEndian;
    
    public BinaryPacketReader(byte[] buffer, int offset, int length, Encoding? encoding, bool swapEndian)
    {
        Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
        cursorPos = offset;
        cursorEnd = offset + length;
        Encoding = encoding ?? Encoding.UTF8;
        this.swapEndian = swapEndian;
        SetPayload(buffer, offset, length);
    }
    
    public BinaryPacketReader(Encoding? encoding, bool swapEndian)
    {
        Buffer = Array.Empty<byte>();
        cursorPos = 0;
        cursorEnd = 0;
        Encoding = encoding ?? Encoding.UTF8;
        this.swapEndian = swapEndian;
    }

    public void SetPayload(byte[]? payload, int offset, int length)
    {
        Buffer = payload ?? Array.Empty<byte>();
        cursorPos = offset;
        cursorEnd = offset + length;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AssertConstraints(int count)
    {
        if (cursorPos + count > cursorEnd)
            throw new InternalBufferOverflowException("An attempt was made to read past the end of the buffer.");
    }

    /// <inheritdoc />
    public bool ReadBool() => ReadInt8() != 0;

    /// <inheritdoc />
    public byte ReadInt8()
    {
        AssertConstraints(SIZEOF_BYTE);
        return Buffer[cursorPos++];
    }

    /// <inheritdoc />
    public short ReadInt16()
    {
        AssertConstraints(SIZEOF_SHORT);
        var value = BitConverter.ToInt16(Buffer, cursorPos);
        cursorPos += SIZEOF_SHORT;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public int ReadInt32()
    {
        AssertConstraints(SIZEOF_INT);
        var value = BitConverter.ToInt32(Buffer, cursorPos);
        cursorPos += SIZEOF_INT;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public long ReadInt64()
    {
        AssertConstraints(SIZEOF_LONG);
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
        AssertConstraints(SIZEOF_FLOAT);
        var value = BitConverter.ToSingle(Buffer, cursorPos);
        cursorPos += SIZEOF_FLOAT;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public double ReadDouble()
    {
        AssertConstraints(SIZEOF_DOUBLE);
        var value = BitConverter.ToDouble(Buffer, cursorPos);
        cursorPos += SIZEOF_DOUBLE;
        return swapEndian ? value.SwapEndian() : value;
    }

    /// <inheritdoc />
    public string ReadString()
    {
        var length = ReadVarInt();
        AssertConstraints(length);
        var str = Encoding.GetString(Buffer, cursorPos, length);
        cursorPos += length;
        return str;
    }

    /// <inheritdoc />
    public Span<byte> ReadBuffer(int count)
    {
        AssertConstraints(count);
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
        AssertConstraints(count);
        var span = new ReadOnlySpan<byte>(Buffer, cursorPos, count);
        cursorPos += count;
        return MemoryMarshal.Read<T>(span);
    }
}