using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

[assembly: CLSCompliant(true)]

namespace Anvil.SndFile;

/// <summary>
/// Strongly-typed constants for error codes.
/// <para/>
/// This enumeration is non-comprehensive, and does not include internal errors that may be emitted, in which case a
/// user can retrieve a description of the error with <see cref="SF.ErrorNumber"/>.
/// </summary>
public enum SoundError
{
	/// <summary>
	/// No error emitted.
	/// </summary>
	None = 0,
	
	/// <summary>
	/// Data is in unrecognized audio format.
	/// </summary>
	UnrecognisedFormat = 1,
	
	/// <summary>
	/// A system error occurred, typically an IO-operation failed.
	/// </summary>
	System = 2,
	
	/// <summary>
	/// The audio data is malformed or corrupted.
	/// </summary>
	MalformedFile = 3,
	
	/// <summary>
	/// The data uses unsupported encoding.
	/// </summary>
	UnsupportedEncoding = 4
}

/// <summary>
/// Provides a managed interface to the <c>libsndfile</c> library.
/// </summary>
[SuppressUnmanagedCodeSecurity, PublicAPI]
public static class SF
{
	internal const int FORMAT_SUBMASK  = 0x0000FFFF;
	internal const int FORMAT_TYPEMASK = 0x0FFF0000;
	internal const int FORMAT_ENDMASK  = 0x30000000;
	
	private const int MODE_SHIFT = 4;
	private const int TRUE = 1;
	private const int FALSE = 0;
	
	// ReSharper disable InconsistentNaming
	private static readonly unsafe delegate *unmanaged[Cdecl]<byte*,int,SoundInfo*,SoundFile> sf_open;
	private static readonly unsafe delegate *unmanaged[Cdecl]<int,int,SoundInfo*,int,SoundFile> sf_open_fd;
	private static readonly unsafe delegate *unmanaged[Cdecl]<VirtualIOImpl*,int,SoundInfo*,IntPtr,SoundFile> sf_open_virtual;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundInfo*,int> sf_format_check;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,long,SeekOrigin,long> sf_seek;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,CommandType,void*,int,int> sf_command;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,SoundError> sf_error;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,IntPtr> sf_strerror;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundError,IntPtr> sf_error_number;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,int> sf_close;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,void> sf_write_sync;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,short*,long,long> sf_read_short;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,int*,long,long> sf_read_int;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,float*,long,long> sf_read_float;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,double*,long,long> sf_read_double;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,short*,long,long> sf_readf_short;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,int*,long,long> sf_readf_int;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,float*,long,long> sf_readf_float;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,double*,long,long> sf_readf_double;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,short*,long,long> sf_write_short;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,int*,long,long> sf_write_int;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,float*,long,long> sf_write_float;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,double*,long,long> sf_write_double;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,short*,long,long> sf_writef_short;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,int*,long,long> sf_writef_int;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,float*,long,long> sf_writef_float;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,double*,long,long> sf_writef_double;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,void*,long,long> sf_read_raw;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,void*,long,long> sf_write_raw;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,StringType,IntPtr> sf_get_string;
	private static readonly unsafe delegate *unmanaged[Cdecl]<SoundFile,StringType,byte*,int> sf_set_string;
	// ReSharper restore InconsistentNaming
    
    private static UnmanagedLibrary Library { get; }
	
    static unsafe SF()
    {
	    Library = new UnmanagedLibrary("/usr/lib/libsndfile.so");
	    sf_open = (delegate *unmanaged[Cdecl]<byte*,int,SoundInfo*,SoundFile>) Library.Import("sf_open");
		sf_open_fd = (delegate *unmanaged[Cdecl]<int,int,SoundInfo*,int,SoundFile>) Library.Import("sf_open_fd");
		sf_open_virtual = (delegate *unmanaged[Cdecl]<VirtualIOImpl*,int,SoundInfo*,IntPtr,SoundFile>) Library.Import("sf_open_virtual");
		sf_format_check = (delegate *unmanaged[Cdecl]<SoundInfo*,int>) Library.Import("sf_format_check");
		sf_seek = (delegate *unmanaged[Cdecl]<SoundFile,long,SeekOrigin,long>) Library.Import("sf_seek");
		sf_command = (delegate *unmanaged[Cdecl]<SoundFile,CommandType,void*,int,int>) Library.Import("sf_command");
		sf_error = (delegate *unmanaged[Cdecl]<SoundFile,SoundError>) Library.Import("sf_error");
		sf_strerror = (delegate *unmanaged[Cdecl]<SoundFile,IntPtr>) Library.Import("sf_strerror");
		sf_error_number = (delegate *unmanaged[Cdecl]<SoundError,IntPtr>) Library.Import("sf_error_number");
		sf_close = (delegate *unmanaged[Cdecl]<SoundFile,int>) Library.Import("sf_close");
		sf_write_sync = (delegate *unmanaged[Cdecl]<SoundFile,void>) Library.Import("sf_write_sync");
		sf_read_short = (delegate *unmanaged[Cdecl]<SoundFile,short*,long,long>) Library.Import("sf_read_short");
		sf_read_int = (delegate *unmanaged[Cdecl]<SoundFile,int*,long,long>) Library.Import("sf_read_int");
		sf_read_float = (delegate *unmanaged[Cdecl]<SoundFile,float*,long,long>) Library.Import("sf_read_float");
		sf_read_double = (delegate *unmanaged[Cdecl]<SoundFile,double*,long,long>) Library.Import("sf_read_double");
		sf_readf_short = (delegate *unmanaged[Cdecl]<SoundFile,short*,long,long>) Library.Import("sf_readf_short");
		sf_readf_int = (delegate *unmanaged[Cdecl]<SoundFile,int*,long,long>) Library.Import("sf_readf_int");
		sf_readf_float = (delegate *unmanaged[Cdecl]<SoundFile,float*,long,long>) Library.Import("sf_readf_float");
		sf_readf_double = (delegate *unmanaged[Cdecl]<SoundFile,double*,long,long>) Library.Import("sf_readf_double");
		sf_write_short = (delegate *unmanaged[Cdecl]<SoundFile,short*,long,long>) Library.Import("sf_write_short");
		sf_write_int = (delegate *unmanaged[Cdecl]<SoundFile,int*,long,long>) Library.Import("sf_write_int");
		sf_write_float = (delegate *unmanaged[Cdecl]<SoundFile,float*,long,long>) Library.Import("sf_write_float");
		sf_write_double = (delegate *unmanaged[Cdecl]<SoundFile,double*,long,long>) Library.Import("sf_write_double");
		sf_writef_short = (delegate *unmanaged[Cdecl]<SoundFile,short*,long,long>) Library.Import("sf_writef_short");
		sf_writef_int = (delegate *unmanaged[Cdecl]<SoundFile,int*,long,long>) Library.Import("sf_writef_int");
		sf_writef_float = (delegate *unmanaged[Cdecl]<SoundFile,float*,long,long>) Library.Import("sf_writef_float");
		sf_writef_double = (delegate *unmanaged[Cdecl]<SoundFile,double*,long,long>) Library.Import("sf_writef_double");
		sf_read_raw = (delegate *unmanaged[Cdecl]<SoundFile,void*,long,long>) Library.Import("sf_read_raw");
		sf_write_raw = (delegate *unmanaged[Cdecl]<SoundFile,void*,long,long>) Library.Import("sf_write_raw");
		sf_get_string = (delegate *unmanaged[Cdecl]<SoundFile,StringType,IntPtr>) Library.Import("sf_get_string");
		sf_set_string = (delegate *unmanaged[Cdecl]<SoundFile,StringType,byte*,int>) Library.Import("sf_set_string");
    }

