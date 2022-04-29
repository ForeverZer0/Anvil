using System.Runtime.InteropServices;
using System.Security;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Anvil.OpenAL;

[SuppressUnmanagedCodeSecurity, PublicAPI]
public static unsafe partial class ALC
{
	private static readonly delegate *unmanaged[Cdecl]<Device,int*,Context> alcCreateContext;
	private static readonly delegate *unmanaged[Cdecl]<Context,bool> alcMakeContextCurrent;
	private static readonly delegate *unmanaged[Cdecl]<Context,void> alcProcessContext;
	private static readonly delegate *unmanaged[Cdecl]<Context,void> alcSuspendContext;
	private static readonly delegate *unmanaged[Cdecl]<Context,void> alcDestroyContext;
	private static readonly delegate *unmanaged[Cdecl]<Context> alcGetCurrentContext;
	private static readonly delegate *unmanaged[Cdecl]<Context,Device> alcGetContextsDevice;
	private static readonly delegate *unmanaged[Cdecl]<byte*,Device> alcOpenDevice;
	private static readonly delegate *unmanaged[Cdecl]<Device,bool> alcCloseDevice;
	private static readonly delegate *unmanaged[Cdecl]<Device,ContextError> alcGetError;
	private static readonly delegate *unmanaged[Cdecl]<Device,byte*,bool> alcIsExtensionPresent;
	private static readonly delegate *unmanaged[Cdecl]<Device,byte*,void*> alcGetProcAddress;
	private static readonly delegate *unmanaged[Cdecl]<Device,byte*,int> alcGetEnumValue;
	private static readonly delegate *unmanaged[Cdecl]<Device,int,byte*> alcGetString;
	private static readonly delegate *unmanaged[Cdecl]<Device,int,int,int*,void> alcGetIntegerv;
	private static readonly delegate *unmanaged[Cdecl]<byte*,int,int,int,Device> alcCaptureOpenDevice;
	private static readonly delegate *unmanaged[Cdecl]<Device,bool> alcCaptureCloseDevice;
	private static readonly delegate *unmanaged[Cdecl]<Device,void> alcCaptureStart;
	private static readonly delegate *unmanaged[Cdecl]<Device,void> alcCaptureStop;
	private static readonly delegate *unmanaged[Cdecl]<Device,void*,int,void> alcCaptureSamples;
	private static readonly delegate *unmanaged[Cdecl]<Context,bool> alcSetThreadContext;
	private static readonly delegate *unmanaged[Cdecl]<Context> alcGetThreadContext;
	private static readonly delegate *unmanaged[Cdecl]<byte*,Device> alcLoopbackOpenDeviceSOFT;
	private static readonly delegate *unmanaged[Cdecl]<Device,int,int,int,bool> alcIsRenderFormatSupportedSOFT;
	private static readonly delegate *unmanaged[Cdecl]<Device,void*,int,void> alcRenderSamplesSOFT;
	private static readonly delegate *unmanaged[Cdecl]<Device,void> alcDevicePauseSOFT;
	private static readonly delegate *unmanaged[Cdecl]<Device,void> alcDeviceResumeSOFT;
	private static readonly delegate *unmanaged[Cdecl]<Device,int,int,byte*> alcGetStringiSOFT;
	private static readonly delegate *unmanaged[Cdecl]<Device,int*,bool> alcResetDeviceSOFT;
	private static readonly delegate *unmanaged[Cdecl]<Device,int,int,long*,void> alcGetInteger64vSOFT;

	private static UnmanagedLibrary Library;
	
