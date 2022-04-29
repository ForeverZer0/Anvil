using System.Numerics;
using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <inheritdoc cref="EffectType.EaxReverb"/>
[PublicAPI]
public class EaxReverb : AudioEffect, IReverb
{
    /// <summary>
    /// Creates a new instance of the <see cref="EaxReverb"/> class.
    /// </summary>
    /// <remarks>Automatically creates and wraps the underlying <see cref="Effect"/> object.</remarks>
    protected EaxReverb() : base(AL.GenEffect(EffectType.EaxReverb))
    {
    }
    
    /// <summary>
    /// Creates a new <see cref="EaxReverb"/> instance.
    /// </summary>
    /// <param name="handle">The effect to wrap.</param>
    protected internal EaxReverb(Effect handle) : base(handle)
    {
    }

    /// <inheritdoc />
    public float Density
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.Density);
        set => SetParam(EaxReverbParam.Density, value, MIN_DENSITY, MAX_DENSITY);
    }

    /// <inheritdoc />
    public float Diffusion
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.Diffusion);
        set => SetParam(EaxReverbParam.Diffusion, value, MIN_DIFFUSION, MAX_DIFFUSION);
    }

    /// <inheritdoc />
    public float Gain
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.Gain);
        set => SetParam(EaxReverbParam.Gain, value, MIN_GAIN, MAX_GAIN);
    }

    /// <inheritdoc />
    public float GainHF
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.GainHF);
        set => SetParam(EaxReverbParam.GainHF, value, MIN_GAINHF, MAX_GAINHF);
    }

    /// <inheritdoc cref="ReverbProperties.GainLF"/>
    public float GainLF
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.GainLF);
        set => SetParam(EaxReverbParam.GainLF, value, MIN_GAINLF, MAX_GAINLF);
    }

    /// <inheritdoc />
    public float DecayTime
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.DecayTime);
        set => SetParam(EaxReverbParam.DecayTime, value, MIN_DECAY_TIME, MAX_DECAY_TIME);
    }

    /// <inheritdoc />
    public float DecayHFRatio
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.DecayHFRatio);
        set => SetParam(EaxReverbParam.DecayHFRatio, value, MIN_DECAY_HFRATIO, MAX_DECAY_HFRATIO);
    }

    /// <inheritdoc cref="ReverbProperties.DecayLFRatio"/>
    public float DecayLFRatio
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.DecayLFRatio);
        set => SetParam(EaxReverbParam.DecayLFRatio, value, MIN_DECAY_LFRATIO, MAX_DECAY_LFRATIO);
    }

    /// <inheritdoc />
    public float ReflectionsGain
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.ReflectionsGain);
        set => SetParam(EaxReverbParam.ReflectionsGain, value, MIN_REFLECTIONS_GAIN, MAX_REFLECTIONS_GAIN);
    }

    /// <inheritdoc />
    public float ReflectionsDelay
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.ReflectionsDelay);
        set => SetParam(EaxReverbParam.ReflectionsDelay, value, MIN_REFLECTIONS_DELAY, MAX_REFLECTIONS_DELAY);
    }

    /// <inheritdoc cref="ReverbProperties.ReflectionsPan"/>
    public Vector3 ReflectionsPan
    {
        get => AL.GetEffectV(Handle, EaxReverbParam.ReflectionsPan);
        set => SetParam(EaxReverbParam.ReflectionsPan, value);
    }

    /// <inheritdoc />
    public float LateReverbGain
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.LateReverbGain);
        set => SetParam(EaxReverbParam.LateReverbGain, value, MIN_LATE_REVERB_GAIN, MAX_LATE_REVERB_GAIN);
    }

    /// <inheritdoc />
    public float LateReverbDelay
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.LateReverbDelay);
        set => SetParam(EaxReverbParam.LateReverbDelay, value, MIN_LATE_REVERB_DELAY, MAX_LATE_REVERB_DELAY);
    }

    /// <inheritdoc cref="ReverbProperties.LateReverbPan"/>
    public Vector3 LateReverbPan
    {
        get => AL.GetEffectV(Handle, EaxReverbParam.LateReverbPan);
        set => SetParam(EaxReverbParam.LateReverbPan, value);
    }

    /// <inheritdoc cref="ReverbProperties.EchoTime"/>
    public float EchoTime
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.EchoTime);
        set => SetParam(EaxReverbParam.EchoTime, value, MIN_ECHO_TIME, MAX_ECHO_TIME);
    }

    /// <inheritdoc cref="ReverbProperties.EchoDepth"/>
    public float EchoDepth
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.EchoDepth);
        set => SetParam(EaxReverbParam.EchoDepth, value, MIN_ECHO_DEPTH, MAX_ECHO_DEPTH);
    }

    /// <inheritdoc cref="ReverbProperties.ModulationDepth"/>
    public float ModulationTime
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.ModulationTime);
        set => SetParam(EaxReverbParam.ModulationTime, value, MIN_MODULATION_TIME, MAX_MODULATION_TIME);
    }

    /// <inheritdoc cref="ReverbProperties.ModulationTime"/>
    public float ModulationDepth
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.ModulationDepth);
        set => SetParam(EaxReverbParam.ModulationDepth, value, MIN_MODULATION_DEPTH, MAX_MODULATION_DEPTH);
    }

    /// <inheritdoc />
    public float AirAbsorptionGainHF
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.AirAbsorptionGainHF);
        set => SetParam(EaxReverbParam.AirAbsorptionGainHF, value, MIN_AIR_ABSORPTION_GAINHF,
            MAX_AIR_ABSORPTION_GAINHF);
    }

    /// <inheritdoc cref="ReverbProperties.HFReference"/>
    public float HFReference
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.HFReference);
        set => SetParam(EaxReverbParam.HFReference, value, MIN_HFREFERENCE, MAX_HFREFERENCE);
    }

    /// <inheritdoc cref="ReverbProperties.LFReference"/>
    public float LFReference
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.LFReference);
        set => SetParam(EaxReverbParam.LFReference, value, MIN_LFREFERENCE, MAX_LFREFERENCE);
    }

    /// <inheritdoc />
    public float RoomRolloffFactor
    {
        get => AL.GetEffectF(Handle, EaxReverbParam.RoomRolloffFactor);
        set => SetParam(EaxReverbParam.RoomRolloffFactor, value, MIN_ROOM_ROLLOFF_FACTOR, MAX_ROOM_ROLLOFF_FACTOR);
    }
	
    /// <inheritdoc />
    public bool DecayHFLimit
    {
        get => AL.GetEffectB(Handle, EaxReverbParam.DecayHFLimit);
        set => SetParam(EaxReverbParam.DecayHFLimit, value);
    }

    /// <inheritdoc />
    public ReverbProperties Preset
    {
        get => AL.GetEffectReverb(Handle);
        set
        {
            AL.EffectReverb(Handle, value);
            OnParameterChanged();
        }
    }

    /// <inheritdoc />
    public override void Restore()
    {
        AL.EffectF(Handle, EaxReverbParam.Density, DEFAULT_DENSITY);
        AL.EffectF(Handle, EaxReverbParam.Diffusion, DEFAULT_DIFFUSION);
        AL.EffectF(Handle, EaxReverbParam.Gain, DEFAULT_GAIN);
        AL.EffectF(Handle, EaxReverbParam.GainHF, DEFAULT_GAINHF);
        AL.EffectF(Handle, EaxReverbParam.GainLF, DEFAULT_GAINLF);
        AL.EffectF(Handle, EaxReverbParam.DecayTime, DEFAULT_DECAY_TIME);
        AL.EffectF(Handle, EaxReverbParam.DecayHFRatio, DEFAULT_DECAY_HFRATIO);
        AL.EffectF(Handle, EaxReverbParam.DecayLFRatio, DEFAULT_DECAY_LFRATIO);
        AL.EffectF(Handle, EaxReverbParam.ReflectionsGain, DEFAULT_REFLECTIONS_GAIN);
        AL.EffectF(Handle, EaxReverbParam.ReflectionsDelay, DEFAULT_REFLECTIONS_DELAY);
        AL.EffectV(Handle, EaxReverbParam.ReflectionsPan, new Vector3(DEFAULT_REFLECTIONS_PAN_XYZ));
        AL.EffectF(Handle, EaxReverbParam.LateReverbGain, DEFAULT_LATE_REVERB_GAIN);
        AL.EffectF(Handle, EaxReverbParam.LateReverbDelay, DEFAULT_LATE_REVERB_DELAY);
        AL.EffectV(Handle, EaxReverbParam.LateReverbPan, new Vector3(DEFAULT_LATE_REVERB_PAN_XYZ));
        AL.EffectF(Handle, EaxReverbParam.EchoTime, DEFAULT_ECHO_TIME);
        AL.EffectF(Handle, EaxReverbParam.EchoDepth, DEFAULT_ECHO_DEPTH);
        AL.EffectF(Handle, EaxReverbParam.ModulationTime, DEFAULT_MODULATION_TIME);
        AL.EffectF(Handle, EaxReverbParam.ModulationDepth, DEFAULT_MODULATION_DEPTH);
        AL.EffectF(Handle, EaxReverbParam.AirAbsorptionGainHF, DEFAULT_AIR_ABSORPTION_GAINHF);
        AL.EffectF(Handle, EaxReverbParam.HFReference, DEFAULT_HFREFERENCE);
        AL.EffectF(Handle, EaxReverbParam.LFReference, DEFAULT_LFREFERENCE);
        AL.EffectF(Handle, EaxReverbParam.RoomRolloffFactor, DEFAULT_ROOM_ROLLOFF_FACTOR);
        AL.EffectB(Handle, EaxReverbParam.DecayHFLimit, DEFAULT_DECAY_HFLIMIT);
        OnParameterChanged();
    }
    
    private const float MIN_DENSITY = 0.0f;
    private const float MAX_DENSITY = 1.0f;
    private const float DEFAULT_DENSITY = 1.0f;
    private const float MIN_DIFFUSION = 0.0f;
    private const float MAX_DIFFUSION = 1.0f;
    private const float DEFAULT_DIFFUSION = 1.0f;
    private const float MIN_GAIN = 0.0f;
    private const float MAX_GAIN = 1.0f;
    private const float DEFAULT_GAIN = 0.32f;
    private const float MIN_GAINHF = 0.0f;
    private const float MAX_GAINHF = 1.0f;
    private const float DEFAULT_GAINHF = 0.89f;
    private const float MIN_GAINLF = 0.0f;
    private const float MAX_GAINLF = 1.0f;
    private const float DEFAULT_GAINLF = 1.0f;
    private const float MIN_DECAY_TIME = 0.1f;
    private const float MAX_DECAY_TIME = 20.0f;
    private const float DEFAULT_DECAY_TIME = 1.49f;
    private const float MIN_DECAY_HFRATIO = 0.1f;
    private const float MAX_DECAY_HFRATIO = 2.0f;
    private const float DEFAULT_DECAY_HFRATIO = 0.83f;
    private const float MIN_DECAY_LFRATIO = 0.1f;
    private const float MAX_DECAY_LFRATIO = 2.0f;
    private const float DEFAULT_DECAY_LFRATIO = 1.0f;
    private const float MIN_REFLECTIONS_GAIN = 0.0f;
    private const float MAX_REFLECTIONS_GAIN = 3.16f;
    private const float DEFAULT_REFLECTIONS_GAIN = 0.05f;
    private const float MIN_REFLECTIONS_DELAY = 0.0f;
    private const float MAX_REFLECTIONS_DELAY = 0.3f;
    private const float DEFAULT_REFLECTIONS_DELAY = 0.007f;
    private const float DEFAULT_REFLECTIONS_PAN_XYZ = 0.0f;
    private const float MIN_LATE_REVERB_GAIN = 0.0f;
    private const float MAX_LATE_REVERB_GAIN = 10.0f;
    private const float DEFAULT_LATE_REVERB_GAIN = 1.26f;
    private const float MIN_LATE_REVERB_DELAY = 0.0f;
    private const float MAX_LATE_REVERB_DELAY = 0.1f;
    private const float DEFAULT_LATE_REVERB_DELAY = 0.011f;
    private const float DEFAULT_LATE_REVERB_PAN_XYZ = 0.0f;
    private const float MIN_ECHO_TIME = 0.075f;
    private const float MAX_ECHO_TIME = 0.25f;
    private const float DEFAULT_ECHO_TIME = 0.25f;
    private const float MIN_ECHO_DEPTH = 0.0f;
    private const float MAX_ECHO_DEPTH = 1.0f;
    private const float DEFAULT_ECHO_DEPTH = 0.0f;
    private const float MIN_MODULATION_TIME = 0.04f;
    private const float MAX_MODULATION_TIME = 4.0f;
    private const float DEFAULT_MODULATION_TIME = 0.25f;
    private const float MIN_MODULATION_DEPTH = 0.0f;
    private const float MAX_MODULATION_DEPTH = 1.0f;
    private const float DEFAULT_MODULATION_DEPTH = 0.0f;
    private const float MIN_AIR_ABSORPTION_GAINHF = 0.892f;
    private const float MAX_AIR_ABSORPTION_GAINHF = 1.0f;
    private const float DEFAULT_AIR_ABSORPTION_GAINHF = 0.994f;
    private const float MIN_HFREFERENCE = 1000.0f;
    private const float MAX_HFREFERENCE = 20000.0f;
    private const float DEFAULT_HFREFERENCE = 5000.0f;
    private const float MIN_LFREFERENCE = 20.0f;
    private const float MAX_LFREFERENCE = 1000.0f;
    private const float DEFAULT_LFREFERENCE = 250.0f;
    private const float MIN_ROOM_ROLLOFF_FACTOR = 0.0f;
    private const float MAX_ROOM_ROLLOFF_FACTOR = 10.0f;
    private const float DEFAULT_ROOM_ROLLOFF_FACTOR = 0.0f;
    private const bool DEFAULT_DECAY_HFLIMIT = true;
}