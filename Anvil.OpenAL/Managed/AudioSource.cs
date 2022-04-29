using System.Numerics;
using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// Object-oriented wrapper encapsulating an OpenAL <see cref="Source"/> object.
/// </summary>
[PublicAPI]
public abstract class AudioSource : AudioHandle<Source>
{
    /// <summary>
    /// Creates a new <see cref="Source"/> and wraps it as a <see cref="AudioSource"/> instance.
    /// </summary>
    protected AudioSource() : this(AL.GenSource())
    {
    }
    
    /// <inheritdoc />
    protected AudioSource(Source handle) : base(handle)
    {
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            AL.DeleteSource(Handle);
    }

    /// <inheritdoc />
    public override bool IsValid => AL.IsSource(Handle);

    /// <summary>
    /// Begins playback of this <see cref="AudioSource"/>.
    /// </summary>
    public void Play() => AL.SourcePlay(Handle);
    
    /// <summary>
    /// Suspends playback of this <see cref="AudioSource"/>, maintaining its current position and state.
    /// </summary>
    /// <remarks>Use <see cref="Play()"/> to resume playback.</remarks>
    public void Pause() => AL.SourcePlay(Handle);
    
    /// <summary>
    /// Stops playback of ths <see cref="AudioSource"/>, resetting its state.
    /// </summary>
    public void Stop() => AL.SourcePlay(Handle);
    
    /// <summary>
    /// Returns the <see cref="AudioSource"/> back to an <see cref="SourceState.Initial"/> state.
    /// </summary>
    public void Rewind() => AL.SourcePlay(Handle);

    /// <summary>
    /// Begins playback of multiple <paramref name="sources"/>, simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to begin playback on.</param>
    public static void Play(params AudioSource[] sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());
    
    /// <summary>
    /// Suspends playback of multiple <paramref name="sources"/>, simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to suspend.</param>
    public static void Pause(params AudioSource[] sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());
    
    /// <summary>
    /// Stops playback of multiple <paramref name="sources"/>, simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to stop.</param>
    public static void Stop(params AudioSource[] sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());
    
    /// <summary>
    /// Rewinds of multiple <paramref name="sources"/> back to their <see cref="SourceState.Initial"/> state,
    /// simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to rewind.</param>
    public static void Rewind(params AudioSource[] sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());
    
    /// <summary>
    /// Begins playback of multiple <paramref name="sources"/>, simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to begin playback on.</param>
    public static void Play(IEnumerable<AudioSource> sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());
    
    /// <summary>
    /// Suspends playback of multiple <paramref name="sources"/>, simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to suspend.</param>
    public static void Pause(IEnumerable<AudioSource> sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());
    
    /// <summary>
    /// Stops playback of multiple <paramref name="sources"/>, simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to stop.</param>
    public static void Stop(IEnumerable<AudioSource> sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());
    
    /// <summary>
    /// Rewinds of multiple <paramref name="sources"/> back to their <see cref="SourceState.Initial"/> state,
    /// simultaneously with sample accuracy.
    /// </summary>
    /// <param name="sources">The sources to rewind.</param>
    public static void Rewind(IEnumerable<AudioSource> sources) => AL.SourcePlay(sources.Select(s => s.Handle).ToArray());

    /// <summary>
    /// Gets a flag indicating if the <see cref="AudioSource"/> is currently playing.
    /// </summary>
    public bool IsPlaying => AL.GetSourceI<SourceState>(Handle, SourceProperty.SourceState) == SourceState.Playing;
    
    /// <summary>
    /// Gets a flag indicating if the <see cref="AudioSource"/> is currently paused.
    /// </summary>
    public bool IsPaused => AL.GetSourceI<SourceState>(Handle, SourceProperty.SourceState) == SourceState.Paused;

    /// <summary>
    /// Gets a flag indicating if the <see cref="AudioSource"/> is currently stopped or still in its initial state.
    /// </summary>
    public bool IsStopped
    {
	    get
	    {
		    var state = AL.GetSourceI<SourceState>(Handle, SourceProperty.SourceState);
		    return state != SourceState.Playing && state != SourceState.Paused;
	    }
	}
    
    /// <inheritdoc cref="SourceProperty.SourceRelative"/>
    public bool Relative
    {
        get => AL.GetSourceB(Handle, SourceProperty.SourceRelative);
        set => AL.SourceB(Handle, SourceProperty.SourceRelative, value);
    }

