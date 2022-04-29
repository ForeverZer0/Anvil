#if JSON_READING
using System.Text.Json;
#endif
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// A color that can be used to define the corner and/or edge of a Wang tile.
/// </summary>
[PublicAPI]
public class WangColor : PropertyContainer, INamed
{
    private float probability;
    
    /// <summary>
    /// Gets or sets the name of this color.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the color value.
    /// </summary>
    public ColorF Color { get; set; }
    
    /// <summary>
    /// Gets or sets the tile ID of the tile representing this color.
    /// </summary>
    public int Tile { get; set; }

    /// <summary>
    /// Gets or sets the relative probability that this color is chosen over others in case of multiple options in the
    /// range of <c>0.0</c> to <c>1.0</c>.
    /// </summary>
    public float Probability
    {
        get => probability;
        set => Math.Clamp(value, 0.0f, 1.0f);
    }
    
    /// <summary>
    /// Creates a new default instance of the <see cref="WangColor"/> class.
    /// </summary>
    public WangColor() : base(Tag.WangColor)
    {
        Name = string.Empty;
        Color = Colors.Transparent;
        Tile = -1;
        probability = 0.0f;
    }

#if JSON_READING
    internal WangColor(Utf8JsonReader reader) : base(Tag.WangColor)
    {
        while (ReadChild(reader, out var propertyName))
        {
            switch (propertyName)
            {
                case Tag.Name:
                    Name = reader.GetString() ?? string.Empty;
                    break;
                case Tag.Color:
                    var str = reader.GetString();
                    Color = str is null ? Colors.Transparent : ColorF.Parse(str);
                    break;
                case Tag.Tile:
                    Tile = reader.GetInt32();
                    break;
                case Tag.Probability:
                    probability = reader.GetSingle();
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                default:
                    UnhandledProperty(propertyName);
                    break;
            }
        }
        Name ??= string.Empty;
    }
#endif

    internal WangColor(XmlReader reader) : base(reader, Tag.WangColor)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Name:
                    Name = reader.ReadContentAsString();
                    break;
                case Tag.Color:
                    Color = ColorF.Parse(reader.Value);
                    break;
                case Tag.Tile:
                    Tile = reader.ReadContentAsInt();
                    break;
                case Tag.Probability:
                    probability = reader.ReadContentAsFloat();
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }
        
        while (ReadChild(reader))
        {
            if (!string.Equals(reader.Name, Tag.Properties, StringComparison.Ordinal))
            {
                UnhandledChild(reader.Name);
                continue;
            }
            Properties = new PropertySet(reader);
        }

        Name ??= string.Empty;
    }
}