using System.Net;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents a network connected client that connects to a server.
/// </summary>
/// <typeparam name="TIncoming">The 16-bit enum type used for incoming packet identifiers.</typeparam>
/// <typeparam name="TOutgoing">The 16-bit enum type used for outgoing packet identifiers.</typeparam>
[PublicAPI]
public interface IClient<TIncoming, in TOutgoing> : INetworkEndPoint<TIncoming>
    where TIncoming : unmanaged, Enum
    where TOutgoing : unmanaged, Enum
{
    /// <summary>
    /// Gets the connection representing the server the client is connected to, or <c>null</c> if it is not connected.
    /// </summary>
    IConnection? Server { get; }
    
    /// <summary>
    /// Gets a value indicating if the client is currently connected to a server.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Initiates a connection with a server at the specified endpoint.
    /// </summary>
    /// <param name="endPoint">The endpoint where the server is hosting from.</param>
    /// <param name="timeout">The maximum amount of time to wait before returning <c>false</c>.</param>
    /// <returns>
    /// <c>true</c> when the connection completed successfully, otherwise <c>false</c> if the <paramref name="timeout"/>
    /// duration elapsed and connection was not established.
    /// </returns>
    bool Connect(IPEndPoint endPoint, TimeSpan timeout);

    Task ConnectAsync(IPEndPoint endPoint, CancellationToken token);

    void Disconnect(DisconnectReason reason = DisconnectReason.Unspecified);

    Task DisconnectAsync(CancellationToken token, DisconnectReason reason = DisconnectReason.Unspecified);

    void Send(TOutgoing id, IPacket packet);
    
    Task SendAsync(TOutgoing id, IPacket packet, CancellationToken token);
}