using System.Numerics;
using JetBrains.Annotations;

namespace Anvil.OpenAL;

/// <summary>
/// Serializable container that describes the state of a reverb <see cref="Effect"/>.
/// </summary>
/// <seealso cref="AL.EffectReverb"/>
/// <seealso cref="AL.GetEffectReverb"/>
/// <seealso cref="Managed.Reverb"/>
/// <seealso cref="Managed.EaxReverb"/>
[PublicAPI, Serializable]
public sealed class ReverbProperties 
{
    /// <summary>
    /// Controls the coloration of the late reverb. Lowering the value adds more coloration to the
    /// late reverb. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>1.0</c></para>
    /// Default: <c>1.0</c>
    /// </remarks>
    public float Density { get; set; }

    /// <summary>
    /// Controls the echo density in the reverberation decay. It is set by default to 1.0, which provides the highest
    /// density. Reducing diffusion gives the reverberation a more “grainy” character that is especially noticeable with
    /// percussive sound sources. If you set a diffusion value of 0.0, the later reverberation sounds like a succession
    /// of distinct echoes. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c></c>1.0</para>
    /// Default: <c>1.0</c>
    /// </remarks>
    public float Diffusion { get; set; }

    /// <summary>
    /// The master volume control for the reflected sound (both early reflections and reverberation) that the reverb
    /// effect adds to all sound sources. It sets the maximum amount of reflections and reverberation added to the final
    /// sound mix.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>1.0</c></para>
    /// Default: <c>0.32</c>
    /// </remarks>
    public float Gain { get; set; }

    /// <summary>
    /// Gain value that further tweaks reflected sound by attenuating it at high frequencies. It controls a low-pass
    /// filter that applies globally to the reflected sound of all sound sources feeding the particular instance of the
    /// reverb effect.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>1.0</c></para>
    /// Default: <c>0.89</c>
    /// </remarks>
    public float GainHF { get; set; }

    /// <summary>
    /// Gain value that further tweaks reflected sound by attenuating it at low frequencies. It controls a high-pass
    /// filter that applies globally to the reflected sound of all sound sources feeding the particular instance of the
    /// reverb effect.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c></c>1.0</para>
    /// Default: <c>0.0</c>
    /// </remarks>
    public float GainLF { get; set; }

    /// <summary>
    /// The reverberation decay time in second units.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.1</c>
    /// <para>Maximum: <c></c>20.0</para>
    /// Default: <c>1.49</c>
    /// </remarks>
    /// <seealso cref="DecayHFRatio"/>
    /// <seealso cref="DecayLFRatio"/>
    public float DecayTime { get; set; }

    /// <summary>
    /// Adjusts the spectral quality of the <see cref="DecayTime"/> property. It is the ratio of high-frequency
    /// decay time relative to the time set by <see cref="DecayTime"/>. The HF ratio value <c>1.0</c> is neutral: the
    /// decay time is equal for all frequencies. As the ratio increases above <c>1.0</c>, the high-frequency decay time
    /// increases so it’s longer than the decay time at mid frequencies. You hear a more brilliant reverberation with a
    /// longer decay at high frequencies. As the value decreases below <c>1.0</c>, the high-frequency decay time
    /// decreases so it’s shorter than the decay time of the mid frequencies. You hear a more natural reverberation. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.1</c>
    /// <para>Maximum: <c>20.0</c></para>
    /// Default: <c>0.83</c>
    /// </remarks>
    /// <seealso cref="DecayTime"/>
    /// <seealso cref="DecayLFRatio"/>
    public float DecayHFRatio { get; set; }

    /// <summary>
    /// Adjusts the spectral quality of the <see cref="DecayTime"/> property. It is the ratio of low-frequency decay
    /// time relative to the time set by <see cref="DecayTime"/>. A value of <c>1.0</c> is neutral: the decay time is
    /// equal for all frequencies. As the value increases above <c>1.0</c>, the low-frequency decay time increases so
    /// it’s longer than the decay time at mid frequencies. You hear a more booming reverberation with a longer decay at
    /// low frequencies. As the value decreases below <c>1.0</c>, the low-frequency decay time decreases so shorter than
    /// the decay time of the mid frequencies. You hear a more "tinny" reverberation. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.1</c>
    /// <para>Maximum: <c>20.0</c></para>
    /// Default: <c>1.0</c>
    /// </remarks>
    /// <seealso cref="DecayTime"/>
    /// <seealso cref="DecayHFRatio"/>
    public float DecayLFRatio { get; set; }

    /// <summary>
    /// Controls the overall amount of initial reflections relative to the <see cref="Gain"/> property, which sets the
    /// overall amount of reflected sound: both initial reflections and later reverberation. The value ranges from a
    /// maximum of <c>3.16</c> (+10 dB) to a minimum of <c>0.0</c> (-100 dB) (no initial reflections at all), and is
    /// corrected by the value of the <see cref="Gain"/> property. This value does not affect the subsequent
    /// reverberation decay.
    /// <para/>
    /// You can increase the amount of initial reflections to simulate a more narrow space or closer walls, especially
    /// effective if you associate the initial reflections increase with a reduction in reflections delays by lowering
    /// the value of the Reflection Delay property. To simulate open or semi-open environments, you can maintain the
    /// amount of early reflections while reducing the value of the <see cref="LateReverbGain"/> property, which
    /// controls later reflections. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>3.16</c></para>
    /// Default: <c>0.05</c>
    /// </remarks>
    /// <seealso cref="ReflectionsPan"/>
    /// <seealso cref="LateReverbGain"/>
    public float ReflectionsGain { get; set; }

    /// <summary>
    /// Controls the amount of delay between the arrival time of the direct path from the source to the first reflection
    /// from the source. It ranges from 0 to 300 milliseconds. You can reduce or increase this value to simulate closer
    /// or more distant reflective surfaces —- and therefore control the perceived size of the room.
    /// <para/>
    /// Values are in second units.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>0.3</c></para>
    /// Default: <c>0.007</c>
    /// </remarks>
    public float ReflectionsDelay { get; set; }

