using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Strongly-typed constants describing different command types.
/// </summary>
/// <remarks>
/// For more information on the command interface and what parameters are expected, see official <c>libsndfile</c>
/// documentation.
/// </remarks>
/// <seealso href="http://www.mega-nerd.com/libsndfile/command.html"/>
[PublicAPI]
public enum CommandType
{
	/// <summary>
	/// Retrieve the version of the library.
	/// </summary>
	GetLibVersion = 0x1000,
	
	/// <summary>
	/// Retrieve the internal per-file operation log.
	/// </summary>
	GetLogInfo = 0x1001,
	
	/// <summary>
	/// Retrieve the current <see cref="SoundInfo"/> structure for the file.
	/// </summary>
	GetCurrentSoundInfo = 0x1002,
	
	/// <summary>
	/// Retrieve the current normalisation behaviour of the double precision floating point reading and writing
	/// functions.
	/// </summary>
	GetNormDouble = 0x1010,
	
	/// <summary>
	/// Retrieve the current normalisation behaviour of the floating point reading and writing functions.
	/// </summary>
	GetNormFloat = 0x1011,
	
	/// <summary>
	/// Modify the normalisation behaviour of the double precision floating point reading and writing functions.
	/// </summary>
	SetNormDouble = 0x1012,
	
	/// <summary>
	/// Modify the normalisation behaviour of the floating point reading and writing functions.
	/// </summary>
	SetNormFloat = 0x1013,
	
	/// <summary>
	/// Set/clear the scale factor when integer (short/int) data is read from a file containing floating point data.
	/// </summary>
	SetScaleFloatIntRead = 0x1014,
	
	/// <summary>
	/// Set/clear the scale factor when integer (short/int) data is written to a file as floating point data.
	/// </summary>
	SetScaleIntFloatWrite = 0x1015,
	
	/// <summary>
	/// Retrieve the number of simple formats supported by <c>libsndfile</c>.
	/// </summary>
	GetSimpleFormatCount = 0x1020,
	
	/// <summary>
	/// Retrieve information about a simple format.
	/// </summary>
	GetSimpleFormat = 0x1021,
	
	/// <summary>
	/// Retrieve information about a major or subtype format.
	/// </summary>
	GetFormatInfo = 0x1028,
	
	/// <summary>
	/// Retrieve the number of major formats.
	/// </summary>
	GetFormatMajorCount = 0x1030,
	
	/// <summary>
	/// Retrieve information about a major format type.
	/// </summary>
	GetFormatMajor = 0x1031,
	
	/// <summary>
	/// Retrieve the number of sub-formats.
	/// </summary>
	GetFormatSubtypeCount = 0x1032,
	
	/// <summary>
	/// Retrieve information about a sub-format.
	/// </summary>
	GetFormatSubtype = 0x1033,
	
	/// <summary>
	/// Retrieve the measured maximum signal value. This involves reading through the whole file which can be slow on
	/// large files.
	/// </summary>
	CalcSignalMax = 0x1040,
	
	/// <summary>
	/// Retrieve the measured normalised maximum signal value. This involves reading through the whole file which can be
	/// slow on large files.
	/// </summary>
	CalcNormSignalMax = 0x1041,
	
	/// <summary>
	/// Calculate the peak value (ie a single number) for each channel. This involves reading through the whole file
	/// which can be slow on large files.
	/// </summary>
	CalcMaxAllChannels = 0x1042,
	
	/// <summary>
	/// Calculate the normalised peak for each channel. This involves reading through the whole file which can be slow
	/// on large files.
	/// </summary>
	CalcNormMaxAllChannels = 0x1043,
	
	/// <summary>
	/// Retrieve the peak value for the file as stored in the file header.
	/// </summary>
	GetSignalMax = 0x1044,
	
	/// <summary>
	/// Retrieve the peak value for the file as stored in the file header.
	/// </summary>
	GetMaxAllChannels = 0x1045,
	
	/// <summary>
	/// Switch the code for adding the PEAK chunk to WAV and AIFF files on or off.
	/// </summary>
	SetAddPeakChunk = 0x1050,
	
	/// <summary>
	/// Used when a file is open for write, this command will update the file header to reflect the data written so far.
	/// </summary>
	UpdateHeaderNow = 0x1060,
	
	/// <summary>
	/// Used when a file is open for write, this command will cause the file header to be updated after each write to
	/// the file.
	/// </summary>
	SetUpdateHeaderAuto = 0x1061,
	
	/// <summary>
	/// Truncate a file open for write or for read/write.
	/// </summary>
	FileTruncate = 0x1080,
	
	/// <summary>
	/// Change the data start offset for files opened up as <see cref="SoundFormat.Raw"/>.
	/// </summary>
	SetRawStartOffset = 0x1090,
	
	/// <summary>
	/// Retrieve information about audio files embedded inside other files.
	/// </summary>
	GetEmbedFileInfo = 0x10B0,
	
	/// <summary>
	/// Turn on/off automatic clipping when doing floating point to integer conversion.
	/// </summary>
	SetClipping = 0x10C0,
	
	/// <summary>
	/// Retrieve current clipping setting.
	/// </summary>
	GetClipping = 0x10C1,

	/// <summary>
	/// Get the cue marker count.
	/// </summary>
	GetCueCount = 0x10CD,
	
	/// <summary>
	/// Get cue marker info.
	/// </summary>
	GetCue = 0x10CE,
	
	/// <summary>
	/// Set cue marker info.
	/// </summary>
	SetCue = 0x10CF,

	/// <summary>
	/// Retrieve instrument information from file including MIDI base note, keyboard mapping and looping
	/// information (start/stop and mode).
	/// </summary>
	GetInstrument = 0x10D0,
	
	/// <summary>
	/// Set the instrument information for the file.
	/// </summary>
	SetInstrument = 0x10D1,

	/// <summary>
	/// Get loop info.
	/// </summary>
	GetLoopInfo = 0x10E0,

	/// <summary>
	/// Retrieve the Broadcast Extension Chunk from WAV (and related) files.
	/// </summary>
	GetBroadcastInfo = 0x10F0,
	
	/// <summary>
	/// Set the Broadcast Extension Chunk for WAV (and related) files.
	/// </summary>
	SetBroadcastInfo = 0x10F1,
	
	/// <summary>
	/// Determine if raw data needs endian-swapping.
	/// </summary>
	RawDataNeedsEndswap = 0x1110,

	/// <summary>
	/// Modify a WAVEX header for Ambisonic format
	/// </summary>
	WavexSetAmbisonic = 0x1200,
	
	/// <summary>
	/// Test a WAVEX file for Ambisonic format.
	/// </summary>
	WavexGetAmbisonic = 0x1201,
	
	/// <summary>
	/// Enable auto downgrade from RF64 to WAV
	/// </summary>
	/// <remarks>
	/// RF64 files can be set so that on-close, writable files that have less than 4GB of data in them are converted to
	/// RIFF/WAV, as per EBU recommendations.
	/// </remarks>
	Rf64AutoDowngrade = 0x1210,

	/// <summary>
	/// Set the Variable Bit Rate encoding quality.
	/// </summary>
	SetVbrEncodingQuality = 0x1300,
	
	/// <summary>
	/// Set the compression level.
	/// </summary>
	SetCompressionLevel = 0x1301,

	/// <summary>
	/// Set the Cart Chunk info.
	/// </summary>
	SetCartInfo = 0x1400,
	
	/// <summary>
	/// Retrieve the Cart Chunk info.
	/// </summary>
	GetCartInfo = 0x1401,
}