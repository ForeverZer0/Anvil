#if JSON_READING
using System.Text.Json;
#endif
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Describes a single frame within an <see cref="Animation"/>.
/// </summary>
[PublicAPI]
public sealed class Frame : TiledEntity, IEquatable<Frame>, IComparable<Frame>, IComparable
{
    /// <summary>
    /// Gets local tile ID to display when this frame is rendered.
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// Gets the duration this frame should be rendered.
    /// </summary>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="Frame"/> class with the specified tile ID and duration.
    /// </summary>
    /// <param name="id">The local tile ID of the tile to render for this frame.</param>
    /// <param name="duration">The duration of the frame.</param>
    public Frame(int id, TimeSpan duration) : base(Tag.Frame)
    {
        Id = id;
        Duration = duration;
    }

#if JSON_READING
    internal Frame(Utf8JsonReader reader) : base(Tag.Frame)
    {
        while (ReadChild(reader, out var propertyName))
        {
            switch (propertyName)
            {
                case Tag.Duration:
                    Duration = TimeSpan.FromMilliseconds(reader.GetInt32());
                    break;
                case Tag.TileId:
                    Id = reader.GetInt32();
                    break;
                default:
                    UnhandledProperty(propertyName);
                    break;
            }
        }
    }
#endif

    internal Frame(XmlReader reader) : base(reader, Tag.Frame)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.TileId:
                    Id = reader.ReadContentAsInt();
                    break;
                case Tag.Duration:
                    Duration = TimeSpan.FromMilliseconds(reader.ReadContentAsDouble());
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }
    }
    
    /// <inheritdoc />
    public bool Equals(Frame? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Duration.Equals(other.Duration);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Frame other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Id, Duration);
    
    /// <inheritdoc />
    public int CompareTo(Frame? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Id.CompareTo(other.Id);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is Frame other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Frame)}");
    }

    /// <summary>
    /// Determines whether two specified frames have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Frame"/> to compare.</param>
    /// <param name="right">The second <see cref="Frame"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Frame? left, Frame? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified frames have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Frame"/> to compare.</param>
    /// <param name="right">The second <see cref="Frame"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Frame? left, Frame? right) => !Equals(left, right);
    
    /// <summary>
    /// Indicates whether a specified <see cref="Frame"/> is less than another specified <see cref="Frame"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Frame"/> to compare.</param>
    /// <param name="right">The second <see cref="Frame"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is less than the value of <paramref name="right"/>,
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <(Frame? left, Frame? right) => Comparer<Frame>.Default.Compare(left, right) < 0;

    /// <summary>
    /// Indicates whether a specified <see cref="Frame"/> is greater than another specified <see cref="Frame"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Frame"/> to compare.</param>
    /// <param name="right">The second <see cref="Frame"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is greater than the value of <paramref name="right"/>,
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >(Frame? left, Frame? right) => Comparer<Frame>.Default.Compare(left, right) > 0;

    /// <summary>
    /// Indicates whether a specified <see cref="Frame"/> is less than or equal to another specified <see cref="Frame"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Frame"/> to compare.</param>
    /// <param name="right">The second <see cref="Frame"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is less than or equal to the value of
    /// <paramref name="right"/>, otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <=(Frame? left, Frame? right) => Comparer<Frame>.Default.Compare(left, right) <= 0;

    /// <summary>
    /// Indicates whether a specified <see cref="Frame"/> is greater than or equal to another specified <see cref="Frame"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Frame"/> to compare.</param>
    /// <param name="right">The second <see cref="Frame"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is greater than or equal to the value of
    /// <paramref name="right"/>, otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >=(Frame? left, Frame? right) => Comparer<Frame>.Default.Compare(left, right) >= 0;
}