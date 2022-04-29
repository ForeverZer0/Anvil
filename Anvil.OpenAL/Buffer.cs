using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using Anvil.Native;
using JetBrains.Annotations;

// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;

/// <summary>
/// Parameters to query/change the properties of <see cref="Source"/> objects.
/// </summary>
[PublicAPI]
public enum BufferProperty
{
	/// <summary>
	/// Buffer frequency (query only).
	/// </summary>
	Frequency = 0x2001,

	/// <summary>
	/// Buffer bits per sample (query only).
	/// </summary>
	Bits = 0x2002,

	/// <summary>
	/// Buffer channel count (query only).
	/// </summary>
	Channels = 0x2003,

	/// <summary>
	/// Buffer data size (query only). 
	/// </summary>
	Size = 0x2004,
}


[SuppressUnmanagedCodeSecurity]
public static unsafe partial class AL
{
	[NativeMethod("alGenBuffers")]
	public static Buffer GenBuffer()
	{
		Buffer buffer = default;
		alGenBuffers(1, &buffer);
		CheckErrorState();
		return buffer;
	}

	public static void DeleteBuffer(Buffer buffer)
	{
		alDeleteBuffers(1, &buffer);
		CheckErrorState();
	}

	[NativeMethod("alGenBuffers"), CLSCompliant(false)]
	public static void GenBuffers(int n, Buffer* buffers)
	{
		alGenBuffers(n, buffers);
		CheckErrorState();
	}

