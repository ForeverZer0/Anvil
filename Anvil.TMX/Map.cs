using System.Collections;
using System.Numerics;
using System.Reflection;
using System.Resources;
using System.Xml;
using JetBrains.Annotations;

[assembly: CLSCompliant(true)]

namespace Anvil.TMX;

/// <summary>
/// Top-level TMX object that encapsulates all the data needed to render a game scene.
/// </summary>
[PublicAPI]
public class Map : PropertyContainer, IGroup
{
    private static readonly Dictionary<string, Map> MapCache;
    private static readonly Dictionary<string, Tileset> TilesetCache;
    private static readonly Dictionary<string, Template> TemplateCache;

    /// <summary>
    /// Occurs when the path to a map needs resolved, giving subscribers an opportunity to locate and load the stream.
    /// </summary>
    public static event ResourceResolveHandler? ResourceResolve;
    
    /// <summary>
    /// Requests for a missing resource to be resolved, or throwing an exception if not found.
    /// </summary>
    /// <param name="path">The path to locate.</param>
    /// <param name="assembly">The assembly containing the embedded resource, or <c>null</c> for filesystem paths.</param>
    /// <param name="type">The type or resource that needs loaded.</param>
    /// <param name="e">An exception instance to throw if resolution fails.</param>
    /// <returns>A stream for the resource that is opened for reading.</returns>
    /// <exception cref="Exception">When the path cannot be found.</exception>
    internal static Stream ResolveResource(string path, Assembly? assembly, ResourceType type, Exception e)
    {
        if (File.Exists(path))
            return File.OpenRead(path);

        if (ResourceResolve == null) 
            throw e;
        
        foreach (var handler in ResourceResolve.GetInvocationList())
        {
            if (handler is not ResourceResolveHandler resolver)
                continue;
                
            var stream = resolver.Invoke(assembly, path, type);
            if (stream is not null)
                return stream;
        }

        throw e;
    }

    /// <summary>
    /// Static constructor.
    /// </summary>
    static Map()
    {
        MapCache = new Dictionary<string, Map>();
        TilesetCache = new Dictionary<string, Tileset>();
        TemplateCache = new Dictionary<string, Template>();
    }

    /// <summary>
    /// Caches the specified <see cref="Template"/> instance so that it can be instantly returned the next time it
    /// attempts to be loaded.
    /// </summary>
    /// <param name="path">The path of the resource.</param>
    /// <param name="template">The instance to cache.</param>
    /// <returns><c>true</c> if object was added to cache, otherwise <c>false</c> if it is already cached.</returns>
    public static bool Cache(string path, Template template)
    {
        if (TemplateCache.ContainsKey(path))
            return false;
        TemplateCache.Add(path, template);
        return true;
    }
    
    /// <summary>
    /// Caches the specified <see cref="Tileset"/> instance so that it can be instantly returned the next time it
    /// attempts to be loaded.
    /// </summary>
    /// <param name="path">The path of the resource.</param>
    /// <param name="tileset">The instance to cache.</param>
    /// <returns><c>true</c> if object was added to cache, otherwise <c>false</c> if it is already cached.</returns>
    public static bool Cache(string path, Tileset tileset)
    {
        if (TilesetCache.ContainsKey(path))
            return false;
        TilesetCache.Add(path, tileset);
        return true;
    }
    
    /// <summary>
    /// Caches the specified <see cref="Map"/> instance so that it can be instantly returned the next time it
    /// attempts to be loaded.
    /// </summary>
    /// <param name="path">The path of the resource.</param>
    /// <param name="map">The instance to cache.</param>
    /// <returns><c>true</c> if object was added to cache, otherwise <c>false</c> if it is already cached.</returns>
    public static bool Cache(string path, Map map)
    {
        if (MapCache.ContainsKey(path))
            return false;
        MapCache.Add(path, map);
        return true;
    }

    /// <summary>
    /// Checks if a map with the specified path is cached and retrieves it if found.
    /// </summary>
    /// <param name="path">The path to query.</param>
    /// <param name="map">The <see cref="Map"/> instance if found, otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if map has been cached and <paramref name="map"/> is valid, otherwise <c>false</c>.</returns>
    public static bool GetCachedMap(string path, out Map? map) => MapCache.TryGetValue(path, out map);

    /// <summary>
    /// Checks if a tileset with the specified path is cached and retrieves it if found.
    /// </summary>
    /// <param name="path">The path to query.</param>
    /// <param name="tileset">The <see cref="Tileset"/> instance if found, otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if map has been cached and <paramref name="tileset"/> is valid, otherwise <c>false</c>.</returns>
    public static bool GetCachedTileset(string path, out Tileset? tileset) => TilesetCache.TryGetValue(path, out tileset);