	/// <summary>
	/// Gets the settings related to the cone.
	/// </summary>
	/// <param name="innerAngle">The angle covered by the inner cone, where the source will not attenuate.</param>
	/// <param name="outerAngle">The angle covered by the outer cone, where the source will be fully attenuated.</param>
	/// <param name="gain">The gain attenuation applied when the listener is outside of the source's outer cone.</param>
	/// <param name="gainHF">
	/// The directivity of high-frequency attenuation for both the direct-path and the reflected sounds of the source.
	/// </param>
    public void GetCone(out float innerAngle, out float outerAngle, out float gain, out float gainHF)
    {
        innerAngle = AL.GetSourceF(Handle, SourceProperty.ConeInnerAngle);
        outerAngle = AL.GetSourceF(Handle, SourceProperty.ConeOuterAngle);
        gain = AL.GetSourceF(Handle, SourceProperty.ConeOuterGain);
        gainHF = AL.GetSourceF(Handle, SourceProperty.ConeOuterGainHF);
    }

	/// <summary>
	/// Sets the settings related to the cone.
	/// </summary>
	/// <param name="innerAngle">The angle covered by the inner cone, where the source will not attenuate.</param>
	/// <param name="outerAngle">The angle covered by the outer cone, where the source will be fully attenuated.</param>
	/// <param name="gain">The gain attenuation applied when the listener is outside of the source's outer cone.</param>
	/// <param name="gainHF">
	/// The directivity of high-frequency attenuation for both the direct-path and the reflected sounds of the source.
	/// </param>
	/// <remarks>Any value that is <c>null</c> is ignored and left untouched.</remarks>
    public void SetCone(float? innerAngle, float? outerAngle, float? gain, float? gainHF)
    {
        if (innerAngle.HasValue)
            AL.SourceF(Handle, SourceProperty.ConeInnerAngle, innerAngle.Value);
        
        if (outerAngle.HasValue)
            AL.SourceF(Handle, SourceProperty.ConeOuterAngle, outerAngle.Value);
        
        if (gain.HasValue)
            AL.SourceF(Handle, SourceProperty.ConeOuterGain, gain.Value);
        
        if (gainHF.HasValue)
            AL.SourceF(Handle, SourceProperty.ConeOuterGainHF, gainHF.Value);
    }

	/// <summary>
	/// Gets the minimum and maximum source gain.
	/// </summary>
	/// <param name="min">The minimum gain allowed for a source, after distance and cone attenuation is applied (if applicable).</param>
	/// <param name="max">The maximum gain allowed for a source, after distance and cone attenuation is applied (if applicable).</param>
	public void GetGainRange(out float min, out float max)
	{
		min = AL.GetSourceF(Handle, SourceProperty.MinGain);
		max = AL.GetSourceF(Handle, SourceProperty.MaxGain);
	}
	
	/// <summary>
	/// Sets the minimum and maximum source gain.
	/// </summary>
	/// <param name="min">The minimum gain allowed for a source, after distance and cone attenuation is applied (if applicable).</param>
	/// <param name="max">The maximum gain allowed for a source, after distance and cone attenuation is applied (if applicable).</param>
	/// <remarks>Any value that is <c>null</c> is ignored and left untouched.</remarks>
	public void SetGainRange(float? min, float? max)
	{
		if (min.HasValue)
			AL.SourceF(Handle, SourceProperty.MinGain, min.Value);
		if (max.HasValue)
			AL.SourceF(Handle, SourceProperty.MaxGain, max.Value);
	}

	/// <inheritdoc cref="SourceProperty.Pitch"/>
	public float Pitch
	{
		get => AL.GetSourceF(Handle, SourceProperty.Pitch);
		set => AL.SourceF(Handle, SourceProperty.Pitch, Math.Clamp(value, 0.5f, 2.0f));
	}
	
	/// <inheritdoc cref="SourceProperty.Gain"/>
	public float Gain
	{
		get => AL.GetSourceF(Handle, SourceProperty.Gain);
		set => AL.SourceF(Handle, SourceProperty.Gain, MathF.Max(value, 0.0f));
	}
	
	/// <inheritdoc cref="SourceProperty.SourceType"/>
	public SourceType Type => AL.GetSourceI<SourceType>(Handle, SourceProperty.SourceType);

	/// <inheritdoc cref="SourceProperty.SourceState"/>
	public SourceState State => AL.GetSourceI<SourceState>(Handle, SourceProperty.SourceState);
	
	/// <inheritdoc cref="SourceProperty.Position"/>
	public Vector3 Position
	{
		get
		{
			AL.GetSourceF(Handle, SourceProperty.Position, out Vector3 vec);
			return vec;
		}
		set => AL.SourceF(Handle, SourceProperty.Position, value);
	}
	
	/// <inheritdoc cref="SourceProperty.Direction"/>
	public Vector3 Direction
	{
		get
		{
			AL.GetSourceF(Handle, SourceProperty.Direction, out Vector3 vec);
			return vec;
		}
		set => AL.SourceF(Handle, SourceProperty.Direction, value);
	}
	
	/// <inheritdoc cref="SourceProperty.Velocity"/>
	public Vector3 Velocity
	{
		get
		{
			AL.GetSourceF(Handle, SourceProperty.Velocity, out Vector3 vec);
			return vec;
		}
		set => AL.SourceF(Handle, SourceProperty.Velocity, value);
	}