    /// <summary>
    /// A 3D vector that controls the spatial distribution of the cluster of early reflections. The direction of this
    /// vector controls the global direction of the reflections, while its magnitude controls how focused the
    /// reflections are towards this direction.
    /// <para/>
    /// It is important to note that the direction of the vector is interpreted in the coordinate system of the user,
    /// without taking into account the orientation of the virtual listener. For instance, assuming a four-point
    /// loudspeaker playback system, setting Reflections Pan to (0., 0., 0.7) means that the reflections are panned to
    /// the front speaker pair, whereas as setting of (0., 0., −0.7) pans the reflections towards the rear speakers.
    /// These vectors follow the a left-handed co-ordinate system, unlike OpenAL uses a right-handed co-ordinate system.
    /// <para/>
    /// If the magnitude of Reflections Pan is zero (the default setting), the early reflections come evenly from all
    /// directions. As the magnitude increases, the reflections become more focused in the direction pointed to by the
    /// vector. A magnitude of 1.0 would represent the extreme case, where all reflections come from a single direction. 
    /// </summary>
    /// <remarks>
    /// Vector is assumed to be a unit vector with a length of <c>1.0</c>.
    /// Minimum: <c>&lt;0.0, 0.0, 0.0&gt;</c>
    /// <para>Maximum: <c></c>&lt;1.0, 1.0, 1.0&gt;</para>
    /// Default: <c>&lt;0.0, 0.0, 0.0&gt;</c>
    /// </remarks>
    public Vector3 ReflectionsPan { get; set; }

    /// <summary>
    /// Controls the overall amount of later reverberation relative to the <see cref="Gain"/> property. The value
    /// ranges from a maximum of <c>10.0</c> (+20 dB) to a minimum of <c>0.0</c> (-100 dB) (no late reverberation at
    /// all). 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>10.0</c></para>
    /// Default: <c>1.26</c>
    /// </remarks>
    public float LateReverbGain { get; set; }

    /// <summary>
    /// Defines the begin time of the late reverberation relative to the time of the initial reflection (the first of
    /// the early reflections). It ranges from <c>0</c> to <c>100</c> milliseconds. Reducing or increasing this value is useful for
    /// simulating a smaller or larger room.
    /// <para/>
    /// Values are in second units.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c></c>0.1</para>
    /// Default: <c>0.011</c>
    /// </remarks>
    public float LateReverbDelay { get; set; }

    /// <summary>
    /// A 3D vector that controls the spatial distribution of the late reverb. The direction of this vector controls the
    /// global direction of the reverb, while its magnitude controls how focused the reverb are towards this direction.
    /// </summary>
    /// <remarks>
    /// Vector is assumed to be a unit vector with a length of <c>1.0</c>.
    /// Minimum: <c>&lt;0.0, 0.0, 0.0&gt;</c>
    /// <para>Maximum: <c></c>&lt;1.0, 1.0, 1.0&gt;</para>
    /// Default: <c>&lt;0.0, 0.0, 0.0&gt;</c>
    /// </remarks>
    public Vector3 LateReverbPan { get; set; }

    /// <summary>
    /// Together with <see cref="EchoDepth"/>, introduces a cyclic echo in the reverberation decay, which will be
    /// noticeable with transient or percussive sounds. A larger value of <see cref="EchoDepth"/> will make this effect
    /// more prominent. This value controls the rate at which the cyclic echo repeats itself along the reverberation
    /// decay. For example, the default setting for this property is 250 ms. causing the echo to occur 4 times per
    /// second. Therefore, if you were to clap your hands in this type of environment, you will hear four repetitions of
    /// clap per second. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.075</c>
    /// <para>Maximum: <c>0.25</c></para>
    /// Default: <c>0.25</c>
    /// </remarks>
    /// <seealso cref="EchoDepth"/>
    public float EchoTime { get; set; }

    /// <summary>
    /// Together with <see cref="EchoTime"/>, introduces a cyclic echo in the reverberation decay, which will be
    /// noticeable with transient or percussive sounds. A larger value will make this effect more prominent. The
    /// <see cref="EchoTime"/> value controls the rate at which the cyclic echo repeats itself along the reverberation
    /// decay. For example, the default setting for this property is 250 ms. causing the echo to occur 4 times per
    /// second. Therefore, if you were to clap your hands in this type of environment, you will hear four repetitions of
    /// clap per second.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>1.0</c></para>
    /// Default: <c>0.0</c>
    /// </remarks>
    /// <see cref="EchoTime"/>
    public float EchoDepth { get; set; }

    /// <summary>
    /// Together with <see cref="ModulationDepth"/>, can create a pitch modulation in the reverberant sound. This will
    /// be most noticeable applied to sources that have tonal color or pitch. You can use this to make some trippy
    /// effects! Modulation Time controls the speed of the vibrato (rate of periodic changes in pitch). 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.004</c>
    /// <para>Maximum: <c>4.0</c></para>
    /// Default: <c>0.25</c>
    /// </remarks>
    /// <seealso cref="ModulationDepth"/>
    public float ModulationTime { get; set; }

    /// <summary>
    /// Together with <see cref="ModulationDepth"/>, can create a pitch modulation in the reverberant sound. This will
    /// be most noticeable applied to sources that have tonal color or pitch. You can use this to make some trippy
    /// effects! Modulation Time controls the speed of the vibrato (rate of periodic changes in pitch).
    /// <para/>
    /// Controls the amount of pitch change. Low values of <see cref="Diffusion"/> will contribute to reinforcing the
    /// perceived effect by reducing the mixing of overlapping reflections in the reverberation decay. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>1.0</c></para>
    /// Default: <c>0.0</c>
    /// </remarks>
    /// <seealso cref="ModulationTime"/>
    public float ModulationDepth { get; set; }

    /// <summary>
    /// Controls the distance-dependent attenuation at high frequencies caused by the propagation medium. It applies to
    /// reflected sound only. You can use this value to simulate sound transmission through foggy air, dry air, smoky
    /// atmosphere, and so on. The default value is <c>0.994</c> (-0.05 dB) per meter, which roughly corresponds to
    /// typical condition of atmospheric humidity, temperature, and so on. Lowering the value simulates a more absorbent
    /// medium (more humidity in the air, for example); raising the value simulates a less absorbent medium (dry desert
    /// air, for example). 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>9.892</c>
    /// <para>Maximum: <c>1.0</c></para>
    /// Default: <c>0.994</c>
    /// </remarks>
    public float AirAbsorptionGainHF { get; set; }

    /// <summary>
    /// Determine the frequencies at which the high-frequency effects effects created by EAX Reverb properties are
    /// measured, for example <see cref="DecayHFRatio"/>. Note that it is necessary to maintain a factor of at least
    /// <c>10</c> between this and the <see cref="LFReference"/> reference frequency so that low frequency and high
    /// frequency properties can be accurately controlled and  will produce independent effects. In other words, the
    /// <see cref="LFReference"/> value should be less than  1/10 of the <see cref="HFReference"/> value.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>1000.0</c>
    /// <para>Maximum: <c>20000.0</c></para>
    /// Default: <c>5000.0</c>
    /// </remarks>
    /// <seealso cref="LFReference"/>
    public float HFReference { get; set; }

