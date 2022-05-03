using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Anvil.Network.API;

namespace Anvil.Network;


public class PacketReader : IPacketReader, IDisposable
{
    /// <inheritdoc />
    public Stream BaseStream { get; }

    private readonly BinaryReader reader;

    public PacketReader(byte[] buffer, Encoding? encoding = null)
    {
        BaseStream = new MemoryStream(buffer, false);
        reader = new BinaryReader(BaseStream, encoding ?? Encoding.UTF8, false);
    }
    
    public PacketReader(Socket socket, Encoding? encoding = null, bool ownsSocket = false)
    {
        BaseStream = new NetworkStream(socket, ownsSocket);
        reader = new BinaryReader(BaseStream, encoding ?? Encoding.UTF8, false);
    }
    
    public PacketReader(Stream stream, Encoding? encoding = null, bool leaveOpen = false) 
    {
        BaseStream = stream ?? throw new ArgumentNullException(nameof(stream));
        reader = new BinaryReader(BaseStream, encoding ?? Encoding.UTF8, false);
    }

    /// <inheritdoc />
    public byte[] ReadBytes(int count) => reader.ReadBytes(count);

    /// <inheritdoc />
    public TEnum8 ReadByte<TEnum8>() where TEnum8 : unmanaged, Enum
    {
        var value = reader.ReadByte();
        return Unsafe.As<byte, TEnum8>(ref value);
    }

    /// <inheritdoc />
    public byte ReadByte() => reader.ReadByte();

    /// <inheritdoc />
    public TEnum16 ReadInt16<TEnum16>() where TEnum16 : unmanaged, Enum
    {
        var value = reader.ReadInt16();
        return Unsafe.As<short, TEnum16>(ref value);
    }

    /// <inheritdoc />
    public short ReadInt16() => reader.ReadInt16();

    /// <inheritdoc />
    public TEnum32 ReadInt32<TEnum32>() where TEnum32 : unmanaged, Enum
    {
        var value = reader.ReadInt32();
        return Unsafe.As<int, TEnum32>(ref value);
    }

    /// <inheritdoc />
    public int ReadInt32() => reader.ReadInt32();

    /// <inheritdoc />
    public TEnum64 ReadInt64<TEnum64>() where TEnum64 : unmanaged, Enum
    {
        var value = reader.ReadInt64();
        return Unsafe.As<long, TEnum64>(ref value);
    }

    /// <inheritdoc />
    public long ReadInt64() => reader.ReadInt64();

    /// <inheritdoc />
    public TEnum32 ReadVarInt<TEnum32>()
    {
        var value = reader.Read7BitEncodedInt();
        return Unsafe.As<int, TEnum32>(ref value);
    }

    /// <inheritdoc />
    public int ReadVarInt() => reader.Read7BitEncodedInt();

    /// <inheritdoc />
    public TEnum64 ReadVarLong<TEnum64>()
    {
        var value = reader.Read7BitEncodedInt64();
        return Unsafe.As<long, TEnum64>(ref value);
    }

    /// <inheritdoc />
    public long ReadVarLong() => reader.Read7BitEncodedInt64();

    /// <inheritdoc />
    public bool ReadBool() => reader.ReadBoolean();

    /// <inheritdoc />
    public double ReadDouble() => reader.ReadDouble();

    /// <inheritdoc />
    public Half ReadHalf() => reader.ReadHalf();

    /// <inheritdoc />
    public float ReadFloat() => reader.ReadSingle();

    /// <inheritdoc />
    public string ReadString() => reader.ReadString();

    /// <inheritdoc />
    public T[] ReadArray<T>() where T : unmanaged
    {
        var size = ReadVarInt();
        if (size == 0)
            return Array.Empty<T>();
        
        var array = new T[size];
        var span = MemoryMarshal.AsBytes(new Span<T>(array));

        var read = 0;
        do
        {
            read += BaseStream.Read(span[read..]);
        } while (read < span.Length);

        return array;
    }
    
    /// <inheritdoc />
    public DateTime ReadTime()
    {
        var value = ReadInt64();
        return Unsafe.As<long, DateTime>(ref value);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            reader.Dispose();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}