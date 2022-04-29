using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.PitchShifter"/>
[PublicAPI]
public class PitchShifter : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="PitchShifter"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    public PitchShifter() : base(AL.GenEffect(EffectType.PitchShifter))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="PitchShifter"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal PitchShifter(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectI(Handle, PitchShifterParam.CoarseTune, DEFAULT_COARSE_TUNE);
        AL.EffectI(Handle, PitchShifterParam.FineTune, DEFAULT_FINE_TUNE);
        OnParameterChanged();
    }
	
    public int CoarseTune
    {
        get => AL.GetEffectI(Handle, PitchShifterParam.CoarseTune);
        set => SetParam(PitchShifterParam.CoarseTune, value, MIN_COARSE_TUNE, MAX_COARSE_TUNE);
    }

    public int FineTune
    {
        get => AL.GetEffectI(Handle, PitchShifterParam.FineTune);
        set => SetParam(PitchShifterParam.FineTune, value, MIN_FINE_TUNE, MAX_FINE_TUNE);
    }

	
    private const int MIN_COARSE_TUNE = -12;
    private const int MAX_COARSE_TUNE = 12;
    private const int DEFAULT_COARSE_TUNE = 12;
    private const int MIN_FINE_TUNE = -50;
    private const int MAX_FINE_TUNE = 50;
    private const int DEFAULT_FINE_TUNE = 0;
}