    /// <summary>
    /// Opens an audio file at the specified <paramref name="path"/>.
    /// </summary>
    /// <param name="path">The path to the audio file to open.</param>
    /// <param name="mode">The desired access mode of the file operation.</param>
    /// <param name="info">
    /// A zeroed structure instance to receive the sound information when opening in read mode, or a structure containing
    /// the format, type, channel, and samplerate information when opening in write mode. 
    /// </param>
    /// <returns>A handle to the sound, or <c>null</c> if an error occured.</returns>
    [NativeMethod("sf_open")]
    public static unsafe SoundFile? Open(string path, FileAccess mode, ref SoundInfo info)
    {
	    fixed (SoundInfo* ptr = &info)
	    {
		    fixed (byte* b = &UTF8String.Pin(path))
		    {
			    var sf = sf_open(b, (int) mode << MODE_SHIFT, ptr);
			    return sf == default ? null : sf;
		    }
	    }
    }
    
    /// <inheritdoc cref="Open(string,System.IO.FileAccess,ref Anvil.SndFile.SoundInfo)"/>
    [NativeMethod("sf_open"), CLSCompliant(false)]
    public static unsafe SoundFile? Open(string path, FileAccess mode, SoundInfo *info)
    {
	    fixed (byte* b = &UTF8String.Pin(path))
	    {
		    var sf = sf_open(b, (int) mode << MODE_SHIFT, info);
		    return sf == default ? null : sf;
	    }
    }

    /// <summary>
    /// Creates a <see cref="SoundFile"/> from an existing file stream.
    /// </summary>
    /// <param name="fileStream">A file stream to read to and/or write from.</param>
    /// <param name="mode">The desired access mode of the file operation.</param>
    /// <param name="info">
    /// A zeroed structure instance to receive the sound information when opening in read mode, or a structure containing
    /// the format, type, channel, and samplerate information when opening in write mode. 
    /// </param>
    /// <returns>A handle to the sound, or <c>null</c> if an error occured.</returns>
	[NativeMethod("sf_open_fd")]
	public static SoundFile? Open(FileStream fileStream, FileAccess mode, ref SoundInfo info)
	{
		var fd = fileStream.SafeFileHandle.DangerousGetHandle().ToInt32();
		unsafe
		{
			fixed (SoundInfo* ptr = &info)
			{
				var sf = sf_open_fd(fd, (int) mode << 4, ptr, FALSE);
				return sf == default ? null : sf;
			}
		}
	}
    
    /// <inheritdoc cref="Open(FileStream,System.IO.FileAccess,ref Anvil.SndFile.SoundInfo)"/>
	[NativeMethod("sf_open_fd"), CLSCompliant(false)]
	public static unsafe SoundFile? Open(FileStream fileStream, FileAccess mode, SoundInfo *info)
	{
		var fd = fileStream.SafeFileHandle.DangerousGetHandle().ToInt32();
		var sf = sf_open_fd(fd, (int) mode << 4, info, FALSE);
		return sf == default ? null : sf;
	}
    
	/// <summary>
	/// Create a <see cref="SoundFile"/> using a virtual IO-like object ass the backing data.
	/// </summary>
	/// <param name="io">An object that represents the interface to interact with the data.</param>
	/// <param name="mode">The desired access mode of the file operation.</param>
	/// <param name="info">
	/// A zeroed structure instance to receive the sound information when opening in read mode, or a structure containing
	/// the format, type, channel, and samplerate information when opening in write mode. 
	/// </param>
	/// <returns>A handle to the sound, or <c>null</c> if an error occured.</returns>
	[NativeMethod("sf_open_virtual")]
	public static unsafe SoundFile? Open(VirtualIO io, FileAccess mode, ref SoundInfo info)
	{
		var ioImpl = io.IO;
		fixed (SoundInfo* ptr = &info)
		{
			var sf = sf_open_virtual(&ioImpl, (int) mode << 4, ptr, IntPtr.Zero);
			return sf == default ? null : sf;
		}
	}

