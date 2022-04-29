using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// An <see cref="AudioSource"/> whose data store comes from multiple buffers that are queued, processed, and
/// dequeued. These are typically implemented as a "ring buffer" or "circular buffer" where buffers are queued,
/// processed by the source, removed from the source, re-filled with new data, and then re-added to the source. This
/// allows for using a few small buffers that contain little data and can be processed quickly, opposed to a
/// <see cref="StaticSource"/> that contains a single large buffer with all data.
/// <para/>
/// If looping is required, it must be implemented by the data provider, as the source is only aware of a small
/// sub-portion of the total data at any given time.
/// </summary>
[PublicAPI]
public class StreamingSource : AudioSource
{
    /// <summary>
    /// Creates a new <see cref="Source"/> and wraps it as a <see cref="StreamingSource"/> instance.
    /// </summary>
    public StreamingSource() : base(AL.GenSource())
    {
    }
    
    /// <inheritdoc />
    protected StreamingSource(Source handle) : base(handle)
    {
    }

    /// <summary>
    /// Gets the number of <see cref="AudioBuffer"/> objects currently queued to play on this <see cref="StreamingSource"/>.
    /// </summary>
    public int BuffersQueued => AL.GetSourceI(Handle, SourceProperty.BuffersQueued);
	
    /// <summary>
    /// Gets the number of <see cref="AudioBuffer"/> objects that have been processed and are ready to be removed.
    /// </summary>
    public int BuffersProcessed => AL.GetSourceI(Handle, SourceProperty.BuffersProcessed);

    /// <summary>
    /// Detaches all buffers that have been processed and returns them in an enumerator.
    /// </summary>
    /// <returns>An enumerator that yields the processed buffers.</returns>
    /// <seealso cref="Dequeue"/>
    public IEnumerable<AudioBuffer> GetProcessed()
    {
        var count = AL.GetSourceI(Handle, SourceProperty.BuffersProcessed);
        if (count == 0)
            yield break;
		
        var buffers = new Buffer[count];
        AL.SourceUnqueueBuffers(Handle, buffers);

        foreach (var buffer in buffers)
            yield return new AudioBuffer(buffer);
    }
	
    /// <summary>
    /// Attaches the specified <paramref name="buffer"/> to the end of the queue feeding this
    /// <see cref="StreamingSource"/> with data.
    /// </summary>
    /// <param name="buffer">The buffer to attach.</param>
    /// <remarks>All buffers attached to a source should have the same data format.</remarks>
    public void Queue(AudioBuffer buffer) => AL.SourceQueueBuffer(Handle, buffer.Handle);
	
    /// <summary>
    /// Attaches the specified <paramref name="buffers"/> to the end of the queue feeding this
    /// <see cref="StreamingSource"/> with data.
    /// </summary>
    /// <param name="buffers">The buffers to attach.</param>
    /// <remarks>All buffers attached to a source should have the same data format.</remarks>
    public void Queue(IEnumerable<AudioBuffer> buffers)
    {
        foreach (var buffer in buffers)
            AL.SourceQueueBuffer(Handle, buffer.Handle);
    }
	
    /// <summary>
    /// Attaches the specified <paramref name="buffers"/> to the end of the queue feeding this
    /// <see cref="StreamingSource"/> with data.
    /// </summary>
    /// <param name="buffers">The buffers to attach.</param>
    /// <remarks>All buffers attached to a source should have the same data format.</remarks>
    public void Queue(params AudioBuffer[] buffers)
    {
        if (buffers.Length == 0)
            return;
		
        var handles = buffers.Select(b => b.Handle).ToArray();
        AL.SourceQueueBuffers(Handle, handles);
    }
	
    /// <summary>
    /// Dequeues the specified number of buffers that are attached to this <see cref="StreamingSource"/>.
    /// </summary>
    /// <param name="count">The number of buffers to detach, or <c>-1</c> to detach all buffers.</param>
    /// <returns>An enumerator that yields each of the detached buffers.</returns>
    /// <seealso cref="GetProcessed"/>
    public IEnumerable<AudioBuffer> Dequeue(int count = -1)
    {
        if (count == 0)
            yield break;

        if (count < 0)
        {
            AL.SourceI(Handle, SourceProperty.Buffer, 0);
            yield break;
        }
		
        var buffers = new Buffer[count];
        AL.SourceUnqueueBuffers(Handle, buffers);

        foreach (var buffer in buffers)
            yield return new AudioBuffer(buffer);
    }
}