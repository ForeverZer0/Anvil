using System.Net.Sockets;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents an object that handles the creation of connections with a <see cref="Socket"/>.
/// </summary>
[PublicAPI]
public interface IConnectionFactory
{
    /// <summary>
    /// Occurs when a connection has been created.
    /// </summary>
    event EventHandler<ConnectionEventArgs>? ConnectionCreated;

    /// <summary>
    /// Creates a new connection with the specified <paramref name="direction"/> and <paramref name="socket"/>.
    /// </summary>
    /// <param name="direction">A value describing the direction of the network traffic.</param>
    /// <param name="socket">The socket to bind to.</param>
    /// <returns>The newly created connection.</returns>
    IClientConnection? Create(NetworkDirection direction, Socket socket);
}

/// <summary>
/// Represents an object that handles the creation of connections with a <see cref="Socket"/>.
/// </summary>
[PublicAPI]
public interface IConnectionFactory<out TConnection> : IConnectionFactory where TConnection : IClientConnection 
{
    /// <summary>
    /// Creates a new connection with the specified <paramref name="direction"/> and <paramref name="socket"/>.
    /// </summary>
    /// <param name="direction">A value describing the direction of the network traffic.</param>
    /// <param name="socket">The socket to bind to.</param>
    /// <returns>The newly created connection.</returns>
    new TConnection? Create(NetworkDirection direction, Socket socket);

    /// <inheritdoc />
    /// <remarks>Default interface implementation.</remarks>
    IClientConnection? IConnectionFactory.Create(NetworkDirection direction, Socket socket) => Create(direction, socket);
}