    /// <summary>
    /// Determine the frequencies at which the low-frequency effects effects created by EAX Reverb properties are
    /// measured, for example <see cref="DecayLFRatio"/>. Note that it is necessary to maintain a factor of at least
    /// <c>10</c> between this and the <see cref="HFReference"/> reference frequency so that low frequency and high
    /// frequency properties can be accurately controlled and  will produce independent effects. In other words, the
    /// <see cref="LFReference"/> value should be less than  1/10 of the <see cref="HFReference"/> value.
    /// </summary>
    /// <remarks>
    /// Minimum: <c>20.0</c>
    /// <para>Maximum: <c>1000.0</c></para>
    /// Default: <c>250.0</c>
    /// </remarks>
    /// <seealso cref="HFReference"/>
    public float LFReference { get; set; }

    /// <summary>
    /// Attenuate the reflected sound (containing both reflections and reverberation) according to source-listener
    /// distance. It is  defined the same way as OpenAL’s Rolloff Factor, but operates on reverb sound instead of
    /// direct-path sound. Setting the value to <c>1.0</c> specifies that the reflected sound will decay by 6 dB every
    /// time the distance doubles. Any value other than <c>1.0</c> is equivalent to a scaling factor applied to the
    /// quantity specified by ((Source listener distance) - (Reference Distance)). Reference distance is an OpenAL
    /// source parameter that specifies the inner border for distance rolloff effects: if the source comes closer to the
    /// listener than the reference distance, the direct-path sound isn’t increased as the source comes closer to the
    /// listener, and neither is the reflected sound. 
    /// </summary>
    /// <remarks>
    /// Minimum: <c>0.0</c>
    /// <para>Maximum: <c>10.0</c></para>
    /// Default: <c>0.0</c>
    /// </remarks>
    public float RoomRolloffFactor { get; set; }

