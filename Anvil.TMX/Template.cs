using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Represents a "prototype" object that <see cref="MapObject"/> instances can reference and inherit properties and
/// values from.
/// <para/>
/// Editing a template will alter all objects that reference it as well, but setting/changing any property of the object
/// will override what is defined by the template.
/// </summary>
[PublicAPI]
public class Template : TiledEntity
{
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
    public static Template Load(Assembly assembly, string path, bool cache = true)
    {
        if (cache && Map.GetCachedTemplate(path, out var map))
            return map!;

        try
        {
            var stream = assembly.GetManifestResourceStream(path) ?? throw new MissingManifestResourceException(path);
            map = Load(stream, true);
        }
        catch (Exception e)
        {
            var stream = Map.ResolveResource(path, assembly, ResourceType.Map, e);
            map = Load(stream, true);
        }
        
        if (cache)
            Map.Cache(path, map);
        return map;    
    }

    /// <inheritdoc cref="Load(System.Reflection.Assembly,string,bool)"/>
    public static async Task<Template> LoadAsync(Assembly assembly, string path, bool cache = true)
    {
        return await Task.Run(() => Load(assembly, path, cache));
    }
    
    /// <summary>
    /// Loads a <see cref="Map"/> from a filesystem <paramref name="path"/>.
    /// </summary>
    /// <param name="path">The path to the resource.</param>
    /// <param name="cache">Flag indicating if the internal cache will be used to retrieve/store the map.</param>
    /// <returns>The loaded map.</returns>
    public static Template Load(string path, bool cache = true)
    {
        if (cache && Map.GetCachedTemplate(path, out var template))
            return template!;
        
        try
        {
            template = Load(File.OpenRead(path), true);
        }
        catch (FileNotFoundException e)
        {
            var steam = Map.ResolveResource(path, null, ResourceType.Map, e);
            template = Load(steam, true);
        }

        if (cache)
            Map.Cache(path, template);
        return template;
    }

    /// <inheritdoc cref="Load(string,bool)"/>
    public static async Task<Template> LoadAsync(string path, bool cache = true)
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
    public static Template Load(Stream stream, bool closeStream = false)
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
        return new Template(reader);
    }

    /// <inheritdoc cref="Load(System.IO.Stream,bool)"/>
    public static async Task<Template> LoadAsync(Stream stream, bool closeStream = false)
    {
        return await Task.Run(() => Load(stream, closeStream));
    }
    
    /// <summary>
    /// Gets the associated <see cref="MapTileset"/> object.
    /// <para/>
    /// Only used for map objects that represent a tile object.
    /// </summary>
    public MapTileset? Tileset { get; }
    
    /// <summary>
    /// Gets the base object instance that whose properties will be inherited.
    /// </summary>
    public MapObject Object { get; }
    
    /// <summary>
    /// Creates a new instance of the <see cref="Template"/> class based on the specified <paramref name="mapObject"/>.
    /// </summary>
    /// <param name="mapObject">A <see cref="MapObject"/> to base the template on.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="mapObject"/> is <c>null</c>.</exception>
    public Template(MapObject mapObject) : base(Tag.Template)
    {
        Object = mapObject ?? throw new ArgumentNullException(nameof(mapObject));
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="Template"/> class based on the specified <paramref name="mapObject"/>.
    /// </summary>
    /// <param name="mapObject">A <see cref="MapObject"/> to base the template on.</param>
    /// <param name="mapTileset">A tileset that <paramref name="mapObject"/> references.</param>
    /// <exception cref="ArgumentNullException">
    /// When <paramref name="mapObject"/> or <paramref name="mapTileset"/> is <c>null</c>.
    /// </exception>
    public Template(MapObject mapObject, MapTileset mapTileset) : base(Tag.Template)
    {
        Object = mapObject ?? throw new ArgumentNullException(nameof(mapObject));
        Tileset = mapTileset;
    }

    internal Template(XmlReader reader) : base(reader, Tag.Template)
    {
        while (reader.MoveToNextAttribute())
        {
            UnhandledAttribute(reader.Name);
        }

        while (ReadChild(reader))
        {
            switch (reader.Name)
            {
                case Tag.Tileset:
                    if (!reader.MoveToAttribute(Tag.FirstGid))
                        throw new FormatException($"Tileset missing required \"{Tag.FirstGid}\" attribute.");
                    var firstGid = reader.ReadContentAsInt();
                    if (!reader.MoveToAttribute(Tag.Source))
                        throw new FormatException($"Tileset missing required \"{Tag.Source}\" attribute.");
                    var source = reader.ReadContentAsString();
                    
                    Tileset = new MapTileset(firstGid, TMX.Tileset.Load(source));
                    break;
                case Tag.Object:
                    Debug.Assert(Object is null, "Template may only contain one object.");
                    Object = new MapObject(null, reader);
                    break;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }

        if (Object is null)
            throw new FormatException("No object described in template.");
    }
}