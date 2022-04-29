using System.Numerics;
using System.Reflection;
using System.Resources;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Defines a collection of source tiles, which are then referenced by a tile layers.
/// </summary>
[PublicAPI]
public class Tileset : PropertyContainer, INamed
{
    /// <summary>
    /// Loads a <see cref="Tileset"/> from an embedded resource in the specified <paramref name="assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly containing the embedded resource.</param>
    /// <param name="path">The path to the resource.</param>
    /// <param name="cache">Flag indicating if the internal cache will be used to retrieve/store the tileset.</param>
    /// <returns>The loaded tileset.</returns>
    /// <remarks>
    /// Note that assembly paths differ from filesystem paths, and typically follow the pattern of using the assembly's
    /// top-level namespace, followed by a dot-separated relative path to the file.
    /// <example>
    /// An assembly named "MyAssembly", with a sub-directory named "Tilesets" containing the embedded resources.
    /// <code>
    /// "MyAssembly.Tilesets.ForestPath.tsx"</code>
    /// </example>
    /// </remarks>
    public static Tileset Load(Assembly assembly, string path, bool cache = true)
    {
        if (cache && Map.GetCachedTileset(path, out var tileset))
            return tileset!;

        try
        {
            var stream = assembly.GetManifestResourceStream(path) ?? throw new MissingManifestResourceException(path);
            tileset = Load(stream, true);
        }
        catch (Exception e)
        {
            var stream = Map.ResolveResource(path, assembly, ResourceType.Tileset, e);
            tileset = Load(stream, true);
        }
        
        if (cache)
            Map.Cache(path, tileset);
        return tileset;
    }

    /// <inheritdoc cref="Load(System.Reflection.Assembly,string,bool)"/>
    public static async Task<Tileset> LoadAsync(Assembly assembly, string path, bool cache = true)
    {
        return await Task.Run(() => Load(assembly, path, cache));
    }
    
    /// <summary>
    /// Loads a <see cref="Tileset"/> from a filesystem <paramref name="path"/>.
    /// </summary>
    /// <param name="path">The path to the resource.</param>
    /// <param name="cache">Flag indicating if the internal cache will be used to retrieve/store the tileset.</param>
    /// <returns>The loaded tileset.</returns>
    public static Tileset Load(string path, bool cache = true)
    {
        if (cache && Map.GetCachedTileset(path, out var tileset))
            return tileset!;
        
        try
        {
            tileset = Load(File.OpenRead(path), true);
        }
        catch (IOException e)
        {
            var stream = Map.ResolveResource(path, null, ResourceType.Tileset, e);
            tileset = Load(stream, true);
        }

        if (cache)
            Map.Cache(path, tileset);
        return tileset;
    }

    /// <inheritdoc cref="Load(string,bool)"/>
    public static async Task<Tileset> LoadAsync(string path, bool cache = true)
    {
        return await Task.Run(() => Load(path, cache));
    }

    /// <summary>
    /// Loads a <see cref="Tileset"/> from the specified <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream">The stream containing the tileset data.</param>
    /// <param name="closeStream">
    /// Flag indicating if the <paramref name="stream"/> will be closed automatically after reading.
    /// </param>
    /// <returns>The loaded tileset.</returns>
    public static Tileset Load(Stream stream, bool closeStream = false)
    {
        var settings = new XmlReaderSettings
        {
            CloseInput = closeStream,
            IgnoreComments = true,
            IgnoreWhitespace = true,
            Async = false
        };

        var reader = XmlReader.Create(stream, settings);
        reader.MoveToContent();
        return new Tileset(reader);
    }

    /// <inheritdoc cref="Load(System.IO.Stream,bool)"/>
    public static async Task<Tileset> LoadAsync(Stream stream, bool closeStream = false)
    {
        return await Task.Run(() => Load(stream, closeStream));
    }

    private Size tileSize;
    private readonly List<Tile> tiles;
    
    /// <summary>
    /// Gets the TMX format version. Was “1.0” so far, and will be incremented to match minor Tiled releases.
    /// </summary>
    public string? Version { get; init; }

    /// <summary>
    /// Gets the Tiled version used to save the file (since Tiled 1.0.1). May be a date (for snapshot builds).
    /// </summary>
    public string? TiledVersion { get; init; }
    
    /// <summary>
    /// Gets a collection of <see cref="WangSet"/> instances used by this tileset.
    /// </summary>
    public WangSets WangSets { get; }

    /// <summary>
    /// Gets or sets the name of this tileset.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the spacing in pixels between the tiles in this tileset.
    /// </summary>
    /// <remarks>
    /// Only applies to the tileset image, and is irrelevant for image collection tilesets.
    /// <para/>
    /// Default: <c>0</c>
    /// </remarks>
    public int Spacing { get; set; }
    
    /// <summary>
    /// Gets or sets the margin around the tiles in this tileset.
    /// </summary>
    /// <remarks>
    /// Only applies to the tileset image, and is irrelevant for image collection tilesets.
    /// <para/>
    /// Default: <c>0</c>
    /// </remarks>
    public int Margin { get; set; }

