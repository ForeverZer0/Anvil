using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

// TODO: Maximum packet size

/// <summary>
/// Concrete implementation of a <see cref="IPacketWriter"/>, suitable for general-purpose writing of data types used
/// by binary network packets.
/// </summary>
[PublicAPI]
public class PacketWriter : IPacketWriter
{
    /// <inheritdoc />
    public Stream BaseStream { get; }

    private readonly BinaryWriter writer;
    
    /// <summary>
    /// Creates a new instance of the <see cref="PacketWriter"/> class, wrapping the specified <paramref name="buffer"/>
    /// as the underlying data store to write to.
    /// </summary>
    /// <param name="buffer">An array of bytes to write to.</param>
    /// <param name="encoding">
    /// The encoding to use for text writing, or <c>null</c> to use the default <see cref=" UTF8Encoding"/> encoding.
    /// </param>
    public PacketWriter(byte[] buffer, Encoding? encoding = null) 
        : this(new MemoryStream(buffer, true), encoding, false)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="PacketWriter"/> class, wrapping the specified <paramref name="socket"/>
    /// as the underlying data store to write to.
    /// </summary>
    /// <param name="socket">The <see cref="Socket"/> to write to.</param>
    /// <param name="encoding">
    /// The encoding to use for text writing, or <c>null</c> to use the default <see cref=" UTF8Encoding"/> encoding.
    /// </param>
    /// <param name="ownsSocket">
    /// Flag indicating if the <paramref name="socket"/> should be left open or closed when this object is disposed.
    /// </param>
    /// <exception cref="ArgumentNullException">The <paramref name="socket"/> is <c>null</c>.</exception>
    /// <exception cref="IOException">
    /// The <paramref name="socket"/> is not open or does not a <see cref="SocketType.Stream"/> type.
    /// </exception>
    public PacketWriter(Socket socket, Encoding? encoding = null, bool ownsSocket = false)
        : this(new NetworkStream(socket, ownsSocket), encoding, false)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="PacketWriter"/> class, wrapping the specified <paramref name="stream"/>
    /// as the underlying data store to write to.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="encoding">
    /// The encoding to use for text writing, or <c>null</c> to use the default <see cref=" UTF8Encoding"/> encoding.
    /// </param>
    /// <param name="leaveOpen">
    /// Flag indicating if the <paramref name="stream"/> should be left open or closed when this object is disposed.
    /// </param>
    /// <exception cref="ArgumentNullException">The <paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="NotSupportedException">The <paramref name="stream"/> is not opened for writing.</exception>
    public PacketWriter(Stream stream, Encoding? encoding = null, bool leaveOpen = false) 
    {
        BaseStream = stream ?? throw new ArgumentNullException(nameof(stream));
        if (!BaseStream.CanWrite)
            throw new NotSupportedException("The stream is not opened for writing.");
        writer = new BinaryWriter(BaseStream, encoding ?? Encoding.UTF8, leaveOpen);
    }

    /// <inheritdoc />
    public void WriteBytes(byte[] buffer) => writer.Write(buffer);

    /// <inheritdoc />
    public void WriteBytes(ReadOnlySpan<byte> buffer) => writer.Write(buffer);

    /// <inheritdoc />
    public void WriteByte<TEnum8>(TEnum8 value) where TEnum8 : unmanaged, Enum
    {
        writer.Write(Unsafe.As<TEnum8, byte>(ref value));
    }

    /// <inheritdoc />
    public void WriteByte(byte value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteInt16<TEnum16>(TEnum16 value) where TEnum16 : unmanaged, Enum
    {
        writer.Write(Unsafe.As<TEnum16, short>(ref value));
    }

    /// <inheritdoc />
    public void WriteInt16(short value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteInt32<TEnum32>(TEnum32 value) where TEnum32 : unmanaged, Enum
    {
        writer.Write(Unsafe.As<TEnum32, int>(ref value));
    }

    /// <inheritdoc />
    public void WriteInt32(int value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteInt64<TEnum64>(TEnum64 value) where TEnum64 : unmanaged, Enum
    {
        writer.Write(Unsafe.As<TEnum64, long>(ref value));
    }

    /// <inheritdoc />
    public void WriteInt64(long value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteVarInt<TEnum32>(TEnum32 value) => writer.Write7BitEncodedInt(Unsafe.As<TEnum32, int>(ref value));

    /// <inheritdoc />
    public void WriteVarInt(int value) => writer.Write7BitEncodedInt64(value);

    /// <inheritdoc />
    public void WriteVarLong<TEnum64>(TEnum64 value) => writer.Write7BitEncodedInt64(Unsafe.As<TEnum64, long>(ref value));

    /// <inheritdoc />
    public void WriteVarLong(long value) => writer.Write7BitEncodedInt64(value);

    /// <inheritdoc />
    public void WriteBool(bool value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteDouble(double value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteHalf(Half value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteFloat(float value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteString(string value) => writer.Write(value);

    /// <inheritdoc />
    public void WriteArray<T>(T[] array) where T : unmanaged
    {
        writer.Write7BitEncodedInt(array.Length);
        if (array.Length == 0)
            return;

        var span = MemoryMarshal.AsBytes(new ReadOnlySpan<T>(array));
        writer.Write(span);
    }

    /// <inheritdoc />
    public void WriteTime(DateTime time) => writer.Write(Unsafe.As<DateTime, long>(ref time));
}