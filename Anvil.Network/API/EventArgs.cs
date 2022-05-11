using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Arguments that are supplied with packet-related events.
/// </summary>
/// <typeparam name="TEnum16">The 16-bit enum type that is used for the packet identifier.</typeparam>
[PublicAPI]
public class PacketEventArgs<TEnum16> : EventArgs where TEnum16 : unmanaged, Enum
{
    private readonly PacketData<TEnum16> data;

    /// <summary>
    /// Gets the connection that sent the packet.
    /// </summary>
    public IConnection Source => data.Connection;

    /// <summary>
    /// Gets the unique ID of the incoming packet.
    /// </summary>
    public TEnum16 Id => data.Id;

    /// <summary>
    /// Gets the packet instance that was received.
    /// </summary>
    public IPacket Packet => data.Packet;
    
    /// <summary>
    /// Gets or sets a flag indicating if the packet was handled by the event or a previous subscriber.
    /// </summary>
    public bool Handled { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="PacketEventArgs{TEnum16}"/> class.
    /// </summary>
    /// <param name="source">The connection the packet was received from.</param>
    /// <param name="id">The unique ID of the incoming packet.</param>
    /// <param name="packet">The packet instance.</param>
    public PacketEventArgs(IConnection source, TEnum16 id, IPacket packet)
    {
        data = new PacketData<TEnum16>(source, id, packet);
        Handled = false;
    }

    internal PacketEventArgs(PacketData<TEnum16> data, bool handled)
    {
        this.data = data;
        Handled = handled;
    }
}

/// <summary>
/// Arguments that are supplied with server/client disconnection events.
/// </summary>
[PublicAPI]
public class DisconnectEventArgs : ConnectionEventArgs
{
    /// <summary>
    /// A flag indicating the general reason for the disconnection.
    /// </summary>
    public DisconnectReason Reason { get; }
    
    /// <summary>
    /// An optional message included with the disconnect.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="DisconnectEventArgs"/> class.
    /// </summary>
    /// <param name="connection">The connection that is being terminated.</param>
    /// <param name="reason">The reason for the disconnection.</param>
    /// <param name="message">Optional message for the disconnect.</param>
    public DisconnectEventArgs(IConnection connection, DisconnectReason reason, string? message = null) : base(connection)
    {
        Reason = reason;
        Message = message;
    }
}

/// <summary>
/// Arguments that are supplied with server/client connection events.
/// </summary>
[PublicAPI]
public class ConnectionEventArgs : EventArgs
{
    /// <summary>
    /// Gets the <see cref="IConnection"/> instance related to this event.
    /// </summary>
    public IConnection Connection { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="ConnectionEventArgs"/> class.
    /// </summary>
    /// <param name="connection">The remote connection.</param>
    public ConnectionEventArgs(IConnection connection)
    {
        Connection = connection;
    }
}