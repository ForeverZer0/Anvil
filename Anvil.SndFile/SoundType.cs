using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Describes a more specialized sub-format of an audio source.
/// </summary>
[PublicAPI]
public enum SoundType
{
    /// <summary>
    /// Signed 8 bit data
    /// </summary>
    PcmS8 = 0x0001,

    /// <summary>
    /// Signed 16 bit data
    /// </summary>
    Pcm16 = 0x0002,

    /// <summary>
    /// Signed 24 bit data
    /// </summary>
    Pcm24 = 0x0003,

    /// <summary>
    /// Signed 32 bit data
    /// </summary>
    Pcm32 = 0x0004,

    /// <summary>
    /// Unsigned 8 bit data (WAV and RAW only)
    /// </summary>
    PcmU8 = 0x0005,

    /// <summary>
    /// 32 bit float data
    /// </summary>
    Float = 0x0006,

    /// <summary>
    /// 64 bit float data
    /// </summary>
    Double = 0x0007,

    /// <summary>
    /// U-Law encoded.
    /// </summary>
    Ulaw = 0x0010,

    /// <summary>
    /// A-Law encoded.
    /// </summary>
    Alaw = 0x0011,

    /// <summary>
    /// IMA ADPCM.
    /// </summary>
    ImaAdpcm = 0x0012,

    /// <summary>
    /// Microsoft ADPCM.
    /// </summary>
    MsAdpcm = 0x0013,

    /// <summary>
    /// GSM 6.10 encoding.
    /// </summary>
    Gsm610 = 0x0020,

    /// <summary>
    /// OKI / Dialogix ADPCM
    /// </summary>
    VoxAdpcm = 0x0021,

    /// <summary>
    /// 16kbs NMS G721-variant encoding.
    /// </summary>
    NmsAdpcm16 = 0x0022,

    /// <summary>
    /// 24kbs NMS G721-variant encoding.
    /// </summary>
    NmsAdpcm24 = 0x0023,

    /// <summary>
    /// 32kbs NMS G721-variant encoding.
    /// </summary>
    NmsAdpcm32 = 0x0024,

    /// <summary>
    /// 32kbs G721 ADPCM encoding.
    /// </summary>
    G72132 = 0x0030,

    /// <summary>
    /// 24kbs G723 ADPCM encoding.
    /// </summary>
    G72324 = 0x0031,

    /// <summary>
    /// 40kbs G723 ADPCM encoding.
    /// </summary>
    G72340 = 0x0032,

    /// <summary>
    /// 12 bit Delta Width Variable Word encoding.
    /// </summary>
    Dwvw12 = 0x0040,

    /// <summary>
    /// 16 bit Delta Width Variable Word encoding.
    /// </summary>
    Dwvw16 = 0x0041,

    /// <summary>
    /// 24 bit Delta Width Variable Word encoding.
    /// </summary>
    Dwvw24 = 0x0042,

    /// <summary>
    /// N bit Delta Width Variable Word encoding.
    /// </summary>
    DwvwN = 0x0043,

    /// <summary>
    /// 8 bit differential PCM (XI only)
    /// </summary>
    Dpcm8 = 0x0050,

    /// <summary>
    /// 16 bit differential PCM (XI only)
    /// </summary>
    Dpcm16 = 0x0051,

    /// <summary>
    /// Xiph Vorbis encoding.
    /// </summary>
    Vorbis = 0x0060,

    /// <summary>
    /// Xiph/Skype Opus encoding.
    /// </summary>
    Opus = 0x0064,

    /// <summary>
    /// Apple Lossless Audio Codec (16 bit).
    /// </summary>
    Alac16 = 0x0070,

    /// <summary>
    /// Apple Lossless Audio Codec (20 bit).
    /// </summary>
    Alac20 = 0x0071,

    /// <summary>
    /// Apple Lossless Audio Codec (24 bit).
    /// </summary>
    Alac24 = 0x0072,

    /// <summary>
    /// Apple Lossless Audio Codec (32 bit).
    /// </summary>
    Alac32 = 0x0073,

    /// <summary>
    /// MPEG-1 Audio Layer I
    /// </summary>
    MpegLayerI = 0x0080,

    /// <summary>
    /// MPEG-1 Audio Layer II
    /// </summary>
    MpegLayerIi = 0x0081,

    /// <summary>
    /// MPEG-2 Audio Layer III
    /// </summary>
    MpegLayerIii = 0x0082,
}