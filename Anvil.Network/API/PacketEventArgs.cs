using System.ComponentModel;
using System.Runtime.CompilerServices;
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
public class PacketEventArgs<TPacketId> : HandledEventArgs where TPacketId : unmanaged, Enum
{
    private readonly PacketData<TPacketId> packetData;

    /// <summary>
    /// Gets the time the packet was received. 
    /// </summary>
    public DateTime Time => packetData.Time;

    /// <summary>
    /// Gets the unique ID for the packet type.
    /// </summary>
    public TPacketId Id => packetData.Id;

    /// <summary>
    /// Gets the packet instance.
    /// </summary>
    public IPacket Packet => packetData.Packet;

    /// <summary>
    /// Creates a new instance of the <see cref="PacketEventArgs{T}"/> class.
    /// </summary>
    /// <param name="time">The time the packet was created in UTC time.</param>
    /// <param name="id">The unique ID for the packet.</param>
    /// <param name="packet">The <see cref="IPacket"/> instance for the event.</param>
    /// <exception cref="ArgumentNullException">When the <paramref name="packet"/> is <c>null</c>.</exception>
    public PacketEventArgs(DateTime time, TPacketId id, IPacket packet)
    {
        packetData = new PacketData<TPacketId>(time, id, packet);
    }

    internal PacketEventArgs(PacketData<TPacketId> data)
    {
        packetData = data;
    }
}

public class ClientPacketEventArgs<TServerBound> : PacketEventArgs<TServerBound> where TServerBound : unmanaged, Enum
{
    public IConnection Client { get; }
    
    internal ClientPacketEventArgs(IConnection client, PacketData<TServerBound> data) : base(data)
    {
        Client = client;
    }
}