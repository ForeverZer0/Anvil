using JetBrains.Annotations;

namespace Anvil.OpenAL;

/// <summary>
/// Describes the different types of <see cref="Effect"/> objects that can be created.
/// </summary>
[PublicAPI]
public enum EffectType
{
    /// <summary>
    /// No effect.
    /// </summary>
    None = 0x0000,
	
    /// <summary>
    /// the standard Effects Extension’s environmental reverberation effect. 
    /// </summary>
    Reverb = 0x0001,
	
    /// <summary>
    /// The chorus effect essentially replays the input audio accompanied by another slightly delayed version of the
    /// signal, creating a ‘doubling’ effect. This was originally intended to emulate the effect of several
    /// musicians playing the same notes simultaneously, to create a thicker, more satisfying sound.
    /// <para/>
    /// To add some variation to the effect, the delay time of the delayed versions of the input signal is modulated by
    /// an adjustable oscillating waveform. This causes subtle shifts in the pitch of the delayed signals, emphasizing
    /// the thickening effect. 
    /// </summary>
    Chorus = 0x0002,
	
    /// <summary>
    /// The distortion effect simulates turning up (overdriving) the gain stage on a guitar amplifier or adding a
    /// distortion pedal to an instrument’s output. It is achieved by clipping the signal (adding more square wave-like
    /// components) and adding rich harmonics.
    /// <para/>
    /// The distortion effect could be very useful for adding extra dynamics to engine sounds in a driving simulator,
    /// or modifying samples such as vocal communications. 
    /// <para/>
    /// The OpenAL Effects Extension distortion effect also includes EQ on the output signal, to help ‘rein in’
    /// excessive frequency content in distorted audio. A low-pass filter is applied to input signal before the
    /// distortion effect, to limit excessive distorted signals at high frequencies. 
    /// </summary>
    Distortion = 0x0003,
	
    /// <summary>
    /// The echo effect generates discrete, delayed instances of the input signal. The amount of delay and feedback is
    /// controllable. The delay is ‘two tap’ – you can control the interaction between two separate instances of echoes. 
    /// </summary>
    Echo = 0x0004,
	
    /// <summary>
    /// The flanger effect creates a “tearing” or “whooshing” sound (like a jet flying overhead). It works by sampling
    /// a portion of the input signal, delaying it by a period modulated between 0 and 4ms by a low-frequency
    /// oscillator, and then mixing it with the source signal. 
    /// </summary>
    Flanger = 0x0005,
	
    /// <summary>
    /// The frequency shifter is a single-sideband modulator, which translates all the component frequencies of the
    /// input signal by an equal amount. Unlike the pitch shifter, which attempts to maintain harmonic relationships in
    /// the signal, the frequency shifter disrupts harmonic relationships and radically alters the sonic qualities of
    /// the signal. Applications of the frequency shifter include the creation of bizarre distortion, phaser, stereo
    /// widening and rotating speaker effects. 
    /// </summary>
    FrequencyShifter = 0x0006,
	
    /// <summary>
    /// The vocal morpher consists of a pair of 4-band formant filters, used to impose vocal tract effects upon the
    /// input signal. If the input signal is a broadband sound such as pink noise or a car engine, the vocal morpher can
    /// provide a wide variety of filtering effects. A low-frequency oscillator can be used to morph the filtering
    /// effect between two different phonemes. The vocal morpher is not necessarily intended for use on voice signals;
    /// it is primarily intended for pitched noise effects, vocal-like wind effects, etc.
    /// </summary>
    VocalMorpher = 0x0007,
	
    /// <summary>
    /// The pitch shifter applies time-invariant pitch shifting to the input signal, over a one octave range and
    /// controllable at a semi-tone and cent resolution.
    /// </summary>
    PitchShifter = 0x0008,
	
    /// <summary>
    /// The ring modulator multiplies an input signal by a carrier signal in the time domain, resulting in tremolo or
    /// inharmonic effects. 
    /// </summary>
    RingModulator = 0x0009,
	
    /// <summary>
    /// The Auto-wah effect emulates the sound of a wah-wah pedal used with an electric guitar, or a mute on a brass
    /// instrument. Such effects allow a musician to control the tone of their instrument by varying the point at which
    /// high frequencies are cut off. This OpenAL Effects Extension effect is called Auto-wah because there is no user
    /// input for modulating the cut-off point. Instead the effect is achieved by analysing the input signal, and
    /// applying a band-pass filter according the intensity of the incoming audio. 
    /// </summary>
    Autowah = 0x000A,
	
    /// <summary>
    /// The Automatic Gain Control effect performs the same task as a studio compressor – evening out the audio dynamic
    /// range of an input sound. This results in audio exhibiting smaller variation in intensity between the loudest and
    /// quietest portions. The AGC Compressor will boost quieter portions of the audio, while louder portions will stay
    /// the same or may even be reduced. The Compressor effect cannot be tweaked in depth – it can just be switched on
    /// and off.
    /// </summary>
    Compressor = 0x000B,
	
    /// <summary>
    /// The OpenAL Effects Extension EQ is very flexible, providing tonal control over four different adjustable
    /// frequency ranges. The lowest frequency range is called “low.” The middle ranges are called “mid1” and “mid2.”
    /// The high range is called “high.”
    /// </summary>
    Equalizer = 0x000C,
	
    /// <summary>
    /// A superset of the standard reverberation, contains additional parameters and provides closer control of the
    /// acoustic quality.
    /// </summary>
    EaxReverb = 0x8000,
}