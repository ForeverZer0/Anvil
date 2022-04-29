#if JSON_READING
using System.Text.Json;
#endif
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Contains application-defined meta-data for TMX objects.
/// </summary>
[PublicAPI, DebuggerDisplay("Name = {Name}, Value = {Value}")]
public class Property : TiledEntity, IEquatable<Property>, INamed
{
    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    /// <remarks>No two properties within the same object can have the same name.</remarks>
    public string Name { get; }
    
    /// <summary>
    /// Gets a strongly-typed constant describing the type of the <see cref="Value"/> property.
    /// </summary>
    public PropertyType Type { get; }
    
    /// <summary>
    /// Gets a custom-defined property type.
    /// </summary>
    /// <remarks>
    /// Custom property types will have a <see cref="Type"/> of <see cref="PropertyType.Class"/>, and the
    /// <see cref="Value"/> field will be a <see cref="PropertySet"/> containing child properties.
    /// </remarks>
    public string? CustomType { get; }
    
    /// <summary>
    /// Gets the value of the property.
    /// </summary>
    /// <seealso cref="PropertyType"/>
    public object Value { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Property"/> class with the specified <paramref name="name"/> and
    /// <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The unique name of this property within its parent collection.</param>
    /// <param name="type">A enumeration describing the type of the <paramref name="value"/>.</param>
    /// <param name="value">The value of the property.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="name"/> is <c>null</c> or empty.</exception>
    public Property(string name, PropertyType type, object value) : base(Tag.Property)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        Name = name;
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Creates a new <see cref="Property"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The name of the property to create.</param>
    /// <param name="value">A <see cref="string"/> value to set.</param>
    /// <param name="isFile">Flag indicating if <paramref name="value"/> represents a filesystem path.</param>
    /// <returns>The newly created <see cref="Property"/>.</returns>
    public static Property Create(string name, string value, bool isFile = false)
    {
        return new Property(name, isFile ? PropertyType.File : PropertyType.String, value);
    }
    
    /// <summary>
    /// Creates a new <see cref="Property"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The name of the property to create.</param>
    /// <param name="value">A <see cref="int"/> value to set.</param>
    /// <returns>The newly created <see cref="Property"/>.</returns>
    public static Property Create(string name, int value) => new(name, PropertyType.Int, value);
    
    /// <summary>
    /// Creates a new <see cref="Property"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The name of the property to create.</param>
    /// <param name="value">A <see cref="float"/> value to set.</param>
    /// <returns>The newly created <see cref="Property"/>.</returns>
    public static Property Create(string name, float value) => new(name, PropertyType.Float, value);

    /// <summary>
    /// Creates a new <see cref="Property"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The name of the property to create.</param>
    /// <param name="value">A <see cref="bool"/> value to set.</param>
    /// <returns>The newly created <see cref="Property"/>.</returns>
    public static Property Create(string name, bool value) => new(name, PropertyType.Bool, value);
    
    /// <summary>
    /// Creates a new <see cref="Property"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The name of the property to create.</param>
    /// <param name="value">A <see cref="ColorF"/> value to set.</param>
    /// <returns>The newly created <see cref="Property"/>.</returns>
    public static Property Create(string name, ColorF value) => new(name, PropertyType.Color, value);

    /// <summary>
    /// Creates a new <see cref="Property"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="name">The name of the property to create.</param>
    /// <param name="value">A <see cref="MapObject"/> value to set.</param>
    /// <returns>The newly created <see cref="Property"/>.</returns>
    public static Property Create(string name, MapObject value) => new(name, PropertyType.Object, value.Id);

#if JSON_READING
    internal Property(Utf8JsonReader reader) : base(Tag.Property)
    {
        string? value = null;
        while (ReadChild(reader, out var propertyName))
        {
            switch (propertyName)
            {
                case Tag.Name:
                    Name = reader.GetString() ?? string.Empty;
                    break;
                case Tag.Type:
                    Type = Enum.TryParse(reader.GetString(), true, out PropertyType type) ? type : PropertyType.String;
                    break;
                case Tag.PropertyType:
                    CustomType = reader.GetString();
                    break;
                case Tag.Value:
                    if (reader.TokenType == JsonTokenType.StartArray)
                        Value = new PropertySet(reader);
                    else
                        value = reader.GetString();
                    break;
                default:
                    UnhandledProperty(propertyName);
                    break;
            }
        }

        if (string.IsNullOrWhiteSpace(Name))
            throw new FormatException("Property name cannot be null, empty, or only whitespace.");
        
        if (CustomType is null)
            Value = ParseValue(value, Type);
        else if (Value is not PropertySet)
            throw new FormatException("Empty child properties for custom property type.");
    }
#endif
    
    internal Property(XmlReader reader) : base(reader, Tag.Property)
    {
        Debug.Assert(reader.NodeType == XmlNodeType.Element);
        Debug.Assert(reader.Name == Tag.Property);

        string? value = null;
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Name:
                    Name = reader.ReadContentAsString();
                    break;
                case Tag.Type:
                    Type = ParseEnum<PropertyType>(reader.Value);
                    break;
                case Tag.PropertyType:
                    CustomType = reader.ReadContentAsString();
                    break;
                case Tag.Value:
                    value = reader.Value;
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }
   
        if (string.IsNullOrWhiteSpace(Name))
            throw new FormatException("Property name cannot be null, empty, or only whitespace.");
        
        if (CustomType is null)
        {
            Value = ParseValue(value, Type);
        }
        else
        {
            Debug.Assert(reader.IsStartElement(Tag.Properties));
            Value = new PropertySet(reader);
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Property"/> class with the specified <paramref name="name"/> and
    /// custom property type.
    /// </summary>
    /// <param name="name">The unique name of this property within its parent collection.</param>
    /// <param name="customType">The name of the custom property type.</param>
    /// <param name="values">A collection of child properties this custom property contains.</param>
    /// <exception cref="ArgumentNullException">
    /// When <paramref name="name"/> or <paramref name="customType"/> is <c>null</c> or empty.
    /// </exception>
    public Property(string name, string customType, IEnumerable<Property> values) : base(Tag.Property)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(customType))
            throw new ArgumentNullException(nameof(customType));
                
        Name = name;
        Type = PropertyType.Class;
        CustomType = customType;
        Value = new PropertySet(values);
    }

    protected static object ParseValue(string? value, PropertyType type)
    {
        value ??= string.Empty;
        return type switch
        {
            PropertyType.String => value,
            PropertyType.Int => int.Parse(value),
            PropertyType.Float => float.Parse(value, NumberStyles.Float),
            PropertyType.Bool => bool.Parse(value),
            PropertyType.Color => ColorF.Parse(value),
            PropertyType.File => value,
            PropertyType.Object => int.Parse(value),
            PropertyType.Class => throw new ArgumentException("Cannot parse custom property types from string."),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    /// <inheritdoc/>
    public bool Equals(Property? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Type == other.Type && CustomType == other.CustomType && Value.Equals(other.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Property) obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => Name.GetHashCode();
    
    /// <summary>
    /// Determines whether two specified properties have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Property"/> to compare.</param>
    /// <param name="right">The second <see cref="Property"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Property? left, Property? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified properties have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Property"/> to compare.</param>
    /// <param name="right">The second <see cref="Property"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Property? left, Property? right) => !Equals(left, right);
}