    /// <summary>
    /// Checks if a template with the specified path is cached and retrieves it if found.
    /// </summary>
    /// <param name="path">The path to query.</param>
    /// <param name="template">The <see cref="Template"/> instance if found, otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if map has been cached and <paramref name="template"/> is valid, otherwise <c>false</c>.</returns>
    public static bool GetCachedTemplate(string path, out Template? template) => TemplateCache.TryGetValue(path, out template);

    /// <summary>
    /// Removes a cached item at the specified <paramref name="path"/>.
    /// </summary>
    /// <param name="path">The path of the item to remove.</param>
    /// <returns><c>true</c> if item was removed, otherwise <c>false</c> if path was not found.</returns>
    public static bool RemoveCached(string path)
    {
        return MapCache.Remove(path) || TilesetCache.Remove(path) || TemplateCache.Remove(path);
    }
    
    /// <summary>
    /// Clears all items in the cache.
    /// </summary>
    public static void ClearCache()
    {
        MapCache.Clear();
        TilesetCache.Clear();
        TemplateCache.Clear();
    }

    /// <summary>
    /// Loads a <see cref="Map"/> from an embedded resource in the specified <paramref name="assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly containing the embedded resource.</param>
    /// <param name="path">The path to the resource.</param>
    /// <param name="cache">Flag indicating if the internal cache will be used to retrieve/store the map.</param>
    /// <returns>The loaded map.</returns>
    /// <remarks>
    /// Note that assembly paths differ from filesystem paths, and typically follow the pattern of using the assembly's
    /// top-level namespace, followed by a dot-separated relative path to the file.
    /// <example>
    /// An assembly named "MyAssembly", with a sub-directory named "Maps" containing the embedded resources.
    /// <code>
    /// "MyAssembly.Maps.ForestPath.tmx"</code>
    /// </example>
    /// </remarks>
    public static Map Load(Assembly assembly, string path, bool cache = true)
    {
        if (cache && GetCachedMap(path, out var map))
            return map!;

        try
        {
            var stream = assembly.GetManifestResourceStream(path) ?? throw new MissingManifestResourceException(path);
            map = Load(stream, true, assembly);
        }
        catch (Exception e)
        {
            var stream = ResolveResource(path, assembly, ResourceType.Map, e);
            map = Load(stream, true, assembly);
        }
        
        if (cache)
            Cache(path, map);
        return map;    
    }

    /// <inheritdoc cref="Load(System.Reflection.Assembly,string,bool)"/>
    public static async Task<Map> LoadAsync(Assembly assembly, string path, bool cache = true)
    {
        return await Task.Run(() => Load(assembly, path, cache));
    }
    
    /// <summary>
    /// Loads a <see cref="Map"/> from a filesystem <paramref name="path"/>.
    /// </summary>
    /// <param name="path">The path to the resource.</param>
    /// <param name="cache">Flag indicating if the internal cache will be used to retrieve/store the map.</param>
    /// <returns>The loaded map.</returns>
    public static Map Load(string path, bool cache = true)
    {
        if (cache && MapCache.TryGetValue(path, out var map))
            return map;
        
        try
        {
            map = Load(File.OpenRead(path), true, null);
        }
        catch (FileNotFoundException e)
        {
            var steam = ResolveResource(path, null, ResourceType.Map, e);
            map = Load(steam, true, null);
        }

        if (cache)
            MapCache.Add(path, map);
        return map;
    }

    /// <inheritdoc cref="Load(string,bool)"/>
    public static async Task<Map> LoadAsync(string path, bool cache = true)
    {
        return await Task.Run(() => Load(path, cache));
    }

    /// <summary>
    /// Loads a <see cref="Map"/> from the specified <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream">The stream containing the map data.</param>
    /// <param name="closeStream">
    /// Flag indicating if the <paramref name="stream"/> will be closed automatically after reading.
    /// </param>
    /// <returns>The loaded map.</returns>
    public static Map Load(Stream stream, bool closeStream = false) => Load(stream, closeStream, null);

    /// <inheritdoc cref="Load(System.IO.Stream,bool)"/>
    public static async Task<Map> LoadAsync(Stream stream, bool closeStream = false)
    {
        return await Task.Run(() => Load(stream, closeStream, null));
    }
    
