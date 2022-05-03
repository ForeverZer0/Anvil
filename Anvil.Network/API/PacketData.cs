
namespace Anvil.Network.API;

/// <summary>
/// Encapsulates a reconstructed network packet and metadata.
/// </summary>
/// <param name="Time">The UTC time the packet was created.</param>
/// <param name="Id">The unique ID of the packet.</param>
/// <param name="Packet">The deserialized packet.</param>
/// <typeparam name="TPacket">The primitive <see cref="Enum"/> type used for the packet identifier.</typeparam>
public sealed record PacketData<TPacket>(DateTime Time, TPacket Id, IPacket Packet) where TPacket : unmanaged, Enum;