	static ALC()
	{
		Library = new UnmanagedLibrary("/usr/lib/libopenal.so");
		alcCreateContext = (delegate *unmanaged[Cdecl]<Device,int*,Context>) Library.Import("alcCreateContext");
		alcMakeContextCurrent = (delegate *unmanaged[Cdecl]<Context,bool>) Library.Import("alcMakeContextCurrent");
		alcProcessContext = (delegate *unmanaged[Cdecl]<Context,void>) Library.Import("alcProcessContext");
		alcSuspendContext = (delegate *unmanaged[Cdecl]<Context,void>) Library.Import("alcSuspendContext");
		alcDestroyContext = (delegate *unmanaged[Cdecl]<Context,void>) Library.Import("alcDestroyContext");
		alcGetCurrentContext = (delegate *unmanaged[Cdecl]<Context>) Library.Import("alcGetCurrentContext");
		alcGetContextsDevice = (delegate *unmanaged[Cdecl]<Context,Device>) Library.Import("alcGetContextsDevice");
		alcOpenDevice = (delegate *unmanaged[Cdecl]<byte*,Device>) Library.Import("alcOpenDevice");
		alcCloseDevice = (delegate *unmanaged[Cdecl]<Device,bool>) Library.Import("alcCloseDevice");
		alcGetError = (delegate *unmanaged[Cdecl]<Device,ContextError>) Library.Import("alcGetError");
		alcIsExtensionPresent = (delegate *unmanaged[Cdecl]<Device,byte*,bool>) Library.Import("alcIsExtensionPresent");
		alcGetProcAddress = (delegate *unmanaged[Cdecl]<Device,byte*,void*>) Library.Import("alcGetProcAddress");
		alcGetEnumValue = (delegate *unmanaged[Cdecl]<Device,byte*,int>) Library.Import("alcGetEnumValue");
		alcGetString = (delegate *unmanaged[Cdecl]<Device,int,byte*>) Library.Import("alcGetString");
		alcGetIntegerv = (delegate *unmanaged[Cdecl]<Device,int,int,int*,void>) Library.Import("alcGetIntegerv");
		alcCaptureOpenDevice = (delegate *unmanaged[Cdecl]<byte*,int,int,int,Device>) Library.Import("alcCaptureOpenDevice", false);
		alcCaptureCloseDevice = (delegate *unmanaged[Cdecl]<Device,bool>) Library.Import("alcCaptureCloseDevice", false);
		alcCaptureStart = (delegate *unmanaged[Cdecl]<Device,void>) Library.Import("alcCaptureStart", false);
		alcCaptureStop = (delegate *unmanaged[Cdecl]<Device,void>) Library.Import("alcCaptureStop", false);
		alcCaptureSamples = (delegate *unmanaged[Cdecl]<Device,void*,int,void>) Library.Import("alcCaptureSamples", false);
		alcSetThreadContext = (delegate *unmanaged[Cdecl]<Context,bool>) Library.Import("alcSetThreadContext", false);
		alcGetThreadContext = (delegate *unmanaged[Cdecl]<Context>) Library.Import("alcGetThreadContext", false);
		alcLoopbackOpenDeviceSOFT = (delegate *unmanaged[Cdecl]<byte*,Device>) Library.Import("alcLoopbackOpenDeviceSOFT", false);
		alcIsRenderFormatSupportedSOFT = (delegate *unmanaged[Cdecl]<Device,int,int,int,bool>) Library.Import("alcIsRenderFormatSupportedSOFT", false);
		alcRenderSamplesSOFT = (delegate *unmanaged[Cdecl]<Device,void*,int,void>) Library.Import("alcRenderSamplesSOFT", false);
		alcDevicePauseSOFT = (delegate *unmanaged[Cdecl]<Device,void>) Library.Import("alcDevicePauseSOFT", false);
		alcDeviceResumeSOFT = (delegate *unmanaged[Cdecl]<Device,void>) Library.Import("alcDeviceResumeSOFT", false);
		alcGetStringiSOFT = (delegate *unmanaged[Cdecl]<Device,int,int,byte*>) Library.Import("alcGetStringiSOFT", false);
		alcResetDeviceSOFT = (delegate *unmanaged[Cdecl]<Device,int*,bool>) Library.Import("alcResetDeviceSOFT", false);
		alcGetInteger64vSOFT = (delegate *unmanaged[Cdecl]<Device,int,int,long*,void>) Library.Import("alcGetInteger64vSOFT", false);
	}

	public const int FALSE = 0;
	public const int TRUE = 1;

	public const int CHAN_MAIN_LOKI = 0x500001;
	public const int CHAN_PCM_LOKI = 0x500002;
	public const int CHAN_CD_LOKI = 0x500003;
	public const int CONNECTED = 0x313;
	public const int FORMAT_CHANNELS = 0x1990;
	public const int FORMAT_TYPE = 0x1991;
	public const int BYTE = 0x1400;
	public const int UNSIGNED_BYTE = 0x1401;
	public const int SHORT = 0x1402;
	public const int UNSIGNED_SHORT = 0x1403;
	public const int INT = 0x1404;
	public const int UNSIGNED_INT = 0x1405;
	public const int FLOAT = 0x1406;
	public const int MONO = 0x1500;
	public const int STEREO = 0x1501;
	public const int QUAD = 0x1503;
	public const int FIVE_POINT_ONE = 0x1504;
	public const int SIX_POINT_ONE = 0x1505;
	public const int SEVEN_POINT_ONE = 0x1506;
	public const int DEFAULT_FILTER_ORDER = 0x1100;
	public const int HRTF = 0x1992;
	public const int DONT_CARE = 0x0002;
	public const int HRTF_STATUS = 0x1993;
	public const int HRTF_DISABLED = 0x0000;
	public const int HRTF_ENABLED = 0x0001;
	public const int HRTF_DENIED = 0x0002;
	public const int HRTF_REQUIRED = 0x0003;
	public const int HRTF_HEADPHONES_DETECTED = 0x0004;
	public const int HRTF_UNSUPPORTED_FORMAT = 0x0005;
	public const int NUM_HRTF_SPECIFIERS = 0x1994;
	public const int HRTF_SPECIFIER = 0x1995;
	public const int HRTF_ID = 0x1996;
	public const int OUTPUT_LIMITER = 0x199A;
	public const int DEVICE_CLOCK = 0x1600;
	public const int DEVICE_LATENCY = 0x1601;
	public const int DEVICE_CLOCK_LATENCY = 0x1602;
	public const int AMBISONIC_LAYOUT = 0x1997;
	public const int AMBISONIC_SCALING = 0x1998;
	public const int AMBISONIC_ORDER = 0x1999;
	public const int MAX_AMBISONIC_ORDER = 0x199B;
	public const int BFORMAT3D = 0x1507;
	public const int FUMA = 0x0000;
	public const int ACN = 0x0001;
	public const int SN3D = 0x0001;
	public const int N3D = 0x0002;
}
