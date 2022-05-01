using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Marks a type as a network packet, allowing them to be registered automatically.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false), PublicAPI]
public class PacketAttribute : Attribute
{
    /// <summary>
    /// Gets the network direction the packet is sent to/from.
    /// </summary>
    public NetworkDirection Direction { get; }
    
    /// <summary>
    /// Gets the client state that the packet is valid for.
    /// </summary>
    public ClientState State { get; }

    /// <summary>
    /// Gets the numerical identifier for the packet, which is unique among other packets registered for the same
    /// <see cref="ClientState"/>.
    /// </summary>
    public int ClientBoundId { get; }

    /// <summary>
    /// Gets the numerical identifier for the packet, which is unique among other packets registered for the same
    /// <see cref="ClientState"/>.
    /// </summary>
    public int ServerBoundId { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="PacketAttribute"/> class.
    /// </summary>
    /// <param name="direction">The direction this packet travels in the network.</param>
    /// <param name="state">The state of the client this packet is used for.</param>
    /// <param name="id">
    /// An identifier for this packet that is unique among other packets with the same <paramref name="direction"/>
    /// and <paramref name="state"/>.
    /// </param>
    public PacketAttribute(NetworkDirection direction, ClientState state, int id)
    {
        Direction = direction;
        State = state;
        ClientBoundId = id;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="PacketAttribute"/> class.
    /// </summary>
    /// <param name="direction">The direction this packet travels in the network.</param>
    /// <param name="state">The state of the client this packet is used for.</param>
    /// <param name="id">
    /// An identifier for this packet that is unique among other packets with the same <paramref name="direction"/>
    /// and <paramref name="state"/>.
    /// </param>
    public PacketAttribute(NetworkDirection direction, ClientState state, Enum id)
    {
        if (Marshal.SizeOf(Enum.GetUnderlyingType(id.GetType())) != sizeof(int))
            throw new ConstraintException("Enum packet ID must be backed by a 32-bit integer.");

        Direction = direction;
        State = state;
        
        var i = Unsafe.As<Enum, int>(ref id);
        if (direction.HasFlag(NetworkDirection.ClientBound))
        {
            ServerBoundId = int.MaxValue;
            ClientBoundId = i;
        }
        else
        {
            ServerBoundId = i;
            ClientBoundId = int.MaxValue;
        }
    }


    /// <summary>
    /// Creates a new instance of the <see cref="PacketAttribute"/> class.
    /// </summary>
    /// <param name="direction">The direction this packet travels in the network.</param>
    /// <param name="state">The state of the client this packet is used for.</param>
    /// <param name="clientBoundId">
    /// An identifier that is unique among other client-bound packets with the specified <paramref name="state"/>. 
    /// </param>
    /// <param name="serverBoundId">
    /// An identifier that is unique among other client-bound packets with the specified <paramref name="state"/>. 
    /// </param>
    /// <remarks>
    /// This overload is only used when specifying <see cref="NetworkDirection.Both"/> for the
    /// <paramref name="direction"/>.
    /// </remarks>
    public PacketAttribute(NetworkDirection direction, ClientState state, int clientBoundId, int serverBoundId)
    {
        Direction = direction;
        State = state;
        ClientBoundId = clientBoundId;
        ServerBoundId = serverBoundId;
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="PacketAttribute"/> class.
    /// </summary>
    /// <param name="direction">The direction this packet travels in the network.</param>
    /// <param name="state">The state of the client this packet is used for.</param>
    /// <param name="clientBoundId">
    /// An identifier that is unique among other client-bound packets with the specified <paramref name="state"/>. 
    /// </param>
    /// <param name="serverBoundId">
    /// An identifier that is unique among other client-bound packets with the specified <paramref name="state"/>. 
    /// </param>
    /// <remarks>
    /// This overload is only used when specifying <see cref="NetworkDirection.Both"/> for the
    /// <paramref name="direction"/>.
    /// </remarks>
    public PacketAttribute(NetworkDirection direction, ClientState state, Enum clientBoundId, Enum serverBoundId)
    {
        if (Marshal.SizeOf(Enum.GetUnderlyingType(clientBoundId.GetType())) != sizeof(int))
            throw new ArgumentException("Enum packet ID must be backed by a 32-bit integer.", nameof(clientBoundId));
        if (Marshal.SizeOf(Enum.GetUnderlyingType(serverBoundId.GetType())) != sizeof(int))
            throw new ArgumentException("Enum packet ID must be backed by a 32-bit integer.", nameof(serverBoundId));
        
        Direction = direction;
        State = state;
        ClientBoundId = Unsafe.As<Enum, int>(ref clientBoundId);
        ServerBoundId = Unsafe.As<Enum, int>(ref serverBoundId);
    }
}