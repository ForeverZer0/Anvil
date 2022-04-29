using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// A method for handling the change of a <see cref="Effect"/> property.
/// </summary>
/// <param name="effect">The <see cref="Effect"/> whose parameter was changed.</param>
/// <param name="param">
/// The index of the changed parameter, or <c>-1</c> if multiple values were changed, such as settings being restored
/// or values updated by a preset.
/// </param>
public delegate void EffectParamHandler(AudioEffect effect, int param);

/// <inheritdoc cref="EffectType.Reverb"/>
[PublicAPI]
public class Reverb : AudioEffect, IReverb
{
	/// <summary>
	/// Creates a new instance of the <see cref="Reverb"/> class.
	/// </summary>
	/// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
	protected Reverb() : base(AL.GenEffect(EffectType.Reverb))
	{
	}
	
	/// <summary>
	/// Creates a new <see cref="Reverb"/> instance.
	/// </summary>
	/// <param name="handle">The effect to wrap.</param>
	protected internal Reverb(Effect handle) : base(handle)
	{
	}

	/// <inheritdoc />
	public float Density
	{
		get => AL.GetEffectF(Handle, ReverbParam.Density);
		set => SetParam(ReverbParam.Density, value, MIN_DENSITY, MAX_DENSITY);
	}

	/// <inheritdoc />
	public float Diffusion
	{
		get => AL.GetEffectF(Handle, ReverbParam.Diffusion);
		set => SetParam(ReverbParam.Diffusion, value, MIN_DIFFUSION, MAX_DIFFUSION);
	}

	/// <inheritdoc />
	public float Gain
	{
		get => AL.GetEffectF(Handle, ReverbParam.Gain);
		set => SetParam(ReverbParam.Gain, value, MIN_GAIN, MAX_GAIN);
	}

	/// <inheritdoc />
	public float GainHF
	{
		get => AL.GetEffectF(Handle, ReverbParam.GainHF);
		set => SetParam(ReverbParam.GainHF, value, MIN_GAINHF, MAX_GAINHF);
	}

	/// <inheritdoc />
	public float DecayTime
	{
		get => AL.GetEffectF(Handle, ReverbParam.DecayTime);
		set => SetParam(ReverbParam.DecayTime, value, MIN_DECAY_TIME, MAX_DECAY_TIME);
	}

	/// <inheritdoc />
	public float DecayHFRatio
	{
		get => AL.GetEffectF(Handle, ReverbParam.DecayHFRatio);
		set => SetParam(ReverbParam.DecayHFRatio, value, MIN_DECAY_HFRATIO, MAX_DECAY_HFRATIO);
	}

	/// <inheritdoc />
	public float ReflectionsGain
	{
		get => AL.GetEffectF(Handle, ReverbParam.ReflectionsGain);
		set => SetParam(ReverbParam.ReflectionsGain, value, MIN_REFLECTIONS_GAIN, MAX_REFLECTIONS_GAIN);
	}

	/// <inheritdoc />
	public float ReflectionsDelay
	{
		get => AL.GetEffectF(Handle, ReverbParam.ReflectionsDelay);
		set => SetParam(ReverbParam.ReflectionsDelay, value, MIN_REFLECTIONS_DELAY, MAX_REFLECTIONS_DELAY);
	}

	/// <inheritdoc />
	public float LateReverbGain
	{
		get => AL.GetEffectF(Handle, ReverbParam.LateReverbGain);
		set => SetParam(ReverbParam.LateReverbGain, value, MIN_LATE_REVERB_GAIN, MAX_LATE_REVERB_GAIN);
	}

	/// <inheritdoc />
	public float LateReverbDelay
	{
		get => AL.GetEffectF(Handle, ReverbParam.LateReverbDelay);
		set => SetParam(ReverbParam.LateReverbDelay, value, MIN_LATE_REVERB_DELAY, MAX_LATE_REVERB_DELAY);
	}

	/// <inheritdoc />
	public float AirAbsorptionGainHF
	{
		get => AL.GetEffectF(Handle, ReverbParam.AirAbsorptionGainHF);
		set => SetParam(ReverbParam.AirAbsorptionGainHF, value, MIN_AIR_ABSORPTION_GAINHF, MAX_AIR_ABSORPTION_GAINHF);
	}

	/// <inheritdoc />
	public float RoomRolloffFactor
	{
		get => AL.GetEffectF(Handle, ReverbParam.RoomRolloffFactor);
		set => SetParam(ReverbParam.RoomRolloffFactor, value, MIN_ROOM_ROLLOFF_FACTOR, MAX_ROOM_ROLLOFF_FACTOR);
	}