	/// <inheritdoc cref="Open(VirtualIO,System.IO.FileAccess,ref Anvil.SndFile.SoundInfo)"/>
	[NativeMethod("sf_open_virtual"), CLSCompliant(false)]
	public static unsafe SoundFile? Open(VirtualIO io, FileAccess mode, SoundInfo *info)
	{
		var ioImpl = io.IO;
		var sf = sf_open_virtual(&ioImpl, (int) mode << 4, info, IntPtr.Zero);
		return sf == default ? null : sf;
	}
	
	/// <summary>
	/// Retrieves a human-friendly message for the specified error.
	/// </summary>
	/// <param name="errorNumber">An error code.</param>
	/// <returns>The message for the specified error, or <c>null</c> if no error or invalid code was given.</returns>
	[NativeMethod("sf_error_number")]
	public static unsafe string? ErrorNumber(SoundError errorNumber)
	{
		var ptr = sf_error_number(errorNumber);
		return ptr == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(ptr);
	}

	/// <summary>
	/// Checks the parameters defined in the given <paramref name="info"/> and indicates if they are valid to be used
	/// when opening a file in write mode.
	/// </summary>
	/// <param name="info">A <see cref="SoundInfo"/> struct filled out with the desired format for writing.</param>
	/// <returns><c>true</c> if format is valid, otherwise <c>false</c>.</returns>
	[NativeMethod("sf_format_check"), CLSCompliant(false)]
	public static unsafe bool FormatCheck(SoundInfo *info) => sf_format_check(info) == TRUE;
	
	/// <inheritdoc cref="FormatCheck(Anvil.SndFile.SoundInfo*)"/>
	[NativeMethod("sf_format_check")]
	public static unsafe bool FormatCheck(SoundInfo info) => sf_format_check(&info) == TRUE;
	
	/// <summary>
	/// Seeks the audio file in multi-channel frame units. The cursor only moves within the audio data portion of the
	/// stream.
	/// </summary>
	/// <param name="file">The file to seek.</param>
	/// <param name="frames">The offset to seek to, in units of multi-channel samples.</param>
	/// <param name="whence">The relative origin to seek from.</param>
	/// <returns>The position of the cursor after the seek operation is performed.</returns>
	[NativeMethod("sf_seek")]
	public static unsafe long Seek(SoundFile file, long frames, SeekOrigin whence) => sf_seek(file, frames, whence);
	
	/// <summary>
	/// Reports the error state of the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file to query for errors.</param>
	/// <returns>An enumeration describing any error that may have occurred.</returns>
	[NativeMethod("sf_error")]
	public static unsafe SoundError Error(SoundFile file) => sf_error(file);

	/// <summary>
	/// Reports the error state of the specified <paramref name="file"/> as a string.
	/// </summary>
	/// <param name="file">The file to query for errors.</param>
	/// <returns>A string describing any error that may have occurred.</returns>
	[NativeMethod("sf_strerror")]
	public static unsafe string? ErrorString(SoundFile file)
	{
		var ptr = sf_strerror(file);
		return ptr == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(ptr);
	}
	
	/// <summary>
	/// Allows the caller to retrieve information from or change aspects of the library behavior. 
	/// </summary>
	/// <param name="file">Specifies the file to perform the command on, or <c>null</c> when not applicable.</param>
	/// <param name="command">A constant describing the command type.</param>
	/// <param name="data">Any data that needs passed with the command.</param>
	/// <param name="dataSize">The size of the data, or optionally used as data argument for some commands.</param>
	/// <returns>A return value from the command, typically indicates success/failure.</returns>
	/// <remarks>
	/// For more information on the command interface and what parameters are expected, see official <c>libsndfile</c>
	/// documentation.
	/// </remarks>
	/// <seealso href="http://www.mega-nerd.com/libsndfile/command.html"/>
	[NativeMethod("sf_command")]
	public static unsafe int Command(SoundFile? file, CommandType command, IntPtr data, int dataSize)
	{
		return sf_command(file ?? default, command, data.ToPointer(), dataSize);
	}
	
	/// <inheritdoc cref="Command(System.Nullable{Anvil.SndFile.SoundFile},Anvil.SndFile.CommandType,System.IntPtr,int)"/>
	[NativeMethod("sf_command"), CLSCompliant(false)]
	public static unsafe int Command(SoundFile? file, CommandType command, void* data, int dataSize)
	{
		return sf_command(file ?? default, command, data, dataSize);
	}
	
	/// <inheritdoc cref="Command(System.Nullable{Anvil.SndFile.SoundFile},Anvil.SndFile.CommandType,System.IntPtr,int)"/>
	[NativeMethod("sf_command")]
	public static unsafe int Command<T>(SoundFile? file, CommandType command, ref T data, int dataSize) where T : unmanaged
	{
		fixed (T* ptr = &data)
		{
			return sf_command(file ?? default, command, ptr, dataSize);
		}
	}
	
	/// <inheritdoc cref="Command(System.Nullable{Anvil.SndFile.SoundFile},Anvil.SndFile.CommandType,System.IntPtr,int)"/>
	[NativeMethod("sf_command"), CLSCompliant(false)]
	public static unsafe int Command<T>(SoundFile? file, CommandType command, T *data, int dataSize) where T : unmanaged
	{
		return sf_command(file ?? default, command, data, dataSize);
	}

