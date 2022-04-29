using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Describes the type of metadata string that can be associated with an audio file.
/// </summary>
[PublicAPI]
public enum StringType
{
    /// <summary>
    /// The title of the song/audio.
    /// </summary>
    Title,
    
    /// <summary>
    /// The copyright string.
    /// </summary>
    Copyright,
    
    /// <summary>
    /// The software used to encode the audio/metadata.
    /// </summary>
    Software,
    
    /// <summary>
    /// The artist/creator of the song/audio.
    /// </summary>
    Artist,
    
    /// <summary>
    /// Arbitrary comment.
    /// </summary>
    Comment,
    
    /// <summary>
    /// The date of the song/audio initial release/creation.
    /// </summary>
    Date,
    
    /// <summary>
    /// The associated album for the song/audio.
    /// </summary>
    Album,
    
    /// <summary>
    /// Applicable license issued for the audio's use.
    /// </summary>
    License,
    
    /// <summary>
    /// The track number of the song/audio.
    /// </summary>
    TrackNumber,
    
    /// <summary>
    /// A genre associated with the song/audio.
    /// </summary>
    Genre
}