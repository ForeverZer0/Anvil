#if JSON_READING
using System.Text.Json;
#endif
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Describes a single tile within a <see cref="Tileset"/>.
/// </summary>
/// <remarks>
/// Tiles are the basic building blocks for creating maps. A map may contain many individual tiles, with each a
/// reference a unique instance of this class. 
/// </remarks>
[PublicAPI]
public class Tile : PropertyContainer
{
    /// <summary>
    /// Gets or sets the local ID of this tile within its parent <see cref="Tileset"/>.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets an tile "type", which as arbitrary string that can be used by tile objects.
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// Gets or sets a percentage indicating the probability that this tile is chosen when it competes with others while
    /// editing with the terrain tool in the Tiled application.
    /// </summary>
    public float Probability { get; set; }
    
    /// <summary>
    /// Gets or sets an <see cref="Image"/> associated with this tile.
    /// </summary>
    public Image? Image { get; set; }
    
    /// <summary>
    /// Gets the collision data for this tile.
    /// </summary>
    public ObjectGroup Collisions { get; }
    
    /// <summary>
    /// Gets or sets a collection of <see cref="Frame"/> objects that can be used to animate the tile at defined
    /// intervals.
    /// </summary>
    /// <remarks>When this value is <c>null</c> or empty, the tile is static and not animated.</remarks>
    public Animation? Animation { get; set; }

    /// <summary>
    /// Gets a flag indicating if this tile contains animation frames.
    /// </summary>
    public bool IsAnimated => Animation is {Count: > 0};

    /// <summary>
    /// Creates a new default instance of the <see cref="Tile"/> ..
    /// </summary>
    public Tile() : this(-1)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="Tile"/> class with the specified unique ID.
    /// </summary>
    /// <param name="id">The local tile ID of this instance within its parent <see cref="Tileset"/>.</param>
    public Tile(int id) : base(Tag.Tile)
    {
        Id = id;
        Collisions = new ObjectGroup();
    }

    #if JSON_READING
    
    internal Tile(Utf8JsonReader reader) : base(Tag.Tile)
    {
        string? imgSrc = null;
        var imgWidth = 0;
        var imgHeight = 0;
        
        while (ReadChild(reader, out var propertyName))
        {
            switch (propertyName)
            {
                case Tag.Id:
                    Id = reader.GetInt32();
                    break;
                case Tag.Image:
                    imgSrc = reader.GetString();
                    break;
                case Tag.ImageWidth:
                    imgWidth = reader.GetInt32();
                    break;
                case Tag.ImageHeight:
                    imgHeight = reader.GetInt32();
                    break;
                case Tag.Probability:
                    Probability = reader.GetSingle();
                    break;
                case Tag.Type:
                    Type = reader.GetString();
                    break;
                case Tag.Terrain:
                    // Obsolete
                    continue;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                case Tag.ObjectGroup:
                    Collisions = new ObjectGroup(reader);
                    break;
            }
        }

        if (!string.IsNullOrWhiteSpace(imgSrc))
        {
            Image = new Image
            {
                Source = imgSrc,
                Width = imgWidth,
                Height = imgHeight
            };
        }
        Collisions ??= new ObjectGroup();
    }

    #endif

    internal Tile(XmlReader reader) : base(reader, Tag.Tile)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Id:
                    Id = reader.ReadContentAsInt();
                    break;
                case Tag.Type:
                    Type = reader.ReadContentAsString();
                    break;
                case Tag.Terrain: continue;
                case Tag.Probability:
                    Probability = reader.ReadContentAsFloat();
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
                case Tag.Image:
                    Image = new Image(reader);
                    break;
                case Tag.Animation:
                    Animation = new Animation(reader);
                    break;
                case Tag.ObjectGroup:
                    Collisions = new ObjectGroup(reader);
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }

        Collisions ??= new ObjectGroup();
    }
}