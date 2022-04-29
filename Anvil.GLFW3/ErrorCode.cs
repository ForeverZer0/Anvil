using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the error state of the GLFW library.
/// </summary>
[PublicAPI]
public enum ErrorCode
{
    /// <summary>
    /// No error is present.
    /// </summary>
    None = 0,
	
    /// <summary>
    /// An attempt was made to use a function that requires the library first be initialized.
    /// </summary>
    /// <seealso cref="GLFW.Init"/>
    NotInitialized = 0x00010001,
	
    /// <summary>
    /// An attempt was made to use an OpenGL-related feature without first making it current on the calling thread.
    /// </summary>
    /// <seealso cref="GLFW.MakeContextCurrent"/>
    NoCurrentContext = 0x00010002,
	
    /// <summary>
    /// An invalid enumeration was specified as a parameter.
    /// </summary>
    InvalidEnum = 0x00010003,
	
    /// <summary>
    /// An invalid value was specified for a function.
    /// </summary>
    InvalidValue = 0x00010004,
	
    /// <summary>
    /// A memory allocation failed.
    /// </summary>
    OutOfMemory = 0x00010005,
	
    /// <summary>
    /// The requested API is not available on the platform.
    /// </summary>
    ApiUnavailable = 0x00010006,
	
    /// <summary>
    /// The requested OpenGL version is not available on the platform.
    /// </summary>
    VersionUnavailable = 0x00010007,
	
    /// <summary>
    /// A platform-specific error occurred.
    /// </summary>
    PlatformError = 0x00010008,
	
    /// <summary>
    /// A configuration or format was specified that is not supported by the system.
    /// </summary>
    FormatUnavailable = 0x00010009,
	
    /// <summary>
    /// No window context is present for an operation that requires it.
    /// </summary>
    NoWindowContext = 0x0001000A
}