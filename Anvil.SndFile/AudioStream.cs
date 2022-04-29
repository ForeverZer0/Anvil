using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Provides a <see cref="Stream"/> for audio, supporting both synchronous and asynchronous read and write operations.
/// </summary>
/// <remarks>
/// This class provides a higher-level abstraction over native <c>libsndfile</c> functions to interface with audio files
/// as a standard <see cref="Stream"/> object, which is suitable for most needs. While it is not as feature-complete as
/// using the <see cref="SF"/> class directly, it provides the basic function of "easily read/write decoded samples into
/// an arbitrary buffer", which covers the majority of use-cases.
/// </remarks>
[PublicAPI]
public class AudioStream : Stream
{
    private readonly SoundFile file;
    private readonly SoundInfo info;
    private readonly int virtualBytesPerFrame;

    /// <summary>
    /// Gets a value describing the format the PCM data that will be supplied during read/write operations.
    /// <para/>
    /// This value does not reflect the internal encoding of the audio stream.
    /// </summary>
    public SampleType SampleType { get; }

    /// <summary>
    /// Gets a value describing the major/container format of the audio source.
    /// </summary>
    public SoundFormat Format => info.Format;

    /// <summary>
    /// Gets a value describing the format of the data.
    /// </summary>
    public SoundType Type => info.Type;

    /// <summary>
    /// Gets the number of channels in the audio stream.
    /// </summary>
    public int Channels => info.Channels;

    /// <summary>
    /// Gets the samplerate of the audio stream.
    /// </summary>
    public int Frequency => info.Frequency;

    /// <summary>
    /// Gets the total number of samples in the audio source.
    /// </summary>
    public long SampleCount => info.SampleCount;

    /// <summary>
    /// Gets or sets a metadata string for this audio stream.
    /// </summary>
    /// <param name="type">A constant describing the string to set.</param>
    /// <exception cref="NotSupportedException">
    /// When trying to retrieve a value when the stream is not opened for reading, or when trying to set a string when
    /// stream is not opened for writing.
    /// </exception>
    public string? this[StringType type]
    {
        get
        {
            if (!CanRead)
                throw new NotSupportedException("Stream is not opened for reading.");
            return SF.GetString(file, type);
        }
        set
        {
            if (!CanWrite)
                throw new NotSupportedException("Stream is not opened for writing.");
            SF.SetString(file, type, value ?? string.Empty);
        }
    }

    private AudioStream(SoundFile soundFile, SoundInfo soundInfo, SampleType sampleType, FileAccess access)
    {
        CheckErrorState(soundFile);

        file = soundFile;
        info = soundInfo;
        SampleType = sampleType;
        CanRead = access.HasFlag(FileAccess.Read);
        CanWrite = access.HasFlag(FileAccess.Write);
        CanSeek = soundInfo.CanSeek;

        virtualBytesPerFrame = info.Channels * sampleType switch
        {
            SampleType.Short => sizeof(short),
            SampleType.Integer => sizeof(int),
            SampleType.Float => sizeof(float),
            SampleType.Double => sizeof(double),
            _ => throw new ArgumentOutOfRangeException(nameof(sampleType), sampleType, null)
        };
    }

    private static void CheckErrorState(SoundFile file)
    {
        var error = SF.Error(file);
        switch (error)
        {
            case SoundError.None: return;
            case SoundError.UnrecognisedFormat:  throw new FormatException("Unrecognized audio format.");
            case SoundError.System: throw new SystemException("A system error occurred.");
            case SoundError.MalformedFile: throw new FormatException("Malformed audio data.");
            case SoundError.UnsupportedEncoding: throw new NotSupportedException("Unsupported encoding.");
            default:
                var msg = SF.ErrorNumber(error);
                throw new InvalidDataException($"The file handle is invalid {msg}");
        }
    }

    /// <summary>
    /// Opens an existing audio file for reading.
    /// </summary>
    /// <param name="path">A path to the audio file to be read.</param>
    /// <param name="dataType">
    /// The desired PCM format to decode the audio samples into. This does not need to match the source encoding of the
    /// file.
    /// </param>
    /// <returns>A read-only <see cref="AudioStream"/> on the specified <paramref name="path"/>.</returns>
    public static AudioStream OpenRead(string path, SampleType dataType)
    {
        SoundInfo info = default;
        var file = SF.Open(path, FileAccess.Read, ref info) ?? throw new InvalidOperationException("Failed to create audio handle.");
        return new AudioStream(file, info, dataType, FileAccess.Read);
    }

    /// <summary>
    /// Opens an existing or creates a new audio file for writing.
    /// </summary>
    /// <param name="path">The path to the file to write to. A new file will be created if it does not exist.</param>
    /// <param name="dataType">
    /// The desired PCM format the data to encode will be supplied as. This does not need to match the source encoding
    /// of the target format.
    /// </param>
    /// <param name="channels">The desired number of channels in the sound.</param>
    /// <param name="frequency">The desired frequency (samplerate) of the sound.</param>
    /// <param name="format">The desired major/container format for the sound.</param>
    /// <param name="type">The desired sub-type of the sound.</param>
    /// <param name="endian">The desired endian to write the data as.</param>
    /// <returns>A write-only stream on the specified <paramref name="path"/>.</returns>
    /// <exception cref="FormatException">The specified configuration is not supported.</exception>
    public static AudioStream OpenWrite(string path, SampleType dataType, int channels, int frequency, SoundFormat format, SoundType type, SoundEndian endian = SoundEndian.Default)
    {
        var info = new SoundInfo(frequency, channels, format, type, endian);
        if (!SF.FormatCheck(info))
            throw new FormatException("The specified format is not supported.");

        var file = SF.Open(path, FileAccess.Write, ref info) ?? throw new InvalidOperationException("Failed to create audio handle.");
        return new AudioStream(file, info, dataType, FileAccess.Write);
    }