	/// <inheritdoc cref="SourceProperty.Spatialize"/>
	public SourceSpatialization Spatialize
	{
		get => AL.GetSourceI<SourceSpatialization>(Handle, SourceProperty.Spatialize);
		set => AL.SourceI(Handle, SourceProperty.Spatialize, value);
	}

	/// <inheritdoc cref="SourceProperty.ReferenceDistance"/>
	public float ReferenceDistance
	{
		get => AL.GetSourceF(Handle, SourceProperty.ReferenceDistance);
		set => AL.SourceF(Handle, SourceProperty.ReferenceDistance, MathF.Max(1.0f, value));
	}
	
	/// <inheritdoc cref="SourceProperty.RolloffFactor"/>
	public float RolloffFactor
	{
		get => AL.GetSourceF(Handle, SourceProperty.RolloffFactor);
		set => AL.SourceF(Handle, SourceProperty.RolloffFactor, MathF.Max(0.0f, value));
	}
	
	/// <inheritdoc cref="SourceProperty.RoomRolloffFactor"/>
	public float RoomRolloffFactor
	{
		get => AL.GetSourceF(Handle, SourceProperty.RoomRolloffFactor);
		set => AL.SourceF(Handle, SourceProperty.RoomRolloffFactor, Math.Clamp(value, 0.0f, 10.0f));
	}
	
	/// <inheritdoc cref="SourceProperty.MaxDistance"/>
	public float MaxDistance
	{
		get => AL.GetSourceF(Handle, SourceProperty.MaxDistance);
		set => AL.SourceF(Handle, SourceProperty.MaxDistance, MathF.Max(0.0f, value));
	}
	
	/// <inheritdoc cref="SourceProperty.DopplerFactor"/>
	public float DopplerFactor
	{
		get => AL.GetSourceF(Handle, SourceProperty.DopplerFactor);
		set => AL.SourceF(Handle, SourceProperty.DopplerFactor, MathF.Max(0.0f, value));
	}
	
	/// <inheritdoc cref="SourceProperty.AirAbsorptionFactor"/>
	public float AirAbsorptionFactor
	{
		get => AL.GetSourceF(Handle, SourceProperty.AirAbsorptionFactor);
		set => AL.SourceF(Handle, SourceProperty.AirAbsorptionFactor, Math.Clamp(value, 0.0f, 10.0f));
	}

	/// <inheritdoc cref="SourceProperty.DirectFilter"/>
	public AudioFilter? DirectFilter
	{
		get
		{
			var id = AL.GetSourceI(Handle, SourceProperty.DirectFilter);
			return AudioFilter.Wrap(id);
		}
		set => AL.SourceI(Handle, SourceProperty.DirectFilter, value?.Handle ?? default);
	}

	/// <inheritdoc cref="SourceProperty.DirectFilterGainHFAuto"/>
	public bool DirectFilterGainHFAuto
	{
		get => AL.GetSourceB(Handle, SourceProperty.DirectFilterGainHFAuto);
		set => AL.SourceB(Handle, SourceProperty.DirectFilterGainHFAuto, value);
	}

	/// <inheritdoc cref="SourceProperty.AuxiliarySendFilterGainAuto"/>
	public bool SendFilterGainAuto
	{
		get => AL.GetSourceB(Handle, SourceProperty.AuxiliarySendFilterGainAuto);
		set => AL.SourceB(Handle, SourceProperty.AuxiliarySendFilterGainAuto, value);
	}
	
	/// <inheritdoc cref="SourceProperty.AuxiliarySendFilterGainHFAuto"/>
	public bool SendFilterGainHFAuto
	{
		get => AL.GetSourceB(Handle, SourceProperty.AuxiliarySendFilterGainHFAuto);
		set => AL.SourceB(Handle, SourceProperty.AuxiliarySendFilterGainHFAuto, value);
	}
	
	/// <inheritdoc cref="SourceProperty.DistanceModel"/>
	public DistanceModel DistanceModel
	{
		get => AL.GetSourceI<DistanceModel>(Handle, SourceProperty.DistanceModel);
		set => AL.SourceI(Handle, SourceProperty.DistanceModel, value);
	}

	/// <summary>
	/// Assigned this <see cref="AudioSource"/> to feed the specified <paramref name="slot"/> and
	/// <paramref name="filter"/>.
	/// </summary>
	/// <param name="index">The index to send to.</param>
	/// <param name="slot">An optional <see cref="AudioEffectSlot"/> to send output to for post-processing.</param>
	/// <param name="filter">An optional <see cref="AudioFilter"/> to send output to for post-processing.</param>
	public void SendFilter(int index, AudioEffectSlot? slot, AudioFilter? filter)
	{
		AL.SourceI(Handle, SourceProperty.AuxiliarySendFilter, slot?.Handle, index, filter?.Handle);
	}
}