	/// <summary>
	/// Reads raw encoded data from the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file to read from.</param>
	/// <param name="data">A buffer to receive the data.</param>
	/// <param name="bytes">The size of the buffer in bytes.</param>
	/// <returns>The actual number of bytes read from the file.</returns>
	[NativeMethod("sf_read_raw")]
	public static unsafe long ReadRaw(SoundFile file, IntPtr data, long bytes) => sf_read_raw(file, data.ToPointer(), bytes);

	/// <inheritdoc cref="ReadRaw(Anvil.SndFile.SoundFile,System.IntPtr,long)"/>
	[NativeMethod("sf_read_raw"), CLSCompliant(false)]
	public static unsafe long ReadRaw(SoundFile file, void *ptr, long bytes) => sf_read_raw(file, ptr, bytes);
	
	/// <inheritdoc cref="ReadRaw(Anvil.SndFile.SoundFile,System.IntPtr,long)"/>
	[NativeMethod("sf_read_raw")]
	public static unsafe long ReadRaw(SoundFile file, Span<byte> data)
	{
		fixed (byte* ptr = &data[0])
		{
			return sf_read_raw(file, ptr, data.Length);
		}
	}
	
	/// <summary>
	/// Reads raw encoded data from the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file to read from.</param>
	/// <param name="data">A buffer to receive the data.</param>
	/// <param name="offset">The offset into the data array to begin writing to.</param>
	/// <param name="size">The number of bytes to write into the buffer.</param>
	/// <returns>The actual number of bytes read from the file.</returns>
	[NativeMethod("sf_read_raw")]
	public static unsafe long ReadRaw(SoundFile file, byte[] data, int offset, int size)
	{
		fixed (byte* ptr = &data[offset])
		{
			return sf_read_raw(file, ptr, size);
		}
	}
	
	/// <summary>
	/// Writes raw unencoded data to the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file to write to.</param>
	/// <param name="data">A buffer to receive the data.</param>
	/// <param name="bytes">The size of the buffer in bytes.</param>
	/// <returns>The actual number of bytes written to the file.</returns>
	[NativeMethod("sf_write_raw")]
	public static unsafe long WriteRaw(SoundFile file, IntPtr data, long bytes) => sf_write_raw(file, data.ToPointer(), bytes);
	
	/// <inheritdoc cref="WriteRaw(Anvil.SndFile.SoundFile,System.IntPtr,long)"/>
	[NativeMethod("sf_write_raw"), CLSCompliant(false)]
	public static unsafe long WriteRaw(SoundFile file, void *ptr, long bytes) => sf_write_raw(file, ptr, bytes);

	/// <inheritdoc cref="WriteRaw(Anvil.SndFile.SoundFile,System.IntPtr,long)"/>
	[NativeMethod("sf_write_raw")]
	public static unsafe long WriteRaw(SoundFile file, ReadOnlySpan<byte> data)
	{
		fixed (byte* ptr = &data[0])
		{
			return sf_write_raw(file, ptr, data.Length);
		}
	}
	
	/// <summary>
	/// Writes raw unencoded data to the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file to write to.</param>
	/// <param name="data">A buffer to receive the data.</param>
	/// <param name="offset">The offset into the data array to begin reading from.</param>
	/// <param name="size">The number of bytes to read from the buffer.</param>
	/// <returns>The actual number of bytes written to the file.</returns>
	[NativeMethod("sf_write_raw")]
	public static unsafe long WriteRaw(SoundFile file, byte[] data, int offset, int size)
	{
		fixed (byte* ptr = &data[offset])
		{
			return sf_write_raw(file, ptr, size);
		}
	}
	
	/// <summary>
	/// Retrieves a metadata string from the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to query.</param>
	/// <param name="type">The type of string to retrieve.</param>
	/// <returns>The value of the string, or <c>null</c> if none is present.</returns>
	[NativeMethod("sf_get_string")]
	public static unsafe string? GetString(SoundFile file, StringType type)
	{
		var ptr = sf_get_string(file, type);
		return ptr == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(ptr);
	}
	
	/// <summary>
	/// Sets a metadata string within the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to change.</param>
	/// <param name="type">The type of string to set.</param>
	/// <param name="str">The value to apply.</param>
	/// <returns><c>true</c> on success, otherwise <c>false</c> if an error occurred.</returns>
	[NativeMethod("sf_set_string")]
	public static unsafe bool SetString(SoundFile file, StringType type, string str)
	{
		fixed (byte* b = &UTF8String.Pin(str))
		{
			return sf_set_string(file, type, b) == 0;
		}
	}
	
	/// <summary>
	/// Closes the native sound <paramref name="file"/>, flushing any unwritten data and freeing unmanaged memory.
	/// </summary>
	/// <param name="file">The file to close.</param>
	/// <returns><c>true</c> on success, otherwise <c>false</c> if an error occurred.</returns>
	[NativeMethod("sf_close")]
	public static unsafe bool Close(SoundFile file) => sf_close(file) == 0;

