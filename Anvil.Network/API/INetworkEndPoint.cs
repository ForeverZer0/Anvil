using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents a type that handles incoming network packets and process them.
/// </summary>
/// <typeparam name="TIncoming">The 16-bit <see cref="Enum"/> type that is used for incoming packet types.</typeparam>
/// <remarks>
/// The <typeparamref name="TIncoming"/> type <b>MUST</b> be backed by 16-bit integral type. This constraint is enforced
/// and will result in a runtime-exception otherwise. 
/// </remarks>
[PublicAPI]
public interface INetworkEndPoint<TIncoming> where TIncoming : unmanaged, Enum
{
    /// <summary>
    /// Occurs when a packet is received.
    /// </summary>
    event EventHandler<PacketEventArgs<TIncoming>>? PacketReceived;
    
    /// <summary>
    /// Occurs when a successful connection between a client and server is established.
    /// </summary>
    event EventHandler<ConnectionEventArgs> Connected;

    /// <summary>
    /// Occurs when a successful connection between a client and server is terminated.
    /// </summary>
    event EventHandler<DisconnectEventArgs>? Disconnected;
    
    /// <summary>
    /// Gets a value that indicates if events/handlers are invoked in real-time.
    /// <para/>
    /// When <c>true</c>, packets are not queued and events/handlers are invoked in real-time as they are received.
    /// <para/>
    /// When <c>false</c>, packets are queued and not emitted until an explicit call to <see cref="Tick"/> is made.
    /// </summary>
    bool RealTimeEvents { get; }
    
    /// <summary>
    /// Gets a value that indicates if packet-related events are always emitted.
    /// <para/>
    /// When <c>true</c>, packet-related events are always emitted, even when a handler is defined by
    /// <see cref="SetHandler"/>.
    /// <para/>
    /// When <c>false</c>, packet-related events are only emitted when a handler is not defined for that packet type.
    /// </summary>
    bool AlwaysInvokePacketEvents { get; }
    
    /// <summary>
    /// Performs a "frame update", invoking events/handlers for all queued packets that have been received since the
    /// last call to this method.
    /// </summary>
    /// <remarks>This method is not used when <see cref="RealTimeEvents"/> is <c>true</c>.</remarks>
    void Tick();
    
    /// <summary>
    /// Sets a callback to be called when a packet with the specified <paramref name="id"/> is received.
    /// </summary>
    /// <param name="id">The unique ID of the packet to set a handler for.</param>
    /// <param name="handler">The callback to handle the packet when it is received.</param>
    /// <returns>
    /// <c>true</c> if handler was successfully set, otherwise <c>false</c> if a handler is already defined.
    /// </returns>
    bool SetHandler(TIncoming id, PacketHandler<TIncoming> handler);
}