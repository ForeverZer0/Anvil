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