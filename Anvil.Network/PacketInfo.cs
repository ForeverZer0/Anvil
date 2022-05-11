using System.Runtime.Serialization;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;


/// <summary>
/// Describes a <see cref="IPacket"/> type.
/// </summary>
[PublicAPI]
public sealed class PacketInfo : ISerializable, IEquatable<PacketInfo>
{
    /// <summary>
    /// Gets the network direction the packet is sent to/from.
    /// </summary>
    public Direction Direction { get; }
    
    /// <summary>
    /// The numerical identifier for the packet.
    /// </summary>
    public short Id { get; }

    /// <summary>
    /// The <see cref="Type"/> of the packet.
    /// </summary>
    public Type Type { get; }

    public PacketInfo(Direction direction, short id, Type type)
    {
        Direction = direction;
        Id = id;
        Type = type;
    }

    public PacketInfo(SerializationInfo info, StreamingContext context)
    {
        Direction = Enum.Parse<Direction>(info.GetString("direction")!, true);
        Id = info.GetInt16("id");
        Type = Type.GetType(info.GetString("type")!) ?? throw new TypeLoadException();
    }

    /// <inheritdoc />
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("direction", Enum.GetName(Direction)?.ToLowerInvariant());
        info.AddValue("id", Id);
        info.AddValue("type",  Type.AssemblyQualifiedName);
    }

    /// <inheritdoc />
    public bool Equals(PacketInfo? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Direction == other.Direction && Id == other.Id && Type == other.Type;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is PacketInfo other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine((int) Direction, Id, Type);

    public static bool operator ==(PacketInfo? left, PacketInfo? right) => Equals(left, right);

    public static bool operator !=(PacketInfo? left, PacketInfo? right) => !Equals(left, right);
}