    /// <summary>
    /// Determines if the high-frequency decay time automatically stays below a limit value that’s derived from the
    /// setting of the property <see cref="AirAbsorptionGainHF"/>. This limit applies regardless of the setting of the
    /// property <see cref="DecayHFRatio"/>, and the limit does not affect the its value. This limit, when on, maintains
    /// a natural sounding reverberation decay by allowing you to increase the value of <see cref="DecayTime"/> without
    /// the risk of getting an unnaturally long decay time at high frequencies. If this flag is set to <c>false</c>,
    /// high-frequency decay time is not automatically limited. 
    /// </summary>
    /// <remarks>
    /// Default: <c>true</c>
    /// </remarks>
    public bool DecayHFLimit { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ReverbProperties"/> class with default values set.
    /// </summary>
    public ReverbProperties()
    {
        Density = DEFAULT_DENSITY;
        Diffusion = DEFAULT_DIFFUSION;
        Gain = DEFAULT_GAIN;
        GainHF = DEFAULT_GAINHF;
        GainLF = DEFAULT_GAINLF;
        DecayTime = DEFAULT_DECAY_TIME;
        DecayHFRatio = DEFAULT_DECAY_HFRATIO;
        DecayLFRatio = DEFAULT_DECAY_LFRATIO;
        ReflectionsGain = DEFAULT_REFLECTIONS_GAIN;
        ReflectionsDelay = DEFAULT_REFLECTIONS_DELAY;
        ReflectionsPan = new Vector3(DEFAULT_REFLECTIONS_PAN_XYZ);
        LateReverbGain = DEFAULT_LATE_REVERB_GAIN;
        LateReverbDelay = DEFAULT_LATE_REVERB_DELAY;
        LateReverbPan = new Vector3(DEFAULT_LATE_REVERB_PAN_XYZ);
        EchoTime = DEFAULT_ECHO_TIME;
        EchoDepth = DEFAULT_ECHO_DEPTH;
        ModulationTime = DEFAULT_MODULATION_TIME;
        ModulationDepth = DEFAULT_MODULATION_DEPTH;
        AirAbsorptionGainHF = DEFAULT_AIR_ABSORPTION_GAINHF;
        HFReference = DEFAULT_HFREFERENCE;
        LFReference = DEFAULT_LFREFERENCE;
        RoomRolloffFactor = DEFAULT_ROOM_ROLLOFF_FACTOR;
        DecayHFLimit = DEFAULT_DECAY_HFLIMIT;
    }

    private ReverbProperties(float density, float diffusion, float gain, float gainHF, float gainLF, float decayTime,
        float decayHFRatio, float decayLFRatio, float reflectionsGain, float reflectionsDelay, Vector3 reflectionsPan,
        float lateReverbGain, float lateReverbDelay, Vector3 lateReverbPan, float echoTime, float echoDepth,
        float modulationTime, float modulationDepth, float airAbsorptionGainHF, float hFReference, float lFReference,
        float roomRolloffFactor, bool decayHFLimit)
    {
        Density = density;
        Diffusion = diffusion;
        Gain = gain;
        GainHF = gainHF;
        GainLF = gainLF;
        DecayTime = decayTime;
        DecayHFRatio = decayHFRatio;
        DecayLFRatio = decayLFRatio;
        ReflectionsGain = reflectionsGain;
        ReflectionsDelay = reflectionsDelay;
        ReflectionsPan = reflectionsPan;
        LateReverbGain = lateReverbGain;
        LateReverbDelay = lateReverbDelay;
        LateReverbPan = lateReverbPan;
        EchoTime = echoTime;
        EchoDepth = echoDepth;
        ModulationTime = modulationTime;
        ModulationDepth = modulationDepth;
        AirAbsorptionGainHF = airAbsorptionGainHF;
        HFReference = hFReference;
        LFReference = lFReference;
        RoomRolloffFactor = roomRolloffFactor;
        DecayHFLimit = decayHFLimit;
    }
    
    private const float DEFAULT_DENSITY = 1.0f;
    private const float DEFAULT_DIFFUSION = 1.0f;
    private const float DEFAULT_GAIN = 0.32f;
    private const float DEFAULT_GAINHF = 0.89f;
    private const float DEFAULT_GAINLF = 1.0f;
    private const float DEFAULT_DECAY_TIME = 1.49f;
    private const float DEFAULT_DECAY_HFRATIO = 0.83f;
    private const float DEFAULT_DECAY_LFRATIO = 1.0f;
    private const float DEFAULT_REFLECTIONS_GAIN = 0.05f;
    private const float DEFAULT_REFLECTIONS_DELAY = 0.007f;
    private const float DEFAULT_REFLECTIONS_PAN_XYZ = 0.0f;
    private const float DEFAULT_LATE_REVERB_GAIN = 1.26f;
    private const float DEFAULT_LATE_REVERB_DELAY = 0.011f;
    private const float DEFAULT_LATE_REVERB_PAN_XYZ = 0.0f;
    private const float DEFAULT_ECHO_TIME = 0.25f;
    private const float DEFAULT_ECHO_DEPTH = 0.0f;
    private const float DEFAULT_MODULATION_TIME = 0.25f;
    private const float DEFAULT_MODULATION_DEPTH = 0.0f;
    private const float DEFAULT_AIR_ABSORPTION_GAINHF = 0.994f;
    private const float DEFAULT_HFREFERENCE = 5000.0f;
    private const float DEFAULT_LFREFERENCE = 250.0f;
    private const float DEFAULT_ROOM_ROLLOFF_FACTOR = 0.0f;
    private const bool DEFAULT_DECAY_HFLIMIT = true;

    /* Default Presets */

    public static ReverbProperties Generic => new(1.0000f, 1.0000f, 0.3162f, 0.8913f, 1.0000f, 1.4900f, 0.8300f,
        1.0000f, 0.0500f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties PaddedCell => new(0.1715f, 1.0000f, 0.3162f, 0.0010f, 1.0000f, 0.1700f, 0.1000f,
        1.0000f, 0.2500f, 0.0010f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2691f, 0.0020f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Room => new(0.4287f, 1.0000f, 0.3162f, 0.5929f, 1.0000f, 0.4000f, 0.8300f, 1.0000f,
        0.1503f, 0.0020f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.0629f, 0.0030f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Bathroom => new(0.1715f, 1.0000f, 0.3162f, 0.2512f, 1.0000f, 1.4900f, 0.5400f,
        1.0000f, 0.6531f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 3.2734f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties LivingRoom => new(0.9766f, 1.0000f, 0.3162f, 0.0010f, 1.0000f, 0.5000f, 0.1000f,
        1.0000f, 0.2051f, 0.0030f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2805f, 0.0040f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties StoneRoom => new(1.0000f, 1.0000f, 0.3162f, 0.7079f, 1.0000f, 2.3100f, 0.6400f,
        1.0000f, 0.4411f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1003f, 0.0170f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Auditorium => new(1.0000f, 1.0000f, 0.3162f, 0.5781f, 1.0000f, 4.3200f, 0.5900f,
        1.0000f, 0.4032f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7170f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties ConcertHall => new(1.0000f, 1.0000f, 0.3162f, 0.5623f, 1.0000f, 3.9200f, 0.7000f,
        1.0000f, 0.2427f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.9977f, 0.0290f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Cave => new(1.0000f, 1.0000f, 0.3162f, 1.0000f, 1.0000f, 2.9100f, 1.3000f, 1.0000f,
        0.5000f, 0.0150f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7063f, 0.0220f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties Arena => new(1.0000f, 1.0000f, 0.3162f, 0.4477f, 1.0000f, 7.2400f, 0.3300f, 1.0000f,
        0.2612f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.0186f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Hangar => new(1.0000f, 1.0000f, 0.3162f, 0.3162f, 1.0000f, 10.0500f, 0.2300f,
        1.0000f, 0.5000f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2560f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties CarpetedHallway => new(0.4287f, 1.0000f, 0.3162f, 0.0100f, 1.0000f, 0.3000f, 0.1000f,
        1.0000f, 0.1215f, 0.0020f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1531f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Hallway => new(0.3645f, 1.0000f, 0.3162f, 0.7079f, 1.0000f, 1.4900f, 0.5900f,
        1.0000f, 0.2458f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.6615f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties StoneCorridor => new(1.0000f, 1.0000f, 0.3162f, 0.7612f, 1.0000f, 2.7000f, 0.7900f,
        1.0000f, 0.2472f, 0.0130f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.5758f, 0.0200f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Alley => new(1.0000f, 0.3000f, 0.3162f, 0.7328f, 1.0000f, 1.4900f, 0.8600f, 1.0000f,
        0.2500f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.9954f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1250f, 0.9500f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Forest => new(1.0000f, 0.3000f, 0.3162f, 0.0224f, 1.0000f, 1.4900f, 0.5400f, 1.0000f,
        0.0525f, 0.1620f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7682f, 0.0880f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1250f, 1.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties City => new(1.0000f, 0.5000f, 0.3162f, 0.3981f, 1.0000f, 1.4900f, 0.6700f, 1.0000f,
        0.0730f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1427f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Mountains => new(1.0000f, 0.2700f, 0.3162f, 0.0562f, 1.0000f, 1.4900f, 0.2100f,
        1.0000f, 0.0407f, 0.3000f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1919f, 0.1000f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 1.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties Quarry => new(1.0000f, 1.0000f, 0.3162f, 0.3162f, 1.0000f, 1.4900f, 0.8300f, 1.0000f,
        0.0000f, 0.0610f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.7783f, 0.0250f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1250f, 0.7000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Plain => new(1.0000f, 0.2100f, 0.3162f, 0.1000f, 1.0000f, 1.4900f, 0.5000f, 1.0000f,
        0.0585f, 0.1790f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1089f, 0.1000f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 1.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties ParkingLot => new(1.0000f, 1.0000f, 0.3162f, 1.0000f, 1.0000f, 1.6500f, 1.5000f,
        1.0000f, 0.2082f, 0.0080f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2652f, 0.0120f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties SewerPipe => new(0.3071f, 0.8000f, 0.3162f, 0.3162f, 1.0000f, 2.8100f, 0.1400f,
        1.0000f, 1.6387f, 0.0140f, new Vector3(0.0000f, 0.0000f, 0.0000f), 3.2471f, 0.0210f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Underwater => new(0.3645f, 1.0000f, 0.3162f, 0.0100f, 1.0000f, 1.4900f, 0.1000f,
        1.0000f, 0.5963f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 7.0795f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 1.1800f, 0.3480f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties Drugged => new(0.4287f, 0.5000f, 0.3162f, 1.0000f, 1.0000f, 8.3900f, 1.3900f,
        1.0000f, 0.8760f, 0.0020f, new Vector3(0.0000f, 0.0000f, 0.0000f), 3.1081f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 1.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties Dizzy => new(0.3645f, 0.6000f, 0.3162f, 0.6310f, 1.0000f, 17.2300f, 0.5600f, 1.0000f,
        0.1392f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.4937f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 1.0000f, 0.8100f, 0.3100f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties Psychotic => new(0.0625f, 0.5000f, 0.3162f, 0.8404f, 1.0000f, 7.5600f, 0.9100f,
        1.0000f, 0.4864f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 2.4378f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 4.0000f, 1.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);


/* Castle Presets */

    public static ReverbProperties CastleSmallRoom => new(1.0000f, 0.8900f, 0.3162f, 0.3981f, 0.1000f, 1.2200f, 0.8300f,
        0.3100f, 0.8913f, 0.0220f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.9953f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1380f, 0.0800f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

    public static ReverbProperties CastleShortPassage => new(1.0000f, 0.8900f, 0.3162f, 0.3162f, 0.1000f, 2.3200f,
        0.8300f, 0.3100f, 0.8913f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0230f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1380f, 0.0800f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

    public static ReverbProperties CastleMediumRoom => new(1.0000f, 0.9300f, 0.3162f, 0.2818f, 0.1000f, 2.0400f,
        0.8300f, 0.4600f, 0.6310f, 0.0220f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.5849f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1550f, 0.0300f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

    public static ReverbProperties CastleLargeRoom => new(1.0000f, 0.8200f, 0.3162f, 0.2818f, 0.1259f, 2.5300f, 0.8300f,
        0.5000f, 0.4467f, 0.0340f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0160f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1850f, 0.0700f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

    public static ReverbProperties CastleLongPassage => new(1.0000f, 0.8900f, 0.3162f, 0.3981f, 0.1000f, 3.4200f,
        0.8300f, 0.3100f, 0.8913f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0230f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1380f, 0.0800f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

    public static ReverbProperties CastleHall => new(1.0000f, 0.8100f, 0.3162f, 0.2818f, 0.1778f, 3.1400f, 0.7900f,
        0.6200f, 0.1778f, 0.0560f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0240f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

    public static ReverbProperties CastleCupboard => new(1.0000f, 0.8900f, 0.3162f, 0.2818f, 0.1000f, 0.6700f, 0.8700f,
        0.3100f, 1.4125f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 3.5481f, 0.0070f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1380f, 0.0800f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

    public static ReverbProperties CastleCourtyard => new(1.0000f, 0.4200f, 0.3162f, 0.4467f, 0.1995f, 2.1300f, 0.6100f,
        0.2300f, 0.2239f, 0.1600f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7079f, 0.0360f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.3700f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties CastleAlcove => new(1.0000f, 0.8900f, 0.3162f, 0.5012f, 0.1000f, 1.6400f, 0.8700f,
        0.3100f, 1.0000f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0340f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1380f, 0.0800f, 0.2500f, 0.0000f, 0.9943f, 5168.6001f, 139.5000f,
        0.0000f, true);

/* Factory Presets */

    public static ReverbProperties FactorySmallRoom => new(0.3645f, 0.8200f, 0.3162f, 0.7943f, 0.5012f, 1.7200f,
        0.6500f, 1.3100f, 0.7079f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.7783f, 0.0240f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1190f, 0.0700f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryShortPassage => new(0.3645f, 0.6400f, 0.2512f, 0.7943f, 0.5012f, 2.5300f,
        0.6500f, 1.3100f, 1.0000f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0380f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1350f, 0.2300f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryMediumRoom => new(0.4287f, 0.8200f, 0.2512f, 0.7943f, 0.5012f, 2.7600f,
        0.6500f, 1.3100f, 0.2818f, 0.0220f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0230f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1740f, 0.0700f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryLargeRoom => new(0.4287f, 0.7500f, 0.2512f, 0.7079f, 0.6310f, 4.2400f,
        0.5100f, 1.3100f, 0.1778f, 0.0390f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0230f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2310f, 0.0700f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryLongPassage => new(0.3645f, 0.6400f, 0.2512f, 0.7943f, 0.5012f, 4.0600f,
        0.6500f, 1.3100f, 1.0000f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0370f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1350f, 0.2300f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryHall => new(0.4287f, 0.7500f, 0.3162f, 0.7079f, 0.6310f, 7.4300f, 0.5100f,
        1.3100f, 0.0631f, 0.0730f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.8913f, 0.0270f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0700f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryCupboard => new(0.3071f, 0.6300f, 0.2512f, 0.7943f, 0.5012f, 0.4900f, 0.6500f,
        1.3100f, 1.2589f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.9953f, 0.0320f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1070f, 0.0700f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryCourtyard => new(0.3071f, 0.5700f, 0.3162f, 0.3162f, 0.6310f, 2.3200f,
        0.2900f, 0.5600f, 0.2239f, 0.1400f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.3981f, 0.0390f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.2900f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);

    public static ReverbProperties FactoryAlcove => new(0.3645f, 0.5900f, 0.2512f, 0.7943f, 0.5012f, 3.1400f, 0.6500f,
        1.3100f, 1.4125f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.0000f, 0.0380f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1140f, 0.1000f, 0.2500f, 0.0000f, 0.9943f, 3762.6001f, 362.5000f,
        0.0000f, true);


/* Ice Palace Presets */

    public static ReverbProperties IcePalaceSmallRoom => new(1.0000f, 0.8400f, 0.3162f, 0.5623f, 0.2818f, 1.5100f,
        1.5300f, 0.2700f, 0.8913f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1640f, 0.1400f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceShortPassage => new(1.0000f, 0.7500f, 0.3162f, 0.5623f, 0.2818f, 1.7900f,
        1.4600f, 0.2800f, 0.5012f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0190f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1770f, 0.0900f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceMediumRoom => new(1.0000f, 0.8700f, 0.3162f, 0.5623f, 0.4467f, 2.2200f,
        1.5300f, 0.3200f, 0.3981f, 0.0390f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0270f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1860f, 0.1200f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceLargeRoom => new(1.0000f, 0.8100f, 0.3162f, 0.5623f, 0.4467f, 3.1400f,
        1.5300f, 0.3200f, 0.2512f, 0.0390f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.0000f, 0.0270f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2140f, 0.1100f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceLongPassage => new(1.0000f, 0.7700f, 0.3162f, 0.5623f, 0.3981f, 3.0100f,
        1.4600f, 0.2800f, 0.7943f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0250f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1860f, 0.0400f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceHall => new(1.0000f, 0.7600f, 0.3162f, 0.4467f, 0.5623f, 5.4900f, 1.5300f,
        0.3800f, 0.1122f, 0.0540f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.6310f, 0.0520f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2260f, 0.1100f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceCupboard => new(1.0000f, 0.8300f, 0.3162f, 0.5012f, 0.2239f, 0.7600f,
        1.5300f, 0.2600f, 1.1220f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.9953f, 0.0160f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1430f, 0.0800f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceCourtyard => new(1.0000f, 0.5900f, 0.3162f, 0.2818f, 0.3162f, 2.0400f,
        1.2000f, 0.3800f, 0.3162f, 0.1730f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.3162f, 0.0430f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2350f, 0.4800f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties IcePalaceAlcove => new(1.0000f, 0.8400f, 0.3162f, 0.5623f, 0.2818f, 2.7600f, 1.4600f,
        0.2800f, 1.1220f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.8913f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1610f, 0.0900f, 0.2500f, 0.0000f, 0.9943f, 12428.5000f, 99.6000f,
        0.0000f, true);

/* Space Station Presets */

    public static ReverbProperties SpaceStationSmallRoom => new(0.2109f, 0.7000f, 0.3162f, 0.7079f, 0.8913f, 1.7200f,
        0.8200f, 0.5500f, 0.7943f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0130f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1880f, 0.2600f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);

    public static ReverbProperties SpaceStationShortPassage => new(0.2109f, 0.8700f, 0.3162f, 0.6310f, 0.8913f, 3.5700f,
        0.5000f, 0.5500f, 1.0000f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0160f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1720f, 0.2000f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);

    public static ReverbProperties SpaceStationMediumRoom => new(0.2109f, 0.7500f, 0.3162f, 0.6310f, 0.8913f, 3.0100f,
        0.5000f, 0.5500f, 0.3981f, 0.0340f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0350f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2090f, 0.3100f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);

    public static ReverbProperties SpaceStationLargeRoom => new(0.3645f, 0.8100f, 0.3162f, 0.6310f, 0.8913f, 3.8900f,
        0.3800f, 0.6100f, 0.3162f, 0.0560f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.8913f, 0.0350f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2330f, 0.2800f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);

    public static ReverbProperties SpaceStationLongPassage => new(0.4287f, 0.8200f, 0.3162f, 0.6310f, 0.8913f, 4.6200f,
        0.6200f, 0.5500f, 1.0000f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0310f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.2300f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);

    public static ReverbProperties SpaceStationHall => new(0.4287f, 0.8700f, 0.3162f, 0.6310f, 0.8913f, 7.1100f,
        0.3800f, 0.6100f, 0.1778f, 0.1000f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.6310f, 0.0470f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.2500f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);

    public static ReverbProperties SpaceStationCupboard => new(0.1715f, 0.5600f, 0.3162f, 0.7079f, 0.8913f, 0.7900f,
        0.8100f, 0.5500f, 1.4125f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.7783f, 0.0180f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1810f, 0.3100f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);

    public static ReverbProperties SpaceStationAlcove => new(0.2109f, 0.7800f, 0.3162f, 0.7079f, 0.8913f, 1.1600f,
        0.8100f, 0.5500f, 1.4125f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.0000f, 0.0180f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1920f, 0.2100f, 0.2500f, 0.0000f, 0.9943f, 3316.1001f, 458.2000f,
        0.0000f, true);


/* Wooden Galleon Presets */

    public static ReverbProperties WoodenSmallRoom => new(1.0000f, 1.0000f, 0.3162f, 0.1122f, 0.3162f, 0.7900f, 0.3200f,
        0.8700f, 1.0000f, 0.0320f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.8913f, 0.0290f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenShortPassage => new(1.0000f, 1.0000f, 0.3162f, 0.1259f, 0.3162f, 1.7500f,
        0.5000f, 0.8700f, 0.8913f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.6310f, 0.0240f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenMediumRoom => new(1.0000f, 1.0000f, 0.3162f, 0.1000f, 0.2818f, 1.4700f,
        0.4200f, 0.8200f, 0.8913f, 0.0490f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.8913f, 0.0290f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenLargeRoom => new(1.0000f, 1.0000f, 0.3162f, 0.0891f, 0.2818f, 2.6500f, 0.3300f,
        0.8200f, 0.8913f, 0.0660f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7943f, 0.0490f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenLongPassage => new(1.0000f, 1.0000f, 0.3162f, 0.1000f, 0.3162f, 1.9900f,
        0.4000f, 0.7900f, 1.0000f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.4467f, 0.0360f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenHall => new(1.0000f, 1.0000f, 0.3162f, 0.0794f, 0.2818f, 3.4500f, 0.3000f,
        0.8200f, 0.8913f, 0.0880f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7943f, 0.0630f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenCupboard => new(1.0000f, 1.0000f, 0.3162f, 0.1413f, 0.3162f, 0.5600f, 0.4600f,
        0.9100f, 1.1220f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0280f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenCourtyard => new(1.0000f, 0.6500f, 0.3162f, 0.0794f, 0.3162f, 1.7900f, 0.3500f,
        0.7900f, 0.5623f, 0.1230f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1000f, 0.0320f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

    public static ReverbProperties WoodenAlcove => new(1.0000f, 1.0000f, 0.3162f, 0.1259f, 0.3162f, 1.2200f, 0.6200f,
        0.9100f, 1.1220f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7079f, 0.0240f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 4705.0000f, 99.6000f,
        0.0000f, true);

/* Sports Presets */

    public static ReverbProperties SportEmptyStadium => new(1.0000f, 1.0000f, 0.3162f, 0.4467f, 0.7943f, 6.2600f,
        0.5100f, 1.1000f, 0.0631f, 0.1830f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.3981f, 0.0380f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties SportSquashCourt => new(1.0000f, 0.7500f, 0.3162f, 0.3162f, 0.7943f, 2.2200f,
        0.9100f, 1.1600f, 0.4467f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7943f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1260f, 0.1900f, 0.2500f, 0.0000f, 0.9943f, 7176.8999f, 211.2000f,
        0.0000f, true);

    public static ReverbProperties SportSmallSwimmingPool => new(1.0000f, 0.7000f, 0.3162f, 0.7943f, 0.8913f, 2.7600f,
        1.2500f, 1.1400f, 0.6310f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7943f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1790f, 0.1500f, 0.8950f, 0.1900f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties SportLargeSwimmingPool => new(1.0000f, 0.8200f, 0.3162f, 0.7943f, 1.0000f, 5.4900f,
        1.3100f, 1.1400f, 0.4467f, 0.0390f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.5012f, 0.0490f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2220f, 0.5500f, 1.1590f, 0.2100f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties SportGymnasium => new(1.0000f, 0.8100f, 0.3162f, 0.4467f, 0.8913f, 3.1400f, 1.0600f,
        1.3500f, 0.3981f, 0.0290f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.5623f, 0.0450f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1460f, 0.1400f, 0.2500f, 0.0000f, 0.9943f, 7176.8999f, 211.2000f,
        0.0000f, true);

    public static ReverbProperties SportFullStadium => new(1.0000f, 1.0000f, 0.3162f, 0.0708f, 0.7943f, 5.2500f,
        0.1700f, 0.8000f, 0.1000f, 0.1880f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2818f, 0.0380f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties SportStadiumTannoy => new(1.0000f, 0.7800f, 0.3162f, 0.5623f, 0.5012f, 2.5300f,
        0.8800f, 0.6800f, 0.2818f, 0.2300f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.5012f, 0.0630f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.2000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

/* Prefab Presets */

    public static ReverbProperties PrefabWorkshop => new(0.4287f, 1.0000f, 0.3162f, 0.1413f, 0.3981f, 0.7600f, 1.0000f,
        1.0000f, 1.0000f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0120f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties PrefabSchoolRoom => new(0.4022f, 0.6900f, 0.3162f, 0.6310f, 0.5012f, 0.9800f,
        0.4500f, 0.1800f, 1.4125f, 0.0170f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0150f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.0950f, 0.1400f, 0.2500f, 0.0000f, 0.9943f, 7176.8999f, 211.2000f,
        0.0000f, true);

    public static ReverbProperties PrefabPracticeRoom => new(0.4022f, 0.8700f, 0.3162f, 0.3981f, 0.5012f, 1.1200f,
        0.5600f, 0.1800f, 1.2589f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0110f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.0950f, 0.1400f, 0.2500f, 0.0000f, 0.9943f, 7176.8999f, 211.2000f,
        0.0000f, true);

    public static ReverbProperties PrefabOuthouse => new(1.0000f, 0.8200f, 0.3162f, 0.1122f, 0.1585f, 1.3800f, 0.3800f,
        0.3500f, 0.8913f, 0.0240f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.6310f, 0.0440f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1210f, 0.1700f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 107.5000f,
        0.0000f, false);

    public static ReverbProperties PrefabCaravan => new(1.0000f, 1.0000f, 0.3162f, 0.0891f, 0.1259f, 0.4300f, 1.5000f,
        1.0000f, 1.0000f, 0.0120f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.9953f, 0.0120f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

/* Dome and Pipe Presets */

    public static ReverbProperties DomeTomb => new(1.0000f, 0.7900f, 0.3162f, 0.3548f, 0.2239f, 4.1800f, 0.2100f,
        0.1000f, 0.3868f, 0.0300f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.6788f, 0.0220f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1770f, 0.1900f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 20.0000f,
        0.0000f, false);

    public static ReverbProperties PipeSmall => new(1.0000f, 1.0000f, 0.3162f, 0.3548f, 0.2239f, 5.0400f, 0.1000f,
        0.1000f, 0.5012f, 0.0320f, new Vector3(0.0000f, 0.0000f, 0.0000f), 2.5119f, 0.0150f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 20.0000f,
        0.0000f, true);

    public static ReverbProperties DomeSaintPauls => new(1.0000f, 0.8700f, 0.3162f, 0.3548f, 0.2239f, 10.4800f, 0.1900f,
        0.1000f, 0.1778f, 0.0900f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0420f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.1200f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 20.0000f,
        0.0000f, true);

    public static ReverbProperties PipeLongThin => new(0.2560f, 0.9100f, 0.3162f, 0.4467f, 0.2818f, 9.2100f, 0.1800f,
        0.1000f, 0.7079f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7079f, 0.0220f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 20.0000f,
        0.0000f, false);

    public static ReverbProperties PipeLarge => new(1.0000f, 1.0000f, 0.3162f, 0.3548f, 0.2239f, 8.4500f, 0.1000f,
        0.1000f, 0.3981f, 0.0460f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.5849f, 0.0320f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 20.0000f,
        0.0000f, true);

    public static ReverbProperties PipeResonant => new(0.1373f, 0.9100f, 0.3162f, 0.4467f, 0.2818f, 6.8100f, 0.1800f,
        0.1000f, 0.7079f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.0000f, 0.0220f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 20.0000f,
        0.0000f, false);

/* Outdoors Presets */

    public static ReverbProperties OutdoorsBackyard => new(1.0000f, 0.4500f, 0.3162f, 0.2512f, 0.5012f, 1.1200f,
        0.3400f, 0.4600f, 0.4467f, 0.0690f, new Vector3(0.0000f, 0.0000f, -0.0000f), 0.7079f, 0.0230f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2180f, 0.3400f, 0.2500f, 0.0000f, 0.9943f, 4399.1001f, 242.9000f,
        0.0000f, false);

    public static ReverbProperties OutdoorsRollingPlains => new(1.0000f, 0.0000f, 0.3162f, 0.0112f, 0.6310f, 2.1300f,
        0.2100f, 0.4600f, 0.1778f, 0.3000f, new Vector3(0.0000f, 0.0000f, -0.0000f), 0.4467f, 0.0190f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 1.0000f, 0.2500f, 0.0000f, 0.9943f, 4399.1001f, 242.9000f,
        0.0000f, false);

    public static ReverbProperties OutdoorsDeepCanyon => new(1.0000f, 0.7400f, 0.3162f, 0.1778f, 0.6310f, 3.8900f,
        0.2100f, 0.4600f, 0.3162f, 0.2230f, new Vector3(0.0000f, 0.0000f, -0.0000f), 0.3548f, 0.0190f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 1.0000f, 0.2500f, 0.0000f, 0.9943f, 4399.1001f, 242.9000f,
        0.0000f, false);

    public static ReverbProperties OutdoorsCreek => new(1.0000f, 0.3500f, 0.3162f, 0.1778f, 0.5012f, 2.1300f, 0.2100f,
        0.4600f, 0.3981f, 0.1150f, new Vector3(0.0000f, 0.0000f, -0.0000f), 0.1995f, 0.0310f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2180f, 0.3400f, 0.2500f, 0.0000f, 0.9943f, 4399.1001f, 242.9000f,
        0.0000f, false);

    public static ReverbProperties OutdoorsValley => new(1.0000f, 0.2800f, 0.3162f, 0.0282f, 0.1585f, 2.8800f, 0.2600f,
        0.3500f, 0.1413f, 0.2630f, new Vector3(0.0000f, 0.0000f, -0.0000f), 0.3981f, 0.1000f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.3400f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 107.5000f,
        0.0000f, false);

/* Mood Presets */

    public static ReverbProperties MoodHeaven => new(1.0000f, 0.9400f, 0.3162f, 0.7943f, 0.4467f, 5.0400f, 1.1200f,
        0.5600f, 0.2427f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0290f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0800f, 2.7420f, 0.0500f, 0.9977f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties MoodHell => new(1.0000f, 0.5700f, 0.3162f, 0.3548f, 0.4467f, 3.5700f, 0.4900f,
        2.0000f, 0.0000f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1100f, 0.0400f, 2.1090f, 0.5200f, 0.9943f, 5000.0000f, 139.5000f,
        0.0000f, false);

    public static ReverbProperties MoodMemory => new(1.0000f, 0.8500f, 0.3162f, 0.6310f, 0.3548f, 4.0600f, 0.8200f,
        0.5600f, 0.0398f, 0.0000f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.1220f, 0.0000f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.4740f, 0.4500f, 0.9886f, 5000.0000f, 250.0000f,
        0.0000f, false);

/* Driving Presets */

    public static ReverbProperties DrivingCommentator => new(1.0000f, 0.0000f, 0.3162f, 0.5623f, 0.5012f, 2.4200f,
        0.8800f, 0.6800f, 0.1995f, 0.0930f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2512f, 0.0170f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 1.0000f, 0.2500f, 0.0000f, 0.9886f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties DrivingPitGarage => new(0.4287f, 0.5900f, 0.3162f, 0.7079f, 0.5623f, 1.7200f,
        0.9300f, 0.8700f, 0.5623f, 0.0000f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0160f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.1100f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties DrivingInCarRacer => new(0.0832f, 0.8000f, 0.3162f, 1.0000f, 0.7943f, 0.1700f,
        2.0000f, 0.4100f, 1.7783f, 0.0070f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7079f, 0.0150f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 10268.2002f, 251.0000f,
        0.0000f, true);

    public static ReverbProperties DrivingInCarSports => new(0.0832f, 0.8000f, 0.3162f, 0.6310f, 1.0000f, 0.1700f,
        0.7500f, 0.4100f, 1.0000f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.5623f, 0.0000f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 10268.2002f, 251.0000f,
        0.0000f, true);

    public static ReverbProperties DrivingInCarLuxury => new(0.2560f, 1.0000f, 0.3162f, 0.1000f, 0.5012f, 0.1300f,
        0.4100f, 0.4600f, 0.7943f, 0.0100f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.5849f, 0.0100f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 10268.2002f, 251.0000f,
        0.0000f, true);

    public static ReverbProperties DrivingFullGrandstand => new(1.0000f, 1.0000f, 0.3162f, 0.2818f, 0.6310f, 3.0100f,
        1.3700f, 1.2800f, 0.3548f, 0.0900f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1778f, 0.0490f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 10420.2002f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties DrivingEmptyGrandstand => new(1.0000f, 1.0000f, 0.3162f, 1.0000f, 0.7943f, 4.6200f,
        1.7500f, 1.4000f, 0.2082f, 0.0900f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2512f, 0.0490f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.0000f, 0.9943f, 10420.2002f, 250.0000f,
        0.0000f, false);

    public static ReverbProperties DrivingTunnel => new(1.0000f, 0.8100f, 0.3162f, 0.3981f, 0.8913f, 3.4200f, 0.9400f,
        1.3100f, 0.7079f, 0.0510f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7079f, 0.0470f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2140f, 0.0500f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 155.3000f,
        0.0000f, true);

/* City Presets */

    public static ReverbProperties CityStreets => new(1.0000f, 0.7800f, 0.3162f, 0.7079f, 0.8913f, 1.7900f, 1.1200f,
        0.9100f, 0.2818f, 0.0460f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1995f, 0.0280f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.2000f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties CitySubway => new(1.0000f, 0.7400f, 0.3162f, 0.7079f, 0.8913f, 3.0100f, 1.2300f,
        0.9100f, 0.7079f, 0.0460f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0280f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1250f, 0.2100f, 0.2500f, 0.0000f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties CityMuseum => new(1.0000f, 0.8200f, 0.3162f, 0.1778f, 0.1778f, 3.2800f, 1.4000f,
        0.5700f, 0.2512f, 0.0390f, new Vector3(0.0000f, 0.0000f, -0.0000f), 0.8913f, 0.0340f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1300f, 0.1700f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 107.5000f,
        0.0000f, false);

    public static ReverbProperties CityLibrary => new(1.0000f, 0.8200f, 0.3162f, 0.2818f, 0.0891f, 2.7600f, 0.8900f,
        0.4100f, 0.3548f, 0.0290f, new Vector3(0.0000f, 0.0000f, -0.0000f), 0.8913f, 0.0200f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1300f, 0.1700f, 0.2500f, 0.0000f, 0.9943f, 2854.3999f, 107.5000f,
        0.0000f, false);

    public static ReverbProperties CityUnderpass => new(1.0000f, 0.8200f, 0.3162f, 0.4467f, 0.8913f, 3.5700f, 1.1200f,
        0.9100f, 0.3981f, 0.0590f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.8913f, 0.0370f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.1400f, 0.2500f, 0.0000f, 0.9920f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties CityAbandoned => new(1.0000f, 0.6900f, 0.3162f, 0.7943f, 0.8913f, 3.2800f, 1.1700f,
        0.9100f, 0.4467f, 0.0440f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2818f, 0.0240f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.2000f, 0.2500f, 0.0000f, 0.9966f, 5000.0000f, 250.0000f,
        0.0000f, true);

/* Misc. Presets */

    public static ReverbProperties DustyRoom => new(0.3645f, 0.5600f, 0.3162f, 0.7943f, 0.7079f, 1.7900f, 0.3800f,
        0.2100f, 0.5012f, 0.0020f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.2589f, 0.0060f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2020f, 0.0500f, 0.2500f, 0.0000f, 0.9886f, 13046.0000f, 163.3000f,
        0.0000f, true);

    public static ReverbProperties Chapel => new(1.0000f, 0.8400f, 0.3162f, 0.5623f, 1.0000f, 4.6200f, 0.6400f, 1.2300f,
        0.4467f, 0.0320f, new Vector3(0.0000f, 0.0000f, 0.0000f), 0.7943f, 0.0490f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.2500f, 0.0000f, 0.2500f, 0.1100f, 0.9943f, 5000.0000f, 250.0000f,
        0.0000f, true);

    public static ReverbProperties SmallWaterRoom => new(1.0000f, 0.7000f, 0.3162f, 0.4477f, 1.0000f, 1.5100f, 1.2500f,
        1.1400f, 0.8913f, 0.0200f, new Vector3(0.0000f, 0.0000f, 0.0000f), 1.4125f, 0.0300f,
        new Vector3(0.0000f, 0.0000f, 0.0000f), 0.1790f, 0.1500f, 0.8950f, 0.1900f, 0.9920f, 5000.0000f, 250.0000f,
        0.0000f, false);

}
