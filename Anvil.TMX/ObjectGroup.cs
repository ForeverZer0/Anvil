#if JSON_READING
using System.Text.Json;
#endif
using System.Collections;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// A list container for <see cref="MapObject"/> instances, most often used for collision data in tiles..
/// </summary>
/// <remarks>While the TMX format uses an <see cref="ObjectLayer"/> for collision data, only a small subset of features
/// is actually utilized, and it makes more sense to differentiate their types and expand on each separately where it
/// makes most sense opposed to strictly following the data format.
/// <para/>
/// Both classes will result in the same serialized output based on their contents.
/// </remarks>
[PublicAPI]
public class ObjectGroup : TiledEntity, IList<MapObject>
{
    private readonly List<MapObject> objectList;
    
    /// <inheritdoc />
    public IEnumerator<MapObject> GetEnumerator() => objectList.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) objectList).GetEnumerator();

    /// <inheritdoc />
    public void Add(MapObject item) => objectList.Add(item);

    /// <inheritdoc />
    public void Clear() => objectList.Clear();

    /// <inheritdoc />
    public bool Contains(MapObject item) => objectList.Contains(item);

    /// <inheritdoc />
    public void CopyTo(MapObject[] array, int arrayIndex) => objectList.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(MapObject item) => objectList.Remove(item);

    /// <inheritdoc />
    public int Count => objectList.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(MapObject item) => objectList.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, MapObject item) => objectList.Insert(index, item);

    /// <inheritdoc />
    public void RemoveAt(int index) => objectList.RemoveAt(index);

    /// <inheritdoc />
    public MapObject this[int index]
    {
        get => objectList[index];
        set => objectList[index] = value;
    }

    /// <inheritdoc cref="List{T}.Sort(Comparison{T})"/>
    public void Sort(Comparison<MapObject> comparison) => objectList.Sort(comparison);
    
    /// <inheritdoc cref="List{T}.Sort(IComparer{T})"/>
    public void Sort(IComparer<MapObject>? comparer) => objectList.Sort(comparer);

    /// <inheritdoc cref="List{T}.Sort()"/>
    public void Sort() => objectList.Sort();

    /// <summary>
    /// Gets or sets a value indicating whether the objects are drawn according to the order of appearance
    /// (<see cref="TMX.DrawOrder.Index"/>) or sorted by their y-coordinate (<see cref="TMX.DrawOrder.TopDown"/>);
    /// </summary>
    public DrawOrder DrawOrder { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ObjectGroup"/> class.
    /// </summary>
    public ObjectGroup() : base(Tag.ObjectGroup)
    {
        objectList = new List<MapObject>();
        DrawOrder = DrawOrder.TopDown;
    }

#if JSON_READING
    internal ObjectGroup(Utf8JsonReader reader) : base(Tag.ObjectGroup)
    {
        // TODO
    }
#endif
    
    internal ObjectGroup(XmlReader reader) : base(reader, Tag.ObjectGroup)
    {
        objectList = new List<MapObject>();

        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.DrawOrder:
                    DrawOrder = ParseEnum<DrawOrder>(reader.Value);
                    break;
                case Tag.Id:
                case Tag.Name:
                case Tag.Color:
                case Tag.X:
                case Tag.Y:
                case Tag.Width:
                case Tag.Height:
                case Tag.Opacity:
                case Tag.Visible:
                case Tag.TintColor:
                case Tag.OffsetX:
                case Tag.OffsetY:
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
                case Tag.Object:
                    objectList.Add(new MapObject(null, reader));
                    break;
                case Tag.Properties:
                    break;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
        }
    }
}