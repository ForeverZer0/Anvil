using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Describes the supported PCM sample types for reading/writing data.
/// </summary>
[PublicAPI]
public enum SampleType
{
    /// <summary>
    /// 16-bit signed integer.
    /// </summary>
    Short,
	
    /// <summary>
    /// 32-bit signed integer.
    /// </summary>
    Integer,
	
    /// <summary>
    /// Single precision 32-bit IEEE Standard 754 floating point number.
    /// </summary>
    Float,
	
    /// <summary>
    /// Double precision 64-bit IEEE Standard 754 floating point number.
    /// </summary>
    Double
}