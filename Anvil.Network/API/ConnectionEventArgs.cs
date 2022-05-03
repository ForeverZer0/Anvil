using System.Net.Sockets;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Arguments used with events related to network sockets.
/// </summary>
[PublicAPI]
public class ConnectionEventArgs : EventArgs
{
    /// <summary>
    /// Gets the connection for the event.
    /// </summary>
    public IConnection Connection { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="ConnectionEventArgs"/> class.
    /// </summary>
    /// <param name="connection">The connection related to the event.</param>
    public ConnectionEventArgs(IConnection connection)
    {
        Connection = connection;
    }
}

/// <summary>
/// Arguments to be supplied with disconnection events.
/// </summary>
[PublicAPI]
public class DisconnectEventArgs : ConnectionEventArgs
{
    /// <summary>
    /// Gets a value describing the reason for the disconnection.
    /// </summary>
    public DisconnectReason Reason { get; }

    /// <summary>
    /// Creates new instance of the <see cref="DisconnectEventArgs"/> class.
    /// </summary>
    /// <param name="connection">The <see cref="IConnection"/> instance that has been disconnected.</param>
    /// <param name="reason">The reason for the disconnection.</param>
    public DisconnectEventArgs(IConnection connection, DisconnectReason reason) : base(connection)
    {
        Reason = reason;
    }
}