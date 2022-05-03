using System.Reflection;
using JetBrains.Annotations;

namespace Anvil.Network.API;


/// <summary>
/// Represents a class that registers packet types and provides factory methods to generate new instances of them based
/// on unique IDs.
/// </summary>
/// <typeparam name="TClientBound">The 32-bit <see cref="Enum"/> type used for client-bound packet IDs.</typeparam>
/// <typeparam name="TServerBound">The 32-bit <see cref="Enum"/> type used for server-bound packet IDs.</typeparam>
/// <remarks>
/// <typeparamref name="TClientBound"/> and <typeparamref name="TServerBound"/> cannot be the same type.
/// </remarks>
[PublicAPI]
public interface IPacketManager<TClientBound, TServerBound> 
    where TClientBound : unmanaged, Enum 
    where TServerBound : unmanaged, Enum
{
    /// <summary>
    /// Registers a client-bound packet <paramref name="type"/> with the specified <paramref name="state"/> and
    /// <paramref name="id"/>. 
    /// </summary>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <param name="type">The type of the packet, which must be assignable to <see cref="IPacket"/>.</param>
    /// <returns>
    /// <c>true</c> if type was successfully registered, otherwise <c>false</c> if an error occurred or a client-bound
    /// packet with the matching <paramref name="state"/> and <paramref name="id"/> is already registered.
    /// </returns>
    bool Register(ClientState state, TClientBound id, Type type);

    /// <summary>
    /// Registers a server-bound packet <paramref name="type"/> with the specified <paramref name="state"/> and
    /// <paramref name="id"/>. 
    /// </summary>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <param name="type">The type of the packet, which must be assignable to <see cref="IPacket"/>.</param>
    /// <returns>
    /// <c>true</c> if type was successfully registered, otherwise <c>false</c> if an error occurred or a server-bound
    /// packet with the matching <paramref name="state"/> and <paramref name="id"/> is already registered.
    /// </returns>
    bool Register(ClientState state, TServerBound id, Type type);

    /// <summary>
    /// Creates a client-bound packet with the <see cref="Type"/> registered under the specified
    /// <paramref name="state"/> and <paramref name="id"/>.
    /// </summary>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <returns>A newly created packet instance.</returns>
    /// <exception cref="KeyNotFoundException">Packet type is not registered.</exception>
    IPacket Factory(ClientState state, TClientBound id);

    /// <summary>
    /// Creates a server-bound packet with the <see cref="Type"/> registered under the specified
    /// <paramref name="state"/> and <paramref name="id"/>.
    /// </summary>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <returns>A newly created packet instance.</returns>
    /// <exception cref="KeyNotFoundException">Packet type is not registered.</exception>
    IPacket Factory(ClientState state, TServerBound id);

    /// <summary>
    /// Scans the specified <see cref="Assembly"/> for packet types decorated with the <see cref="PacketAttribute"/>,
    /// and attempts to register them.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to scan.</param>
    /// <returns>The number of packets registered.</returns>
    int ScanAssembly(Assembly assembly);
}