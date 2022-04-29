using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// An <see cref="AudioSource"/> whose data store comes from a single <see cref="AudioBuffer"/> containing the entirety
/// of the data to be played.
/// </summary>
[PublicAPI]
public class StaticSource : AudioSource
{
    /// <summary>
    /// Gets or sets the <see cref="AudioBuffer"/> providing data to this <see cref="StaticSource"/>.
    /// </summary>
    public AudioBuffer? Buffer
    {
        get
        {
            var id = AL.GetSourceI(Handle, SourceProperty.Buffer);
            if (id == 0)
                return null;
            var buffer = Unsafe.As<int, Buffer>(ref id);
            return new AudioBuffer(buffer);
        }
        set => AL.SourceI(Handle, SourceProperty.Buffer, value?.Handle ?? default);
    }

    /// <summary>
    /// Gets or sets a flag indicating if the audio source will automatically replay the data once it has reached the
    /// end of the <see cref="Buffer"/>.
    /// </summary>
    public bool Looping
    {
        get => AL.GetSourceB(Handle, SourceProperty.Looping);
        set => AL.SourceB(Handle, SourceProperty.Looping, value);
    }
	
    /// <summary>
    /// Creates a new <see cref="Source"/> and wraps it as a <see cref="StaticSource"/> instance.
    /// </summary>
    public StaticSource() : base(AL.GenSource())
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="Source"/> and wraps it as a <see cref="StaticSource"/> instance.
    /// </summary>
    /// <param name="buffer">A <see cref="AudioBuffer"/> to feed this <see cref="AudioSource"/>.</param>
    public StaticSource(AudioBuffer buffer) : base(AL.GenSource())
    {
        AL.SourceI(Handle, SourceProperty.Buffer, buffer.Handle);
    }
}