    /// <summary>
    /// Convenience method to create a write-only file in FLAC format.
    /// </summary>
    /// <param name="path">The path to the file to write to. A new file will be created if it does not exist.</param>
    /// <param name="dataType">
    /// The desired PCM format the data to encode will be supplied as. This does not need to match the source encoding
    /// of the target format.
    /// </param>
    /// <param name="channels">The desired number of channels in the sound.</param>
    /// <param name="frequency">The desired frequency (samplerate) of the sound.</param>
    /// <returns>A write-only stream on the specified <paramref name="path"/> for encoding in FLAC format.</returns>
    public static AudioStream OpenWriteFlac(string path, SampleType dataType, int channels = 2, int frequency = 44100)
    {
        return OpenWrite(path, dataType, channels, frequency, SoundFormat.Flac, SoundType.Pcm16);
    }
    
    /// <summary>
    /// Convenience method to create a write-only file in OGG/Vorbis format.
    /// </summary>
    /// <param name="path">The path to the file to write to. A new file will be created if it does not exist.</param>
    /// <param name="dataType">
    /// The desired PCM format the data to encode will be supplied as. This does not need to match the source encoding
    /// of the target format.
    /// </param>
    /// <param name="channels">The desired number of channels in the sound.</param>
    /// <param name="frequency">The desired frequency (samplerate) of the sound.</param>
    /// <returns>A write-only stream on the specified <paramref name="path"/> for encoding in Vorbis format.</returns>
    public static AudioStream OpenWriteVorbis(string path, SampleType dataType, int channels = 2, int frequency = 44100)
    {
        return OpenWrite(path, dataType, channels, frequency, SoundFormat.Ogg, SoundType.Vorbis);
    }
    
    /// <summary>
    /// Retrieves a metadata string from the specified audio stream.
    /// </summary>
    /// <param name="type">The type of string to retrieve.</param>
    /// <returns>The value of the string, or <c>null</c> if none is present of the specified <paramref name="type"/>.</returns>
    public string? GetString(StringType type) => SF.GetString(file, type);
    
    /// <summary>
    /// Sets a metadata string within the specified audio stream.
    /// </summary>
    /// <param name="type">The type of string to set.</param>
    /// <param name="value">The value to apply.</param>
    /// <returns><c>true</c> on success, otherwise <c>false</c> if an error occurred.</returns>
    public bool SetString(StringType type, string value) => SF.SetString(file, type, value);

    /// <inheritdoc />
    public override void Flush() => SF.Flush(file);

    /// <inheritdoc />
    /// <remarks>
    /// The granularity of the seek position is the size of a multi-channel audio frame. For example, a 2-channel source
    /// with 32-bit float samples has a "frame size" of 8-bytes. Attempting to seek to byte offset of <c>19</c> will
    /// move the position to the lowest whole frame, resulting in actually seeking to a byte offset of <c>16</c>.
    /// </remarks>
    public override long Seek(long offset, SeekOrigin origin)
    {
        if (!CanSeek)
            throw new NotSupportedException("Stream does not support seeking.");
        return SF.Seek(file, offset / virtualBytesPerFrame, origin) * virtualBytesPerFrame;
    }

    /// <inheritdoc />
    public override void SetLength(long value)
    {
        throw new NotSupportedException("The stream does not support resizing.");
    }
    
    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        if (!CanRead)
            throw new NotSupportedException("Stream is not opened for reading.");
        
        unsafe
        {
            fixed (void* ptr = &buffer[offset])
            {
                var n = SF.ReadFrames(file, ptr, count / virtualBytesPerFrame, SampleType);
                return (int) n * virtualBytesPerFrame;
            }
        }
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        if (!CanWrite)
            throw new NotSupportedException("Stream is not opened for writing.");

        unsafe
        {
            fixed (void* ptr = &buffer[offset])
            {
                SF.WriteFrames(file, ptr, count / virtualBytesPerFrame, SampleType);
            }
        }
    }

    /// <inheritdoc />
    public override bool CanRead { get; }

    /// <inheritdoc />
    public override bool CanSeek { get; }

    /// <inheritdoc />
    public override bool CanWrite { get; }

    /// <inheritdoc />
    public override long Length => info.SampleCount * virtualBytesPerFrame;

    /// <inheritdoc />
    /// <remarks>
    /// The granularity of the seek position is the size of a multi-channel audio frame. For example, a 2-channel source
    /// with 32-bit float samples has a "frame size" of 8-bytes. Attempting to seek to byte offset of <c>19</c> will
    /// move the position to the lowest whole frame, resulting in actually seeking to a byte offset of <c>16</c>.
    /// </remarks>
    public override long Position
    {
        get
        {
            if (!CanSeek)
                throw new NotSupportedException("Stream does not support seeking.");
            return SF.Seek(file, 0, SeekOrigin.Current) * virtualBytesPerFrame;
        }
        set
        {
            if (!CanSeek)
                throw new NotSupportedException("Stream does not support seeking.");
            SF.Seek(file, value / virtualBytesPerFrame, SeekOrigin.Begin);
        }
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        SF.Flush(file);
        SF.Close(file);
        base.Dispose(disposing);
    }
}