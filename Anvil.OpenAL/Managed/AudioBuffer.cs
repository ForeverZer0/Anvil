using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// Object-oriented wrapper encapsulating an OpenAL <see cref="Buffer"/> object.
/// </summary>
[PublicAPI]
public class AudioBuffer : AudioHandle<Buffer>
{
    /// <summary>
    /// Generates multiple <see cref="AudioBuffer"/> instances simultaneously.
    /// <para/>
    /// When multiple buffers are required, this method is slightly more efficient than creating them individually, as
    /// it can be performed with a single API invocation.
    /// </summary>
    /// <param name="count">The number of buffers to create. Must be a positive number.</param>
    /// <returns>An array containing the newly created <see cref="AudioBuffer"/> objects.</returns>
    public static AudioBuffer[] Generate(int count)
    {
        var buffers = new Buffer[count];
        AL.GenBuffers(buffers);
        return buffers.Select(buffer => new AudioBuffer(buffer)).ToArray();
    }
    
    /// <summary>
    /// Gets the frequency (samplerate) of the data in this <see cref="AudioBuffer"/>.
    /// </summary>
    public int Frequency => AL.GetBufferI(Handle, BufferProperty.Frequency);
    
    /// <summary>
    /// Gets the bits per sample of the data in this <see cref="AudioBuffer"/>.
    /// </summary>
    public int Bits => AL.GetBufferI(Handle, BufferProperty.Bits);
    
    /// <summary>
    /// Gets the channel count of the data in this <see cref="AudioBuffer"/>/
    /// </summary>
    public int Channels => AL.GetBufferI(Handle, BufferProperty.Channels);
    
    /// <summary>
    /// Gets the size of the data in this <see cref="AudioBuffer"/> in bytes.
    /// </summary>
    public int Size => AL.GetBufferI(Handle, BufferProperty.Size);
    
    /// <summary>
    /// Creates a new <see cref="Buffer"/> and wraps it as a <see cref="AudioBuffer"/> instance.
    /// </summary>
    public AudioBuffer() : base(AL.GenBuffer())
    {
    }

    /// <inheritdoc />
    internal AudioBuffer(Buffer handle) : base(handle)
    {
    }

    /// <summary>
    /// Fills the buffer with PCM <paramref name="data"/> in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/>.
    /// </summary>
    /// <param name="data">A buffer containing the samples.</param>
    /// <param name="format">The format of the <paramref name="data"/>.</param>
    /// <param name="frequency">The frequency/samplerate of the <paramref name="data"/>.</param>
    /// <typeparam name="T">An unmanaged primitive type.</typeparam>
    public void Data<T>(ReadOnlySpan<T> data, AudioFormat format, int frequency) where T : unmanaged
    {
        AL.BufferData(Handle, format, data, frequency);
    }
    
    /// <summary>
    /// Fills the buffer with PCM <paramref name="data"/> in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/>.
    /// </summary>
    /// <param name="data">A buffer containing the samples.</param>
    /// <param name="format">The format of the <paramref name="data"/>.</param>
    /// <param name="frequency">The frequency/samplerate of the <paramref name="data"/>.</param>
    /// <typeparam name="T">An unmanaged primitive type.</typeparam>
    public void Data<T>(T[] data, AudioFormat format, int frequency) where T : unmanaged
    {
        AL.BufferData(Handle, format, data, 0, data.Length, frequency);
    }
    
    /// <summary>
    /// Fills the buffer with PCM <paramref name="data"/> in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/>.
    /// </summary>
    /// <param name="data">A buffer containing the samples.</param>
    /// <param name="start">The index of the first element in the <paramref name="data"/> to begin copying.</param>
    /// <param name="length">The number of elements <paramref name="data"/> that should be copied.</param>
    /// <param name="format">The format of the <paramref name="data"/>.</param>
    /// <param name="frequency">The frequency/samplerate of the <paramref name="data"/>.</param>
    /// <typeparam name="T">An unmanaged primitive type.</typeparam>
    public void Data<T>(T[] data, int start, int length, AudioFormat format, int frequency) where T : unmanaged
    {
        AL.BufferData(Handle, format, data, start, length, frequency);
    }
    
    /// <summary>
    /// Fills the buffer with PCM <paramref name="data"/> in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/>.
    /// </summary>
    /// <param name="data">A buffer containing the samples.</param>
    /// <param name="dataSize">The number of bytes contained in the <paramref name="data"/>.</param>
    /// <param name="format">The format of the <paramref name="data"/>.</param>
    /// <param name="frequency">The frequency/samplerate of the <paramref name="data"/>.</param>
    public void Data(IntPtr data, int dataSize, AudioFormat format, int frequency)
    {
        AL.BufferData(Handle, format, data, dataSize, frequency);
    }

