using JetBrains.Annotations;

namespace Anvil.Logging;

/// <summary>
/// Strongly-typed enumeration containing values for various logging levels.
/// <para>Can bitwise OR values together for multiple levels.</para>
/// </summary>
[Flags, PublicAPI]
public enum LogLevel : byte
{
    /// <summary>
    /// No logging.
    /// </summary>
    None = 0x00,
        
    /// <summary>
    /// An error that is incapable of being handled and is causing an application crash.
    /// </summary>
    Fatal = 0x01,
        
    /// <summary>
    /// An error that is capable of being handled to prevent a crash.
    /// </summary>
    Error = 0x02,
        
    /// <summary>
    /// A possible issue that may need corrected to prevent future errors.
    /// </summary>
    Warn = 0x04,
        
    /// <summary>
    /// General informational messages.
    /// </summary>
    Info = 0x08,
        
    /// <summary>
    /// Messages regarding internal state of the application for debugging purposed.
    /// </summary>
    Debug = 0x10,
        
    /// <summary>
    /// Verbose messages regarding internal state for profiling purposes.
    /// </summary>
    Trace = 0x20
}