	/// <summary>
	/// Asynchronously closes the native sound <paramref name="file"/>, flushing any unwritten data and freeing unmanaged memory.
	/// </summary>
	/// <param name="file">The file to close.</param>
	/// <returns><c>true</c> on success, otherwise <c>false</c> if an error occurred.</returns>
	[NativeMethod("sf_close")]
	public static async Task<bool> CloseAsync(SoundFile file)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				return sf_close(file) == 0;
			}
		});
	}
	
	/// <summary>
	/// Call the operating system's function to force the writing of all <paramref name="file"/> cache buffers to disk.
	/// <para/>
	/// Does nothing if file is not opened for writing.
	/// </summary>
	/// <param name="file">The file whose buffers should be flushed.</param>
	[NativeMethod("sf_write_sync")]
	public static unsafe void Flush(SoundFile file) => sf_write_sync(file);

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> in the data type.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="size">The number of sample to be read. Buffer must be large enough to contain them.</param>
	/// <param name="type">The desired type of the data to retrieve, which does not need to match the source type.</param>
	/// <returns>The actual number of samples written to the buffer.</returns>
	/// <exception cref="ArgumentOutOfRangeException">When an invalid type is specified.</exception>
	[NativeMethod("sf_read_short"), NativeMethod("sf_read_int"), NativeMethod("sf_read_float"), NativeMethod("sf_read_double"), CLSCompliant(false)]
	public static unsafe long Read(SoundFile file, void* ptr, long size, SampleType type)
	{
		return type switch
		{
			SampleType.Short => sf_read_short(file, (short*) ptr, size),
			SampleType.Integer => sf_read_int(file, (int*) ptr, size),
			SampleType.Float => sf_read_float(file, (float*) ptr, size),
			SampleType.Double => sf_read_double(file, (double*) ptr, size),
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> in the data type.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="size">The number of sample to be read. Buffer must be large enough to contain them.</param>
	/// <param name="type">The desired type of the data to retrieve, which does not need to match the source type.</param>
	/// <returns>The actual number of samples written to the buffer.</returns>
	/// <exception cref="ArgumentOutOfRangeException">When an invalid type is specified.</exception>
	[NativeMethod("sf_read_short"), NativeMethod("sf_read_int"), NativeMethod("sf_read_float"), NativeMethod("sf_read_double")]
	public static unsafe long Read(SoundFile file, IntPtr ptr, long size, SampleType type)
	{
		return type switch
		{
			SampleType.Short => sf_read_short(file, (short*) ptr, size),
			SampleType.Integer => sf_read_int(file, (int*) ptr, size),
			SampleType.Float => sf_read_float(file, (float*) ptr, size),
			SampleType.Double => sf_read_double(file, (double*) ptr, size),
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> in the data type.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="type">The desired type of the data to retrieve, which does not need to match the source type.</param>
	/// <typeparam name="T">An unmanaged value type.</typeparam>
	/// <returns>The actual number of samples written to the buffer.</returns>
	/// <exception cref="ArgumentOutOfRangeException">When an invalid type is specified.</exception>n>
	[NativeMethod("sf_read_short"), NativeMethod("sf_read_int"), NativeMethod("sf_read_float"), NativeMethod("sf_read_double")]
	public static unsafe long Read<T>(SoundFile file, Span<T> data, SampleType type) where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data[0])
		{
			return type switch
			{
				SampleType.Short => sf_read_short(file, (short*) ptr, size),
				SampleType.Integer => sf_read_int(file, (int*) ptr, size),
				SampleType.Float => sf_read_float(file, (float*) ptr, size),
				SampleType.Double => sf_read_double(file, (double*) ptr, size),
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}
	}
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 16-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_short")]
	public static unsafe long Read(SoundFile file, Span<short> data)
	{
		fixed (short* ptr = &data[0])
		{
			return sf_read_short(file, ptr, data.Length);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_int")]
	public static unsafe long Read(SoundFile file, Span<int> data)
	{
		fixed (int* ptr = &data[0])
		{
			return sf_read_int(file, ptr, data.Length);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit floating point numbers..
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_float")]
	public static unsafe long Read(SoundFile file, Span<float> data)
	{
		fixed (float* ptr = &data[0])
		{
			return sf_read_float(file, ptr, data.Length);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 64-bit floating point numbers..
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_double")]
	public static unsafe long Read(SoundFile file, Span<double> data)
	{
		fixed (double* ptr = &data[0])
		{
			return sf_read_double(file, ptr, data.Length);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 16-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_short")]
	public static unsafe long Read(SoundFile file, short[] data, int offset, int size)
	{
		fixed (short* ptr = &data[offset])
		{
			return sf_read_short(file, ptr, size);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_int")]
	public static unsafe long Read(SoundFile file, int[] data, int offset, int size)
	{
		fixed (int* ptr = &data[offset])
		{
			return sf_read_int(file, ptr, size);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_float")]
	public static unsafe long Read(SoundFile file, float[] data, int offset, int size)
	{
		fixed (float* ptr = &data[offset])
		{
			return sf_read_float(file, ptr, size);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 64-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_double")]
	public static unsafe long Read(SoundFile file, double[] data, int offset, int size)
	{
		fixed (double* ptr = &data[offset])
		{
			return sf_read_double(file, ptr, size);
		}
	}
	
	/// <summary>
	/// Asynchronously reads decoded samples from the <paramref name="file"/> as 16-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_short")]
	public static async Task<long> ReadAsync(SoundFile file, short[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (short* ptr = &data[offset])
				{
					return sf_read_short(file, ptr, size);
				}
			}
		});
	}
	
	/// <summary>
	/// Asynchronously reads decoded samples from the <paramref name="file"/> as 32-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_int")]
	public static async Task<long> ReadAsync(SoundFile file, int[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (int* ptr = &data[offset])
				{
					return sf_read_int(file, ptr, size);
				}
			}
		});
	}
	
	/// <summary>
	/// Asynchronously reads decoded samples from the <paramref name="file"/> as 32-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_float")]
	public static async Task<long> ReadAsync(SoundFile file, float[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (float* ptr = &data[offset])
				{
					return sf_read_float(file, ptr, size);
				}
			}
		});
	}

	/// <summary>
	/// Asynchronously reads decoded samples from the <paramref name="file"/> as 64-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin writing into.</param>
	/// <param name="size">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_double")]
	public static async Task<long> ReadAsync(SoundFile file, double[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (double* ptr = &data[offset])
				{
					return sf_read_double(file, ptr, size);
				}
			}
		});
	}

	/// <summary>
	/// Encodes and writes raw 16-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_short")]
	public static unsafe long Write(SoundFile file, ReadOnlySpan<short> data)
	{
		fixed (short* ptr = &data[0])
		{
			return sf_write_short(file, ptr, data.Length);	
		}
	}

	/// <summary>
	/// Encodes and writes raw 32-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_int")]
	public static unsafe long Write(SoundFile file, ReadOnlySpan<int> data)
	{
		fixed (int* ptr = &data[0])
		{
			return sf_write_int(file, ptr, data.Length);
		}
	}

	/// <summary>
	/// Encodes and writes raw 32-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_float")]
	public static unsafe long Write(SoundFile file, ReadOnlySpan<float> data)
	{
		fixed (float* ptr = &data[0])
		{
			return sf_write_float(file, ptr, data.Length);
		}
	}

	/// <summary>
	/// Encodes and writes raw 64-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_double")]
	public static unsafe long Write(SoundFile file, ReadOnlySpan<double> data)
	{
		fixed (double* ptr = &data[0])
		{
			return sf_write_double(file, ptr, data.Length);	
		}
	}
	
	/// <summary>
	/// Encodes and writes raw 16-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_short")]
	public static unsafe long Write(SoundFile file, short[] data, int offset, int size)
	{
		fixed (short* ptr = &data[offset])
		{
			return sf_write_short(file, ptr, size);
		}
	}

	/// <summary>
	/// Encodes and writes raw 32-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_int")]
	public static unsafe long Write(SoundFile file, int[] data, int offset, int size)
	{
		fixed (int* ptr = &data[offset])
		{
			return sf_write_int(file, ptr, size);
		}
	}

	/// <summary>
	/// Encodes and writes raw 32-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_float")]
	public static unsafe long Write(SoundFile file, float[] data, int offset, int size)
	{
		fixed (float* ptr = &data[offset])
		{
			return sf_write_float(file, ptr, size);
		}
	}

	/// <summary>
	/// Encodes and writes raw 64-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_double")]
	public static unsafe long Write(SoundFile file, double[] data, int offset, int size)
	{
		fixed (double* ptr = &data[offset])
		{
			return sf_write_double(file, ptr, size);
		}
	}
	
	/// <summary>
	/// Asynchronously encodes and writes raw 16-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_short")]
	public static async Task<long> WriteAsync(SoundFile file, short[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (short* ptr = &data[offset])
				{
					return sf_write_short(file, ptr, size);
				}
			}
		});
	}
	
	/// <summary>
	/// Asynchronously encodes and writes raw 32-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_int")]
	public static async Task<long> WriteAsync(SoundFile file, int[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (int* ptr = &data[offset])
				{
					return sf_write_int(file, ptr, size);
				}
			}
		});
	}
	
	/// <summary>
	/// Asynchronously encodes and writes raw 32-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_float")]
	public static async Task<long> WriteAsync(SoundFile file, float[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (float* ptr = &data[offset])
				{
					return sf_write_float(file, ptr, size);
				}
			}
		});
	}

	/// <summary>
	/// Asynchronously encodes and writes raw 64-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="offset">The offset into the <paramref name="data"/> to begin reading from.</param>
	/// <param name="size">The number of elements in the buffer to write.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_double")]
	public static async Task<long> WriteAsync(SoundFile file, double[] data, int offset, int size)
	{
		return await Task.Run(() =>
		{
			unsafe
			{
				fixed (double* ptr = &data[offset])
				{
					return sf_write_double(file, ptr, size);
				}
			}
		});
	}

	/// <summary>
	/// Encodes and writes raw samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="items">The number of elements in the <paramref name="ptr"/> array.</param>
	/// <param name="type">The type of the sample data.</param>
	/// <returns>The actual number of items written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_short"), NativeMethod("sf_write_int"), NativeMethod("sf_write_float"), NativeMethod("sf_write_double"), CLSCompliant(false)]
	public static unsafe long Write(SoundFile file, void* ptr, long items, SampleType type)
	{
		return type switch
		{
			SampleType.Short => sf_write_short(file, (short*) ptr, items),
			SampleType.Integer => sf_write_int(file, (int*) ptr, items),
			SampleType.Float => sf_write_float(file, (float*) ptr, items),
			SampleType.Double => sf_write_double(file, (double*) ptr, items),
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
	
	/// <summary>
	/// Encodes and writes raw samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="items">The number of elements in the <paramref name="ptr"/> array.</param>
	/// <param name="type">The type of the sample data.</param>
	/// <returns>The actual number of items written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_short"), NativeMethod("sf_write_int"), NativeMethod("sf_write_float"), NativeMethod("sf_write_double")]
	public static unsafe long Write(SoundFile file, IntPtr ptr, long items, SampleType type)
	{
		return type switch
		{
			SampleType.Short => sf_write_short(file, (short*) ptr, items),
			SampleType.Integer => sf_write_int(file, (int*) ptr, items),
			SampleType.Float => sf_write_float(file, (float*) ptr, items),
			SampleType.Double => sf_write_double(file, (double*) ptr, items),
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
	
	/// <summary>
	/// Encodes and writes raw samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="type">The type of the sample data.</param>
	/// <typeparam name="T">A primitive value type.</typeparam>
	/// <returns>The actual number of items written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_short"), NativeMethod("sf_write_int"), NativeMethod("sf_write_float"), NativeMethod("sf_write_double")]
	public static unsafe long Write<T>(SoundFile file, ReadOnlySpan<T> data, SampleType type) where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data[0])
		{
			return type switch
			{
				SampleType.Short => sf_write_short(file, (short*) ptr, size),
				SampleType.Integer => sf_write_int(file, (int*) ptr, size),
				SampleType.Float => sf_write_float(file, (float*) ptr, size),
				SampleType.Double => sf_write_double(file, (double*) ptr, size),
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}
	}
	
	/// <summary>
	/// Reads decoded frames from the <paramref name="file"/> in the data type.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of frames to be read. Buffer must be large enough to contain them.</param>
	/// <param name="type">The desired type of the data to retrieve, which does not need to match the source type.</param>
	/// <returns>The actual number of samples written to the buffer.</returns>
	/// <exception cref="ArgumentOutOfRangeException">When an invalid type is specified.</exception>
	[NativeMethod("sf_readf_short"), NativeMethod("sf_readf_int"), NativeMethod("sf_readf_float"), NativeMethod("sf_readf_double"), CLSCompliant(false)]
	public static unsafe long ReadFrames(SoundFile file, void* ptr, long frames, SampleType type)
	{
		return type switch
		{
			SampleType.Short => sf_readf_short(file, (short*) ptr, frames),
			SampleType.Integer => sf_readf_int(file, (int*) ptr, frames),
			SampleType.Float => sf_readf_float(file, (float*) ptr, frames),
			SampleType.Double => sf_readf_double(file, (double*) ptr, frames),
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
	
	/// <summary>
	/// Asynchronously encodes and writes raw 64-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of frames in the buffer to write.</param>
	/// <param name="type">The desired type of the data to retrieve, which does not need to match the source type.</param>
	/// <returns>The actual number of samples written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_short"), NativeMethod("sf_writef_int"), NativeMethod("sf_writef_float"), NativeMethod("sf_writef_double"), CLSCompliant(false)]
	public static unsafe long WriteFrames(SoundFile file, void* ptr, long frames, SampleType type)
	{
		return type switch
		{
			SampleType.Short => sf_write_short(file, (short*) ptr, frames),
			SampleType.Integer => sf_write_int(file, (int*) ptr, frames),
			SampleType.Float => sf_write_float(file, (float*) ptr, frames),
			SampleType.Double => sf_write_double(file, (double*) ptr, frames),
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 16-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_short")]
	public static unsafe long ReadFrames(SoundFile file, Span<short> data, long frames)
	{
		fixed (short* ptr = &data[0])
		{
			return sf_readf_short(file, ptr, frames);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_int")]
	public static unsafe long ReadFrames(SoundFile file, Span<int> data, long frames)
	{
		fixed (int* ptr = &data[0])
		{
			return sf_readf_int(file, ptr, frames);
		}
	}
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_float")]
	public static unsafe long ReadFrames(SoundFile file, Span<float> data, long frames)
	{
		fixed (float* ptr = &data[0])
		{
			return sf_readf_float(file, ptr, frames);
		}
	}

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 64-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="data">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_double")]
	public static unsafe long ReadFrames(SoundFile file, Span<double> data, long frames)
	{
		fixed (double* ptr = &data[0])
		{
			return sf_readf_double(file, ptr, frames);	
		}
	}

	/// <summary>
	/// Encodes and writes raw 16-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="data"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_short")]
	public static unsafe long WriteFrames(SoundFile file, ReadOnlySpan<short> data, long frames)
	{
		fixed (short* ptr = &data[0])
		{
			return sf_writef_short(file, ptr, frames);
		}
	}

	/// <summary>
	/// Encodes and writes raw 32-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="data"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_int")]
	public static unsafe long WriteFrames(SoundFile file, ReadOnlySpan<int> data, long frames)
	{
		fixed (int* ptr = &data[0])
		{
			return sf_writef_int(file, ptr, frames);
		}
	}

	/// <summary>
	/// Encodes and writes raw 32-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="data"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_float")]
	public static unsafe long WriteFrames(SoundFile file, ReadOnlySpan<float> data, long frames)
	{
		fixed (float* ptr = &data[0])
		{
			return sf_writef_float(file, ptr, frames);
		}
	}

	/// <summary>
	/// Encodes and writes raw 64-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="data">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="data"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_double")]
	public static unsafe long WriteFrames(SoundFile file, ReadOnlySpan<double> data, long frames)
	{
		fixed (double* ptr = &data[0])
		{
			return sf_writef_double(file, ptr, frames);
		}
	}
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 16-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="items">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_short"), CLSCompliant(false)]
	public static unsafe long Read(SoundFile file, short *ptr, long items) => sf_read_short(file, ptr, items);
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="items">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_int"), CLSCompliant(false)]
	public static unsafe long Read(SoundFile file, int *ptr, long items) => sf_read_int(file, ptr, items);
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="items">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_float"), CLSCompliant(false)]
	public static unsafe long Read(SoundFile file, float *ptr, long items) => sf_read_float(file, ptr, items);
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 64-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="items">The number of elements to read into the buffer.</param>
	/// <returns>The actual number of samples copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_read_double"), CLSCompliant(false)]
	public static unsafe long Read(SoundFile file, double *ptr, long items) => sf_read_double(file, ptr, items);

	/// <summary>
	/// Encodes and writes raw 16-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="items">The number of elements in the <paramref name="ptr"/> array.</param>
	/// <returns>The actual number of items written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_short"), CLSCompliant(false)]
	public static unsafe long Write(SoundFile file, short *ptr, long items) => sf_write_short(file, ptr, items);
	
	/// <summary>
	/// Encodes and writes raw 32-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="items">The number of elements in the <paramref name="ptr"/> array.</param>
	/// <returns>The actual number of items written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_int"), CLSCompliant(false)]
	public static unsafe long Write(SoundFile file, int *ptr, long items) => sf_write_int(file, ptr, items);
	
	/// <summary>
	/// Encodes and writes raw 32-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="items">The number of elements in the <paramref name="ptr"/> array.</param>
	/// <returns>The actual number of items written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_float"), CLSCompliant(false)]
	public static unsafe long Write(SoundFile file, float *ptr, long items) => sf_write_float(file, ptr, items);
	
	/// <summary>
	/// Encodes and writes raw 64-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="items">The number of elements in the <paramref name="ptr"/> array.</param>
	/// <returns>The actual number of items written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_write_double"), CLSCompliant(false)]
	public static unsafe long Write(SoundFile file, double *ptr, long items) => sf_write_double(file, ptr, items);

	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 16-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_short"), CLSCompliant(false)]
	public static unsafe long ReadFrames(SoundFile file, short *ptr, long frames) => sf_readf_short(file, ptr, frames);
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit signed integers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_int"), CLSCompliant(false)]
	public static unsafe long ReadFrames(SoundFile file, int *ptr, long frames) => sf_readf_int(file, ptr, frames);
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 32-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_float"), CLSCompliant(false)]
	public static unsafe long ReadFrames(SoundFile file, float *ptr, long frames) => sf_readf_float(file, ptr, frames);
	
	/// <summary>
	/// Reads decoded samples from the <paramref name="file"/> as 64-bit floating point numbers.
	/// </summary>
	/// <param name="file">The file instance to read from.</param>
	/// <param name="ptr">A buffer to receive the data being read.</param>
	/// <param name="frames">The number of multi-channel frames to read into the buffer.</param>
	/// <returns>The actual number of frames copied into the buffer.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_readf_double"), CLSCompliant(false)]
	public static unsafe long ReadFrames(SoundFile file, double *ptr, long frames) => sf_readf_double(file, ptr, frames);
	
	/// <summary>
	/// Encodes and writes raw 16-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="ptr"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_short"), CLSCompliant(false)]
	public static unsafe long WriteFrames(SoundFile file, short *ptr, long frames) => sf_writef_short(file, ptr, frames);
	
	/// <summary>
	/// Encodes and writes raw 32-bit integer samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="ptr"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_int"), CLSCompliant(false)]
	public static unsafe long WriteFrames(SoundFile file, int *ptr, long frames) => sf_writef_int(file, ptr, frames);
	
	/// <summary>
	/// Encodes and writes raw 32-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="ptr"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_float"), CLSCompliant(false)]
	public static unsafe long WriteFrames(SoundFile file, float *ptr, long frames) => sf_writef_float(file, ptr, frames);
	
	/// <summary>
	/// Encodes and writes raw 64-bit floating point samples to the <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file instance to write to.</param>
	/// <param name="ptr">A buffer containing the raw sample data to be written.</param>
	/// <param name="frames">The number of multi-channel frames in the <paramref name="ptr"/>.</param>
	/// <returns>The actual number of frames written to the file.</returns>
	/// <remarks>The buffer size must be evenly divisible by the number of channels.</remarks>
	[NativeMethod("sf_writef_double"), CLSCompliant(false)]
	public static unsafe long WriteFrames(SoundFile file, double *ptr, long frames) => sf_writef_double(file, ptr, frames);


	/// <summary>
	/// Gets a string describing the native <c>libsndfile</c> version.
	/// </summary>
	/// <returns>The version string.</returns>
	[NativeMethod("sf_command")]
	public static unsafe string GetLibraryVersion()
	{
		const int bufferSize = 128;
		
		var buffer = stackalloc byte[bufferSize];
		var length = sf_command(default, CommandType.GetLibVersion, buffer, bufferSize);
		return length == 0 ? string.Empty : Encoding.UTF8.GetString(buffer, length);
	}

	/// <summary>
	/// Retrieve the log buffer generated when opening a file as a string. This log buffer can often contain a good
	/// reason for why <c>libsndfile</c> failed to open a particular file.
	/// </summary>
	/// <param name="file">The file for the log to retrieve.</param>
	/// <param name="bufferSize">The size of the temporary buffer to retrieve the string data.</param>
	/// <returns>The log information, or an empty string if none exists or an error occured.</returns>
	[NativeMethod("sf_command")]
	public static unsafe string GetLogInfo(SoundFile file, int bufferSize = 2048)
	{
		var buffer = stackalloc byte[bufferSize];
		var length = sf_command(file, CommandType.GetLogInfo, buffer, bufferSize);
		return length == 0 ? string.Empty : Encoding.UTF8.GetString(buffer, length);
	}

	/// <summary>
	/// Retrieve the log buffer generated when opening a file as a string. This log buffer can often contain a good
	/// reason for why <c>libsndfile</c> failed to open a particular file.
	/// </summary>
	/// <param name="file">The file for the log to retrieve.</param>
	/// <param name="buffer">A buffer to retrieve the log.</param>
	/// <returns>The number of bytes written to the <paramref name="buffer"/>.</returns>
	[NativeMethod("sf_command")]
	public static unsafe int GetLogInfo(SoundFile file, Span<byte> buffer)
	{
		fixed (byte* ptr = &buffer[0])
		{
			return sf_command(file, CommandType.GetLogInfo, ptr, buffer.Length);
		}
	}
	
	/// <summary>
	/// Retrieves the <see cref="SoundInfo"/> structure for the specified <paramref name="file"/>.
	/// </summary>
	/// <param name="file">The file to query.</param>
	/// <returns>The <see cref="SoundInfo"/> structure for the file.</returns>
	[NativeMethod("sf_command")]
	public static unsafe SoundInfo GetCurrentSoundInfo(SoundFile file)
	{
		SoundInfo info = default;
		sf_command(file, CommandType.GetCurrentSoundInfo, &info, Unsafe.SizeOf<SoundInfo>());
		return info;
	}
}
