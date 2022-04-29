#if JSON_READING
using System.Text.Json;
#endif
using System.Collections;
using System.Diagnostics;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Represents a collection of unique <see cref="Property"/> objects within a dictionary-like container, where no two
/// properties contain the same name.
/// </summary>
[PublicAPI, DebuggerDisplay("Count = {Count}")]
public class PropertySet : TiledEntity, ICollection<Property>
{
    private readonly Dictionary<string, Property> properties;
    
    /// <summary>
    /// Creates a new default instance of the <see cref="PropertySet"/> class.
    /// </summary>
    public PropertySet() : base(Tag.Properties)
    {
        properties = new Dictionary<string, Property>();
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="PropertySet"/> class with the given <paramref name="values"/>.
    /// </summary>
    /// <param name="values">A collection of properties to add.</param>
    /// <remarks>No two properties may contain the same name.</remarks>
    public PropertySet(IEnumerable<Property> values) : base(Tag.Properties)
    {
        properties = new Dictionary<string, Property>();
        foreach (var property in values)
            properties.Add(property.Name, property);    
    }

    internal PropertySet(XmlReader reader) : base(reader, Tag.Properties)
    {
        properties = new Dictionary<string, Property>();
        
        while (ReadChild(reader))
        {
            if (!string.Equals(reader.Name, Tag.Property, StringComparison.Ordinal))
            {
                UnhandledChild(reader.Name);
                continue;
            }
            Add(new Property(reader));
        }
    }

#if JSON_READING
    internal PropertySet(Utf8JsonReader reader) : base(Tag.Properties)
    {
        Debug.Assert(reader.TokenType == JsonTokenType.StartArray);
        properties = new Dictionary<string, Property>();
        
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                break;
            if (reader.TokenType != JsonTokenType.StartObject)
                continue;
            Add(new Property(reader));
        }
    }
#endif

    /// <inheritdoc />
    public IEnumerator<Property> GetEnumerator()
    {
        foreach (var property in properties.Values)
            yield return property;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public void Add(Property item)
    {
        if (properties.ContainsKey(item.Name))
            return;
        properties.Add(item.Name, item);
    }

    /// <inheritdoc />
    public void Clear() => properties.Clear();

    /// <inheritdoc />
    public bool Contains(Property item) => properties.ContainsValue(item);
    
    /// <inheritdoc cref="Contains(Anvil.TMX.Property)"/>
    public bool Contains(string propertyName) => properties.ContainsKey(propertyName);

    /// <inheritdoc />
    public void CopyTo(Property[] array, int arrayIndex)
    {
        var i = 0;
        foreach (var property in properties.Values)
            array[arrayIndex + i++] = property;
    }

    /// <inheritdoc />
    public bool Remove(Property item) => properties.Remove(item.Name);
    
    /// <inheritdoc cref="Remove(Anvil.TMX.Property)"/>
    public bool Remove(string propertyName) => properties.Remove(propertyName);

    /// <inheritdoc />
    public int Count => properties.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets the property with the specified <paramref name="name"/> from the collection.
    /// </summary>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <exception cref="KeyNotFoundException">When a property with the specified <paramref name="name"/> cannot be found.</exception>
    /// <seealso cref="TryGetValue"/>
    public Property this[string name] => properties[name];

    /// <summary>
    /// Gets the property associated with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <param name="property">The property with the specified name, or <c>null</c> when not found.</param>
    /// <returns><c>true</c> if property was successfully found in the collection, otherwise <c>false</c>.</returns>
    public bool TryGetValue(string name, out Property? property) => properties.TryGetValue(name, out property);
}