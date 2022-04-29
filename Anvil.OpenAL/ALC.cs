using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;

public delegate void ContextErrorHandler(Device? device, ContextError error);

[SuppressUnmanagedCodeSecurity]
public static unsafe partial class ALC
{
	public const string EfxName = "ALC_EXT_EFX";
	
	/// <summary>
	/// Occurs when OpenAL reports an error state.
	/// </summary>
	public static event ContextErrorHandler? ErrorEmitted;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("AL_ERROR_EVENTS")]
	internal static void CheckErrorState(Device device)
	{
		var error = alcGetError(device);
		if (error != ContextError.None)
			ErrorEmitted?.Invoke(device, error);
	}

	[NativeMethod("alcCreateContext"), NativeMethod("alcMakeContextCurrent"), CLSCompliant(false)]
	public static Context CreateContext(Device device, int* attrList, bool makeCurrent = true)
	{
		var ctx = alcCreateContext(device, attrList);
		CheckErrorState(device);
		if (makeCurrent && !alcMakeContextCurrent(ctx))
			CheckErrorState(device);
		return ctx;
	}

	[NativeMethod("alcCreateContext"), NativeMethod("alcMakeContextCurrent")]
	public static Context CreateContext(Device device, ReadOnlySpan<int> attrList, bool makeCurrent = true)
	{
		Context ctx;
		fixed (int* ptr = &attrList[0])
		{
			ctx = alcCreateContext(device, ptr);
		}
		CheckErrorState(device);
		if (makeCurrent && !alcMakeContextCurrent(ctx))
			CheckErrorState(device);
		return ctx;
	}
	
	[NativeMethod("alcCreateContext"), NativeMethod("alcMakeContextCurrent")]
	public static Context CreateContext(Device device, int[] attrList, bool makeCurrent = true)
	{
		Context ctx;
		fixed (int* ptr = &attrList[0])
		{
			ctx = alcCreateContext(device, ptr);
		}
		CheckErrorState(device);
		if (makeCurrent && !alcMakeContextCurrent(ctx))
			CheckErrorState(device);
		return ctx;
	}
	
	[NativeMethod("alcCreateContext"), NativeMethod("alcMakeContextCurrent")]
	public static Context CreateContext(Device device, ReadOnlySpan<(ContextAttribute, int)> attrList, bool makeCurrent = true)
	{
		var attrs = stackalloc int[(attrList.Length * 2) + 1];
		var srcSize = attrList.Length * sizeof(int) * 2;
		var dstSize = srcSize + sizeof(int);
		
		fixed ((ContextAttribute, int)* ptr = &attrList[0])
		{
			System.Buffer.MemoryCopy(ptr, attrs, dstSize, srcSize);
		}

		var ctx = alcCreateContext(device, attrs);
		CheckErrorState(device);
		if (makeCurrent && !alcMakeContextCurrent(ctx))
			CheckErrorState(device);
		return ctx;
	}

	[NativeMethod("alcCreateContext"), NativeMethod("alcMakeContextCurrent")]
	public static Context CreateContext(Device device, bool makeCurrent = true)
	{
		var ctx = alcCreateContext(device, (int*) 0);
		CheckErrorState(device);
		if (makeCurrent && !alcMakeContextCurrent(ctx))
			CheckErrorState(device);
		return ctx;
	}

	[NativeMethod("alcMakeContextCurrent")]
	public static bool MakeContextCurrent(Context context)
	{
		if (alcMakeContextCurrent(context)) 
			return true;
		
		var device = alcGetContextsDevice(context);
		CheckErrorState(device);
		return false;
	}

	[NativeMethod("alcProcessContext")]
	public static void ProcessContext(Context context) => alcProcessContext(context);

	[NativeMethod("alcSuspendContext")]
	public static void SuspendContext(Context context) => alcSuspendContext(context);

	[NativeMethod("alcDestroyContext")]
	public static void DestroyContext(Context context) => alcDestroyContext(context);

	[NativeMethod("alcGetCurrentContext")]
	public static Context? GetCurrentContext()
	{
		var ctx =  alcGetCurrentContext();
		return ctx == default ? null : ctx;
	}

	[NativeMethod("alcGetContextsDevice")]
	public static Device GetContextsDevice(Context context) => alcGetContextsDevice(context);
	
	[NativeMethod("alcOpenDevice")]
	public static Device OpenDevice(string? deviceName)
	{
		Device device;
		if (deviceName is null)
			device = alcOpenDevice((byte*)0);
		else
		{
			fixed (byte* ptr = &UTF8String.Pin(deviceName))
			{
				device = alcOpenDevice(ptr);
			}
		}
		CheckErrorState(device);
		AL.Load(device);
		return device;
	}

	[NativeMethod("alcOpenDevice")]
	public static Device OpenDevice()
	{
		var device = alcOpenDevice((byte*) 0);
		CheckErrorState(device);
		AL.Load(device);
		return device;
	}

	[NativeMethod("alcCloseDevice")]
	public static bool CloseDevice(Device device) => alcCloseDevice(device);

	[NativeMethod("alcGetError")]
	public static ContextError GetError(Device device) => alcGetError(device);
	
	[NativeMethod("alcIsExtensionPresent"), ContractAnnotation("extName:null => false")]
	public static bool IsExtensionPresent(Device device, string? extName)
	{
		if (extName is null)
			return false;
		fixed (byte* ptr = &UTF8String.Pin(extName))
		{
			return alcIsExtensionPresent(device, ptr);
		}
	}
	
	[NativeMethod("alcGetProcAddress")]
	public static IntPtr GetProcAddress(Device device, string? funcName)
	{
		if (funcName is null)
			return IntPtr.Zero;

		IntPtr ptr;
		fixed (byte* b = &UTF8String.Pin(funcName))
		{
			ptr = new IntPtr(alcGetProcAddress(device, b));
		}
		if (ptr == IntPtr.Zero)
			Debug.WriteLine($"Failed to find entry point for \"{funcName}\"");
		return ptr;
	}
	
	[NativeMethod("alcGetProcAddress")]
	public static TDelegate GetProcAddress<TDelegate>(Device device, string funcName) where TDelegate : Delegate
	{
		fixed (byte* b = &UTF8String.Pin(funcName))
		{
			var ptr = new IntPtr(alcGetProcAddress(device, b));
			return Marshal.GetDelegateForFunctionPointer<TDelegate>(ptr);
		}
	}
	
	[NativeMethod("alcGetEnumValue")]
	public static int GetEnumValue(Device device, string enumName)
	{
		int value;
		fixed (byte* ptr = &UTF8String.Pin(enumName))
		{
			value = alcGetEnumValue(device, ptr);
		}
		CheckErrorState(device);
		return value;
	}

	[NativeMethod("alcGetString")]
	public static string? GetString(Device device, int parameter)
	{
		var ptr = alcGetString(device, parameter);
		var length = 0;
		while (ptr[length] != 0)
		{
			length++;
		}
		return length == 0 ? null : Encoding.UTF8.GetString(ptr, length);
	}
	
	[NativeMethod("alcGetString")]
	public static string? GetString<TEnum>(Device device, TEnum parameter) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		var ptr = alcGetString(device, Unsafe.As<TEnum, int>(ref parameter));
		var length = 0;
		while (ptr[length] != 0)
		{
			length++;
		}
		return length == 0 ? null : Encoding.UTF8.GetString(ptr, length);
	}

	[NativeMethod("alcGetIntegerv"), CLSCompliant(false)]
	public static void GetInteger(Device device, int parameter, int size, int* values)
	{
		alcGetIntegerv(device, parameter, size, values);
		CheckErrorState(device);
	}

	[NativeMethod("alcGetIntegerv")]
	public static void GetInteger(Device device, int parameter, Span<int> values)
	{
		fixed (int* ptr = &values[0])
		{
			alcGetIntegerv(device, parameter, values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetIntegerv")]
	public static void GetInteger(Device device, int parameter, int[] values)
	{
		fixed (int* ptr = &values[0])
		{
			alcGetIntegerv(device, parameter, values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetIntegerv")]
	public static int GetInteger(Device device, int parameter)
	{
		var value = 0;
		alcGetIntegerv(device, parameter, 1, &value);
		CheckErrorState(device);
		return value;
	}

	[NativeMethod("alcGetIntegerv"), CLSCompliant(false)]
	public static void GetInteger<TEnum>(Device device, TEnum parameter, int size, int* values) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		alcGetIntegerv(device, Unsafe.As<TEnum, int>(ref parameter), size, values);
		CheckErrorState(device);
	}

	[NativeMethod("alcGetIntegerv")]
	public static void GetInteger<TEnum>(Device device, TEnum parameter, Span<int> values) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		fixed (int* ptr = &values[0])
		{
			alcGetIntegerv(device, Unsafe.As<TEnum, int>(ref parameter), values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetIntegerv")]
	public static void GetInteger<TEnum>(Device device, TEnum parameter, int[] values) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		fixed (int* ptr = &values[0])
		{
			alcGetIntegerv(device, Unsafe.As<TEnum, int>(ref parameter), values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetIntegerv")]
	public static int GetInteger<TEnum>(Device device, TEnum parameter) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		var value = 0;
		alcGetIntegerv(device, Unsafe.As<TEnum, int>(ref parameter), 1, &value);
		CheckErrorState(device);
		return value;
	}
	
	[NativeMethod("alcCaptureOpenDevice")]
	public static Device CaptureOpenDevice(string? deviceName, int frequency, int format, int buffersize)
	{
		Device device;
		if (deviceName is null)
			device = alcCaptureOpenDevice((byte*)0, frequency, format, buffersize);

		else
		{
			fixed (byte* ptr = &UTF8String.Pin(deviceName))
			{
				device = alcCaptureOpenDevice(ptr, frequency, format, buffersize);
			}
		}
		CheckErrorState(device);
		AL.Load(device);
		return device;
	}
	
	[NativeMethod("alcCaptureOpenDevice")]
	public static Device CaptureOpenDevice<TEnum>(string? deviceName, int frequency, TEnum format, int buffersize) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		Device device;
		
		if (deviceName is null)
			device = alcCaptureOpenDevice((byte*)0, frequency, Unsafe.As<TEnum, int>(ref format), buffersize);
		else
		{
			fixed (byte* ptr = &UTF8String.Pin(deviceName))
			{
				device = alcCaptureOpenDevice(ptr, frequency, Unsafe.As<TEnum, int>(ref format), buffersize);
			}
		}
		CheckErrorState(device);
		AL.Load(device);
		return device;
	}

	[NativeMethod("alcCaptureCloseDevice")]
	public static bool CaptureCloseDevice(Device device) => alcCaptureCloseDevice(device);

	[NativeMethod("alcCaptureStart")]
	public static void CaptureStart(Device device)
	{
		alcCaptureStart(device);
		CheckErrorState(device);
	}

	[NativeMethod("alcCaptureStop")]
	public static void CaptureStop(Device device)
	{
		alcCaptureStop(device);
		CheckErrorState(device);
	}

	[NativeMethod("alcCaptureSamples"), CLSCompliant(false)]
	public static void CaptureSamples(Device device, void* buffer, int samples)
	{
		alcCaptureSamples(device, buffer, samples);
		CheckErrorState(device);
	}

	[NativeMethod("alcCaptureSamples")]
	public static void CaptureSamples(Device device, IntPtr buffer, int samples)
	{
		alcCaptureSamples(device, buffer.ToPointer(), samples);
		CheckErrorState(device);
	}

	[NativeMethod("alcCaptureSamples")]
	public static void CaptureSamples<T>(Device device, Span<T> buffer, int samples) where T : unmanaged
	{
		fixed (T* ptr = &buffer[0])
		{
			alcCaptureSamples(device, ptr, samples);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcCaptureSamples")]
	public static void CaptureSamples<T>(Device device, T[] buffer, int samples) where T : unmanaged
	{
		fixed (T* ptr = &buffer[0])
		{
			alcCaptureSamples(device, ptr, samples);
		}
		CheckErrorState(device);
	}

	[NativeMethod("alcSetThreadContext"), RequiredExtension("ALC_EXT_thread_local_context")]
	public static bool SetThreadContext(Context context) => alcSetThreadContext(context);

	[NativeMethod("alcGetThreadContext"), RequiredExtension("ALC_EXT_thread_local_context")]
	public static Context GetThreadContext() => alcGetThreadContext();
	
	[NativeMethod("alcLoopbackOpenDeviceSOFT")]
	public static Device LoopbackOpenDevice(string? deviceName)
	{
		Device device;
		
		if (deviceName is null)
			device = alcLoopbackOpenDeviceSOFT((byte*) 0);
		else
		{
			fixed (byte* ptr = &UTF8String.Pin(deviceName))
			{
				device = alcLoopbackOpenDeviceSOFT(ptr);
			}
		}
		
		CheckErrorState(device);
		AL.Load(device);
		return device;
	}

	[NativeMethod("alcIsRenderFormatSupportedSOFT")]
	public static bool IsRenderFormatSupported(Device device, int frequency, int channels, int type) => alcIsRenderFormatSupportedSOFT(device, frequency, channels, type);

	[NativeMethod("alcIsRenderFormatSupportedSOFT")]
	public static bool IsRenderFormatSupported<TEnum>(Device device, int frequency, int channels, TEnum type) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		var result = alcIsRenderFormatSupportedSOFT(device, frequency, channels, Unsafe.As<TEnum, int>(ref type));
		CheckErrorState(device);
		return result;
	}

	[NativeMethod("alcRenderSamplesSOFT"), CLSCompliant(false)]
	public static void RenderSamples(Device device, void* buffer, int samples)
	{
		alcRenderSamplesSOFT(device, buffer, samples);
		CheckErrorState(device);
	}

	[NativeMethod("alcRenderSamplesSOFT")]
	public static void RenderSamples(Device device, IntPtr buffer, int samples)
	{
		alcRenderSamplesSOFT(device, buffer.ToPointer(), samples);
		CheckErrorState(device);
	}

	[NativeMethod("alcRenderSamplesSOFT")]
	public static void RenderSamples<T>(Device device, Span<T> buffer, int samples) where T : unmanaged
	{
		fixed (T* ptr = &buffer[0])
		{
			alcRenderSamplesSOFT(device, ptr, samples);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcRenderSamplesSOFT")]
	public static void RenderSamples<T>(Device device, T[] buffer, int samples) where T : unmanaged
	{
		fixed (T* ptr = &buffer[0])
		{
			alcRenderSamplesSOFT(device, ptr, samples);
		}
		CheckErrorState(device);
	}

	[NativeMethod("alcDevicePauseSOFT")]
	public static void DevicePause(Device device)
	{
		alcDevicePauseSOFT(device);
		CheckErrorState(device);
	}

	[NativeMethod("alcDeviceResumeSOFT")]
	public static void DeviceResume(Device device)
	{
		alcDeviceResumeSOFT(device);
		CheckErrorState(device);
	}

	[NativeMethod("alcGetStringiSOFT")]
	public static string? GetString(Device device, int parameter, int index)
	{
		var ptr = alcGetStringiSOFT(device, parameter, index);
		CheckErrorState(device);
		var length = 0;
		while (ptr[length] != 0)
		{
			length++;
		}
		return length == 0 ? null : Encoding.UTF8.GetString(ptr, length);
	}
	
	[NativeMethod("alcGetStringiSOFT")]
	public static string? GetString<TEnum>(Device device, TEnum parameter, int index) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		var ptr = alcGetStringiSOFT(device, Unsafe.As<TEnum, int>(ref parameter), index);
		CheckErrorState(device);
		
		var length = 0;
		while (ptr[length] != 0)
		{
			length++;
		}
		return length == 0 ? null : Encoding.UTF8.GetString(ptr, length);
	}

	[NativeMethod("alcResetDeviceSOFT"), CLSCompliant(false)]
	public static bool ResetDevice(Device device, int* attrList)
	{
		var result = alcResetDeviceSOFT(device, attrList);
		CheckErrorState(device);
		return result;
	}

	[NativeMethod("alcResetDeviceSOFT")]
	public static bool ResetDevice(Device device, ReadOnlySpan<int> attrList)
	{
		bool result;
		fixed (int* ptr = &attrList[0])
		{
			result = alcResetDeviceSOFT(device, ptr);
		}
		CheckErrorState(device);
		return result;
	}
	
	[NativeMethod("alcResetDeviceSOFT")]
	public static bool ResetDevice(Device device, int[] attrList)
	{
		bool result;
		fixed (int* ptr = &attrList[0])
		{
			result = alcResetDeviceSOFT(device, ptr);
		}
		CheckErrorState(device);
		return result;
	}

	[NativeMethod("alcGetInteger64vSOFT"), CLSCompliant(false)]
	public static void GetInteger64(Device device, int parameter, int size, long *values) => alcGetInteger64vSOFT(device, parameter, size, values);

	[NativeMethod("alcGetInteger64vSOFT")]
	public static void GetInteger64(Device device, int parameter, Span<long> values)
	{
		fixed (long* ptr = &values[0])
		{
			alcGetInteger64vSOFT(device, parameter, values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetInteger64vSOFT")]
	public static void GetInteger64(Device device, int parameter, long[] values)
	{
		fixed (long* ptr = &values[0])
		{
			alcGetInteger64vSOFT(device, parameter, values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetInteger64vSOFT")]
	public static long GetInteger64(Device device, int parameter)
	{
		long value = 0;
		alcGetInteger64vSOFT(device, parameter, 1, &value);
		CheckErrorState(device);
		return value;
	}

	[NativeMethod("alcGetInteger64vSOFT"), CLSCompliant(false)]
	public static void GetInteger64<TEnum>(Device device, TEnum parameter, int size, long* values) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		alcGetInteger64vSOFT(device, Unsafe.As<TEnum, int>(ref parameter), size, values);
		CheckErrorState(device);
	}

	[NativeMethod("alcGetInteger64vSOFT")]
	public static void GetInteger64<TEnum>(Device device, TEnum parameter, Span<long> values) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		fixed (long* ptr = &values[0])
		{
			alcGetInteger64vSOFT(device, Unsafe.As<TEnum, int>(ref parameter), values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetInteger64vSOFT")]
	public static void GetInteger64<TEnum>(Device device, TEnum parameter, long[] values) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		fixed (long* ptr = &values[0])
		{
			alcGetInteger64vSOFT(device, Unsafe.As<TEnum, int>(ref parameter), values.Length, ptr);
		}
		CheckErrorState(device);
	}
	
	[NativeMethod("alcGetInteger64vSOFT")]
	public static long GetInteger64<TEnum>(Device device, TEnum parameter) where TEnum : Enum
	{
		AL.DebugAssertInt32<TEnum>();
		long value = 0;
		alcGetInteger64vSOFT(device, Unsafe.As<TEnum, int>(ref parameter), 1, &value);
		CheckErrorState(device);
		return value;
	}
}
