#if JSON_READING
using System.Text.Json;
#endif
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Defines a list of colors and any number of Wang tiles using these colors.
/// </summary>
[PublicAPI]
public class WangSet : PropertyContainer, INamed
{
    /// <summary>
    /// Gets or sets the name of the Wang set.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the tile ID of the tile representing this Wang set.
    /// </summary>
    public int Tile { get; set; }
    
    /// <summary>
    /// Gets a list of the colors used to define the corner and/or edge of a Wang tile.
    /// </summary>
    public IList<WangColor> Colors { get; }
    
    /// <summary>
    /// Gets a list of the child Wang tiles in this set, and their association with the tile IDs in the tileset.
    /// </summary>
    public IList<WangTile> Tiles { get; }

    /// <summary>
    /// Creates a new default instance of the <see cref="WangSet"/> class.
    /// </summary>
    public WangSet() : base(Tag.WangSet)
    {
        Name = string.Empty;
        Tile = -1;
        Colors = new List<WangColor>();
        Tiles = new List<WangTile>();
    }

#if JSON_READING
    internal WangSet(Utf8JsonReader reader) : base(Tag.WangSet)
    {
        Tile = -1;
        Colors = new List<WangColor>();
        Tiles = new List<WangTile>();

        while (ReadChild(reader, out var propertyName))
        {
            switch (propertyName)
            {
                case Tag.Type:
                    // TODO: Is this even necessary? 
                    continue;
                case Tag.Name:
                    Name = reader.GetString() ?? string.Empty;
                    break;
                case Tag.Tile:
                    Tile = reader.GetInt32();
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                case Tag.Colors:
                {
                    do
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                            break;
                        if (reader.TokenType != JsonTokenType.StartObject)
                            continue;
                        Colors.Add(new WangColor(reader));
                    } while (reader.Read());

                    break;
                }
                case Tag.WangTiles:
                {
                    do
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                            break;
                        if (reader.TokenType != JsonTokenType.StartObject)
                            continue;
                        Tiles.Add(new WangTile(reader));
                    } while (reader.Read());

                    break;
                }
            }
        }
        
        Name ??= string.Empty;
    }
#endif

    internal WangSet(XmlReader reader) : base(reader, Tag.WangSet)
    {
        Tile = -1;
        Colors = new List<WangColor>();
        Tiles = new List<WangTile>();
        
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Name:
                    Name = reader.ReadContentAsString();
                    break;
                case Tag.Tile:
                    Tile = reader.ReadContentAsInt();
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }
        
        while (ReadChild(reader))
        {
            switch (reader.Name)
            {
                case Tag.WangColor:
                    Colors.Add(new WangColor(reader));
                    break;
                case Tag.WangTile:
                    Tiles.Add(new WangTile(reader));
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }
       
        Name ??= string.Empty;
    }
}