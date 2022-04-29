using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.Distortion"/>
[PublicAPI]
public class Distortion : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="Distortion"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected Distortion() : base(AL.GenEffect(EffectType.Distortion))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="Distortion"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal Distortion(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectF(Handle, DistortionParam.Edge, DEFAULT_EDGE);
        AL.EffectF(Handle, DistortionParam.Gain, DEFAULT_GAIN);
        AL.EffectF(Handle, DistortionParam.LowpassCutoff, DEFAULT_LOWPASS_CUTOFF);
        AL.EffectF(Handle, DistortionParam.Center, DEFAULT_EQCENTER);
        AL.EffectF(Handle, DistortionParam.Bandwidth, DEFAULT_EQBANDWIDTH);
        OnParameterChanged();
    }
	
    public float Edge
    {
        get => AL.GetEffectF(Handle, DistortionParam.Edge);
        set => SetParam(DistortionParam.Edge, value, MIN_EDGE, MAX_EDGE);
    }

    public float Gain
    {
        get => AL.GetEffectF(Handle, DistortionParam.Gain);
        set => SetParam(DistortionParam.Gain, value, MIN_GAIN, MAX_GAIN);
    }

    public float LowpassCutoff
    {
        get => AL.GetEffectF(Handle, DistortionParam.LowpassCutoff);
        set => SetParam(DistortionParam.LowpassCutoff, value, MIN_LOWPASS_CUTOFF, MAX_LOWPASS_CUTOFF);
    }

    public float Center
    {
        get => AL.GetEffectF(Handle, DistortionParam.Center);
        set => SetParam(DistortionParam.Center, value, MIN_EQCENTER, MAX_EQCENTER);
    }

    public float Bandwidth
    {
        get => AL.GetEffectF(Handle, DistortionParam.Bandwidth);
        set => SetParam(DistortionParam.Bandwidth, value, MIN_EQBANDWIDTH, MAX_EQBANDWIDTH);
    }

	
    private const float MIN_EDGE = 0.0f;
    private const float MAX_EDGE = 1.0f;
    private const float DEFAULT_EDGE = 0.2f;
    private const float MIN_GAIN = 0.01f;
    private const float MAX_GAIN = 1.0f;
    private const float DEFAULT_GAIN = 0.05f;
    private const float MIN_LOWPASS_CUTOFF = 80.0f;
    private const float MAX_LOWPASS_CUTOFF = 24000.0f;
    private const float DEFAULT_LOWPASS_CUTOFF = 8000.0f;
    private const float MIN_EQCENTER = 80.0f;
    private const float MAX_EQCENTER = 24000.0f;
    private const float DEFAULT_EQCENTER = 3600.0f;
    private const float MIN_EQBANDWIDTH = 80.0f;
    private const float MAX_EQBANDWIDTH = 24000.0f;
    private const float DEFAULT_EQBANDWIDTH = 3600.0f;
}