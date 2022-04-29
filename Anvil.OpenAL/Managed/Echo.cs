using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.Echo"/>
[PublicAPI]
public class Echo : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="Echo"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected Echo() : base(AL.GenEffect(EffectType.Echo))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="Echo"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal Echo(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectF(Handle, EchoParam.Delay, DEFAULT_DELAY);
        AL.EffectF(Handle, EchoParam.LrDelay, DEFAULT_LRDELAY);
        AL.EffectF(Handle, EchoParam.Damping, DEFAULT_DAMPING);
        AL.EffectF(Handle, EchoParam.Feedback, DEFAULT_FEEDBACK);
        AL.EffectF(Handle, EchoParam.Spread, DEFAULT_SPREAD);
        OnParameterChanged();
    }
	
    public float Delay
    {
        get => AL.GetEffectF(Handle, EchoParam.Delay);
        set => SetParam(EchoParam.Delay, value, MIN_DELAY, MAX_DELAY);
    }

    public float LrDelay
    {
        get => AL.GetEffectF(Handle, EchoParam.LrDelay);
        set => SetParam(EchoParam.LrDelay, value, MIN_LRDELAY, MAX_LRDELAY);
    }

    public float Damping
    {
        get => AL.GetEffectF(Handle, EchoParam.Damping);
        set => SetParam(EchoParam.Damping, value, MIN_DAMPING, MAX_DAMPING);
    }

    public float Feedback
    {
        get => AL.GetEffectF(Handle, EchoParam.Feedback);
        set => SetParam(EchoParam.Feedback, value, MIN_FEEDBACK, MAX_FEEDBACK);
    }

    public float Spread
    {
        get => AL.GetEffectF(Handle, EchoParam.Spread);
        set => SetParam(EchoParam.Spread, value, MIN_SPREAD, MAX_SPREAD);
    }

	
    private const float MIN_DELAY = 0.0f;
    private const float MAX_DELAY = 0.207f;
    private const float DEFAULT_DELAY = 0.1f;
    private const float MIN_LRDELAY = 0.0f;
    private const float MAX_LRDELAY = 0.404f;
    private const float DEFAULT_LRDELAY = 0.1f;
    private const float MIN_DAMPING = 0.0f;
    private const float MAX_DAMPING = 0.99f;
    private const float DEFAULT_DAMPING = 0.5f;
    private const float MIN_FEEDBACK = 0.0f;
    private const float MAX_FEEDBACK = 1.0f;
    private const float DEFAULT_FEEDBACK = 0.5f;
    private const float MIN_SPREAD = -1.0f;
    private const float MAX_SPREAD = 1.0f;
    private const float DEFAULT_SPREAD = -1.0f;
}