using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.Flanger"/>
[PublicAPI]
public class Flanger : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="Flanger"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected Flanger() : base(AL.GenEffect(EffectType.Flanger))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="Flanger"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal Flanger(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectI(Handle, FlangerParam.Waveform, DEFAULT_WAVEFORM);
        AL.EffectI(Handle, FlangerParam.Phase, DEFAULT_PHASE);
        AL.EffectF(Handle, FlangerParam.Rate, DEFAULT_RATE);
        AL.EffectF(Handle, FlangerParam.Depth, DEFAULT_DEPTH);
        AL.EffectF(Handle, FlangerParam.Feedback, DEFAULT_FEEDBACK);
        AL.EffectF(Handle, FlangerParam.Delay, DEFAULT_DELAY);
        OnParameterChanged();
    }
	
    public FlangerWaveform Waveform
    {
        get => AL.GetEffectI<FlangerWaveform>(Handle, FlangerParam.Waveform);
        set => SetParam(FlangerParam.Waveform, value, MIN_WAVEFORM, MAX_WAVEFORM);
    }

    public float Phase
    {
        get => AL.GetEffectF(Handle, FlangerParam.Phase);
        set => SetParam(FlangerParam.Phase, value, MIN_PHASE, MAX_PHASE);
    }

    public float Rate
    {
        get => AL.GetEffectF(Handle, FlangerParam.Rate);
        set => SetParam(FlangerParam.Rate, value, MIN_RATE, MAX_RATE);
    }

    public float Depth
    {
        get => AL.GetEffectF(Handle, FlangerParam.Depth);
        set => SetParam(FlangerParam.Depth, value, MIN_DEPTH, MAX_DEPTH);
    }

    public float Feedback
    {
        get => AL.GetEffectF(Handle, FlangerParam.Feedback);
        set => SetParam(FlangerParam.Feedback, value, MIN_FEEDBACK, MAX_FEEDBACK);
    }

    public float Delay
    {
        get => AL.GetEffectF(Handle, FlangerParam.Delay);
        set => SetParam(FlangerParam.Delay, value, MIN_DELAY, MAX_DELAY);
    }

    private const int MIN_WAVEFORM = 0;
    private const int MAX_WAVEFORM = 1;
    private const int DEFAULT_WAVEFORM = 1;
    private const int MIN_PHASE = -180;
    private const int MAX_PHASE = 180;
    private const int DEFAULT_PHASE = 0;
    private const float MIN_RATE = 0.0f;
    private const float MAX_RATE = 10.0f;
    private const float DEFAULT_RATE = 0.27f;
    private const float MIN_DEPTH = 0.0f;
    private const float MAX_DEPTH = 1.0f;
    private const float DEFAULT_DEPTH = 1.0f;
    private const float MIN_FEEDBACK = -1.0f;
    private const float MAX_FEEDBACK = 1.0f;
    private const float DEFAULT_FEEDBACK = -0.5f;
    private const float MIN_DELAY = 0.0f;
    private const float MAX_DELAY = 0.004f;
    private const float DEFAULT_DELAY = 0.002f;
}