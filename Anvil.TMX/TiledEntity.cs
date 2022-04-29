#if JSON_READING
using System.Text.Json;
#endif
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;

namespace Anvil.TMX;

/// <summary>
/// Abstract base class for all serializable types defined by the TMX specification.
/// </summary>
public abstract class TiledEntity
{
    /// <summary>
    /// The XML element name that describes this object.
    /// </summary>
    protected readonly string TagName;
    private readonly bool isEmpty;

    /// <summary>
    /// Initializes a new instance of the <see cref="TiledEntity"/> class.
    /// </summary>
    /// <param name="tagName">The XML element name that describes this object.</param>
    protected TiledEntity(string tagName)
    {
        TagName = tagName;
    }

#if JSON_READING
    private protected bool ReadChild(Utf8JsonReader reader, out string propertyName)
    {
        Debug.Assert(reader.TokenType == JsonTokenType.StartObject);
        
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;
            if (reader.TokenType != JsonTokenType.PropertyName)
                continue;
            
            propertyName = reader.GetString() ?? string.Empty;
            if (propertyName.Length == 0)
                continue;

            reader.Read();
            return true;
        }

        propertyName = string.Empty;
        return false;
    }
#endif

    private protected TiledEntity(XmlReader reader, string tagName)
    {
        Debug.Assert(reader.NodeType == XmlNodeType.Element);
        Debug.Assert(string.Equals(reader.Name, tagName, StringComparison.Ordinal));

        reader.MoveToContent();
        TagName = tagName;
        isEmpty = reader.IsEmptyElement;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private protected bool ReadChild(XmlReader reader)
    {
        if (isEmpty)
            return false;

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement &&
                string.Equals(reader.Name, TagName, StringComparison.Ordinal))
                return false;

            if (reader.NodeType == XmlNodeType.Element)
                return true;
        }

        return false;
    }
    
    [Conditional("DEBUG")]
    private protected void UnhandledAttribute(string attribute)
    {
        Console.Error.WriteLine($"Unhandled attribute \"{attribute}\" in <{TagName}> element.");
    }
    
    [Conditional("DEBUG")]
    private protected void UnhandledChild(string childElement)
    {
        Console.Error.WriteLine($"Unhandled child <{childElement}> in <{TagName}> element.");
    }

#if JSON_READING
    [Conditional("DEBUG")]
    private protected void UnhandledProperty(string name)
    {
        Console.Error.WriteLine($"Unhandled child property \"{name}\" in \"{TagName}\" JSON object.");
    }
#endif
    
    private protected static TEnum ParseEnum<TEnum>(string value) where TEnum : struct, Enum
    {
        var stripped = Regex.Replace(value, "[_-]", string.Empty);
        return Enum.Parse<TEnum>(stripped, true);
    }
}