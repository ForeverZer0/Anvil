using System.Net;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents a network connected server that hosts to clients.
/// </summary>
/// <typeparam name="TIncoming">The 16-bit enum type used for incoming packet identifiers.</typeparam>
/// <typeparam name="TOutgoing">The 16-bit enum type used for outgoing packet identifiers.</typeparam>
[PublicAPI]
public interface IServer<TIncoming, TOutgoing> : INetworkEndPoint<TIncoming> 
    where TOutgoing : unmanaged, Enum
    where TIncoming : unmanaged, Enum
{

    /// <summary>
    /// Gets the maximum number of clients that can be connected to the server at one time.
    /// </summary>
    int MaxClients { get; }
    
    /// <summary>
    /// Gets a flag indicating if the server is actively running and available to clients.
    /// </summary>
    bool IsRunning { get; }
    
    /// <summary>
    /// Gets the IP address where the server is hosting.
    /// </summary>
    IPAddress Host { get; }
    
    /// <summary>
    /// Gets the port where the server is hosting.
    /// </summary>
    int Port { get; }
    
    /// <summary>
    /// Gets a collection of the currently connected clients.
    /// </summary>
    /// <remarks>This collection is read-only.</remarks>
    ICollection<IConnection> Clients { get; }

    /// <summary>
    /// Asynchronously starts the server.
    /// </summary>
    /// <param name="token">A token that can be used to abort the running task.</param>
    /// <returns>The task of the running server.</returns>
    Task StartAsync(CancellationToken token);

    /// <summary>
    /// Stops the running state of the server and closes all connections.
    /// </summary>
    void Stop();
    
    /// <summary>
    /// Sends a <paramref name="packet"/> to the specified <paramref name="connection"/>.
    /// </summary>
    /// <param name="connection">The connection used for communicating with the client.</param>
    /// <param name="id">The outgoing ID for client-bound packet.</param>
    /// <param name="packet">A <see cref="IPacket"/> instance containing the payload.</param>
    void Send(IConnection connection, TOutgoing id, IPacket packet);
    
    /// <summary>
    /// Asynchronously sends a <paramref name="packet"/> to the specified <paramref name="connection"/>.
    /// </summary>
    /// <param name="connection">The connection used for communicating with the client.</param>
    /// <param name="id">The outgoing ID for client-bound packet.</param>
    /// <param name="packet">A <see cref="IPacket"/> instance containing the payload.</param>
    /// <param name="token">A token that can be used to abort the running task.</param>
    /// <returns>A <see cref="Task"/> that completes when the packet completes sending.</returns>
    Task SendAsync(IConnection connection, TOutgoing id, IPacket packet, CancellationToken token);
    
    /// <summary>
    /// Broadcasts a <paramref name="packet"/> to all connected clients.
    /// </summary>
    /// <param name="id">The outgoing ID for client-bound packet.</param>
    /// <param name="packet">A <see cref="IPacket"/> instance containing the payload.</param>
    void SendAll(TOutgoing id, IPacket packet);
    
    /// <summary>
    /// Asynchronously broadcasts a <paramref name="packet"/> to all connected clients.
    /// </summary>
    /// <param name="id">The outgoing ID for client-bound packet.</param>
    /// <param name="packet">A <see cref="IPacket"/> instance containing the payload.</param>
    /// <param name="token">A token that can be used to abort the running task.</param>
    /// <returns>A <see cref="Task"/> that completes when all packets are sent.</returns>
    Task SendAllAsync(TOutgoing id, IPacket packet, CancellationToken token);

    /// <summary>
    /// Disconnects the specified <paramref name="connection"/> and sends a disconnect packet with optional message.
    /// </summary>
    /// <param name="connection">The connection to remove.</param>
    /// <param name="reason">A strongly-typed constant describing the general reason for the disconnection.</param>
    /// <param name="message">An optional text message to send with the disconnect packets.</param>
    void Disconnect(IConnection connection, DisconnectReason reason, string? message = null);
}