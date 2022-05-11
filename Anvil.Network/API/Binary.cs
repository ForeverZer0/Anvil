using System.Buffers.Binary;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Provides extension methods for reading and writing from objects that implement <see cref="IBinaryReader"/> and/or
/// <see cref="IBinaryWriter"/>.
/// </summary>
[PublicAPI]
public static class Binary
{
    /// <summary>
    /// Threshold determining when temporary memory gets stack-allocated or heap-allocated.
    /// </summary>
    private const int STACKALLOC_MAX = byte.MaxValue;
    
    /// <summary>
    /// Asserts that that the underlying enum type is the required number of bytes.
    /// </summary>
    /// <param name="required">The number of bytes required.</param>
    /// <typeparam name="TEnum">An enum type.</typeparam>
    /// <exception cref="ConstraintException">When the actual size differs from the required size.</exception>
    internal static void AssetSize<TEnum>(int required) where TEnum : unmanaged, Enum
    {
        var actual = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TEnum)));
        if (actual != required)
            throw new ConstraintException($"Enumeration type must be backed by a {required * 8}-bit integer type.");
    }

    /// <summary>
    /// Read bytes from the underlying memory into the specified <paramref name="buffer"/>, and advances the stream by
    /// the length of the bytes read.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <param name="buffer">A byte array to receive the data.</param>
    /// <param name="offset">The offset into the <paramref name="buffer"/> to begin writing.</param>
    /// <param name="count">The number of bytes to read into the <paramref name="buffer"/>.</param>
    /// <remarks>The number of bytes read from the underlying buffer.</remarks>
    public static int Read(this IBinaryReader reader, byte[] buffer, int offset, int count)
    {
        return reader.Read(new Span<byte>(buffer, offset, count));
    }
    
    /// <summary>
    /// Reads a boolean value from the underlying buffer, and advances the read cursor by one byte.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static bool ReadBool(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[1];
        reader.Read(buffer);
        return buffer[0] != 0;
    }
    
    /// <summary>
    /// Reads a 8-bit integer from the underlying buffer, and advances the read cursor by one byte.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static byte ReadInt8(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[1];
        reader.Read(buffer);
        return buffer[0];
    }
    
    /// <summary>
    /// Reads a 16-bit integer from the underlying buffer, and advances the read cursor by two bytes.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static short ReadInt16(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[2];
        reader.Read(buffer);
        return reader.Endian == Endianness.Little
            ? BinaryPrimitives.ReadInt16LittleEndian(buffer)
            : BinaryPrimitives.ReadInt16BigEndian(buffer);
    }
    
    /// <summary>
    /// Reads a 32-bit integer from the underlying buffer, and advances the read cursor by four bytes.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static int ReadInt32(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[4];
        reader.Read(buffer);
        return reader.Endian == Endianness.Little
            ? BinaryPrimitives.ReadInt32LittleEndian(buffer)
            : BinaryPrimitives.ReadInt32BigEndian(buffer);
    }
    
    /// <summary>
    /// Reads a 64-bit integer from the underlying buffer, and advances the read cursor by eight byte.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static long ReadInt64(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[8];
        reader.Read(buffer);
        return reader.Endian == Endianness.Little
            ? BinaryPrimitives.ReadInt64LittleEndian(buffer)
            : BinaryPrimitives.ReadInt64BigEndian(buffer);
    }
    
    /// <summary>
    /// Reads a 32-bit integer from the underlying buffer 7-bits at a time, and advances the read cursor by one to
    /// five bytes.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static int ReadVarInt(this IBinaryReader reader)
    {
        uint value = 0;
        for (var index = 0; index < 28; index += 7)
        {
            var b = ReadInt8(reader);
            value |= (uint) ((b & 0x7F) << index);
            if (b <= 0x7F)
                return Unsafe.As<uint, int>(ref value);
        }
        var last = ReadInt8(reader);
        if (last > 0xF)
            throw new FormatException("Invalid formatted VarInt.");
        return (int) (value | (uint) last << 28);
    }
    
    /// <summary>
    /// Reads a 64-bit integer from the underlying buffer 7-bits at a time, and advances the read cursor by one to
    /// ten bytes.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static long ReadVarLong(this IBinaryReader reader)
    {
        ulong value = 0;
        for (var index = 0; index < 63; index += 7)
        {
            var b = ReadInt8(reader);
            value |= ((b & 0x7FUL) << index);
            if (b <= 0x7F)
                return Unsafe.As<ulong, long>(ref value);
        }
        
        var last = ReadInt8(reader);
        if (last > 1)
            throw new FormatException("Invalid formatted VarLong.");
        return (long) (value | (ulong) last << 63);
    }

    /// <summary>
    /// Reads a half-precision 16-bit floating point number from the underlying buffer, and advances the read cursor by
    /// two bytes.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static Half ReadHalf(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[2];
        reader.Read(buffer);
        return reader.Endian == Endianness.Little
            ? BinaryPrimitives.ReadHalfLittleEndian(buffer)
            : BinaryPrimitives.ReadHalfBigEndian(buffer);
    }
    
    /// <summary>
    /// Reads a single-precision 32-bit floating point number from the underlying buffer, and advances the read cursor
    /// by four bytes.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static float ReadSingle(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[4];
        reader.Read(buffer);
        return reader.Endian == Endianness.Little
            ? BinaryPrimitives.ReadSingleLittleEndian(buffer)
            : BinaryPrimitives.ReadSingleBigEndian(buffer);
    }
    
    /// <summary>
    /// Reads a single-precision 64-bit floating point number from the underlying buffer, and advances the read cursor
    /// by eight bytes.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    public static double ReadDouble(this IBinaryReader reader)
    {
        Span<byte> buffer = stackalloc byte[8];
        reader.Read(buffer);
        return reader.Endian == Endianness.Little
            ? BinaryPrimitives.ReadDoubleLittleEndian(buffer)
            : BinaryPrimitives.ReadDoubleBigEndian(buffer);
    }

    /// <summary>
    /// Reads a string from the underlying buffer with the <see cref="IBinaryBuffer.Encoding"/>, and advances the read
    /// cursor by the number of bytes read. The string is prefixed with its size as an integer encoded 7-bits at a time.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <returns>The value read from the buffer.</returns>
    /// <remarks>When string length is <c>0</c>, an empty string will be returned.</remarks>
    public static string ReadString(this IBinaryReader reader)
    {
        var length = ReadVarInt(reader);
        if (length <= 0)
            return string.Empty;
        
        var buffer = new byte[length];
        reader.Read(buffer);
        return reader.Encoding.GetString(buffer, 0, length);
    }
    
    /// <summary>
    /// Reads an arbitrary value-type from the underlying buffer, and advances the read cursor by the number of bytes
    /// read.
    /// </summary>
    /// <param name="reader">The <see cref="IBinaryReader"/> instance.</param>
    /// <typeparam name="T">A value type (struct) consisting of only primitive types.</typeparam>
    /// <returns>The value read from the buffer.</returns>
    public static T Read<T>(this IBinaryReader reader) where T : unmanaged
    {
        var size = Unsafe.SizeOf<T>();

        if (size <= STACKALLOC_MAX)
        {
            Span<byte> buffer = stackalloc byte[size];
            reader.Read(buffer);
            return MemoryMarshal.Read<T>(buffer);
        }

        var bytes = new byte[size];
        var span = new Span<byte>(bytes, 0, size);
        reader.Read(span);
        return MemoryMarshal.Read<T>(span);
    }

    
    /// <inheritdoc cref="ReadInt8(IBinaryReader)" />
    /// <typeparam name="TEnum8">An <see cref="Enum"/> type backed by a 8-bit integer.</typeparam>
    /// <remarks>
    /// The bit-width of the enumeration type is not checked, and passing an incorrectly sized value will result in
    /// undefined behavior, it is up to the caller to ensure the constraints.
    /// </remarks>
    public static TEnum8 ReadInt8<TEnum8>(this IBinaryReader reader) where TEnum8 : unmanaged, Enum
    {
        var value = ReadInt8(reader);
        return Unsafe.As<byte, TEnum8>(ref value);
    }

    /// <inheritdoc cref="ReadInt16(IBinaryReader)" />
    /// <typeparam name="TEnum16">An <see cref="Enum"/> type backed by a 16-bit integer.</typeparam>
    /// <remarks>
    /// The bit-width of the enumeration type is not checked, and passing an incorrectly sized value will result in
    /// undefined behavior, it is up to the caller to ensure the constraints.
    /// </remarks>
    public static TEnum16 ReadInt16<TEnum16>(this IBinaryReader reader) where TEnum16 : unmanaged, Enum
    {
        var value = ReadInt16(reader);
        return Unsafe.As<short, TEnum16>(ref value);
    }

    /// <inheritdoc cref="ReadInt32(IBinaryReader)" />
    /// <typeparam name="TEnum32">An <see cref="Enum"/> type backed by a 32-bit integer.</typeparam>
    /// <remarks>
    /// The bit-width of the enumeration type is not checked, and passing an incorrectly sized value will result in
    /// undefined behavior, it is up to the caller to ensure the constraints.
    /// </remarks>
    public static TEnum32 ReadInt32<TEnum32>(this IBinaryReader reader) where TEnum32 : unmanaged, Enum
    {
        var value = ReadInt32(reader);
        return Unsafe.As<int, TEnum32>(ref value);
    }

    /// <inheritdoc cref="ReadInt64(IBinaryReader)" />
    /// <typeparam name="TEnum64">An <see cref="Enum"/> type backed by a 64-bit integer.</typeparam>
    /// <remarks>
    /// The bit-width of the enumeration type is not checked, and passing an incorrectly sized value will result in
    /// undefined behavior, it is up to the caller to ensure the constraints.
    /// </remarks>
    public static TEnum64 ReadInt64<TEnum64>(this IBinaryReader reader) where TEnum64 : unmanaged, Enum
    {
        var value = ReadInt64(reader);
        return Unsafe.As<long, TEnum64>(ref value);
    }

    /// <inheritdoc cref="ReadVarInt(IBinaryReader)" />
    /// <typeparam name="TEnum32">An <see cref="Enum"/> type backed by a 32-bit integer.</typeparam>
    /// <remarks>
    /// The bit-width of the enumeration type is not checked, and passing an incorrectly sized value will result in
    /// undefined behavior, it is up to the caller to ensure the constraints.
    /// </remarks>
    public static TEnum32 ReadVarInt<TEnum32>(this IBinaryReader reader) where TEnum32 : unmanaged, Enum
    {
        var value = ReadVarInt(reader);
        return Unsafe.As<int, TEnum32>(ref value);
    }

    /// <inheritdoc cref="ReadVarLong(IBinaryReader)" />
    /// <typeparam name="TEnum64">An <see cref="Enum"/> type backed by a 8-bit integer.</typeparam>
    /// <remarks>
    /// The bit-width of the enumeration type is not checked, and passing an incorrectly sized value will result in
    /// undefined behavior, it is up to the caller to ensure the constraints.
    /// </remarks>
    public static TEnum64 ReadVarLong<TEnum64>(this IBinaryReader reader) where TEnum64 : unmanaged, Enum
    {
        var value = ReadVarLong(reader);
        return Unsafe.As<long, TEnum64>(ref value);
    }

    /// <summary>
    /// Writes a contiguous region of bytes to the underlying buffer and advances the write cursor by the number of
    /// bytes written.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="buffer">An array containing the bytes to write.</param>
    /// <param name="offset">The offset into the <paramref name="buffer"/> to begin copying from.</param>
    /// <param name="count">The number of bytes to copy from the <paramref name="buffer"/>.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int Write(this IBinaryWriter writer, byte[] buffer, int offset, int count)
    {
        return writer.Write(new ReadOnlySpan<byte>(buffer, offset, count));
    }
    
    /// <summary>
    /// Writes a boolean to the underlying buffer and advances the position of write cursor by one byte.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteBool(this IBinaryWriter writer, bool value)
    {
        ReadOnlySpan<byte> buffer = stackalloc byte[] { (byte)(value ? 1 : 0) };
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a 8-bit integer to the underlying buffer and advances the position of write cursor by one byte.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteInt8(this IBinaryWriter writer, byte value)
    {
        ReadOnlySpan<byte> buffer = stackalloc byte[] {value};
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a 16-bit integer to the underlying buffer and advances the position of write cursor by two bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteInt16(this IBinaryWriter writer, short value)
    {
        Span<byte> buffer = stackalloc byte[2];
        if (writer.Endian == Endianness.Little)
            BinaryPrimitives.WriteInt16LittleEndian(buffer, value);
        else
            BinaryPrimitives.WriteInt16BigEndian(buffer, value);
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a 32-bit integer to the underlying buffer and advances the position of write cursor by four bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteInt32(this IBinaryWriter writer, int value)
    {
        Span<byte> buffer = stackalloc byte[4];
        if (writer.Endian == Endianness.Little)
            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);
        else
            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a 64-bit integer to the underlying buffer and advances the position of write cursor by eight bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteInt64(this IBinaryWriter writer, long value)
    {
        Span<byte> buffer = stackalloc byte[8];
        if (writer.Endian == Endianness.Little)
            BinaryPrimitives.WriteInt64LittleEndian(buffer, value);
        else
            BinaryPrimitives.WriteInt64BigEndian(buffer, value);
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a 32-bit integer to the underlying buffer 7-bits at a time and advances the position of write cursor by
    /// one to five bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteVarInt(this IBinaryWriter writer, int value)
    {
        Span<byte> buffer = stackalloc byte[5];
        var pos = 0;
        
        uint num;
        for (num = Unsafe.As<int, uint>(ref value); num > 0x7FU; num >>= 7)
            buffer[pos++] = (byte) (num | 0xFFFFFF80U);
        
        buffer[pos++] = (byte) num;
        return writer.Write(buffer[..pos]);
    }

    /// <summary>
    /// Writes a 64-bit integer to the underlying buffer 7-bits at a time and advances the position of write cursor by
    /// one to ten bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteVarLong(this IBinaryWriter writer, long value)
    {
        Span<byte> buffer = stackalloc byte[10];
        var pos = 0;
        
        ulong num;
        for (num = Unsafe.As<long, ulong>(ref value); num > 0x7FUL; num >>= 7)
            buffer[pos++] = (byte) ((uint) num | 0xFFFFFF80U);

        buffer[pos++] = (byte) num;
        return writer.Write(buffer[..pos]);
    }

    /// <summary>
    /// Writes a half-precision 16-bit floating point number to the underlying buffer and advances the position of
    /// write cursor by two bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteHalf(this IBinaryWriter writer, Half value)
    {
        Span<byte> buffer = stackalloc byte[2];
        if (writer.Endian == Endianness.Little)
            BinaryPrimitives.WriteHalfLittleEndian(buffer, value);
        else
            BinaryPrimitives.WriteHalfBigEndian(buffer, value);
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a single-precision 32-bit floating point number to the underlying buffer and advances the position of
    /// write cursor by four bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteSingle(this IBinaryWriter writer, float value)
    {
        Span<byte> buffer = stackalloc byte[4];
        if (writer.Endian == Endianness.Little)
            BinaryPrimitives.WriteSingleLittleEndian(buffer, value);
        else
            BinaryPrimitives.WriteSingleBigEndian(buffer, value);
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a double-precision 64-bit floating point number to the underlying buffer and advances the position of
    /// write cursor by eight bytes.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int WriteDouble(this IBinaryWriter writer, double value)
    {
        Span<byte> buffer = stackalloc byte[8];
        if (writer.Endian == Endianness.Little)
            BinaryPrimitives.WriteDoubleLittleEndian(buffer, value);
        else
            BinaryPrimitives.WriteDoubleBigEndian(buffer, value);
        return writer.Write(buffer);
    }

    /// <summary>
    /// Writes a string to the underlying buffer with the <see cref="IBinaryBuffer.Encoding"/>, and advances the write
    /// cursor by the number of bytes written. The string is prefixed with its size as integer encoded 7-bits at a time.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <returns>The number of bytes written to the underlying buffer, including the size prefix.</returns>
    /// <remarks>When the string is <c>null</c>, the size prefix of <c>0</c> is still written.</remarks>
    public static int WriteString(this IBinaryWriter writer, string? value)
    {
        if (string.IsNullOrEmpty(value))
            return WriteInt8(writer, 0);

        var length = writer.Encoding.GetByteCount(value);
        if (length == 0)
            return WriteInt8(writer, 0);
        
        if (length <= STACKALLOC_MAX)
        {
            Span<byte> span = stackalloc byte[length];
            writer.Encoding.GetBytes(value, span);
            return WriteVarInt(writer, length) + writer.Write(span);
        }

        var bytes = new byte[length];
        var buffer = new Span<byte>(bytes);
        writer.Encoding.GetBytes(value, buffer);
        return WriteVarInt(writer, length) + writer.Write(buffer);
    }

    /// <summary>
    /// Writes an arbitrary value-type to the underlying buffer, and advances the write cursor by the number of bytes
    /// written.
    /// </summary>
    /// <param name="writer">The writer object.</param>
    /// <param name="value">The value to write.</param>
    /// <typeparam name="T">A value type (struct) consisting of only primitive types.</typeparam>
    /// <returns>The number of bytes written to the underlying buffer.</returns>
    public static int Write<T>(this IBinaryWriter writer, T value) where T : unmanaged
    {
        var size = Unsafe.SizeOf<T>();
        if (size <= STACKALLOC_MAX)
        {
            Span<byte> buffer = stackalloc byte[size];
            MemoryMarshal.Write(buffer, ref value);
            return writer.Write(buffer);
        }

        var bytes = new byte[size];
        var span = new Span<byte>(bytes, 0, size);
        MemoryMarshal.Write(span, ref value);
        return writer.Write(span);
    }
    
    /// <inheritdoc cref="WriteInt8(IBinaryWriter,byte)"/>
    public static int WriteInt8<TEnum8>(this IBinaryWriter writer, TEnum8 value) where TEnum8 : unmanaged, Enum
    {
        return WriteInt8(writer, Unsafe.As<TEnum8, byte>(ref value));
    }
    
    /// <inheritdoc cref="WriteInt16(IBinaryWriter,short)"/>
    public static int WriteInt16<TEnum16>(this IBinaryWriter writer, TEnum16 value) where TEnum16 : unmanaged, Enum
    {
        return WriteInt16(writer, Unsafe.As<TEnum16, short>(ref value));
    }
    
    /// <inheritdoc cref="WriteInt32(IBinaryWriter,int)"/>
    public static int WriteInt32<TEnum32>(this IBinaryWriter writer, TEnum32 value) where TEnum32 : unmanaged, Enum
    {
        return WriteInt32(writer, Unsafe.As<TEnum32, int>(ref value));
    }
    
    /// <inheritdoc cref="WriteInt64(IBinaryWriter,long)"/>
    public static int WriteInt64<TEnum64>(this IBinaryWriter writer, TEnum64 value) where TEnum64 : unmanaged, Enum
    {
        return WriteInt64(writer, Unsafe.As<TEnum64, long>(ref value));
    }
    
    /// <inheritdoc cref="WriteVarInt(IBinaryWriter,int)"/>
    public static int WriteVarInt<TEnum32>(this IBinaryWriter writer, TEnum32 value) where TEnum32 : unmanaged, Enum
    {
        return WriteVarInt(writer, Unsafe.As<TEnum32, int>(ref value));
    }
    
    /// <inheritdoc cref="WriteVarLong(IBinaryWriter,long)"/>
    public static int WriteVarLong<TEnum64>(this IBinaryWriter writer, TEnum64 value) where TEnum64 : unmanaged, Enum
    {
        return WriteVarLong(writer, Unsafe.As<TEnum64, long>(ref value));
    }
}