	/// <inheritdoc />
	public bool DecayHFLimit
	{
		get => AL.GetEffectB(Handle, ReverbParam.DecayHFLimit);
		set => SetParam(ReverbParam.DecayHFLimit, value);
	}

	/// <inheritdoc />
	public ReverbProperties Preset
	{
		get => AL.GetEffectReverb(Handle);
		set
		{
			AL.EffectReverb(Handle, value);
			OnParameterChanged();
		}
	}

	/// <inheritdoc />
	public override void Restore()
	{
		AL.EffectF(Handle, ReverbParam.Density, DEFAULT_DENSITY);
		AL.EffectF(Handle, ReverbParam.Diffusion, DEFAULT_DIFFUSION);
		AL.EffectF(Handle, ReverbParam.Gain, DEFAULT_GAIN);
		AL.EffectF(Handle, ReverbParam.GainHF, DEFAULT_GAINHF);
		AL.EffectF(Handle, ReverbParam.DecayTime, DEFAULT_DECAY_TIME);
		AL.EffectF(Handle, ReverbParam.DecayHFRatio, DEFAULT_DECAY_HFRATIO);
		AL.EffectF(Handle, ReverbParam.ReflectionsGain, DEFAULT_REFLECTIONS_GAIN);
		AL.EffectF(Handle, ReverbParam.ReflectionsDelay, DEFAULT_REFLECTIONS_DELAY);
		AL.EffectF(Handle, ReverbParam.LateReverbGain, DEFAULT_LATE_REVERB_GAIN);
		AL.EffectF(Handle, ReverbParam.LateReverbDelay, DEFAULT_LATE_REVERB_DELAY);
		AL.EffectF(Handle, ReverbParam.AirAbsorptionGainHF, DEFAULT_AIR_ABSORPTION_GAINHF);
		AL.EffectF(Handle, ReverbParam.RoomRolloffFactor, DEFAULT_ROOM_ROLLOFF_FACTOR);
		AL.EffectB(Handle, ReverbParam.DecayHFLimit, DEFAULT_DECAY_HFLIMIT);
		OnParameterChanged();
	}

	private const float MIN_DENSITY = 0.0f;
	private const float MAX_DENSITY = 1.0f;
	private const float DEFAULT_DENSITY = 1.0f;
	private const float MIN_DIFFUSION = 0.0f;
	private const float MAX_DIFFUSION = 1.0f;
	private const float DEFAULT_DIFFUSION = 1.0f;
	private const float MIN_GAIN = 0.0f;
	private const float MAX_GAIN = 1.0f;
	private const float DEFAULT_GAIN = 0.32f;
	private const float MIN_GAINHF = 0.0f;
	private const float MAX_GAINHF = 1.0f;
	private const float DEFAULT_GAINHF = 0.89f;
	private const float MIN_DECAY_TIME = 0.1f;
	private const float MAX_DECAY_TIME = 20.0f;
	private const float DEFAULT_DECAY_TIME = 1.49f;
	private const float MIN_DECAY_HFRATIO = 0.1f;
	private const float MAX_DECAY_HFRATIO = 2.0f;
	private const float DEFAULT_DECAY_HFRATIO = 0.83f;
	private const float MIN_REFLECTIONS_GAIN = 0.0f;
	private const float MAX_REFLECTIONS_GAIN = 3.16f;
	private const float DEFAULT_REFLECTIONS_GAIN = 0.05f;
	private const float MIN_REFLECTIONS_DELAY = 0.0f;
	private const float MAX_REFLECTIONS_DELAY = 0.3f;
	private const float DEFAULT_REFLECTIONS_DELAY = 0.007f;
	private const float MIN_LATE_REVERB_GAIN = 0.0f;
	private const float MAX_LATE_REVERB_GAIN = 10.0f;
	private const float DEFAULT_LATE_REVERB_GAIN = 1.26f;
	private const float MIN_LATE_REVERB_DELAY = 0.0f;
	private const float MAX_LATE_REVERB_DELAY = 0.1f;
	private const float DEFAULT_LATE_REVERB_DELAY = 0.011f;
	private const float MIN_AIR_ABSORPTION_GAINHF = 0.892f;
	private const float MAX_AIR_ABSORPTION_GAINHF = 1.0f;
	private const float DEFAULT_AIR_ABSORPTION_GAINHF = 0.994f;
	private const float MIN_ROOM_ROLLOFF_FACTOR = 0.0f;
	private const float MAX_ROOM_ROLLOFF_FACTOR = 10.0f;
	private const float DEFAULT_ROOM_ROLLOFF_FACTOR = 0.0f;
	private const bool DEFAULT_DECAY_HFLIMIT = true;
}