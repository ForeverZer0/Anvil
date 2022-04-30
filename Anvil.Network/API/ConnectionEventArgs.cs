using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Arguments used with network connection events.
/// </summary>
[PublicAPI]
public class ConnectionEventArgs
{
    /// <summary>
    /// Gets the connection for the event.
    /// </summary>
    public IClientConnection Connection { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="ConnectionEventArgs"/> class.
    /// </summary>
    /// <param name="connection">The connection for the event.</param>
    public ConnectionEventArgs(IClientConnection connection)
    {
        Connection = connection;
    }
}

/// <summary>
/// Arguments used with network disconnection events.
/// </summary>
[PublicAPI]
public class DisconnectEventArgs : ConnectionEventArgs
{
    /// <summary>
    /// Gets a flag indicating if the server has been notified that the connection has been closed.
    /// </summary>
    public bool Notified { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="DisconnectEventArgs"/> class.
    /// </summary>
    /// <param name="connection">The connection for the event.</param>
    /// <param name="notified">Indicates if the server has been notified that the connection has been closed.</param>
    public DisconnectEventArgs(IClientConnection connection, bool notified) : base(connection)
    {
        Notified = notified;
    }
}