using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.Equalizer"/>
[PublicAPI]
public class Equalizer : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="Equalizer"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    public Equalizer() : base(AL.GenEffect(EffectType.Equalizer))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="Equalizer"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal Equalizer(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectF(Handle, EqualizerParam.LowGain, DEFAULT_LOW_GAIN);
        AL.EffectF(Handle, EqualizerParam.LowCutoff, DEFAULT_LOW_CUTOFF);
        AL.EffectF(Handle, EqualizerParam.Mid1Gain, DEFAULT_MID1_GAIN);
        AL.EffectF(Handle, EqualizerParam.Mid1Center, DEFAULT_MID1_CENTER);
        AL.EffectF(Handle, EqualizerParam.Mid1Width, DEFAULT_MID1_WIDTH);
        AL.EffectF(Handle, EqualizerParam.Mid2Gain, DEFAULT_MID2_GAIN);
        AL.EffectF(Handle, EqualizerParam.Mid2Center, DEFAULT_MID2_CENTER);
        AL.EffectF(Handle, EqualizerParam.Mid2Width, DEFAULT_MID2_WIDTH);
        AL.EffectF(Handle, EqualizerParam.HighGain, DEFAULT_HIGH_GAIN);
        AL.EffectF(Handle, EqualizerParam.HighCutoff, DEFAULT_HIGH_CUTOFF);
        OnParameterChanged();
    }
	
    public float LowGain
    {
        get => AL.GetEffectF(Handle, EqualizerParam.LowGain);
        set => SetParam(EqualizerParam.LowGain, value, MIN_LOW_GAIN, MAX_LOW_GAIN);
    }

    public float LowCutoff
    {
        get => AL.GetEffectF(Handle, EqualizerParam.LowCutoff);
        set => SetParam(EqualizerParam.LowCutoff, value, MIN_LOW_CUTOFF, MAX_LOW_CUTOFF);
    }

    public float Mid1Gain
    {
        get => AL.GetEffectF(Handle, EqualizerParam.Mid1Gain);
        set => SetParam(EqualizerParam.Mid1Gain, value, MIN_MID1_GAIN, MAX_MID1_GAIN);
    }

    public float Mid1Center
    {
        get => AL.GetEffectF(Handle, EqualizerParam.Mid1Center);
        set => SetParam(EqualizerParam.Mid1Center, value, MIN_MID1_CENTER, MAX_MID1_CENTER);
    }

    public float Mid1Width
    {
        get => AL.GetEffectF(Handle, EqualizerParam.Mid1Width);
        set => SetParam(EqualizerParam.Mid1Width, value, MIN_MID1_WIDTH, MAX_MID1_WIDTH);
    }

    public float Mid2Gain
    {
        get => AL.GetEffectF(Handle, EqualizerParam.Mid2Gain);
        set => SetParam(EqualizerParam.Mid2Gain, value, MIN_MID2_GAIN, MAX_MID2_GAIN);
    }

    public float Mid2Center
    {
        get => AL.GetEffectF(Handle, EqualizerParam.Mid2Center);
        set => SetParam(EqualizerParam.Mid2Center, value, MIN_MID2_CENTER, MAX_MID2_CENTER);
    }

    public float Mid2Width
    {
        get => AL.GetEffectF(Handle, EqualizerParam.Mid2Width);
        set => SetParam(EqualizerParam.Mid2Width, value, MIN_MID2_WIDTH, MAX_MID2_WIDTH);
    }

    public float HighGain
    {
        get => AL.GetEffectF(Handle, EqualizerParam.HighGain);
        set => SetParam(EqualizerParam.HighGain, value, MIN_HIGH_GAIN, MAX_HIGH_GAIN);
    }

    public float HighCutoff
    {
        get => AL.GetEffectF(Handle, EqualizerParam.HighCutoff);
        set => SetParam(EqualizerParam.HighCutoff, value, MIN_HIGH_CUTOFF, MAX_HIGH_CUTOFF);
    }
    
    private const float MIN_LOW_GAIN = 0.126f;
    private const float MAX_LOW_GAIN = 7.943f;
    private const float DEFAULT_LOW_GAIN = 1.0f;
    private const float MIN_LOW_CUTOFF = 50.0f;
    private const float MAX_LOW_CUTOFF = 800.0f;
    private const float DEFAULT_LOW_CUTOFF = 200.0f;
    private const float MIN_MID1_GAIN = 0.126f;
    private const float MAX_MID1_GAIN = 7.943f;
    private const float DEFAULT_MID1_GAIN = 1.0f;
    private const float MIN_MID1_CENTER = 200.0f;
    private const float MAX_MID1_CENTER = 3000.0f;
    private const float DEFAULT_MID1_CENTER = 500.0f;
    private const float MIN_MID1_WIDTH = 0.01f;
    private const float MAX_MID1_WIDTH = 1.0f;
    private const float DEFAULT_MID1_WIDTH = 1.0f;
    private const float MIN_MID2_GAIN = 0.126f;
    private const float MAX_MID2_GAIN = 7.943f;
    private const float DEFAULT_MID2_GAIN = 1.0f;
    private const float MIN_MID2_CENTER = 1000.0f;
    private const float MAX_MID2_CENTER = 8000.0f;
    private const float DEFAULT_MID2_CENTER = 3000.0f;
    private const float MIN_MID2_WIDTH = 0.01f;
    private const float MAX_MID2_WIDTH = 1.0f;
    private const float DEFAULT_MID2_WIDTH = 1.0f;
    private const float MIN_HIGH_GAIN = 0.126f;
    private const float MAX_HIGH_GAIN = 7.943f;
    private const float DEFAULT_HIGH_GAIN = 1.0f;
    private const float MIN_HIGH_CUTOFF = 4000.0f;
    private const float MAX_HIGH_CUTOFF = 16000.0f;
    private const float DEFAULT_HIGH_CUTOFF = 6000.0f;
}