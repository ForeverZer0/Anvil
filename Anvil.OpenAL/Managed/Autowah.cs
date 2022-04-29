using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.Autowah"/>
[PublicAPI]
public class Autowah : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="Autowah"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected Autowah() : base(AL.GenEffect(EffectType.Autowah))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="Autowah"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal Autowah(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectF(Handle, AutowahParam.AttackTime, DEFAULT_ATTACK_TIME);
        AL.EffectF(Handle, AutowahParam.ReleaseTime, DEFAULT_RELEASE_TIME);
        AL.EffectF(Handle, AutowahParam.Resonance, DEFAULT_RESONANCE);
        AL.EffectF(Handle, AutowahParam.PeakGain, DEFAULT_PEAK_GAIN);
        OnParameterChanged();
    }
	
    public float AttackTime
    {
        get => AL.GetEffectF(Handle, AutowahParam.AttackTime);
        set => SetParam(AutowahParam.AttackTime, value, MIN_ATTACK_TIME, MAX_ATTACK_TIME);
    }

    public float ReleaseTime
    {
        get => AL.GetEffectF(Handle, AutowahParam.ReleaseTime);
        set => SetParam(AutowahParam.ReleaseTime, value, MIN_RELEASE_TIME, MAX_RELEASE_TIME);
    }

    public float Resonance
    {
        get => AL.GetEffectF(Handle, AutowahParam.Resonance);
        set => SetParam(AutowahParam.Resonance, value, MIN_RESONANCE, MAX_RESONANCE);
    }

    public float PeakGain
    {
        get => AL.GetEffectF(Handle, AutowahParam.PeakGain);
        set => SetParam(AutowahParam.PeakGain, value, MIN_PEAK_GAIN, MAX_PEAK_GAIN);
    }

    private const float MIN_ATTACK_TIME = 0.0001f;
    private const float MAX_ATTACK_TIME = 1.0f;
    private const float DEFAULT_ATTACK_TIME = 0.06f;
    private const float MIN_RELEASE_TIME = 0.0001f;
    private const float MAX_RELEASE_TIME = 1.0f;
    private const float DEFAULT_RELEASE_TIME = 0.06f;
    private const float MIN_RESONANCE = 2.0f;
    private const float MAX_RESONANCE = 1000.0f;
    private const float DEFAULT_RESONANCE = 1000.0f;
    private const float MIN_PEAK_GAIN = 0.00003f;
    private const float MAX_PEAK_GAIN = 31621.0f;
    private const float DEFAULT_PEAK_GAIN = 11.22f;
}