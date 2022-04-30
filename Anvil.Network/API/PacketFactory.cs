using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Provides an interface for registering and organizing numerous packet types, as well as efficiently creating new
/// instances of the them based on their registered IDs.
/// </summary>
/// <typeparam name="TPacketId">An enumeration/numerical type that is used as the identifier for the packet types.</typeparam>
/// <typeparam name="TReader">The reader type used by the packets managed by this instance.</typeparam>
/// <typeparam name="TWriter">The writer type used by the packets managed by this instance.</typeparam>
/// <remarks>This class is thread-safe.</remarks>
[PublicAPI]
public class PacketFactory<TPacketId, TReader, TWriter> 
    where TPacketId : unmanaged, IComparable<TPacketId>, IComparable, IEquatable<TPacketId>
    where TReader : IPacketReader 
    where TWriter : IPacketWriter
{
    private readonly ConcurrentDictionary<Type, TPacketId> packetTypes;
    private readonly ConcurrentDictionary<TPacketId, Func<IPacket<TReader, TWriter>>> packetActivators;

    /// <summary>
    /// Creates a new instance of the <see cref="PacketFactory{TPacketId,TReader,TWriter}"/> class.
    /// </summary>
    public PacketFactory()
    {
        packetTypes = new ConcurrentDictionary<Type, TPacketId>();
        packetActivators = new ConcurrentDictionary<TPacketId, Func<IPacket<TReader, TWriter>>>();
    }

    /// <summary>
    /// Registers a compatible packet type to use the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">A unique identifier for the specified packet type.</param>
    /// <typeparam name="TPacket">A packet type to register.</typeparam>
    /// <exception cref="ArgumentException">The packet <paramref name="id"/> is already registered.</exception>
    public void Register<TPacket>(TPacketId id) where TPacket : IPacket<TReader, TWriter>, new()
    {
        if (packetActivators.ContainsKey(id))
            throw new ArgumentException($"Packet with ID {id} is already registered", nameof(id));

        var type = typeof(TPacket);
        var activator = Emit.Ctor<Func<IPacket<TReader, TWriter>>>(type);

        if (!packetActivators.TryAdd(id, activator) || !packetTypes.TryAdd(type, id))
            throw new InvalidOperationException($"Failed to register packet with ID: {id}");
    }

    /// <summary>
    /// Registers a compatible packet type to use the specified <paramref name="id"/>.
    /// </summary>
    /// <param name="id">A unique identifier for the specified packet type.</param>
    /// <param name="type">A packet type to register.</param>
    /// <exception cref="ArgumentException">
    /// The packet <paramref name="id"/> is already registered or incompatible <paramref name="type"/> is specified.
    /// </exception>
    public void Register(TPacketId id, Type type)
    {
        if (!type.IsAssignableTo(typeof(IPacket<TReader, TWriter>)))
            throw new ArgumentException($"The specified type must be assignable to {typeof(IPacket<TReader, TWriter>)}.", nameof(type));
        
        var activator = Emit.Ctor<Func<IPacket<TReader, TWriter>>>(type);
        packetActivators[id] = activator;
        packetTypes[type] = id;
    }

    /// <summary>
    /// Retrieves the ID used for the specified packet <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A <see cref="Type"/> registered with this instance.</param>
    /// <returns>The unique ID for the specified packet <paramref name="type"/>.</returns>
    /// <exception cref="ArgumentException">The specified <paramref name="type"/> is not registered.</exception>
    public TPacketId GetId(Type type)
    {
        if (!packetTypes.TryGetValue(type, out var id))
            throw new ArgumentException($"The type {type} is not registered.", nameof(type));
        return id;
    }
    
    /// <summary>
    /// Retrieves the ID used for the specified packet type.
    /// </summary>
    /// <typeparam name="TPacket">A packet type registered with this instance.</typeparam>
    /// <returns>The unique ID for the specified packet type.</returns>
    /// <exception cref="ArgumentException">The specified type is not registered.</exception>
    public TPacketId GetId<TPacket>() where TPacket : IPacket<TReader, TWriter> => GetId(typeof(TPacket));

    /// <summary>
    /// Safely attempts to retrieve the ID used for the specified packet <paramref name="type"/>.
    /// </summary>
    /// <param name="type">A packet <see cref="Type"/> registered with this instance.</param>
    /// <param name="id">When successful, contains the ID of the registered <paramref name="type"/>.</param>
    /// <returns><c>true</c> if type is registered and <paramref name="id"/> is valid, otherwise <c>false</c>.</returns>
    public bool TryGetId(Type type, out TPacketId id) => packetTypes.TryGetValue(type, out id);

    /// <summary>
    /// Safely attempts to retrieve the ID used for the specified packet <see cref="Type"/>.
    /// </summary>
    /// <param name="id">When successful, contains the ID of the registered <see cref="Type"/>.</param>
    /// <typeparam name="TPacket">A packet <see cref="Type"/> registered with this instance.</typeparam>
    /// <returns><c>true</c> if type is registered and <paramref name="id"/> is valid, otherwise <c>false</c>.</returns>
    public bool TryGetId<TPacket>(out TPacketId id) where TPacket : IPacket<TReader, TWriter>
    {
        return TryGetId(typeof(TPacket), out id);
    }

    /// <summary>
    /// Retrieves a packet instance with the registered packet <see cref="id"/>.
    /// </summary>
    /// <param name="id">The registered ID of the packet.</param>
    /// <typeparam name="TPacket">A packet type.</typeparam>
    /// <returns>The newly created packet instance.</returns>
    /// <exception cref="ArgumentException">A packet with the <paramref name="id"/> is not registered.</exception>
    public TPacket GetPacket<TPacket>(TPacketId id) where TPacket : IPacket<TReader, TWriter>
    {
        if (!packetActivators.TryGetValue(id, out var activator))
            throw new ArgumentException($"No registered packet type with  ID {id}.", nameof(id));
        return (TPacket) activator.Invoke();
    }
    
    /// <summary>
    /// Retrieves a packet instance with the registered packet <see cref="id"/>.
    /// </summary>
    /// <param name="id">The registered ID of the packet.</param>
    /// <returns>The newly created packet instance.</returns>
    /// <exception cref="ArgumentException">A packet with the <paramref name="id"/> is not registered.</exception>
    public IPacket<TReader, TWriter> GetPacket(TPacketId id)
    {
        if (!packetActivators.TryGetValue(id, out var activator))
            throw new ArgumentException($"No registered packet type with  ID {id}.", nameof(id));
        return activator.Invoke();
    }

    /// <summary>
    /// Safely attempts to retrieve a packet with the registered packet <see cref="id"/>.
    /// </summary>
    /// <param name="id">The registered ID of the packet to retrieve.</param>
    /// <param name="value">A variable to receive the packet when successful.</param>
    /// <typeparam name="TPacket">The packet type registered with <paramref name="id"/>.</typeparam>
    /// <returns>
    /// <c>true</c> if packet with <paramref name="id"/> was found and successfully created, otherwise <c>false</c>.
    /// </returns>
    public bool TryGetPacket<TPacket>(TPacketId id, out TPacket value) where TPacket : IPacket<TReader, TWriter>
    {
        if (packetActivators.TryGetValue(id, out var activator))
        {
            value = (TPacket) activator.Invoke();
            return true;
        }

        value = default!;
        return false;
    }
    
    /// <summary>
    /// Safely attempts to retrieve a packet with the registered packet <see cref="id"/>.
    /// </summary>
    /// <param name="id">The registered ID of the packet to retrieve.</param>
    /// <param name="value">A variable to receive the packet when successful.</param>
    /// <returns>
    /// <c>true</c> if packet with <paramref name="id"/> was found and successfully created, otherwise <c>false</c>.
    /// </returns>
    public bool TryGetPacket(TPacketId id, out IPacket<TReader, TWriter> value)
    {
        if (packetActivators.TryGetValue(id, out var activator))
        {
            value = activator.Invoke();
            return true;
        }

        value = default!;
        return false;
    }
}