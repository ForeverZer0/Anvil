#if JSON_READING
using System.Text.Json;
#endif
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Describe which transformations can be applied to the tiles (e.g. to extend a Wang set by transforming existing
/// tiles) within a <see cref="Tileset"/>.
/// </summary>
[PublicAPI]
public sealed class Transformations : TiledEntity
{
    /// <summary>
    /// Gets or sets a flag indicating if the tiles in this parent <see cref="Tileset"/> can be flipped horizontally.
    /// </summary>
    public bool FlipHorizontal { get; set; }
    
    /// <summary>
    /// Gets or sets a flag indicating if the tiles in this parent <see cref="Tileset"/> can be flipped vertically.
    /// </summary>
    public bool FlipVertical { get; set; }
    
    /// <summary>
    /// Gets or sets a flag indicating if the tiles in this parent <see cref="Tileset"/> can be rotated.
    /// </summary>
    public bool Rotate { get; set; }
    
    /// <summary>
    /// Gets or sets a flag indicating if untransformed tiles remain preferred, otherwise transformed tiles are used to
    /// produce more variations. 
    /// </summary>
    public bool PreferUntransformed { get; set; }

    /// <summary>
    /// Creates a new default instance of the <see cref="Transformations"/> class.
    /// </summary>
    public Transformations() : base(Tag.Transformations)
    {
    }

#if JSON_READING
    internal Transformations(Utf8JsonReader reader) : base(Tag.Transformations)
    {
        while (ReadChild(reader, out var propertyName))
        {
            switch (propertyName)
            {
                case Tag.HFlip:
                    FlipHorizontal = reader.GetBoolean();
                    break;
                case Tag.VFlip:
                    FlipVertical = reader.GetBoolean();
                    break;
                case Tag.Rotate:
                    Rotate = reader.GetBoolean();
                    break;
                case Tag.PreferUntransformed:
                    PreferUntransformed = reader.GetBoolean();
                    break;
                default:
                    UnhandledProperty(propertyName);
                    break;
            }
        }
    }
#endif

    internal Transformations(XmlReader reader) : base(reader, Tag.Transformations)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.HFlip:
                    FlipHorizontal = reader.ReadContentAsBoolean();
                    break;
                case Tag.VFlip:
                    FlipHorizontal = reader.ReadContentAsBoolean();
                    break;
                case Tag.Rotate:
                    FlipHorizontal = reader.ReadContentAsBoolean();
                    break;
                case Tag.PreferUntransformed:
                    FlipHorizontal = reader.ReadContentAsBoolean();
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }
    }
}