    /// <summary>
    /// Gets the number of tiles in this tileset.
    /// </summary>
    /// <remarks>
    /// Note that there can be tiles with a higher ID than the tile count, in case the tileset is an image collection
    /// from which tiles have been removed.
    /// </remarks>
    public int TileCount => tiles.Count;

    /// <summary>
    /// Gets or sets yhe (maximum) size of the tiles in this tileset.
    /// </summary>
    /// <remarks>Irrelevant for image collection tilesets, but stores the maximum tile size.</remarks>
    public Size TileSize
    {
        get => tileSize;
        set => tileSize = value;
    }

    /// <summary>
    /// Gets or sets yhe (maximum) width of the tiles in this tileset.
    /// </summary>
    /// <remarks>Irrelevant for image collection tilesets, but stores the maximum tile width.</remarks>
    public int TileWidth
    {
        get => tileSize.Width;
        set => tileSize.Width = value;
    }
    
    /// <summary>
    /// Gets or sets yhe (maximum) height of the tiles in this tileset.
    /// </summary>
    /// <remarks>Irrelevant for image collection tilesets, but stores the maximum tile height.</remarks>
    public int TileHeight
    {
        get => tileSize.Height;
        set => tileSize.Height = value;
    }
    
    /// <summary>
    /// Gets or sets the number of tile columns in the tileset. For image collection tilesets it is editable and is used
    /// when displaying the tileset.
    /// </summary>
    public int Columns { get; set; }
    
    /// <summary>
    /// Gets or sets a value that controls the alignment for tile objects.
    /// </summary>
    public ObjectAlignment Alignment { get; set; }
    
    /// <summary>
    /// Gets or sets offset in pixel units tbe applied when drawing a tile from this tileset.
    /// </summary>
    public Vector2 TileOffset { get; set; }
    
    /// <summary>
    /// Gets or sets a value that determines how tile overlays for terrain and collision information are rendered.
    /// </summary>
    /// <remarks>Only used in case of isometric orientation, otherwise is <c>null</c>.</remarks>
    public Grid? Grid { get; set; }
    
    /// <summary>
    /// Gets or sets a value to describe which transformations can be applied to the tiles in this tileset (e.g. to
    /// extend a Wang set by transforming existing tiles).
    /// </summary>
    public Transformations Transformations { get; set; }
    
    /// <summary>
    /// Gets the source image used by this tileset.
    /// </summary>
    /// <remarks>
    /// A tileset can be either based on <i>a single image</i>, which is cut into tiles based on the given parameters,
    /// or a <i>collection of images</i>, in which case each tile defines its own image. In the first case this property
    /// will not be <c>null</c>. In the latter case, each child <see cref="Tile"/> contains an <see cref="TMX.Image"/>.
    /// </remarks>
    public Image? Image { get; set; }
    
    /// <summary>
    /// Creates a new default instance of the <see cref="Tileset"/> class.
    /// </summary>
    public Tileset() : base(Tag.Tileset)
    {
        Name = string.Empty;
        tileSize = new Size(16, 16);
        tiles = new List<Tile>();
        WangSets = new WangSets();
        Transformations = new Transformations();
    }

    internal Tileset(XmlReader reader) : base(reader, Tag.Tileset)
    {
        tiles = new List<Tile>();
        
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Name:
                    Name = reader.ReadContentAsString();
                    break;
                case Tag.TileWidth:
                    tileSize.Width = reader.ReadContentAsInt();
                    break;
                case Tag.TileHeight:
                    tileSize.Height = reader.ReadContentAsInt();
                    break;
                case Tag.Spacing:
                    Spacing = reader.ReadContentAsInt();
                    break;
                case Tag.Margin:
                    Margin = reader.ReadContentAsInt();
                    break;
                case Tag.TileCount:
                    tiles.EnsureCapacity(reader.ReadContentAsInt());
                    break;
                case Tag.Columns:
                    Columns = reader.ReadContentAsInt();
                    break;
                case Tag.ObjectAlignment:
                    Alignment = ParseEnum<ObjectAlignment>(reader.Value);
                    break;
                case Tag.Version:
                    Version = reader.ReadContentAsString();
                    break;
                case Tag.TiledVersion:
                    TiledVersion = reader.ReadContentAsString();
                    break;
                case Tag.FirstGid:
                case Tag.Source:
                    continue;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }

        while (ReadChild(reader))
        {
            switch (reader.Name)
            {
                case Tag.Tile:
                    tiles.Add(new Tile(reader));
                    break;
                case Tag.Image:
                    Image = new Image(reader);
                    break;
                case Tag.TileOffset:
                    Vector2 offset = default;
                    reader.MoveToAttribute(Tag.X);
                    offset.X = reader.ReadContentAsFloat();
                    reader.MoveToAttribute(Tag.Y);
                    offset.Y = reader.ReadContentAsFloat();
                    TileOffset = offset;
                    break;
                case Tag.Grid:
                    Grid = new Grid(reader);
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);  
                    break;
                case Tag.WangSets:
                    WangSets = new WangSets(reader);
                    break;
                case Tag.Transformations:
                    Transformations = new Transformations(reader);
                    break;
                case Tag.TerrainTypes: 
                    continue;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }

        Name ??= string.Empty;
        WangSets ??= new WangSets();
        Transformations ??= new Transformations();
    }
}