using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.Chorus"/>
[PublicAPI]
public class Chorus : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="Chorus"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected Chorus() : base(AL.GenEffect(EffectType.Chorus))
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="Chorus"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal Chorus(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectI(Handle, ChorusParam.Waveform, DEFAULT_WAVEFORM);
        AL.EffectI(Handle, ChorusParam.Phase, DEFAULT_PHASE);
        AL.EffectF(Handle, ChorusParam.Rate, DEFAULT_RATE);
        AL.EffectF(Handle, ChorusParam.Depth, DEFAULT_DEPTH);
        AL.EffectF(Handle, ChorusParam.Feedback, DEFAULT_FEEDBACK);
        AL.EffectF(Handle, ChorusParam.Delay, DEFAULT_DELAY);
        OnParameterChanged();
    }

    public ChorusWaveform Waveform
    {
        get => AL.GetEffectI<ChorusWaveform>(Handle, ChorusParam.Waveform);
        set => SetParam(ChorusParam.Waveform, value, MIN_WAVEFORM, MAX_WAVEFORM);
    }
	
    public int Phase
    {
        get => AL.GetEffectI(Handle, ChorusParam.Phase);
        set => SetParam(ChorusParam.Phase, value, MIN_PHASE, MAX_PHASE);
    }

    public float Rate
    {
        get => AL.GetEffectF(Handle, ChorusParam.Rate);
        set => SetParam(ChorusParam.Rate, value, MIN_RATE, MAX_RATE);
    }

    public float Depth
    {
        get => AL.GetEffectF(Handle, ChorusParam.Depth);
        set => SetParam(ChorusParam.Depth, value, MIN_DEPTH, MAX_DEPTH);
    }

    public float Feedback
    {
        get => AL.GetEffectF(Handle, ChorusParam.Feedback);
        set => SetParam(ChorusParam.Feedback, value, MIN_FEEDBACK, MAX_FEEDBACK);
    }

    public float Delay
    {
        get => AL.GetEffectF(Handle, ChorusParam.Delay);
        set => SetParam(ChorusParam.Delay, value, MIN_DELAY, MAX_DELAY);
    }
	
    private const int MIN_WAVEFORM = (0);
    private const int MAX_WAVEFORM = (1);
    private const int DEFAULT_WAVEFORM = (1);
    private const int MIN_PHASE = (-180);
    private const int MAX_PHASE = (180);
    private const int DEFAULT_PHASE = (90);
    private const float MIN_RATE = (0.0f);
    private const float MAX_RATE = (10.0f);
    private const float DEFAULT_RATE = (1.1f);
    private const float MIN_DEPTH = (0.0f);
    private const float MAX_DEPTH = (1.0f);
    private const float DEFAULT_DEPTH = (0.1f);
    private const float MIN_FEEDBACK = (-1.0f);
    private const float MAX_FEEDBACK = (1.0f);
    private const float DEFAULT_FEEDBACK = (0.25f);
    private const float MIN_DELAY = (0.0f);
    private const float MAX_DELAY = (0.016f);
    private const float DEFAULT_DELAY = (0.016f);
}