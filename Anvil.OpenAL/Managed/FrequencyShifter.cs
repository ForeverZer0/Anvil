using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.FrequencyShifter"/>
[PublicAPI]
public class FrequencyShifter : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="FrequencyShifter"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected FrequencyShifter() : base(AL.GenEffect(EffectType.FrequencyShifter))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="FrequencyShifter"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal FrequencyShifter(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectF(Handle, FrequencyShifterParam.Frequency, DEFAULT_FREQUENCY);
        AL.EffectI(Handle, FrequencyShifterParam.LeftDirection, DEFAULT_LEFT_DIRECTION);
        AL.EffectI(Handle, FrequencyShifterParam.RightDirection, DEFAULT_RIGHT_DIRECTION);
        OnParameterChanged();
    }
	
    public float Frequency
    {
        get => AL.GetEffectF(Handle, FrequencyShifterParam.Frequency);
        set => SetParam(FrequencyShifterParam.Frequency, value, MIN_FREQUENCY, MAX_FREQUENCY);
    }

    public FrequencyShifterDirection LeftDirection
    {
        get => AL.GetEffectI(Handle, FrequencyShifterParam.LeftDirection);
        set => SetParam(FrequencyShifterParam.LeftDirection, value, MIN_LEFT_DIRECTION, MAX_LEFT_DIRECTION);
    }

    public FrequencyShifterDirection RightDirection
    {
        get => AL.GetEffectI(Handle, FrequencyShifterParam.RightDirection);
        set => SetParam(FrequencyShifterParam.RightDirection, value, MIN_RIGHT_DIRECTION, MAX_RIGHT_DIRECTION);
    }
    
    private const float MIN_FREQUENCY = 0.0f;
    private const float MAX_FREQUENCY = 24000.0f;
    private const float DEFAULT_FREQUENCY = 0.0f;
    private const int MIN_LEFT_DIRECTION = 0;
    private const int MAX_LEFT_DIRECTION = 2;
    private const int DEFAULT_LEFT_DIRECTION = 0;
    private const int MIN_RIGHT_DIRECTION = 0;
    private const int MAX_RIGHT_DIRECTION = 2;
    private const int DEFAULT_RIGHT_DIRECTION = 0;
}