using JetBrains.Annotations;
#pragma warning disable CS1591

namespace Anvil.OpenAL;

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Reverb"/> effects.
/// </summary>
[PublicAPI]
public enum ReverbParam
{
    Density = 0x0001,
    Diffusion = 0x0002,
    Gain = 0x0003,
    GainHF = 0x0004,
    DecayTime = 0x0005,
    DecayHFRatio = 0x0006,
    ReflectionsGain = 0x0007,
    ReflectionsDelay = 0x0008,
    LateReverbGain = 0x0009,
    LateReverbDelay = 0x000A,
    AirAbsorptionGainHF = 0x000B,
    RoomRolloffFactor = 0x000C,
    DecayHFLimit = 0x000D,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.EaxReverb"/> effects.
/// </summary>
[PublicAPI]
public enum EaxReverbParam
{
    Density = 0x0001,
    Diffusion = 0x0002,
    Gain = 0x0003,
    GainHF = 0x0004,
    GainLF = 0x0005,
    DecayTime = 0x0006,
    DecayHFRatio = 0x0007,
    DecayLFRatio = 0x0008,
    ReflectionsGain = 0x0009,
    ReflectionsDelay = 0x000A,
    ReflectionsPan = 0x000B,
    LateReverbGain = 0x000C,
    LateReverbDelay = 0x000D,
    LateReverbPan = 0x000E,
    EchoTime = 0x000F,
    EchoDepth = 0x0010,
    ModulationTime = 0x0011,
    ModulationDepth = 0x0012,
    AirAbsorptionGainHF = 0x0013,
    HFReference = 0x0014,
    LFReference = 0x0015,
    RoomRolloffFactor = 0x0016,
    DecayHFLimit = 0x0017,
}

/// <summary>
/// Strongly-typed constants describing the waveform shape of <see cref="EffectType.Chorus"/> effects.
/// </summary>
[PublicAPI]
public enum ChorusWaveform
{
    Sinusoid = 0,
    Triangle = 1,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Chorus"/> effects.
/// </summary>
[PublicAPI]
public enum ChorusParam
{
    Waveform = 0x0001,
    Phase = 0x0002,
    Rate = 0x0003,
    Depth = 0x0004,
    Feedback = 0x0005,
    Delay = 0x0006,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Distortion"/> effects.
/// </summary>
[PublicAPI]
public enum DistortionParam
{
    Edge = 0x0001,
    Gain = 0x0002,
    LowpassCutoff = 0x0003,
    Center = 0x0004,
    Bandwidth = 0x0005,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Echo"/> effects.
/// </summary>
[PublicAPI]
public enum EchoParam
{
    Delay = 0x0001,
    LrDelay = 0x0002,
    Damping = 0x0003,
    Feedback = 0x0004,
    Spread = 0x0005,
}

/// <summary>
/// Strongly-typed constants describing the waveform shape of <see cref="EffectType.Flanger"/> effects.
/// </summary>
[PublicAPI]
public enum FlangerWaveform
{
    Sinusoid = 0,
    Triangle = 1,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Flanger"/> effects.
/// </summary>
[PublicAPI]
public enum FlangerParam
{
    Waveform = 0x0001,
    Phase = 0x0002,
    Rate = 0x0003,
    Depth = 0x0004,
    Feedback = 0x0005,
    Delay = 0x0006,
}

/// <summary>
/// Strongly-typed constants describing the direction in <see cref="EffectType.FrequencyShifter"/> effects.
/// </summary>
[PublicAPI]
public enum FrequencyShifterDirection
{
    Down = 0,
    Up = 1,
    Off = 2,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.FrequencyShifter"/> effects.
/// </summary>
[PublicAPI]
public enum FrequencyShifterParam
{
    Frequency = 0x0001,
    LeftDirection = 0x0002,
    RightDirection = 0x0003,
}

/// <summary>
/// Strongly-typed constants describing the phoneme in <see cref="EffectType.VocalMorpher"/> effects.
/// </summary>
[PublicAPI]
public enum Phoneme
{
    A = 0,
    E = 1,
    I = 2,
    O = 3,
    U = 4,
    // ReSharper disable InconsistentNaming
    AA = 5,
    AE = 6,
    AH = 7,
    AO = 8,
    EH = 9,
    ER = 10,
    IH = 11,
    IY = 12,
    UH = 13,
    UW = 14,
    // ReSharper restore InconsistentNaming
    B = 15,
    D = 16,
    F = 17,
    G = 18,
    J = 19,
    K = 20,
    L = 21,
    M = 22,
    N = 23,
    P = 24,
    R = 25,
    S = 26,
    T = 27,
    V = 28,
    Z = 29,
}

/// <summary>
/// Strongly-typed constants describing the waveform shape of <see cref="EffectType.VocalMorpher"/> effects.
/// </summary>
[PublicAPI]
public enum VocalMorpherWaveform
{
    Sinusoid       = 0,
    Triangle       = 1,
    Sawtooth       = 2,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.VocalMorpher"/> effects.
/// </summary>
[PublicAPI]
public enum VocalMorpherParam
{
    PhonemeA = 0x0001,
    PhonemeACoarseTuning = 0x0002,
    PhonemeB = 0x0003,
    PhonemeBCoarseTuning = 0x0004,
    Waveform = 0x0005,
    Rate = 0x0006,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.PitchShifter"/> effects.
/// </summary>
[PublicAPI]
public enum PitchShifterParam
{
    CoarseTune = 0x0001,
    FineTune = 0x0002,
}

/// <summary>
/// Strongly-typed constants describing the waveform shape of <see cref="EffectType.RingModulator"/> effects.
/// </summary>
[PublicAPI]
public enum RingModulatorWaveform
{
    Sinusoid               = 0,
    Sawtooth               = 1,
    Square                 = 2,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.RingModulator"/> effects.
/// </summary>
[PublicAPI]
public enum RingModulatorParam
{
    Frequency = 0x0001,
    HighpassCutoff = 0x0002,
    Waveform = 0x0003,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Autowah"/> effects.
/// </summary>
[PublicAPI]
public enum AutowahParam
{
    AttackTime = 0x0001,
    ReleaseTime = 0x0002,
    Resonance = 0x0003,
    PeakGain = 0x0004,
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Compressor"/> effects.
/// </summary>
[PublicAPI]
public enum CompressorParam
{
    Enabled = 0x0001
}

/// <summary>
/// Strongly-typed parameter indices to use for <see cref="EffectType.Equalizer"/> effects.
/// </summary>
[PublicAPI]
public enum EqualizerParam
{
    LowGain                    = 0x0001,
    LowCutoff                  = 0x0002,
    Mid1Gain                   = 0x0003,
    Mid1Center                 = 0x0004,
    Mid1Width                  = 0x0005,
    Mid2Gain                   = 0x0006,
    Mid2Center                 = 0x0007,
    Mid2Width                  = 0x0008,
    HighGain                   = 0x0009,
    HighCutoff                 = 0x000A,
}