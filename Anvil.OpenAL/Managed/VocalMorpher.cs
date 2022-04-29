using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.VocalMorpher"/>
[PublicAPI]
public class VocalMorpher : AudioEffect
{
    /// <summary>
    /// Creates a new instance of the <see cref="VocalMorpher"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected VocalMorpher() : base(AL.GenEffect(EffectType.VocalMorpher))
    {
    }
	
    /// <summary>
    /// Creates a new <see cref="VocalMorpher"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal VocalMorpher(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectI(Handle, VocalMorpherParam.PhonemeA, DEFAULT_PHONEMEA);
        AL.EffectI(Handle, VocalMorpherParam.PhonemeACoarseTuning, DEFAULT_PHONEMEA_COARSE_TUNING);
        AL.EffectI(Handle, VocalMorpherParam.PhonemeB, DEFAULT_PHONEMEB);
        AL.EffectI(Handle, VocalMorpherParam.PhonemeBCoarseTuning, DEFAULT_PHONEMEB_COARSE_TUNING);
        AL.EffectI(Handle, VocalMorpherParam.Waveform, DEFAULT_WAVEFORM);
        AL.EffectF(Handle, VocalMorpherParam.Rate, DEFAULT_RATE);
        OnParameterChanged();
    }
	
    public Phoneme PhonemeA
    {
        get => AL.GetEffectI<Phoneme>(Handle, VocalMorpherParam.PhonemeA);
        set => SetParam(VocalMorpherParam.PhonemeA, value, MIN_PHONEMEA, MAX_PHONEMEA);
    }

    public float PhonemeACoarseTuning
    {
        get => AL.GetEffectF(Handle, VocalMorpherParam.PhonemeACoarseTuning);
        set => SetParam(VocalMorpherParam.PhonemeACoarseTuning, value, MIN_PHONEMEA_COARSE_TUNING, MAX_PHONEMEA_COARSE_TUNING);
    }

    public Phoneme PhonemeB
    {
        get => AL.GetEffectI<Phoneme>(Handle, VocalMorpherParam.PhonemeB);
        set => SetParam(VocalMorpherParam.PhonemeB, value, MIN_PHONEMEB, MAX_PHONEMEB);
    }

    public float PhonemeBCoarseTuning
    {
        get => AL.GetEffectF(Handle, VocalMorpherParam.PhonemeBCoarseTuning);
        set => SetParam(VocalMorpherParam.PhonemeBCoarseTuning, value, MIN_PHONEMEB_COARSE_TUNING, MAX_PHONEMEB_COARSE_TUNING);
    }

    public VocalMorpherWaveform Waveform
    {
        get => AL.GetEffectI<VocalMorpherWaveform>(Handle, VocalMorpherParam.Waveform);
        set => SetParam(VocalMorpherParam.Waveform, value, MIN_WAVEFORM, MAX_WAVEFORM);
    }

    public float Rate
    {
        get => AL.GetEffectF(Handle, VocalMorpherParam.Rate);
        set => SetParam(VocalMorpherParam.Rate, value, MIN_RATE, MAX_RATE);
    }

    private const int MIN_PHONEMEA = 0;
    private const int MAX_PHONEMEA = 29;
    private const int DEFAULT_PHONEMEA = 0;
    private const int MIN_PHONEMEA_COARSE_TUNING = -24;
    private const int MAX_PHONEMEA_COARSE_TUNING = 24;
    private const int DEFAULT_PHONEMEA_COARSE_TUNING = 0;
    private const int MIN_PHONEMEB = 0;
    private const int MAX_PHONEMEB = 29;
    private const int DEFAULT_PHONEMEB = 10;
    private const int MIN_PHONEMEB_COARSE_TUNING = -24;
    private const int MAX_PHONEMEB_COARSE_TUNING = 24;
    private const int DEFAULT_PHONEMEB_COARSE_TUNING = 0;
    private const int MIN_WAVEFORM = 0;
    private const int MAX_WAVEFORM = 2;
    private const int DEFAULT_WAVEFORM = 0;
    private const float MIN_RATE = 0.0f;
    private const float MAX_RATE = 10.0f;
    private const float DEFAULT_RATE = 1.41f;
}