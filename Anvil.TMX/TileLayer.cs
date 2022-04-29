using System.Numerics;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// A layer that consists of tile data.
/// </summary>
[PublicAPI]
public class TileLayer : Layer
{
    private Rectangle bounds;
    private Vector2 parallax = Vector2.One;
    
    /// <summary>
    /// Gets or sets the x-coordinate of the layer in tile units.
    /// </summary>
    public int X
    {
        get => bounds.X;
        set => bounds.X = value;
    }
    
    /// <summary>
    /// Gets or sets the y-coordinate of the layer in tile units.
    /// </summary>
    public int Y
    {
        get => bounds.Y;
        set => bounds.Y = value;
    }
    
    /// <summary>
    /// Gets or sets the width of the layer in tile units.
    /// </summary>
    /// <remarks>For fixed-size maps, this is always the same as the map width.</remarks>
    public int Width
    {
        get => bounds.Width;
        set => bounds.Width = value;
    }
    
    /// <summary>
    /// Gets or sets the height of the layer in tile units.
    /// </summary>
    /// <remarks>For fixed-size maps, this is always the same as the map height.</remarks>
    public int Height
    {
        get => bounds.Height;
        set => bounds.Height = value;
    }
        
    /// <summary>
    /// Gets or sets the size of the layer in tile units.
    /// </summary>
    /// <remarks>For fixed-size maps, this is always the same as the map size.</remarks>
    public Size Size
    {
        get => bounds.Size;
        set => bounds.Size = value;
    }
    
    /// <summary>
    /// Gets or sets the location and size of the layer in tile units.
    /// </summary>
    /// <remarks>For fixed-size maps, this is always the same as the map bounds.</remarks>
    public Rectangle Bounds
    {
        get => bounds;
        set => bounds = value;
    }

    /// <summary>
    /// Gets or sets the parallax factor for this layer.
    /// </summary>
    /// <remarks>Defaults to <c>(1.0, 1.0}</c>.</remarks>
    public Vector2 Parallax
    {
        get => parallax;
        set => parallax = value;
    }
    
    /// <summary>
    /// Gets or sets the parallax factor for this layer on the x-axis.
    /// </summary>
    /// <remarks>Defaults to <c>1.0</c>.</remarks>
    public float ParallaxX
    {
        get => parallax.X;
        set => parallax.X = value;
    }
    
    /// <summary>
    /// Gets or sets the parallax factor for this layer on the y-axis.
    /// </summary>
    /// <remarks>Defaults to <c>1.0</c>.</remarks>
    public float ParallaxY
    {
        get => parallax.Y;
        set => parallax.Y = value;
    }
    
    /// <summary>
    /// Gets a list of global tile IDs, which define the layer data.
    /// </summary>
    /// <remarks>
    /// For infinite maps, this list will be empty and the <see cref="Chunks"/> list will be populated.
    /// </remarks>
    public List<Gid> Tiles { get; }
    
    /// <summary>
    /// Gets a list of <see cref="Chunk"/> instances, which define the layer data.
    /// </summary>
    /// <remarks>
    /// Only for infinite maps, otherwise this list will be empty and the <see cref="Tiles"/> list will be populated.
    /// </remarks>
    public IList<Chunk> Chunks { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="TileLayer"/> class.
    /// </summary>
    /// <param name="map">The parent <see cref="Map"/> instance this layer is being created within.</param>
    public TileLayer(Map map) : base(map, Tag.Layer)
    {
        Tiles = new List<Gid>();
        Chunks = new List<Chunk>();
    }

    internal TileLayer(Map map, XmlReader reader) : base(map, reader, Tag.Layer)
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
                case Tag.ParallaxX:
                    parallax.X = reader.ReadContentAsFloat();
                    break;
                case Tag.ParallaxY:
                    parallax.Y = reader.ReadContentAsFloat();
                    break;
                default:
                    ProcessAttribute(reader);
                    break;
            }
        }
        
        while (ReadChild(reader))
        {
            switch (reader.Name)
            {
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                case Tag.Data:
                    (Tiles, Chunks) = DataReader.ReadTileData(reader);
                    break;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }

        Tiles ??= new List<Gid>();
        Chunks ??= new List<Chunk>();
    }

    /// <inheritdoc />
    public override string ToString() => Id.ToString();
}