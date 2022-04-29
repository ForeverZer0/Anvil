using System.Diagnostics;
using System.Numerics;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Abstract base class for objects that can be placed on a <see cref="Map"/>.
/// </summary>
[PublicAPI, DebuggerDisplay("Name = {Name}, Type = {ObjectType}")]
public class MapObject : PropertyContainer, INamed
{
    private Vector2 location;
    private Vector2 size;
    private readonly List<Vector2> points;

    /// <summary>
    /// Gets or sets unique ID for this object.
    /// <para/>
    /// No two objects will share the same value.
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// Gets a strongly-typed constant describing the general type of this object.
    /// </summary>
    public ObjectType ObjectType { get; }

    /// <summary>
    /// Gets or sets an arbitrary string representing the name of the object.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value describing the object type. This is merely an arbitrary string with application-defined
    /// meaning. 
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the x-coordinate of the object in pixels.
    /// </summary>
    public float X
    {
        get => location.X;
        set => location.X = value;
    }
    
    /// <summary>
    /// Gets or sets the y-coordinate of the object in pixels.
    /// </summary>
    public float Y
    {
        get => location.Y;
        set => location.Y = value;
    }
    
    /// <summary>
    /// Gets or sets the width of the object in pixels.
    /// </summary>
    /// <remarks>Not applicable for all object types, in which case it will be <c>0.0</c>.</remarks>
    public float Width
    {
        get => size.X;
        set => size.X = value;
    }
    
    /// <summary>
    /// Gets or sets the height of the object in pixels.
    /// </summary>
    /// <remarks>Not applicable for all object types, in which case it will be <c>0.0</c>.</remarks>
    public float Height
    {
        get => size.Y;
        set => size.Y = value;
    }
    
    /// <summary>
    /// Gets or sets the location of the object in pixels.
    /// </summary>
    public Vector2 Location
    {
        get => location;
        set => location = value;
    }
    
    /// <summary>
    /// Gets or sets the size of the object in pixels.
    /// </summary>
    public Vector2 Size
    {
        get => size;
        set => size = value;
    }

    /// <summary>
    /// Gets or sets the rotation of the object in degrees clockwise.
    /// </summary>
    public float Rotation { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating the object visibility.
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Gets a list of points that described the shape of the object.
    /// </summary>
    /// <remarks>
    /// The position of the points is relative to the location of the object, where <c>0,0</c> is equal to the origin of
    /// object's location.
    /// <para/>
    /// Only used when the object type is <see cref="TMX.ObjectType.Polygon"/> or <see cref="TMX.ObjectType.Polyline"/>.
    /// </remarks>
    public IList<Vector2> Points => points;

    /// <summary>
    /// Gets or sets the global tile ID this object represents.
    /// </summary>
    /// <remarks>Only used when the object type is <see cref="TMX.ObjectType.Tile"/>.</remarks>
    public Gid TileId { get; set; }
    
    /// <summary>
    /// Gets a container for properties that describe how text should be rendered.
    /// </summary>
    /// <remarks>Only defined when the object type is <see cref="TMX.ObjectType.Text"/>.</remarks>
    public Text? Text { get; private set; }
    
    /// <summary>
    /// Gets the parent map of this object.
    /// <para/>
    /// Can be <c>null</c> when object is part of a collision data or not part of the map scene.
    /// </summary>
    public Map? Map { get; }
    
    /// <summary>
    /// Initializes a new instance of a the <see cref="MapObject"/>.
    /// </summary>
    /// <param name="id">A unique ID for this object.</param>
    /// <param name="type">The general object type.</param>
    public MapObject(int id, ObjectType type) : base(Tag.Object)
    {
        Map = null;
        Id = id;
        points = new List<Vector2>();
        ObjectType = type;
    }
    
    /// <summary>
    /// Initializes a new instance of a <see cref="MapObject"/> from the specified <paramref name="template"/>.
    /// </summary>
    /// <param name="id">A unique ID for this object.</param>
    /// <param name="template">A template instance supplying the base definition of this object.</param>
    public MapObject(int id, Template template) : this(id, template.Object.ObjectType)
    {
        CopyFrom(template.Object);
    }

    /// <summary>
    /// Initializes a new instance of a the <see cref="MapObject"/>.
    /// </summary>
    /// <param name="map">The parent map of this object.</param>
    /// <param name="type">The general object type.</param>
    public MapObject(Map map, ObjectType type) : this(map.NextObjectId++, type)
    {
        Map = map;
    }
    
    /// <summary>
    /// Initializes a new instance of a <see cref="MapObject"/> from the specified <paramref name="template"/>.
    /// </summary>
    /// <param name="map">The parent map of this object.</param>
    /// <param name="template">A template instance supplying the base definition of this object.</param>
    public MapObject(Map map, Template template) : this(map.NextLayerId++, template)
    {
        Map = map;
    }

    internal MapObject(Map? map, XmlReader reader) : base(reader, Tag.Object)
    {
        Map = map;
        points = new List<Vector2>();
        ObjectType = ObjectType.Unspecified;

        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Id:
                    Id = reader.ReadContentAsInt();
                    break;
                case Tag.Name:
                    Name = reader.ReadContentAsString();
                    break;
                case Tag.Type:
                    Type = reader.ReadContentAsString();
                    break;
                case Tag.X:
                    location.X = reader.ReadContentAsFloat();
                    break;
                case Tag.Y:
                    location.Y = reader.ReadContentAsFloat();
                    break;
                case Tag.Width:
                    size.X = reader.ReadContentAsFloat();
                    break;
                case Tag.Height:
                    size.Y = reader.ReadContentAsFloat();
                    break;
                case Tag.Rotation:
                    Rotation = reader.ReadContentAsFloat();
                    break;
                case Tag.Visible:
                    Visible = reader.ReadContentAsBoolean();
                    break;
                case Tag.Gid:
                    ObjectType = ObjectType.Tile;
                    TileId = new Gid(uint.Parse(reader.Value));
                    break;
                case Tag.Template:
                    // Templates are the first attribute after "id", so other values will not be overwritten.
                    var template = Template.Load(reader.Value);
                    ObjectType = template.Object.ObjectType;
                    CopyFrom(template.Object);
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
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                case Tag.Ellipse:
                    ObjectType = ObjectType.Ellipse;
                    break;
                case Tag.Point:
                    ObjectType = ObjectType.Point;
                    break;
                case Tag.Polygon:
                    ObjectType = ObjectType.Polygon;
                    ParsePoints(reader);
                    break;
                case Tag.PolyLine:
                    ObjectType = ObjectType.Polyline;
                    ParsePoints(reader);
                    break;
                case Tag.Text:
                    ObjectType = ObjectType.Text;
                    Text = new Text(reader);
                    break;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }

        if (ObjectType == ObjectType.Unspecified)
            ObjectType = ObjectType.Rectangle;
    }

    private void CopyFrom(MapObject other, bool copyLocation = false)
    {
        Name = other.Name;
        Type = other.Type;
        Rotation = other.Rotation;
        Visible = other.Visible;
        TileId = other.TileId;
        
        if (copyLocation)
            location = other.location;
        size = other.size;
        Text = other.Text;
        points.AddRange(other.points);
    }
    

    private void ParsePoints(XmlReader reader)
    {
        if (!reader.MoveToAttribute(Tag.Points))
            throw new FormatException($"Missing required \"{Tag.Points}\" attribute in polygon/polyline element.");

        var items = reader.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in items)
        {
            var xy = item.Split(',');
            Points.Add(new Vector2(float.Parse(xy[0]), float.Parse(xy[1])));
        }
    }
}