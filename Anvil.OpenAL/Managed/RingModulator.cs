using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.RingModulator"/>
[PublicAPI]
public class RingModulator : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="RingModulator"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected RingModulator() : base(AL.GenEffect(EffectType.RingModulator))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="RingModulator"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal RingModulator(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectF(Handle, RingModulatorParam.Frequency, DEFAULT_FREQUENCY);
        AL.EffectF(Handle, RingModulatorParam.HighpassCutoff, DEFAULT_HIGHPASS_CUTOFF);
        AL.EffectI(Handle, RingModulatorParam.Waveform, DEFAULT_WAVEFORM);
        OnParameterChanged();
    }
	
    public float Frequency
    {
        get => AL.GetEffectF(Handle, RingModulatorParam.Frequency);
        set => SetParam(RingModulatorParam.Frequency, value, MIN_FREQUENCY, MAX_FREQUENCY);
    }

    public float HighpassCutoff
    {
        get => AL.GetEffectF(Handle, RingModulatorParam.HighpassCutoff);
        set => SetParam(RingModulatorParam.HighpassCutoff, value, MIN_HIGHPASS_CUTOFF, MAX_HIGHPASS_CUTOFF);
    }

    public RingModulatorWaveform Waveform
    {
        get => AL.GetEffectI(Handle, RingModulatorParam.Waveform);
        set => SetParam(RingModulatorParam.Waveform, value, MIN_WAVEFORM, MAX_WAVEFORM);
    }

    private const float MIN_FREQUENCY = 0.0f;
    private const float MAX_FREQUENCY = 8000.0f;
    private const float DEFAULT_FREQUENCY = 440.0f;
    private const float MIN_HIGHPASS_CUTOFF = 0.0f;
    private const float MAX_HIGHPASS_CUTOFF = 24000.0f;
    private const float DEFAULT_HIGHPASS_CUTOFF = 800.0f;
    private const int MIN_WAVEFORM = 0;
    private const int MAX_WAVEFORM = 2;
    private const int DEFAULT_WAVEFORM = 0;
}