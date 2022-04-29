using System.Xml;

namespace Anvil.TMX;

/// <summary>
/// Abstract base class for all <see cref="TiledEntity"/> types that contain user-defined properties.
/// </summary>
public abstract class PropertyContainer : TiledEntity, IPropertyContainer
{
    /// <inheritdoc />
    public PropertySet Properties { get; protected init; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyContainer"/> class.
    /// </summary>
    protected PropertyContainer(string tagName) : base(tagName)
    {
        Properties = new PropertySet();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyContainer"/> class.
    /// </summary>
    protected PropertyContainer(XmlReader reader, string tagName) : base(reader, tagName)
    {
        Properties = new PropertySet();
    }
}