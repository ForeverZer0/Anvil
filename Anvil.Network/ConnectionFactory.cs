using System.Net.Sockets;
using Anvil.Network.API;

namespace Anvil.Network;

/// <summary>
/// Concrete <see cref="IConnectionFactory"/> implementation.
/// </summary>
internal class ConnectionFactory : IConnectionFactory<ClientConnection>
{
    /// <inheritdoc />
    public event EventHandler<ConnectionEventArgs>? ConnectionCreated;

    /// <inheritdoc />
    public ClientConnection Create(NetworkDirection direction, Socket socket)
    {
        var connection = new ClientConnection(direction, socket);
        ConnectionCreated?.Invoke(this, new ConnectionEventArgs(connection));
        return connection;
    }
}