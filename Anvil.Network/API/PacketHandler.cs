using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Prototype for methods that handle a specific packet type.
/// </summary>
/// <param name="source">The <see cref="IConnection"/> the packet was received from.</param>
/// <param name="id">The unique identifier for the packet.</param>
/// <param name="packet">The <see cref="IPacket"/> instance.</param>
/// <typeparam name="TIncoming">The 16-bit enum type used for packet identifiers.</typeparam>
[PublicAPI]
public delegate void PacketHandler<in TIncoming>(IConnection source, TIncoming id, IPacket packet) where TIncoming : unmanaged, Enum;