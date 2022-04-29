using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Used for infinite maps, stores a subset of tile data for a region of the map.
/// </summary>
[PublicAPI]
public sealed class Chunk : TiledEntity
{
    private Rectangle bounds;

    /// <summary>
    /// Gets or sets the x-coordinate of the chunk in tile units.
    /// </summary>
    public int X
    {
        get => bounds.X;
        set => bounds.X = value;
    }
    
    /// <summary>
    /// Gets or sets the y-coordinate of the chunk in tile units.
    /// </summary>
    public int Y
    {
        get => bounds.Y;
        set => bounds.Y = value;
    }
    
    /// <summary>
    /// Gets or sets the width of the chunk in tile units.
    /// </summary>
    public int Width
    {
        get => bounds.Width;
        set => bounds.Width = value;
    }
    
    /// <summary>
    /// Gets or sets the height of the chunk in tile units.
    /// </summary>
    public int Height
    {
        get => bounds.Height;
        set => bounds.Height = value;
    }
    
    /// <summary>
    /// Gets or sets the location of the chunk in tile units.
    /// </summary>
    public Point Location
    {
        get => bounds.Location;
        set => bounds.Location = value;
    }
    
    /// <summary>
    /// Gets or sets the size of the chunk in tile units.
    /// </summary>
    public Size Size
    {
        get => bounds.Size;
        set => bounds.Size = value;
    }

    /// <summary>
    /// Gets or sets a rectangle describing the bounds of the chunk in tile units.
    /// </summary>
    public Rectangle Bounds
    {
        get => bounds;
        set => bounds = value;
    }

    /// <summary>
    /// Gets or sets an array of global tile IDs that defines the layer data.
    /// </summary>
    public IList<Gid> Tiles { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Chunk"/> class.
    /// </summary>
    public Chunk() : base(Tag.Chunk)
    {
        Tiles = new List<Gid>();
    }

    internal Chunk(XmlReader reader, DataCompression compression, DataEncoding encoding) : base(reader, Tag.Chunk)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.X:
                    bounds.X = reader.ReadContentAsInt();
                    break;
                case Tag.Y:
                    bounds.Y = reader.ReadContentAsInt();
                    break;
                case Tag.Width:
                    bounds.Width = reader.ReadContentAsInt();
                    break;
                case Tag.Height:
                    bounds.Height = reader.ReadContentAsInt();
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == TagName)
                break;

            if (reader.NodeType == XmlNodeType.Text)
            {
                var payload = reader.Value.Trim();
                Tiles = DataReader.ReadTileData(payload, compression, encoding);
                break;
            }
            
            if (reader.NodeType != XmlNodeType.Element || reader.Name != Tag.Tile)
                continue;
            
            Tiles = new List<Gid>();
            while (ReadChild(reader))
            {
                if (reader.Name != Tag.Tile)
                    continue;
                Tiles.Add(new Gid(uint.Parse(reader.Value)));
            }
            break;
        }

        Tiles ??= new List<Gid>();
    }
}