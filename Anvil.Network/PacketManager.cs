using System.Collections.Concurrent;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Anvil.Logging;
using Anvil.Network.API;
using JetBrains.Annotations;

namespace Anvil.Network;

/// <summary>
/// Provides an interface for registering packet types, and efficiently creating new instances with factory
/// methods. Any packet type that is used by this library must first be registered via this class.
/// </summary>
[PublicAPI]
public static class PacketManager
{
    /// <summary>
    /// Static constructor.
    /// </summary>
    static PacketManager()
    {
        ActivatorCache = new ConcurrentDictionary<int, Func<IPacket>>();
        Log = LogManager.GetLogger("PacketManager");
    }

    /// <summary>
    /// Creates a message with the <see cref="Type"/> registered under the specified <paramref name="direction"/> and
    /// unique <paramref name="id"/>.
    /// </summary>
    /// <param name="direction">Value indicating the target the message is sent to.</param>
    /// <param name="id">The unique identifier for this message.</param>
    /// <returns>A newly created message instance.</returns>
    /// <exception cref="TypeLoadException">Message type is not registered.</exception>
    public static IPacket Factory(Direction direction, short id) => Factory(direction, Unsafe.As<short, ushort>(ref id));
    
    /// <inheritdoc cref="Factory(Anvil.Network.API.Direction,short)"/>
    /// <typeparam name="TEnum16">A 16-bit enum type used for packet identifiers.</typeparam>
    public static IPacket Factory<TEnum16>(Direction direction, TEnum16 id) => Factory(direction, Unsafe.As<TEnum16, ushort>(ref id));
    
    /// <inheritdoc cref="Factory(Anvil.Network.API.Direction,short)"/>
    [CLSCompliant(false)]
    public static IPacket Factory(Direction direction, ushort id)
    {
        var hashCode = ComputeHash(direction, id);
        if (ActivatorCache.TryGetValue(hashCode, out var func))
            return func.Invoke();
        throw new TypeLoadException("No packet type with the specified direction, state, and ID is registered.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ConstraintException"></exception>
    public static bool Register(Direction direction, short id, Type type)
    {
        return Register(direction, Unsafe.As<short, ushort>(ref id), type);
    }
    
    /// <inheritdoc cref="Register(Anvil.Network.API.Direction,short,System.Type)"/>
    /// <typeparam name="TEnum16">A 16-bit enum type used for packet identifiers.</typeparam>
    public static bool Register<TEnum16>(Direction direction, TEnum16 id, Type type) where TEnum16 : unmanaged, Enum
    {
        Binary.AssetSize<TEnum16>(sizeof(short));
        return Register(direction, Unsafe.As<TEnum16, ushort>(ref id), type);
    }
    
    /// <inheritdoc cref="Register(Anvil.Network.API.Direction,short,System.Type)"/>
    [CLSCompliant(false)]
    public static bool Register(Direction direction, ushort id, Type type)
    {
        var hashCode = ComputeHash(direction, id);
        if (ActivatorCache.TryGetValue(hashCode, out var func))
            return false;

        try
        {
            func = Emit.Ctor<Func<IPacket>>(type, Emit.PublicAndPrivate);
            return ActivatorCache.TryAdd(hashCode, func);
        }
        catch (Exception e)
        {
            Log.Error(e, $"Failed to register packet type: {type}");
            return false;
        }
    }

    /// <summary>
    /// Scans the specified <paramref name="assembly"/>, and registers any exported type that implements
    /// <see cref="IPacket"/> and is decorated with a <see cref="PacketAttribute"/>.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to register packet types from.</param>
    /// <returns>The number of packet types that were registered.</returns>
    public static int Register(Assembly assembly)
    {
        return ScanAssembly(assembly).Count(info => Register(info.Direction, info.Id, info.Type));
    }

    /// <summary>
    /// Scans the specified <paramref name="assembly"/> for types that implement <see cref="IPacket"/> and are decorated
    /// with a <see cref="PacketAttribute"/>, and yields each to an enumerator.
    /// </summary>
    /// <param name="assembly">The <see cref="Assembly"/> to query.</param>
    /// <returns>An enumerator that iterates over each exported packet type.</returns>
    public static IEnumerable<PacketInfo> ScanAssembly(Assembly assembly)
    {
        return from type in assembly.GetExportedTypes() 
            where type.IsAssignableTo(typeof(IPacket)) 
            from attribute in type.GetCustomAttributes<PacketAttribute>() 
            select new PacketInfo(attribute.Direction, attribute.Id, type);
    }

    private static int ComputeHash(Direction direction, ushort id)
    {
        return (Unsafe.As<Direction, int>(ref direction) << DIRECTION_SHIFT) | id;
    }
    
    private const int DIRECTION_SHIFT = 16;

    private static readonly ConcurrentDictionary<int, Func<IPacket>> ActivatorCache;
    private static readonly ILogger Log;
}