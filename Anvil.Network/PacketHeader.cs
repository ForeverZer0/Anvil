using System.Runtime.InteropServices;

namespace Anvil.Network;


[StructLayout(LayoutKind.Explicit, Size = 8, Pack = 0)]
public struct PacketHeader
{
    [FieldOffset(0)]
    public short PacketType;
    
    [FieldOffset(2)]
    public readonly PacketFlags Flags;
    
    [FieldOffset(3)]
    private readonly byte Reserved;

    [FieldOffset(4)]
    public readonly int Salt;

    public PacketHeader(short packetType, PacketFlags flags, int salt)
    {
        PacketType = packetType;
        Flags = flags;
        Reserved = 0;
        Salt = salt;
    }
}

public enum PacketFlags : byte
{
    /// <summary>
    /// No specialty flags.
    /// </summary>
    None,
    
    /// <summary>
    /// Connection request, sending unique salt to server, salt value of <c>0</c> in header.
    /// </summary>
    /// <remarks>Client to Server (padded to 1024 byte payload)</remarks>
    ConnectionRequest,
    
    /// <summary>
    /// Challenge reply from server, sharing unique server salt in header, and confirming salt sent from client in
    /// payload.
    /// </summary>
    /// <remarks>Server to Client</remarks>
    Challenge,
    
    /// <summary>
    /// The client returning the server and client salt XOR'ed together in header.
    /// </summary>
    /// <remarks>Client to Server (padded to 1024 byte payload)</remarks>
    ChallengeReply,
    
    /// <summary>
    /// Server replies with confirmation that connection has been successful, client changes to connected state.
    /// </summary>
    /// <remarks>Server to Client</remarks>
    ConnectionConfirm,
    
    /// <summary>
    /// Header-only packets that get sent periodically to keep connections from timing out when no data is being
    /// transmitted.
    /// </summary>
    /// <remarks>Client to Server</remarks>
    KeepAlive,
    
    /// <summary>
    /// Indicates a disconnect from either a server or client. Once received and verified, the connection should be
    /// considered terminated by both sides.
    /// </summary>
    Disconnect,
    
    
    
    Reliable,
    Partial,
    Compressed,
}