using JetBrains.Annotations;

namespace Anvil.OpenAL;

	




[PublicAPI]
public enum ContextError
{
    None = 0,
    InvalidDevice = 0xA001,
    InvalidContext = 0xA002,
    InvalidEnum = 0xA003,
    InvalidValue = 0xA004,
    OutOfMemory = 0xA005
}

[PublicAPI]
public enum Error
{
    None = 0,
    InvalidName = 0xA001,
    InvalidEnum = 0xA002,
    InvalidValue = 0xA003,
    InvalidOperation = 0xA004,
    OutOfMemory = 0xA005
}

[PublicAPI]
public enum DistanceModel
{
    Inverse = 0xD001,
    InverseClamped = 0xD002,
    Linear = 0xD003,
    LinearClamped = 0xD004,
    Exponent = 0xD005,
    ExponentClamped = 0xD006
}

[PublicAPI]
public enum SourceState
{
    Initial = 0x1011,
    Playing = 0x1012,
    Paused = 0x1013,
    Stopped = 0x1014,
}

[PublicAPI]
public enum AudioFormat
{
    Mono8 = 0x1100,
    Mono16 = 0x1101,
    Stereo8 = 0x1102,
    Stereo16 = 0x1103,
    ImaAdpcmMono16 = 0x10000,
    ImaAdpcmStereo16 = 0x10001,
    Wave = 0x10002,
    Vorbis = 0x10003,
    Quad8Loki = 0x10004,
    Quad16Loki = 0x10005,
    MonoFloat32 = 0x10010,
    StereoFloat32 = 0x10011,
    MonoDouble = 0x10012,
    StereoDouble = 0x10013,
    MonoMulaw = 0x10014,
    StereoMulaw = 0x10015,
    MonoAlaw = 0x10016,
    StereoAlaw = 0x10017,
    Quad8 = 0x1204,
    Quad16 = 0x1205,
    Quad32 = 0x1206,
    Rear8 = 0x1207,
    Rear16 = 0x1208,
    Rear32 = 0x1209,
    FivePointOne8 = 0x120A,
    FivePointOne16 = 0x120B,
    FivePointOne32 = 0x120C,
    SizePointOne8 = 0x120D,
    SizePointOne16 = 0x120E,
    SizePointOne32 = 0x120F,
    SevenPointOne8 = 0x1210,
    SevenPointOne16 = 0x1211,
    SevenPointOne32 = 0x1212,
    QuadMulaw = 0x10021,
    RearMulaw = 0x10022,
    FivePointOneMulaw = 0x10023,
    SizePointOneMulaw = 0x10024,
    SevenPointOneMulaw = 0x10025,
    MonoIma4 = 0x1300,
    StereoIma4 = 0x1301,
}

[PublicAPI]
public enum BufferFormat
{
    Mono8 = 0x1100,
    Mono16 = 0x1101,
    Mono32F = 0x10010,
    Stereo8 = 0x1102,
    Stereo16 = 0x1103,
    Stereo32F = 0x10011,
    Quad8 = 0x1204,
    Quad16 = 0x1205,
    Quad32F = 0x1206,
    Rear8 = 0x1207,
    Rear16 = 0x1208,
    Rear32F = 0x1209,
    FivePointOne8 = 0x120A,
    FivePointOne16 = 0x120B,
    FivePointOne32F = 0x120C,
    SixPointOne8 = 0x120D,
    SixPointOne16 = 0x120E,
    SixPointOne32F = 0x120F,
    SevenPointOne8 = 0x1210,
    SevenPointOne16 = 0x1211,
    SevenPointOne32F = 0x1212,
}

[PublicAPI]
public enum BufferChannels
{
    Mono = 0x1500,
    Stereo = 0x1501,
    Quad = 0x1502,
    Rear = 0x1503,
    FivePointOne = 0x1504,
    SixPointOne = 0x1505,
    SevenPointOne = 0x1506,
}

[PublicAPI]
public enum SampleType
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    Short = 0x1402,
    UnsignedShort = 0x1403,
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Double = 0x1407,
    Byte3 = 0x1408,
    UnsignedByte3 = 0x1409,
}

[PublicAPI]
public enum FoldbackMode
{
    Mono = 0x4101,
    Stereo = 0x4102,
}
	
[PublicAPI]
public enum FoldbackEventType
{
    Block = 0x4112,
    Start = 0x4111,
    Stop = 0x4113,
}

public enum ContextAttribute
{
    Frequency = 0x1007,
    Refresh = 0x1008,
    Sync = 0x1009,
    MonoSources = 0x1010,
    StereoSources = 0x1011

}

public enum ContextProperty
{
    EfxMajorVersion = 0x20001,
    EfxMinorVersion = 0x20002,
    MaxAuxiliarySends = 0x20003,
    MajorVersion = 0x1000,
    MinorVersion = 0x1001,
    AttributesSize = 0x1002,
    AllAttributes = 0x1003,
    DefaultDeviceSpecifier = 0x1004,
    DeviceSpecifier = 0x1005,
    Extensions = 0x1006,
    CaptureDeviceSpecifier = 0x310,
    CaptureDefaultDeviceSpecifier = 0x311,
    CaptureSamples = 0x312,
    DefaultAllDevicesSpecifier = 0x1012,
    AllDevicesSpecifier = 0x1013
}