	[NativeMethod("alGenBuffers")]
	public static void GenBuffers(Span<Buffer> buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alGenBuffers(buffers.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGenBuffers")]
	public static void GenBuffers(Buffer[] buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alGenBuffers(buffers.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alDeleteBuffers"), CLSCompliant(false)]
	public static void DeleteBuffers(int n, Buffer* buffers)
	{
		alDeleteBuffers(n, buffers);
		CheckErrorState();
	}

	[NativeMethod("alDeleteBuffers")]
	public static void DeleteBuffers(Span<Buffer> buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alDeleteBuffers(buffers.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alDeleteBuffers")]
	public static void DeleteBuffers(Buffer[] buffers)
	{
		fixed (Buffer* ptr = &buffers[0])
		{
			alDeleteBuffers(buffers.Length, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alIsBuffer"), ContractAnnotation("buffer:null => false")]
	public static bool IsBuffer(Buffer? buffer)
	{
		return buffer.HasValue && alIsBuffer(buffer.Value);
	}

	[NativeMethod("alBufferData"), CLSCompliant(false)]
	public static void BufferData(Buffer buffer, AudioFormat format, void* data, int size, int frequency)
	{
		alBufferData(buffer, format, data, size, frequency);
		CheckErrorState();
	}

	[NativeMethod("alBufferData")]
	public static void BufferData(Buffer buffer, AudioFormat format, IntPtr data, int size, int frequency)
	{
		alBufferData(buffer, format, data.ToPointer(), size, frequency);
		CheckErrorState();
	}

	[NativeMethod("alBufferData")]
	public static void BufferData<T>(Buffer buffer, AudioFormat format, ReadOnlySpan<T> data, int frequency)
		where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data[0])
		{
			alBufferData(buffer, format, ptr, size, frequency);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferData")]
	public static void BufferData<T>(Buffer buffer, AudioFormat format, T[] data, int offset, int length, int frequency) where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * length;
		fixed (T* ptr = &data[offset])
		{
			alBufferData(buffer, format, ptr, size, frequency);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferDataStatic"), CLSCompliant(false), RequiredExtension("AL_EXT_STATIC_BUFFER")]
	public static void BufferDataStatic(Buffer buffer, AudioFormat format, void* data, int size, int frequency)
	{
		alBufferDataStatic(buffer, format, data, size, frequency);
		CheckErrorState();
	}

	[NativeMethod("alBufferDataStatic"), RequiredExtension("AL_EXT_STATIC_BUFFER")]
	public static void BufferDataStatic(Buffer buffer, AudioFormat format, IntPtr data, int size, int frequency)
	{
		alBufferDataStatic(buffer, format, data.ToPointer(), size, frequency);
		CheckErrorState();
	}

	[NativeMethod("alBufferDataStatic"), RequiredExtension("AL_EXT_STATIC_BUFFER")]
	public static void BufferDataStatic<T>(Buffer buffer, AudioFormat format, ReadOnlySpan<T> data, int frequency)
		where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data[0])
		{
			alBufferDataStatic(buffer, format, ptr, size, frequency);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferDataStatic"), RequiredExtension("AL_EXT_STATIC_BUFFER")]
	public static void BufferDataStatic<T>(Buffer buffer, AudioFormat format, T[] data, int frequency)
		where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data[0])
		{
			alBufferDataStatic(buffer, format, ptr, size, frequency);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alBufferSubDataSOFT"), CLSCompliant(false)]
	public static void BufferSubData(Buffer buffer, AudioFormat format, void* data, int offset, int size)
	{
		alBufferSubDataSOFT(buffer, format, data, offset, size);
		CheckErrorState();
	}
	
	[NativeMethod("alBufferSubDataSOFT")]
	public static void BufferSubData(Buffer buffer, AudioFormat format, IntPtr data, int offset, int size)
	{
		alBufferSubDataSOFT(buffer, format, data.ToPointer(), offset, size);
		CheckErrorState();
	}

	[NativeMethod("alBufferSubDataSOFT")]
	public static void BufferSubData<T>(Buffer buffer, AudioFormat format, ReadOnlySpan<T> data, int offset)
		where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data[0])
		{
			alBufferSubDataSOFT(buffer, format, ptr, offset, size);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferSubDataSOFT")]
	public static void BufferSubData<T>(Buffer buffer, AudioFormat format, T[] data, int offset) where T : unmanaged
	{
		var size = Unsafe.SizeOf<T>() * data.Length;
		fixed (T* ptr = &data[0])
		{
			alBufferSubDataSOFT(buffer, format, ptr, offset, size);
		}
		CheckErrorState();
	}

	[NativeMethod("alIsBufferFormatSupportedSOFT")]
	public static bool IsBufferFormatSupported(BufferFormat format)
	{
		return alIsBufferFormatSupportedSOFT(format);
	}

	[NativeMethod("alBufferSamplesSOFT"), CLSCompliant(false)]
	public static void BufferSamples(Buffer buffer, int frequency, BufferFormat internalFormat, int samples,
		BufferChannels channels, SampleType type, void* data)
	{
		alBufferSamplesSOFT(buffer, frequency, internalFormat, samples, channels, type, data);
		CheckErrorState();
	}

	[NativeMethod("alBufferSubSamplesSOFT"), CLSCompliant(false)]
	public static void BufferSubSamples(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, void* data)
	{
		alBufferSubSamplesSOFT(buffer, offset, samples, channels, type, data);
		CheckErrorState();
	}

	[NativeMethod("alGetBufferSamplesSOFT"), CLSCompliant(false)]
	public static void GetBufferSamples(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, void* data)
	{
		alGetBufferSamplesSOFT(buffer, offset, samples, channels, type, data);
		CheckErrorState();
	}

	[NativeMethod("alBufferSamplesSOFT")]
	public static void BufferSamples(Buffer buffer, int frequency, BufferFormat internalFormat, int samples,
		BufferChannels channels, SampleType type, IntPtr data)
	{
		alBufferSamplesSOFT(buffer, frequency, internalFormat, samples, channels, type, data.ToPointer());
		CheckErrorState();
	}

	[NativeMethod("alBufferSubSamplesSOFT")]
	public static void BufferSubSamples(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, IntPtr data)
	{
		alBufferSubSamplesSOFT(buffer, offset, samples, channels, type, data.ToPointer());
		CheckErrorState();
	}

	[NativeMethod("alGetBufferSamplesSOFT")]
	public static void GetBufferSamples(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, IntPtr data)
	{
		alGetBufferSamplesSOFT(buffer, offset, samples, channels, type, data.ToPointer());
		CheckErrorState();
	}

	[NativeMethod("alBufferSamplesSOFT")]
	public static void BufferSamples<T>(Buffer buffer, int frequency, BufferFormat internalFormat, int samples,
		BufferChannels channels, SampleType type, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data[0])
		{
			alBufferSamplesSOFT(buffer, frequency, internalFormat, samples, channels, type, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferSubSamplesSOFT")]
	public static void BufferSubSamples<T>(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data[0])
		{
			alBufferSubSamplesSOFT(buffer, offset, samples, channels, type, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetBufferSamplesSOFT")]
	public static void GetBufferSamples<T>(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, ReadOnlySpan<T> data) where T : unmanaged
	{
		fixed (T* ptr = &data[0])
		{
			alGetBufferSamplesSOFT(buffer, offset, samples, channels, type, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferSamplesSOFT")]
	public static void BufferSamples<T>(Buffer buffer, int frequency, BufferFormat internalFormat, int samples,
		BufferChannels channels, SampleType type, T[] data) where T : unmanaged
	{
		fixed (T* ptr = &data[0])
		{
			alBufferSamplesSOFT(buffer, frequency, internalFormat, samples, channels, type, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferSubSamplesSOFT")]
	public static void BufferSubSamples<T>(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, T[] data) where T : unmanaged
	{
		fixed (T* ptr = &data[0])
		{
			alBufferSubSamplesSOFT(buffer, offset, samples, channels, type, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetBufferSamplesSOFT")]
	public static void GetBufferSamples<T>(Buffer buffer, int offset, int samples, BufferChannels channels,
		SampleType type, T[] data) where T : unmanaged
	{
		fixed (T* ptr = &data[0])
		{
			alGetBufferSamplesSOFT(buffer, offset, samples, channels, type, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferf")]
	public static void BufferF(Buffer buffer, BufferProperty parameter, float value)
	{
		alBufferf(buffer, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alBuffer3f")]
	public static void BufferF(Buffer buffer, BufferProperty parameter, float value1, float value2, float value3)
	{
		alBuffer3f(buffer, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alBufferfv"), CLSCompliant(false)]
	public static void BufferF(Buffer buffer, BufferProperty parameter, float* values)
	{
		alBufferfv(buffer, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alBufferfv")]
	public static void BufferF(Buffer buffer, BufferProperty parameter, ReadOnlySpan<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alBufferfv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferfv")]
	public static void BufferF(Buffer buffer, BufferProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alBufferfv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferfv")]
	public static void BufferF(Buffer buffer, BufferProperty parameter, Vector3 value)
	{
		alBufferfv(buffer, parameter, &value.X);
		CheckErrorState();
	}

	[NativeMethod("alBufferfv"), CLSCompliant(false)]
	public static void BufferF(Buffer buffer, BufferProperty parameter, Vector3* value)
	{
		alBufferfv(buffer, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alBufferi")]
	public static void BufferI(Buffer buffer, BufferProperty parameter, int value)
	{
		alBufferi(buffer, parameter, value);
		CheckErrorState();
	}

	[NativeMethod("alBufferi")]
	public static void BufferI<TEnum>(Buffer buffer, BufferProperty parameter, TEnum value) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		alBufferi(buffer, parameter, Unsafe.As<TEnum, int>(ref value));
		CheckErrorState();
	}
	
	[NativeMethod("alBuffer3i")]
	public static void BufferI(Buffer buffer, BufferProperty parameter, int value1, int value2, int value3)
	{
		alBuffer3i(buffer, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alBufferiv"), CLSCompliant(false)]
	public static void BufferI(Buffer buffer, BufferProperty parameter, int* values)
	{
		alBufferiv(buffer, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alBufferiv")]
	public static void BufferI(Buffer buffer, BufferProperty parameter, ReadOnlySpan<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alBufferiv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alBufferiv")]
	public static void BufferI(Buffer buffer, BufferProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alBufferiv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetBufferf")]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, out float value)
	{
		float temp = default;
		alGetBufferf(buffer, parameter, &temp);
		CheckErrorState();
		value = temp;
	}

	[NativeMethod("alGetBuffer3f"), CLSCompliant(false)]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, float* value1, float* value2, float* value3)
	{
		alGetBuffer3f(buffer, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alGetBuffer3f")]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, out float value1, out float value2, out float value3)
	{
		var v = stackalloc float[3];
		alGetBuffer3f(buffer, parameter, &v[0], &v[1], &v[2]);
		CheckErrorState();
		value1 = v[0];
		value2 = v[1];
		value3 = v[2];
	}

	[NativeMethod("alGetBufferfv"), CLSCompliant(false)]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, float* values)
	{
		alGetBufferfv(buffer, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetBufferfv")]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, Span<float> values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetBufferfv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetBufferfv")]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, float[] values)
	{
		fixed (float* ptr = &values[0])
		{
			alGetBufferfv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetBufferfv"), CLSCompliant(false)]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, Vector3* value)
	{
		alGetBufferfv(buffer, parameter, (float*) value);
		CheckErrorState();
	}

	[NativeMethod("alGetBufferfv")]
	public static void GetBufferF(Buffer buffer, BufferProperty parameter, out Vector3 value)
	{
		Vector3 temp = default;
		alGetBufferfv(buffer, parameter, &temp.X);
		CheckErrorState();
		value = temp;
	}
	
	[NativeMethod("alGetBufferi")]
	public static int GetBufferI(Buffer buffer, BufferProperty parameter)
	{
		int temp;
		alGetBufferi(buffer, parameter, &temp);
		CheckErrorState();
		return temp;
	}

	[NativeMethod("alGetBufferi")]
	public static void GetBufferI(Buffer buffer, BufferProperty parameter, out int value)
	{
		int temp = default;
		alGetBufferi(buffer, parameter, &temp);
		CheckErrorState();
		value = temp;
	}
	
	[NativeMethod("alGetBufferi")]
	public static void GetBufferI<TEnum>(Buffer buffer, BufferProperty parameter, out TEnum value) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		int temp = default;
		alGetBufferi(buffer, parameter, &temp);
		CheckErrorState();
		value = Unsafe.As<int, TEnum>(ref temp);
	}

	[NativeMethod("alGetBuffer3i"), CLSCompliant(false)]
	public static void GetBufferI(Buffer buffer, BufferProperty parameter, int* value1, int* value2, int* value3)
	{
		alGetBuffer3i(buffer, parameter, value1, value2, value3);
		CheckErrorState();
	}

	[NativeMethod("alGetBuffer3i")]
	public static void GetBufferI(Buffer buffer, BufferProperty parameter, out int value1, out int value2, out int value3)
	{
		var v = stackalloc int[3];
		alGetBuffer3i(buffer, parameter, &v[0], &v[1], &v[2]);
		CheckErrorState();
		value1 = v[0];
		value2 = v[1];
		value3 = v[2];
	}

	[NativeMethod("alGetBufferiv"), CLSCompliant(false)]
	public static void GetBufferI(Buffer buffer, BufferProperty parameter, int* values)
	{
		alGetBufferiv(buffer, parameter, values);
		CheckErrorState();
	}

	[NativeMethod("alGetBufferiv")]
	public static void GetBufferI(Buffer buffer, BufferProperty parameter, Span<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetBufferiv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}

	[NativeMethod("alGetBufferiv")]
	public static void GetBufferI(Buffer buffer, BufferProperty parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alGetBufferiv(buffer, parameter, ptr);
		}
		CheckErrorState();
	}
	
	[NativeMethod("alBufferCallbackSOFT"), RequiredExtension("AL_SOFT_callback_buffer")]
	public static void BufferCallback(Buffer buffer, int format, int freq, BufferCallbackHandler callback, IntPtr userPtr)
    {
        alBufferCallbackSOFT(buffer, format, freq, callback, userPtr);
        CheckErrorState();
    }

    [NativeMethod("alGetBufferPtrSOFT"), CLSCompliant(false)]
	public static void GetBufferPtr(Buffer buffer, int parameter, void **ptr)
    {
        alGetBufferPtrSOFT(buffer, parameter, ptr);
        CheckErrorState();
    }

    [NativeMethod("alGetBufferPtrSOFT")]
	public static IntPtr GetBufferPtr(Buffer buffer, int parameter)
	{
		void* temp;
		alGetBufferPtrSOFT(buffer, parameter, &temp);
        CheckErrorState();
		return new IntPtr(temp);
	}

	[NativeMethod("alGetBuffer3PtrSOFT"), CLSCompliant(false)]
	public static void GetBufferPtr(Buffer buffer, int parameter, void **ptr0, void **ptr1, void **ptr2)
    {
        alGetBuffer3PtrSOFT(buffer, parameter, ptr0, ptr1, ptr2);
        CheckErrorState();
    }

    [NativeMethod("alGetBuffer3PtrSOFT")]
	public static void GetBufferPtr(Buffer buffer, int parameter, out IntPtr ptr0, out IntPtr ptr1, out IntPtr ptr2)
	{
		var temp = stackalloc void*[3];
		alGetBuffer3PtrSOFT(buffer, parameter, &temp[0], &temp[1], &temp[2]);
        CheckErrorState();
		ptr0 = new IntPtr(temp[0]);
		ptr1 = new IntPtr(temp[1]);
		ptr2 = new IntPtr(temp[2]);
	}

	[NativeMethod("alGetBufferPtrvSOFT")]
	public static void GetBufferPtr(Buffer buffer, int parameter, Span<IntPtr> values)
	{
		fixed (IntPtr* ptr = &values[0])
		{
			alGetBufferPtrvSOFT(buffer, parameter, (void**) ptr);
		}
        CheckErrorState();
	}
	
	[NativeMethod("alGetBufferPtrvSOFT")]
	public static void GetBufferPtr(Buffer buffer, int parameter, IntPtr[] values)
	{
		fixed (IntPtr* ptr = &values[0])
		{
			alGetBufferPtrvSOFT(buffer, parameter, (void**) ptr);
		}
        CheckErrorState();
	}

	[NativeMethod("alGetBufferPtrSOFT")]
	public static IntPtr GetBufferPtr<TEnum>(Buffer buffer, TEnum parameter) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		void* temp;
		alGetBufferPtrSOFT(buffer, Unsafe.As<TEnum, int>(ref parameter), &temp);
        CheckErrorState();
		return new IntPtr(temp);
	}

	[NativeMethod("alGetBuffer3PtrSOFT"), CLSCompliant(false)]
	public static void GetBufferPtr<TEnum>(Buffer buffer, TEnum parameter, void** ptr0, void** ptr1, void** ptr2)  where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		alGetBuffer3PtrSOFT(buffer, Unsafe.As<TEnum, int>(ref parameter), ptr0, ptr1, ptr2);
        CheckErrorState();
	}

	[NativeMethod("alGetBuffer3PtrSOFT")]
	public static void GetBufferPtr<TEnum>(Buffer buffer, TEnum parameter, out IntPtr ptr0, out IntPtr ptr1, out IntPtr ptr2)  where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		var temp = stackalloc void*[3];
		alGetBuffer3PtrSOFT(buffer, Unsafe.As<TEnum, int>(ref parameter), &temp[0], &temp[1], &temp[2]);
        CheckErrorState();
		ptr0 = new IntPtr(temp[0]);
		ptr1 = new IntPtr(temp[1]);
		ptr2 = new IntPtr(temp[2]);
	}

	[NativeMethod("alGetBufferPtrvSOFT")]
	public static void GetBufferPtr<TEnum>(Buffer buffer, TEnum parameter, Span<IntPtr> values) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		fixed (IntPtr* ptr = &values[0])
		{
			alGetBufferPtrvSOFT(buffer, Unsafe.As<TEnum, int>(ref parameter), (void**) ptr);
		}
        CheckErrorState();
	}
	
	[NativeMethod("alGetBufferPtrvSOFT")]
	public static void GetBufferPtr<TEnum>(Buffer buffer, TEnum parameter, IntPtr[] values) where TEnum : Enum
	{
		DebugAssertInt32<TEnum>();
		fixed (IntPtr* ptr = &values[0])
		{
			alGetBufferPtrvSOFT(buffer, Unsafe.As<TEnum, int>(ref parameter), (void**) ptr);
		}
        CheckErrorState();
	}
}