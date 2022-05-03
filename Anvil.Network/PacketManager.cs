using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

/// <summary>
/// Concrete general-purpose implementation of an <see cref="IPacketManager{TClientBoundId,TServerBoundId}"/>.
/// </summary>
/// <typeparam name="TClientBound">The 32-bit <see cref="Enum"/> type used for client-bound packet IDs.</typeparam>
/// <typeparam name="TServerBound">The 32-bit <see cref="Enum"/> type used for server-bound packet IDs.</typeparam>
/// <remarks>
/// <typeparamref name="TClientBound"/> and <typeparamref name="TServerBound"/> cannot be the same type.
/// <para/>
/// The API <see cref="Emit"/> class is used to dynamically generate and compile constructor delegates for the factory
/// methods, so it will be on-par with equivalent code of directly calling a constructor of a known-type at compile
/// time.
/// <para/>
/// This class is thread-safe.
/// </remarks>
[PublicAPI]
public class PacketManager<TClientBound, TServerBound> : IPacketManager<TClientBound, TServerBound>
    where TClientBound : unmanaged, Enum 
    where TServerBound : unmanaged, Enum
{
    private readonly ConcurrentDictionary<Type, PacketHash> packetTypes;
    private readonly ConcurrentDictionary<PacketHash, Func<IPacket<TClientBound>>> clientBound;
    private readonly ConcurrentDictionary<PacketHash, Func<IPacket<TServerBound>>> serverBound;

    /// <summary>
    /// Creates a new instance of the <see cref="PacketManager{TClientBoundId,TServerBoundId}"/> class.
    /// </summary>
    public PacketManager()
    {
        clientBound = new ConcurrentDictionary<PacketHash, Func<IPacket<TClientBound>>>();
        serverBound = new ConcurrentDictionary<PacketHash, Func<IPacket<TServerBound>>>();
        packetTypes = new ConcurrentDictionary<Type, PacketHash>();
    }

    /// <summary>
    /// Registers a client-bound <b>or</b> server-bound> packet <paramref name="type"/> with the specified
    /// <paramref name="state"/> and <paramref name="id"/>. 
    /// </summary>
    /// <param name="direction">
    /// The direction of the packet. Must be one of <see cref="NetworkDirection.ClientBound"/> or
    /// <see cref="NetworkDirection.ServerBound"/>, the flags should not be combined.
    /// </param>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <param name="type">The type of the packet, which must be assignable to <see cref="IPacket"/>.</param>
    /// <returns>
    /// <c>true</c> if type was successfully registered, otherwise <c>false</c> if an error occurred or a packet with
    /// the matching <paramref name="direction"/>, <paramref name="state"/> and <paramref name="id"/> is already
    /// registered.
    /// </returns>
    public bool Register(NetworkDirection direction, ClientState state, int id, Type type)
    {
        var hash = new PacketHash(direction, state, id);
        if (direction.HasFlag(NetworkDirection.ClientBound))
        {
            var func = Emit.Ctor<Func<IPacket<TClientBound>>>(type);
            if (clientBound.TryAdd(hash, func))
            {
                packetTypes.TryAdd(type, hash);
                return true;
            }
        }

        if (direction.HasFlag(NetworkDirection.ServerBound))
        {
            var func = Emit.Ctor<Func<IPacket<TServerBound>>>(type);
            if (serverBound.TryAdd(hash, func))
            {
                packetTypes.TryAdd(type, hash);
                return true;
            }
        }

        return false;
    }
    
    /// <inheritdoc />
    public bool Register(ClientState state, TClientBound id, Type type)
    {
        return Register(NetworkDirection.ClientBound, state, Unsafe.As<TClientBound, int>(ref id), type);
    }

    /// <inheritdoc />
    public bool Register(ClientState state, TServerBound id, Type type)
    {
        return Register(NetworkDirection.ServerBound, state, Unsafe.As<TServerBound, int>(ref id), type);
    }

    /// <summary>
    /// Creates a client-bound <b>or</b> server-bound packet with the <see cref="Type"/> registered under the specified
    /// <paramref name="state"/> and <paramref name="id"/>.
    /// </summary>
    /// <param name="direction">
    /// The direction of the packet. Must be one of <see cref="NetworkDirection.ClientBound"/> or
    /// <see cref="NetworkDirection.ServerBound"/>, the flags should not be combined.
    /// </param>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <returns>A newly created packet instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="direction"/> is not <see cref="NetworkDirection.ServerBound"/> or
    /// <see cref="NetworkDirection.ClientBound"/>.
    /// </exception>
    /// <exception cref="KeyNotFoundException">Packet type is not registered.</exception>
    public IPacket Factory(NetworkDirection direction, ClientState state, int id)
    {
        var hash = new PacketHash(direction, state, id);
        switch (direction)
        {
            case NetworkDirection.ServerBound:
                if (serverBound.TryGetValue(hash, out var server))
                    return server.Invoke();
                break;
            case NetworkDirection.ClientBound:
                if (clientBound.TryGetValue(hash, out var client))
                    return client.Invoke();
                break;
            case NetworkDirection.None:
            case NetworkDirection.Both:
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        throw new KeyNotFoundException("No packet is registered with specified direction, state, and ID.");
    }

    /// <summary>
    /// Creates a client-bound <b>or</b> server-bound packet with the <see cref="Type"/> registered under the specified
    /// <paramref name="state"/> and <paramref name="id"/>.
    /// </summary>
    /// <param name="direction">
    /// The direction of the packet. Must be one of <see cref="NetworkDirection.ClientBound"/> or
    /// <see cref="NetworkDirection.ServerBound"/>, the flags should not be combined.
    /// </param>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <typeparam name="TPacket">The packet type being returned.</typeparam>
    /// <returns>A newly created packet instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="direction"/> is not <see cref="NetworkDirection.ServerBound"/> or
    /// <see cref="NetworkDirection.ClientBound"/>.
    /// </exception>
    /// <exception cref="KeyNotFoundException">Packet type is not registered.</exception>
    public TPacket Factory<TPacket>(NetworkDirection direction, ClientState state, int id) where TPacket : IPacket
    {
        return (TPacket) Factory(direction, state, id);
    }

    /// <inheritdoc />
    public IPacket<TClientBound> Factory(ClientState state, TClientBound id)
    {
        var hash = new PacketHash(NetworkDirection.ClientBound, state, Unsafe.As<TClientBound, int>(ref id));
        if (clientBound.TryGetValue(hash, out var func))
            return func.Invoke();

        throw new KeyNotFoundException("No packet is registered with specified state and ID.");
    }
    
    /// <inheritdoc />
    public IPacket<TServerBound> Factory(ClientState state, TServerBound id)
    {
        var hash = new PacketHash(NetworkDirection.ServerBound, state, Unsafe.As<TServerBound, int>(ref id));
        if (serverBound.TryGetValue(hash, out var func))
            return func.Invoke();

        throw new KeyNotFoundException("No packet is registered with specified state and ID.");
    }
    
    /// <summary>
    /// Creates a client-bound packet with the <see cref="Type"/> registered under the specified
    /// <paramref name="state"/> and <paramref name="id"/>.
    /// </summary>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <typeparam name="TPacket">The packet type being returned.</typeparam>
    /// <returns>A newly created packet instance.</returns>
    /// <exception cref="KeyNotFoundException">Packet type is not registered.</exception>
    public TPacket Factory<TPacket>(ClientState state, TClientBound id) where TPacket : IPacket<TClientBound>
    {
        return (TPacket) Factory(state, id);
    }
    
    /// <summary>
    /// Creates a server-bound packet with the <see cref="Type"/> registered under the specified
    /// <paramref name="state"/> and <paramref name="id"/>.
    /// </summary>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <typeparam name="TPacket">The packet type being returned.</typeparam>
    /// <returns>A newly created packet instance.</returns>
    /// <exception cref="KeyNotFoundException">Packet type is not registered.</exception>
    public TPacket Factory<TPacket>(ClientState state, TServerBound id) where TPacket : IPacket<TServerBound>
    {
        return (TPacket) Factory(state, id);
    }

    /// <summary>
    /// Queries information about a registered packet <see cref="Type"/>.
    /// </summary>
    /// <param name="packetType">The registered packet type to query.</param>
    /// <param name="direction">A variable to receive the network direction of the packet.</param>
    /// <param name="state">A variable to receive the client state the packet is used with.</param>
    /// <param name="id">A variable to receive the packet ID.</param>
    /// <returns>
    /// <c>true</c> if type is registered and valid values were written, otherwise <c>false</c> if the type was not
    /// found.
    /// </returns>
    public bool TryDescribe(Type packetType, out NetworkDirection direction, out ClientState state, out int id)
    {
        if (!packetTypes.TryGetValue(packetType, out var hash))
        {
            direction = NetworkDirection.None;
            state = ClientState.Initial;
            id = -1;
            return false;
        }

        direction = hash.Direction;
        state = hash.State;
        id = hash.Packet;
        return true;
    }
    
    /// <inheritdoc />
    public int ScanAssembly(Assembly assembly)
    {
        var count = 0;
        foreach (var type in assembly.GetExportedTypes())
        {
            if (!type.IsAssignableTo(typeof(IPacket)))
                continue;

            foreach (var obj in type.GetCustomAttributes(false))
            {
                if (obj is not PacketAttribute attribute)
                    continue;

                if (attribute.Direction.HasFlag(NetworkDirection.ServerBound))
                {
                    Register(NetworkDirection.ServerBound, attribute.State, attribute.ServerBoundId, type);
                    count++;
                }
                if (attribute.Direction.HasFlag(NetworkDirection.ClientBound))
                {
                    Register(NetworkDirection.ClientBound, attribute.State, attribute.ClientBoundId, type);
                    count++;
                }
            }
        }

        return count;
    }
  
}