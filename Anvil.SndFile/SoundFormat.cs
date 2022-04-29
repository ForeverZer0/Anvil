using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Describes the major format of an audio source.
/// </summary>
[PublicAPI]
public enum SoundFormat
{
    /// <summary>
    /// Microsoft WAV format (little endian default).
    /// </summary>
    Wav = 0x010000,

    /// <summary>
    /// Apple/SGI AIFF format (big endian).
    /// </summary>
    Aiff = 0x020000,

    /// <summary>
    /// Sun/NeXT AU format (big endian).
    /// </summary>
    Au = 0x030000,

    /// <summary>
    /// RAW PCM data.
    /// </summary>
    Raw = 0x040000,

    /// <summary>
    /// Ensoniq PARIS file format.
    /// </summary>
    Paf = 0x050000,

    /// <summary>
    /// Amiga IFF / SVX8 / SV16 format.
    /// </summary>
    Svx = 0x060000,

    /// <summary>
    /// Sphere NIST format.
    /// </summary>
    Nist = 0x070000,

    /// <summary>
    /// VOC files.
    /// </summary>
    Voc = 0x080000,

    /// <summary>
    /// Berkeley/IRCAM/CARL
    /// </summary>
    Ircam = 0x0A0000,

    /// <summary>
    /// Sonic Foundry's 64 bit RIFF/WAV
    /// </summary>
    W64 = 0x0B0000,

    /// <summary>
    /// Matlab (tm) V4.2 / GNU Octave 2.0
    /// </summary>
    Mat4 = 0x0C0000,

    /// <summary>
    /// Matlab (tm) V5.0 / GNU Octave 2.1
    /// </summary>
    Mat5 = 0x0D0000,

    /// <summary>
    /// Portable Voice Format
    /// </summary>
    Pvf = 0x0E0000,

    /// <summary>
    /// Fasttracker 2 Extended Instrument
    /// </summary>
    Xi = 0x0F0000,

    /// <summary>
    /// HMM Tool Kit format
    /// </summary>
    Htk = 0x100000,

    /// <summary>
    /// Midi Sample Dump Standard
    /// </summary>
    Sds = 0x110000,

    /// <summary>
    /// Audio Visual Research
    /// </summary>
    Avr = 0x120000,

    /// <summary>
    /// MS WAVE with WAVEFORMATEX
    /// </summary>
    Wavex = 0x130000,

    /// <summary>
    /// Sound Designer 2
    /// </summary>
    Sd2 = 0x160000,

    /// <summary>
    /// FLAC lossless file format
    /// </summary>
    Flac = 0x170000,

    /// <summary>
    /// Core Audio File format
    /// </summary>
    Caf = 0x180000,

    /// <summary>
    /// Psion WVE format
    /// </summary>
    Wve = 0x190000,

    /// <summary>
    /// Xiph OGG container
    /// </summary>
    Ogg = 0x200000,

    /// <summary>
    /// Akai MPC 2000 sampler
    /// </summary>
    Mpc2K = 0x210000,

    /// <summary>
    /// RF64 WAV file
    /// </summary>
    Rf64 = 0x220000,

    /// <summary>
    /// MPEG-1/2 audio stream
    /// </summary>
    Mpeg = 0x230000,
}