    private static Map Load(Stream stream, bool closeStream, Assembly? assembly)
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
        return new Map(reader, assembly);
    }
    
    
    private Size mapSize;
    private Size tileSize;
    private Vector2 parallaxOrigin;

    /// <summary>
    /// Gets the TMX format version. Was “1.0” so far, and will be incremented to match minor Tiled releases.
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Gets the Tiled version used to save the file (since Tiled 1.0.1). May be a date (for snapshot builds).
    /// </summary>
    public string? TiledVersion { get; }

    /// <summary>
    /// Gets or sets a value indicating the map orientation.
    /// </summary>
    /// <remarks>Default: <see cref="TMX.Orientation.Orthogonal"/></remarks>
    public Orientation Orientation { get; set; } = Orientation.Orthogonal;

    /// <summary>
    /// Gets or sets a value indicating the order in which tiles on tile layers are rendered.
    /// </summary>
    /// <remarks>
    /// In all cases, the map is drawn row-by-row, and this value only applies to orthogonal maps.
    /// <para/>
    /// Default: <see cref="TMX.RenderOrder.RightDown"/>
    /// </remarks>
    public RenderOrder RenderOrder { get; set; } = RenderOrder.RightDown;

    /// <summary>
    /// Gets or sets a value indicating the level of compression to use for tile data.
    /// </summary>
    /// <remarks>Default: <c>-1</c> (algorithm default)</remarks>
    public int CompressionLevel { get; set; } = -1;

    /// <summary>
    /// Gets or sets the width of the map in tile units.
    /// </summary>
    public int Width
    {
        get => mapSize.Width;
        set => mapSize.Width = value;
    }
    
    /// <summary>
    /// Gets or sets the height of the map in tile units.
    /// </summary>
    public int Height
    {
        get => mapSize.Height;
        set => mapSize.Height = value;
    }

    /// <summary>
    /// Gets or sets the size of the map in tile units.
    /// </summary>
    public Size Size
    {
        get => mapSize;
        set => mapSize = value;
    }

    /// <summary>
    /// Gets or sets the width of a tile in pixel units.
    /// </summary>
    public int TileWidth
    {
        get => tileSize.Width;
        set => tileSize.Width = value;
    }
    
    /// <summary>
    /// Gets or sets the height of a tile in pixel units.
    /// </summary>
    public int TileHeight
    {
        get => tileSize.Height;
        set => tileSize.Height = value;
    }

    /// <summary>
    /// Gets or sets the size of a tile in pixel units.
    /// </summary>
    public Size TileSize
    {
        get => tileSize;
        set => tileSize = value;
    }

    /// <summary>
    /// Gets or sets the coordinate of the parallax origin in pixel units.
    /// </summary>
    public Vector2 ParallaxOrigin
    {
        get => parallaxOrigin;
        set => parallaxOrigin = value;
    }

    /// <summary>
    /// Gets or sets a value that determines the width or height (depending on the staggered axis) of the tile’s edge
    /// in pixel units.
    /// </summary>
    /// <remarks>Only valid for hexagonal maps.</remarks>
    public int HexSideLength { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the axis is staggered.
    /// </summary>
    /// <remarks>Only valid for staggered and hexagonal maps.</remarks>
    public StaggerAxis StaggerAxis { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating which indices along the staggered axis are shifted. 
    /// </summary>
    public StaggerIndex StaggerIndex { get; set; }
    
    /// <summary>
    /// Gets or sets the background color to render for the map.
    /// </summary>
    /// <remarks>Default: <see cref="Colors.Transparent"/></remarks>
    public ColorF BackgroundColor { get; set; }
    
    /// <summary>
    /// Gets or sets the next available ID to use for a new layer.
    /// </summary>
    /// <remarks>Layer IDs are not reused, even when removed from the map.</remarks>
    public int NextLayerId { get; set; }
    
    /// <summary>
    /// Gets or sets the next available ID to use for a new map object.
    /// </summary>
    /// <remarks>Object IDs are not reused, even when removed from the map.</remarks>
    public int NextObjectId { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if the map is infinite, meaning it has no fixed size and can grow in all
    /// directions.
    /// </summary>
    public bool IsInfinite { get; set; }
    
    /// <summary>
    /// Gets a list containing the tilesets used in this map.
    /// </summary>
    public IList<MapTileset> Tilesets { get; }

    /// <summary>
    /// Gets a list containing the layers defined in this map.
    /// </summary>
    public IList<Layer> Layers { get; }

    /// <summary>
    /// Creates a new default <see cref="Map"/> instance.
    /// </summary>
    public Map() : base(Tag.Map)
    {
        Layers = new List<Layer>();
        Tilesets = new List<MapTileset>();
    }

    private Map(XmlReader reader, Assembly? assembly) : base(reader, Tag.Map)
    {
        Layers = new List<Layer>();
        Tilesets = new List<MapTileset>();

        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Version:
                    Version = reader.ReadContentAsString();
                    break;
                case Tag.TiledVersion:
                    TiledVersion = reader.ReadContentAsString();
                    break;
                case Tag.Orientation:
                    Orientation = ParseEnum<Orientation>(reader.Value);
                    break;
                case Tag.RenderOrder:
                    RenderOrder = ParseEnum<RenderOrder>(reader.Value);
                    break;
                case Tag.CompressionLevel:
                    CompressionLevel = reader.ReadContentAsInt();
                    break;
                case Tag.Width:
                    mapSize.Width = reader.ReadContentAsInt();
                    break;
                case Tag.Height:
                    mapSize.Height = reader.ReadContentAsInt();
                    break;
                case Tag.TileWidth:
                    tileSize.Width = reader.ReadContentAsInt();
                    break;
                case Tag.TileHeight:
                    tileSize.Height = reader.ReadContentAsInt();
                    break;
                case Tag.HexSideLength:
                    HexSideLength = reader.ReadContentAsInt();
                    break;
                case Tag.StaggerAxis:
                    StaggerAxis = ParseEnum<StaggerAxis>(reader.Value);
                    break;
                case Tag.StaggerIndex:
                    StaggerIndex = ParseEnum<StaggerIndex>(reader.Value);
                    break;
                case Tag.ParallaxOriginX:
                    parallaxOrigin.X = reader.ReadContentAsFloat();
                    break;
                case Tag.ParallaxOriginY:
                    parallaxOrigin.Y = reader.ReadContentAsFloat();
                    break;
                case Tag.BackgroundColor:
                    BackgroundColor = ColorF.Parse(reader.Value);
                    break;
                case Tag.NextLayerId:
                    NextLayerId = reader.ReadContentAsInt();
                    break;
                case Tag.NextObjectId:
                    NextObjectId = reader.ReadContentAsInt();
                    break;
                case Tag.Infinite:
                    IsInfinite = reader.ReadContentAsBoolean();
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
                case Tag.Tileset:
                    Tileset tileset;
                    if (!reader.MoveToAttribute(Tag.FirstGid))
                        throw new FormatException($"Tileset missing required \"{Tag.FirstGid}\" attribute.");
                    var firstGid = reader.ReadContentAsInt();
                    if (reader.MoveToAttribute(Tag.Source))
                    {
                        var source = reader.ReadContentAsString();
                        tileset = assembly != null ? Tileset.Load(assembly, source) : Tileset.Load(source);
                    }
                    else // Embedded tileset
                    {
                        tileset = new Tileset(reader)
                        {
                            Version = Version,
                            TiledVersion = TiledVersion
                        };
                    }
                    Tilesets.Add(new MapTileset(firstGid, tileset));
                    break;
                case Tag.Layer:
                    Layers.Add(new TileLayer(this, reader));
                    break;
                case Tag.ImageLayer:
                    Layers.Add(new ImageLayer(this, reader));
                    break;
                case Tag.ObjectGroup:
                    Layers.Add(new ObjectLayer(this, reader));
                    break;
                case Tag.Group:
                    Layers.Add(new GroupLayer(this, reader));
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                case Tag.EditorSettings:
                case Tag.ChunkSize:
                case Tag.Export:
                    continue;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }
    }

    /// <inheritdoc />
    public IEnumerable<Layer> Filter(LayerType type, bool recursive = false)
    {
        foreach (var layer in Layers)
        {
            switch (layer)
            {
                case TileLayer when type.HasFlag(LayerType.Tile):
                    yield return layer;
                    continue;
                case ImageLayer when type.HasFlag(LayerType.Image):
                    yield return layer;
                    continue;
                case ObjectLayer when type.HasFlag(LayerType.Object):
                    yield return layer;
                    continue;
                case GroupLayer group:
                {
                    if (type.HasFlag(LayerType.Group))
                        yield return layer;

                    if (recursive)
                    {
                        foreach (var child in group.Filter(type, recursive))
                            yield return child;
                    }
                    break;
                }
            }
        }
    }
    
    /// <inheritdoc />
    public IEnumerator<Layer> GetEnumerator() => Layers.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Layers).GetEnumerator();
}

