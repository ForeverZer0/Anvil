using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Describes the endian used in an audio-sources data.
/// </summary>
[PublicAPI]
public enum SoundEndian
{
	/// <summary>
	/// Default file endian-ness.
	/// </summary>
	Default = 0x00000000,

	/// <summary>
	/// Force little endian-ness.
	/// </summary>
	Little = 0x10000000,

	/// <summary>
	/// Force big endian-ness.
	/// </summary>
	Big = 0x20000000,

	/// <summary>
	/// Force CPU endian-ness.
	/// </summary>
	Cpu = 0x30000000
}