using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Event handler delegate for server-related events.
/// </summary>
/// <typeparam name="TArgs"></typeparam>
[PublicAPI]
public delegate void ServerEventHandler<in TArgs>(IServer server, TArgs args) where TArgs : EventArgs;

/// <summary>
/// Arguments to be supplied with packet-related events.
/// </summary>
/// <typeparam name="TPacketId"></typeparam>
[PublicAPI]
public class PacketEventArgs<TPacketId> : EventArgs where TPacketId : unmanaged, Enum
{
    /// <summary>
    /// Gets the time the packet was received. 
    /// </summary>
    public DateTime Time { get; }
    
    /// <summary>
    /// Gets the unique ID for the packet type.
    /// </summary>
    public TPacketId Id { get; }
    
    /// <summary>
    /// Gets the packet instance.
    /// </summary>
    public IPacket<TPacketId> Packet { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="PacketEventArgs{T}"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="packet">The <see cref="IPacket"/> instance for the event.</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="packet"/> is <c>null</c>.</exception>
    public PacketEventArgs(TPacketId id, IPacket<TPacketId> packet)
    {
        Id = id;
        Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        Time = DateTime.UtcNow;
    }
}