    /// <summary>
    /// Fills the buffer with PCM data in the specified <paramref name="format"/> and <paramref name="frequency"/> that
    /// is read from a <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream">A <see cref="Stream"/> object containing PCM samples.</param>
    /// <param name="length">The number of bytes to read from the <paramref name="stream"/>.</param>
    /// <param name="format">The format of the data that the <paramref name="stream"/> contains.</param>
    /// <param name="frequency">The frequency/samplerate of the data in the <paramref name="stream"/>.</param>
    /// <returns>The number of bytes actually read from the stream.</returns>
    /// <remarks>
    /// A temporary buffer is is created, used, and discarded each time this overload is used. This behavior makes it
    /// largely inefficient for streaming sources, where a user-defined buffer can be allocated once and reused with
    /// each read operation.
    /// <para/>
    /// For non-streaming sources, where it is desirable to just fill the buffer with the entire contents of a
    /// file/stream, this overload is useful in simplifying that process.
    /// </remarks>
    public int Data(Stream stream, int length, AudioFormat format, int frequency)
    {
        var buffer = new byte[length];
        length = stream.Read(buffer, 0, length);
        AL.BufferData(Handle, format, buffer, 0, length, frequency);
        return length;
    }
    
    /// <summary>
    /// Asynchronously fills the buffer with PCM <paramref name="data"/> in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/>.
    /// </summary>
    /// <param name="data">A buffer containing the samples.</param>
    /// <param name="format">The format of the <paramref name="data"/>.</param>
    /// <param name="frequency">The frequency/samplerate of the <paramref name="data"/>.</param>
    /// <typeparam name="T">An unmanaged primitive type.</typeparam>
    public async Task DataAsync<T>(T[] data, AudioFormat format, int frequency) where T : unmanaged
    {
        await Task.Run(() => AL.BufferData(Handle, format, data, 0, data.Length, frequency));
    }
    
    /// <summary>
    /// Asynchronously fills the buffer with PCM <paramref name="data"/> in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/>.
    /// </summary>
    /// <param name="data">A buffer containing the samples.</param>
    /// <param name="start">The index of the first element in the <paramref name="data"/> to begin copying.</param>
    /// <param name="length">The number of elements <paramref name="data"/> that should be copied.</param>
    /// <param name="format">The format of the <paramref name="data"/>.</param>
    /// <param name="frequency">The frequency/samplerate of the <paramref name="data"/>.</param>
    /// <typeparam name="T">An unmanaged primitive type.</typeparam>
    public async Task DataAsync<T>(T[] data, int start, int length, AudioFormat format, int frequency) where T : unmanaged
    {
        await Task.Run(() => AL.BufferData(Handle, format, data, start, length, frequency));
    }
    
    /// <summary>
    /// Asynchronously fills the buffer with PCM <paramref name="data"/> in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/>.
    /// </summary>
    /// <param name="data">A buffer containing the samples.</param>
    /// <param name="dataSize">The number of bytes contained in the <paramref name="data"/>.</param>
    /// <param name="format">The format of the <paramref name="data"/>.</param>
    /// <param name="frequency">The frequency/samplerate of the <paramref name="data"/>.</param>
    public async Task DataAsync(IntPtr data, int dataSize, AudioFormat format, int frequency)
    {
        await Task.Run(() => AL.BufferData(Handle, format, data, dataSize, frequency));
    }
    
    /// <summary>
    /// Asynchronously fills the buffer with PCM data in the specified <paramref name="format"/> and
    /// <paramref name="frequency"/> that is read from a <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream">A <see cref="Stream"/> object containing PCM samples.</param>
    /// <param name="length">The number of bytes to read from the <paramref name="stream"/>.</param>
    /// <param name="format">The format of the data that the <paramref name="stream"/> contains.</param>
    /// <param name="frequency">The frequency/samplerate of the data in the <paramref name="stream"/>.</param>
    /// <returns>The number of bytes actually read from the stream.</returns>
    /// <remarks>
    /// A temporary buffer is is created, used, and discarded each time this overload is used. This behavior makes it
    /// largely inefficient for streaming sources, where a user-defined buffer can be allocated once and reused with
    /// each read operation.
    /// <para/>
    /// For non-streaming sources, where it is desirable to just fill the buffer with the entire contents of a
    /// file/stream, this overload is useful in simplifying that process.
    /// </remarks>
    public async Task<int> DataAsync(Stream stream, int length, AudioFormat format, int frequency)
    {
        var buffer = new byte[length];
        length = await stream.ReadAsync(buffer, 0, length);
        await Task.Run(() => AL.BufferData(Handle, format, buffer, 0, length, frequency));
        return length;
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            AL.DeleteBuffer(Handle);
    }

    /// <inheritdoc />
    public override bool IsValid => AL.IsBuffer(Handle);
}