using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// A callback that is used to get the length of a virtual IO-like object.
/// </summary>
/// <param name="userData">Not used.</param>
/// <seealso cref="VirtualIO"/>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
public delegate long LengthHandler(IntPtr userData);

/// <summary>
/// A callback that is used to seek a virtual IO-like object.
/// </summary>
/// <param name="offset">The byte offset to seek to.</param>
/// <param name="whence">The origin of the seek operation.</param>
/// <param name="userData">Not used.</param>
/// <returns>The position of the cursor within the virtual IO object.</returns>
/// <seealso cref="VirtualIO"/>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
public delegate long SeekHandler(long offset, SeekOrigin whence, IntPtr userData);

/// <summary>
/// A callback that is used to read from a virtual IO-like object.
/// </summary>
/// <param name="ptr">A buffer to receive the read bytes.</param>
/// <param name="count">The maximum number of bytes that can be read into the buffer.</param>
/// <param name="userData">Not used.</param>
/// <returns>The actual number of bytes copied to the buffer.</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
public delegate long ReadHandler(IntPtr ptr, long count, IntPtr userData);

/// <summary>
/// A callback that is used to write to a virtual IO-like object.
/// </summary>
/// <param name="ptr">A buffer to containing the byte to writes.</param>
/// <param name="count">The maximum number of bytes that can be read from the buffer.</param>
/// <param name="userData">Not used.</param>
/// <returns>The actual number of bytes consumed from the buffer.</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
public delegate long WriteHandler(IntPtr ptr, long count, IntPtr userData);

/// <summary>
/// A callback that is used to get the current cursor position within a virtual IO-like object.
/// </summary>
/// <param name="userData">Not used.</param>
/// <returns>The byte offset of the cursor within the virtual IO object.</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
public delegate long PositionHandler(IntPtr userData);

/// <summary>
/// Internal structure that gets passed to the native library with pointers to the callbacks.
/// </summary>
internal struct VirtualIOImpl
{
    // ReSharper disable NotAccessedField.Global
    public IntPtr Length;
    public IntPtr Seek;
    public IntPtr Read;
    public IntPtr Write;
    public IntPtr Position;
    // ReSharper restore NotAccessedField.Global
}

/// <summary>
/// Provides an interface to the native library for performing file operations on an object that is not a
/// <see cref="FileStream"/>.
/// <para/>
/// This can be used as a wrapper for other stream types or writing to arbitrary buffers.
/// </summary>
[PublicAPI]
public class VirtualIO
{
    private Stream? Stream { get; }
    internal readonly VirtualIOImpl IO;
    private readonly LengthHandler length;
    private readonly SeekHandler seek;
    private readonly ReadHandler read;
    private readonly WriteHandler write;
    private readonly PositionHandler position;

    /// <summary>
    /// Creates a new instance of the <see cref="VirtualIO"/> class by wrapping the the specified
    /// <paramref name="stream"/> object.
    /// </summary>
    /// <param name="stream">A stream instance to wrap.</param>
    /// <remarks>
    /// The underlying <paramref name="stream"/> must not be disposed while this object is in use.
    /// </remarks>
    private VirtualIO(Stream stream)
    {
        Stream = stream;
        length = LengthCallback;
        seek = SeekCallback;
        read = ReadCallback;
        write = WriteCallback;
        position = PositionCallback;
        IO = CreateImpl();
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="VirtualIO"/> class with the specified handlers.
    /// </summary>
    /// <param name="length">A callback that will be invoked when to get the length of the virtual stream in bytes.</param>
    /// <param name="seek">A callback that will be invoked when a seek operation needs performed.</param>
    /// <param name="read">A callback that will be invoked when a read operation needs performed.</param>
    /// <param name="write">A callback that will be invoked when a write operation needs performed.</param>
    /// <param name="position">A callback that will be invoked when to get the position  of the virtual stream in bytes.</param>
    /// <remarks>
    /// A reference to the delegates is held internally to ensure they are not garbage collected as long as this object
    /// is being used.
    /// </remarks>
    public VirtualIO(LengthHandler length, SeekHandler seek, ReadHandler read, WriteHandler write, PositionHandler position)
    {
        this.length = length;
        this.seek = seek;
        this.read = read;
        this.write = write;
        this.position = position;
        IO = CreateImpl();
    }

    private VirtualIOImpl CreateImpl()
    {
        return new VirtualIOImpl
        {
            Length = Marshal.GetFunctionPointerForDelegate(length),
            Seek = Marshal.GetFunctionPointerForDelegate(seek),
            Read = Marshal.GetFunctionPointerForDelegate(read),
            Write = Marshal.GetFunctionPointerForDelegate(write),
            Position = Marshal.GetFunctionPointerForDelegate(position),
        };
    }
    
    private long LengthCallback(IntPtr userdata) => Stream?.Length ?? 0;
    
    private long SeekCallback(long offset, SeekOrigin origin, IntPtr userdata) =>  Stream?.Seek(offset, origin) ?? 0;

    private unsafe long ReadCallback(IntPtr ptr, long count, IntPtr userdata)
    {
        if (Stream is {CanRead: false})
            return 0;
        
        var data = new Span<byte>(ptr.ToPointer(), (int) count);
        return Stream?.Read(data) ?? 0;
    }

    private unsafe long WriteCallback(IntPtr ptr, long count, IntPtr userdata)
    {
        if (Stream is {CanWrite: false})
            return 0;
        
        var data = new ReadOnlySpan<byte>(ptr.ToPointer(), (int) count);
        Stream?.Write(data);
        return count;
    }

    private long PositionCallback(IntPtr userdata) => Stream?.Position ?? 0;

}