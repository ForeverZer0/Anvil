using System.Runtime.InteropServices;

namespace Anvil.SndFile;

/// <summary>
/// Structure for describing the format of an audio file.
/// </summary>
/// <remarks>
/// When opening in read-mode, a default instance should be passed in to be filled out by the library with the relevant
/// data.
/// <para/>
/// When opening in write-mode, the format, type, endian, channels, and frequency need to be supplied with a valid
/// configuration to notify the library how data should be written.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct SoundInfo
{
	private long samples;	
    private int	samplerate;
    private int	channels;
    private int	format;
    private int	sections;
    private int	seekable;

    /// <summary>
    /// Gets the number of channels in the audio source.
    /// </summary>
    public int Channels => channels;

    /// <summary>
    /// Gets the number of samples in the audio source.
    /// </summary>
    public long SampleCount => samples;

    /// <summary>
    /// Gets the number of samples per second (samplerate) the audio source consumes.
    /// </summary>
    public int Frequency => samplerate;

    /// <summary>
    /// Gets a value describing the major/container format of the audio source.
    /// </summary>
    public SoundFormat Format => (SoundFormat) (format & SF.FORMAT_TYPEMASK);

    /// <summary>
    /// Gets a value describing the format of the data.
    /// </summary>
    public SoundType Type => (SoundType) (format & SF.FORMAT_SUBMASK);

    /// <summary>
    /// Gets the endian of the audio data.
    /// </summary>
    public SoundEndian Endian => (SoundEndian) (format & SF.FORMAT_ENDMASK);

    /// <summary>
    /// Gets a flag indicating if the position cursor can be moved freely within the audio source.
    /// </summary>
    public bool CanSeek => seekable != 0;

    /// <summary>
    /// Creates a new instance of a <see cref="SoundInfo"/> object to be used as a configuration when opening a
    /// <see cref="SoundFile"/> in write-mode.
    /// </summary>
    /// <param name="samplerate">The desired frequency of the audio samples.</param>
    /// <param name="channels">The desired number of audio channels.</param>
    /// <param name="format">The desired format.</param>
    /// <param name="type">The desired sample type.</param>
    /// <param name="endian">The desired endian of the audio data.</param>
    public SoundInfo(int samplerate, int channels, SoundFormat format, SoundType type, SoundEndian endian) : this()
    {
	    this.samplerate = samplerate;
	    this.channels = channels;
	    this.format = (int) format | (int) type | (int) endian;
    }
}