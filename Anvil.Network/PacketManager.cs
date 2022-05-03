using System.Buffers;
using System.Collections.Concurrent;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using Anvil.Logging;
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
/// </remarks>
[PublicAPI]
public class PacketManager<TClientBound, TServerBound> : IPacketManager<TClientBound, TServerBound>
    where TClientBound : unmanaged, Enum 
    where TServerBound : unmanaged, Enum
{
    private readonly ArrayPool<byte> memoryPool;
    private readonly ConcurrentDictionary<Type, PacketHash> packetTypes;
    private readonly ConcurrentDictionary<PacketHash, Func<IPacket>> packetActivators;
    private readonly ILogger? log;
    
    public EventHandler<ClientPacketEventArgs<TServerBound>>? ClientPacketReceived;
    
    public EventHandler<PacketEventArgs<TClientBound>>? ServerPacketReceived;

    /// <summary>
    /// Gets a the minimum number of bytes required to enable compression of data sent over the network, or <c>-1</c>
    /// to indicate that compression is disabled.
    /// </summary>
    public int CompressionThreshold { get; }
    
    /// <summary>
    /// Creates a new instance of the <see cref="PacketManager{TClientBoundId,TServerBoundId}"/> class.
    /// </summary>
    public PacketManager(ILogger? logger, int compressionThreshold = -1)
    {
        packetActivators = new ConcurrentDictionary<PacketHash, Func<IPacket>>();
        packetTypes = new ConcurrentDictionary<Type, PacketHash>();
        memoryPool = ArrayPool<byte>.Create();
        log = logger;
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
        var func = Emit.Ctor<Func<IPacket>>(type);
        
        if (packetActivators.TryAdd(hash, func))
        {
            packetTypes.TryAdd(type, hash);
            return true;
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
        if (packetActivators.TryGetValue(hash, out var activator))
            return activator.Invoke();
        
        throw new KeyNotFoundException("No packet is registered with specified direction, state, and ID.");
    }
    
    /// <inheritdoc />
    public IPacket Factory(ClientState state, TClientBound id)
    {
        return Factory(NetworkDirection.ClientBound, state, Unsafe.As<TClientBound, int>(ref id));
    }
    
    /// <inheritdoc />
    public IPacket Factory(ClientState state, TServerBound id)
    {
        return Factory(NetworkDirection.ClientBound, state, Unsafe.As<TServerBound, int>(ref id));
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
    
    /// <summary>
    /// Creates a client-bound packet with the <see cref="Type"/> registered under the specified
    /// <paramref name="state"/> and <paramref name="id"/>.
    /// </summary>
    /// <param name="state">The client state that the packet is valid with.</param>
    /// <param name="id">The unique identifier for this packet.</param>
    /// <typeparam name="TPacket">The packet type being returned.</typeparam>
    /// <returns>A newly created packet instance.</returns>
    /// <exception cref="KeyNotFoundException">Packet type is not registered.</exception>
    public TPacket Factory<TPacket>(ClientState state, TClientBound id) where TPacket : IPacket
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
    public TPacket Factory<TPacket>(ClientState state, TServerBound id) where TPacket : IPacket
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

    public async Task ReadClientBound(IPacketReader reader, ClientState state, CancellationToken token)
    {
        var data = await ReadPacket<TClientBound>(reader, NetworkDirection.ClientBound, state, token);
        if (data is null)
            return;
        
        ServerPacketReceived?.Invoke(this, new PacketEventArgs<TClientBound>(data));
    }
    
    public async Task ReadServerBound(IPacketReader reader, IConnection client, CancellationToken token)
    {
        var data = await ReadPacket<TServerBound>(reader, NetworkDirection.ServerBound, client.State, token);
        if (data is null)
            return;
        
        ClientPacketReceived?.Invoke(this, new ClientPacketEventArgs<TServerBound>(client, data));
    }

    private async Task<PacketData<T>?> ReadPacket<T>(IPacketReader reader, NetworkDirection direction, ClientState state, CancellationToken token) where T : unmanaged, Enum
    {
        if (!reader.Available)
            return null;
        
        var time = reader.ReadTime();
        var id = reader.ReadVarInt();
        var uncompressedSize = reader.ReadVarInt();
        var compressedSize = CompressionThreshold >= 0 && uncompressedSize > CompressionThreshold
            ? reader.ReadVarInt()
            : -1;
        
        var read = 0;
        var readSize = compressedSize > -1 ? compressedSize : uncompressedSize;
        var buffer = memoryPool.Rent(readSize);
        do
        {
            read += await reader.BaseStream.ReadAsync(buffer, read, readSize - read, token);
        } while (read < readSize);

        try
        {
            var packet = Factory(direction, state, id);
            var streamBuffer = new MemoryStream(buffer);
            if (compressedSize >= 0)
            {
                await using var gzip = new GZipStream(streamBuffer, CompressionMode.Decompress);
                packet.Decode(new PacketReader(gzip));
            }
            else
            {
                packet.Decode(new PacketReader(streamBuffer));
            }

            return new PacketData<T>(time, Unsafe.As<int, T>(ref id), packet);

        }
        catch (KeyNotFoundException e)
        {
            log?.Error(e, $"Packet is not registered with key: {direction}, {state}, and {id}");
        }
        catch (ArgumentOutOfRangeException e)
        {
            log?.Error(e, "Invalid direction specified.");
        }
        catch (Exception e)
        {
            log?.Error(e, "Packet error occurred.");
        }
        finally
        {
            memoryPool.Return(buffer);
        }

        return null;
    }
}