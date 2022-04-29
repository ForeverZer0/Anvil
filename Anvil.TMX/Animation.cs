using System.Collections;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// List-like container for a collection of animation <see cref="Frame"/> objects.
/// </summary>
[PublicAPI]
public class Animation : TiledEntity, IList<Frame>
{
    private readonly List<Frame> frames;

    /// <summary>
    /// Creates a new default instance of the <see cref="Animation"/> class.
    /// </summary>
    public Animation() : base(Tag.Animation)
    {
        frames = new List<Frame>();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Animation"/> class.
    /// </summary>
    /// <param name="items">A collection of <see cref="Frame"/> objects to populate the list with.</param>
    public Animation(IEnumerable<Frame> items) : base(Tag.Animation)
    {
        frames = new List<Frame>(items);
    }

    internal Animation(XmlReader reader) : base(reader, Tag.Animation)
    {
        frames = new List<Frame>();
        while (reader.MoveToNextAttribute())
        {
            UnhandledAttribute(reader.Name);
        }
        while (ReadChild(reader))
        {
            if (reader.Name != Tag.Frame)
            {
                UnhandledChild(reader.Name);
                continue;
            }
            Add(new Frame(reader));
        }
    }
    

    /// <inheritdoc />
    public IEnumerator<Frame> GetEnumerator() => frames.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public void Add(Frame item) => frames.Add(item);

    /// <inheritdoc />
    public void Clear() => frames.Clear();

    /// <inheritdoc />
    public bool Contains(Frame item) => frames.Contains(item);

    /// <inheritdoc />
    public void CopyTo(Frame[] array, int arrayIndex) => frames.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(Frame item) => frames.Remove(item);

    /// <inheritdoc />
    public int Count => frames.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(Frame item) => frames.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, Frame item) => frames.Insert(index, item);

    /// <inheritdoc />
    public void RemoveAt(int index) => frames.RemoveAt(index);

    /// <inheritdoc />
    public Frame this[int index]
    {
        get => frames[index];
        set => frames[index] = value;
    }
}