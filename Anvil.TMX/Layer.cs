using System.Diagnostics;
using System.Numerics;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Abstract base class for all layer types that make up a <see cref="Map"/>.
/// </summary>
[PublicAPI, DebuggerDisplay("{Name} (Id = {Id})")]
public abstract class Layer : PropertyContainer, IEquatable<Layer>, INamed
{
    private float opacity;
    private Vector2 offset;
    
    /// <summary>
    /// Gets the parent map of this layer.
    /// </summary>
    public Map Map { get; }
    
    /// <summary>
    /// Gets the unique ID for this map layer.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets or sets the name of this layer.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the opacity of this layer, where <c>0.0</c> is fully translucent, and <c>1.0</c>
    /// if fully opaque.
    /// </summary>
    public float Opacity
    {
        get => opacity;
        set => opacity = Math.Clamp(value, 0.0f, 1.0f);
    }

    /// <summary>
    /// Gets or sets a value indicating if this layer is hidden.
    /// </summary>
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the offset of this layer on each axis.
    /// </summary>
    public Vector2 Offset
    {
        get => offset;
        set => offset = value;
    }

    /// <summary>
    /// Gets or sets the offset of this layer on the x-axis.
    /// </summary>
    public float OffsetX
    {
        get => offset.X;
        set => offset.X = value;
    }
    
    /// <summary>
    /// Gets or sets the offset of this layer on the y-axis.
    /// </summary>
    public float OffsetY
    {
        get => offset.Y;
        set => offset.Y = value;
    }
    
    /// <summary>
    /// Gets or sets a tint color that will be multiplied with any pixels drawn by this layer.
    /// </summary>
    public ColorF? TintColor { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Layer"/> class.
    /// </summary>
    private protected Layer(Map map, string tagName) : base(tagName)
    {
        Map = map;
        Id = map.NextLayerId++;
        Name = string.Empty;
        opacity = 1.0f;
        Visible = true;
    }

    private protected Layer(Map map, XmlReader reader, string tagName) : base(reader, tagName)
    {
        Map = map;
        Name = string.Empty;
        opacity = 1.0f;
        Visible = true;

        if (!reader.MoveToAttribute(Tag.Id))
            throw new FormatException($"Required \"{Tag.Id}\" attribute not present in element.");
        Id = reader.ReadContentAsInt();
    }

    private protected void ProcessAttribute(XmlReader reader)
    {
        switch (reader.Name)
        {
            case Tag.Id:
                // Handled in constructor
                break;
            case Tag.Name:
                Name = reader.Value;
                break;
            case Tag.OffsetX:
                OffsetX = reader.ReadContentAsFloat();
                break;
            case Tag.OffsetY:
                OffsetY = reader.ReadContentAsFloat();
                break;
            case Tag.Opacity:
                Opacity = reader.ReadContentAsFloat();
                break;
            case Tag.Visible:
                Visible = reader.ReadContentAsBoolean();
                break;
            case Tag.TintColor:
                TintColor = ColorF.Parse(reader.Value);
                break;
            default:
                UnhandledAttribute(reader.Name);
                break;
        }
    }

    /// <inheritdoc />
    public bool Equals(Layer? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Map.Equals(other.Map) && Id == other.Id;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Layer) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Map, Id);

    /// <summary>
    /// Determines whether two specified layers have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Layer"/> to compare.</param>
    /// <param name="right">The second <see cref="Layer"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Layer? left, Layer? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified layers have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Layer"/> to compare.</param>
    /// <param name="right">The second <see cref="Layer"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Layer? left, Layer? right) => !Equals(left, right);
}