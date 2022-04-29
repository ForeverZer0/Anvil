using System.Collections;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// List-like container for <see cref="WangSet"/> instances.
/// </summary>
[PublicAPI]
public class WangSets : PropertyContainer, IList<WangSet>
{
    private IList<WangSet> wangSets;

    /// <inheritdoc />
    public IEnumerator<WangSet> GetEnumerator() => wangSets.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) wangSets).GetEnumerator();

    /// <inheritdoc />
    public void Add(WangSet item) => wangSets.Add(item);

    /// <inheritdoc />
    public void Clear() => wangSets.Clear();

    /// <inheritdoc />
    public bool Contains(WangSet item) => wangSets.Contains(item);

    /// <inheritdoc />
    public void CopyTo(WangSet[] array, int arrayIndex) => wangSets.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(WangSet item) => wangSets.Remove(item);

    /// <inheritdoc />
    public int Count => wangSets.Count;

    /// <inheritdoc />
    public bool IsReadOnly => wangSets.IsReadOnly;

    /// <inheritdoc />
    public int IndexOf(WangSet item) => wangSets.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, WangSet item) => wangSets.Insert(index, item);

    /// <inheritdoc />
    public void RemoveAt(int index) => wangSets.RemoveAt(index);

    /// <inheritdoc />
    public WangSet this[int index]
    {
        get => wangSets[index];
        set => wangSets[index] = value;
    }

    /// <summary>
    /// Creates a new default instance of the <see cref="WangSets"/> class.
    /// </summary>
    public WangSets() : base(Tag.WangSets)
    {
        wangSets = new List<WangSet>();
    }

    /// <summary>
    /// Creates a new instance of the <see cref="WangSets"/> class with the specified <paramref name="items"/>.
    /// </summary>
    /// <param name="items">A collection of <see cref="WangSet"/> instances to populate this list,</param>
    public WangSets(IEnumerable<WangSet> items) : base(Tag.WangSets)
    {
        wangSets = new List<WangSet>(items);
    }

    internal WangSets(XmlReader reader) : base(reader, Tag.WangSets)
    {
        wangSets = new List<WangSet>();
        while (ReadChild(reader))
        {
            if (!string.Equals(reader.Name, Tag.WangSet, StringComparison.Ordinal))
            {
                UnhandledChild(reader.Name);
                continue;
            }
            
            Add(new WangSet(reader));
        }
    }
}