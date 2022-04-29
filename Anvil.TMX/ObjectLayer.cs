using System.Collections;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// A layer consisting of <see cref="MapObject"/> instances.
/// </summary>
[PublicAPI]
public class ObjectLayer : Layer, IList<MapObject>
{
    private readonly List<MapObject> objects;
    
    /// <summary>
    /// Gets or sets an optional color to display the the objects in this group, or <c>null</c> to ignore.
    /// </summary>
    public ColorF? Color { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the objects are drawn according to the order of appearance
    /// (<see cref="TMX.DrawOrder.Index"/>) or sorted by their y-coordinate (<see cref="TMX.DrawOrder.TopDown"/>);
    /// </summary>
    public DrawOrder DrawOrder { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ObjectLayer"/> class.
    /// </summary>
    /// <param name="map">The parent <see cref="Map"/> instance this layer is being created within.</param>
    public ObjectLayer(Map map) : base(map, Tag.ObjectGroup)
    {
        Color = null;
        DrawOrder = DrawOrder.TopDown;
        objects = new List<MapObject>();
    }

    internal ObjectLayer(Map map, XmlReader reader) : base(map, reader, Tag.ObjectGroup)
    {
        Color = null;
        DrawOrder = DrawOrder.TopDown;
        objects = new List<MapObject>();
        
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.DrawOrder:
                    DrawOrder = ParseEnum<DrawOrder>(reader.Value);
                    break;
                case Tag.Color:
                    Color = ColorF.Parse(reader.Value);
                    break;
                // Deprecated and unused, but consume so no debug output is emitted.
                case Tag.X:
                case Tag.Y:
                case Tag.Width:
                case Tag.Height:
                    continue;
                default:
                    ProcessAttribute(reader);
                    break;
            }
        }

        while (ReadChild(reader))
        {
            switch (reader.Name)
            {
                case Tag.Object:
                    Add(new MapObject(Map, reader));
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }
    }

    /// <inheritdoc />
    public IEnumerator<MapObject> GetEnumerator() => objects.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) objects).GetEnumerator();

    /// <inheritdoc />
    public void Add(MapObject item) => objects.Add(item);

    /// <inheritdoc />
    public void Clear() => objects.Clear();

    /// <inheritdoc />
    public bool Contains(MapObject item) => objects.Contains(item);

    /// <inheritdoc />
    public void CopyTo(MapObject[] array, int arrayIndex) => objects.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(MapObject item) => objects.Remove(item);

    /// <inheritdoc />
    public int Count => objects.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(MapObject item) => objects.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, MapObject item) => objects.Insert(index, item);

    /// <inheritdoc />
    public void RemoveAt(int index) => objects.RemoveAt(index);

    /// <inheritdoc />
    public MapObject this[int index]
    {
        get => objects[index];
        set => objects[index] = value;
    }
}