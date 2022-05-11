namespace Anvil.Network.API;

/// <summary>
/// Describes a packet with its unique identifier and a <see cref="IConnection"/> .
/// </summary>
/// <param name="Connection">The <see cref="IConnection"/> the packet was received from or to be sent to.</param>
/// <param name="Id">The unique identifier for the packet.</param>
/// <param name="Packet">The <see cref="IPacket"/> instance.</param>
/// <typeparam name="T">The 16-bit enum type used for packet identifiers.</typeparam>
public record PacketData<T>(IConnection Connection, T Id, IPacket Packet) where T : unmanaged, Enum;