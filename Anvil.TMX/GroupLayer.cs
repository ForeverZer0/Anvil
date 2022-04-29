using System.Collections;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// A layer that consists of a collection of other layers, used to organize the layers of the map in a hierarchy. 
/// </summary>
[PublicAPI]
public class GroupLayer : Layer, IGroup, IList<Layer>
{
    private readonly List<Layer> layers;

    /// <summary>
    /// Creates a new instance of the <see cref="GroupLayer"/> class.
    /// </summary>
    /// <param name="map">The parent <see cref="Map"/> instance this layer is being created within.</param>
    public GroupLayer(Map map) : base(map, Tag.Group)
    {
        layers = new List<Layer>();
    }

    internal GroupLayer(Map map, XmlReader reader) : base(map, reader, Tag.Group)
    {
        layers = new List<Layer>();
        
        while (reader.MoveToNextAttribute())
        {
            ProcessAttribute(reader);
        }

        while (ReadChild(reader))
        {
            switch (reader.Name)
            {
                case Tag.Layer:
                    Add(new TileLayer(map, reader));
                    break;
                case Tag.ImageLayer:
                    Add(new ImageLayer(map, reader));
                    break;
                case Tag.ObjectGroup:
                    Add(new ObjectLayer(map, reader));
                    break;
                case Tag.Group:
                    Add(new GroupLayer(map, reader));
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
    public IEnumerator<Layer> GetEnumerator() => layers.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => layers.GetEnumerator();

    /// <inheritdoc />
    public IEnumerable<Layer> Filter(LayerType type, bool recursive = false)
    {
        foreach (var layer in layers)
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
    public void Add(Layer item) => layers.Add(item);

    /// <inheritdoc />
    public void Clear() => layers.Clear();

    /// <inheritdoc />
    public bool Contains(Layer item) => layers.Contains(item);

    /// <inheritdoc />
    public void CopyTo(Layer[] array, int arrayIndex) => layers.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(Layer item) => layers.Remove(item);

    /// <inheritdoc />
    public int Count => layers.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(Layer item) => layers.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, Layer item) => layers.Insert(index, item);

    /// <inheritdoc />
    public void RemoveAt(int index) => layers.RemoveAt(index);

    /// <inheritdoc />
    public Layer this[int index]
    {
        get => layers[index];
        set => layers[index] = value;
    }
}