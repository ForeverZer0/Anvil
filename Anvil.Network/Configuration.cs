using System.Net;
using System.Net.Sockets;
using Anvil.Network.API;

namespace Anvil.Network;

public abstract class BaseConfiguration
{
    /// <inheritdoc cref="NetworkEndPoint{TIncoming,TOutgoing}.Protocol"/>
    public int Protocol { get; set; } = 0;

    /// <inheritdoc cref="NetworkEndPoint{TIncoming,TOutgoing}.SocketFlags"/>
    public SocketFlags SocketFlags { get; set; } = SocketFlags.None;
    
    /// <inheritdoc cref="NetworkEndPoint{TIncoming,TOutgoing}.AlwaysInvokePacketEvents"/>
    public bool AlwaysInvokePacketEvents { get; set; } = false;
    
    /// <inheritdoc cref="NetworkEndPoint{TIncoming,TOutgoing}.RealTimeEvents"/>
    public bool RealTime { get; set; } = true;

    /// <summary>
    /// Gets or sets the endian of packet data reading/writing to the network.
    /// </summary>
    /// <remarks>
    /// This may not, nor does it need to, match the native endianness of the local machine. 
    /// </remarks>
    public Endianness Endianness { get; set; } = Endianness.Little;
}

public class ClientConfiguration : BaseConfiguration
{
    /// <summary>
    /// Gets or sets the interval between "keep-alive" or "heartbeat" packets are automatically sent to the server.
    /// </summary>
    /// <remarks>Default: 1 second</remarks>
    public TimeSpan KeepAliveInterval { get; set; } = TimeSpan.FromSeconds(1);
    
    /// <inheritdoc cref="ICloneable.Clone"/>
    public ClientConfiguration Clone() => (ClientConfiguration) MemberwiseClone();
}

public class ServerConfiguration : BaseConfiguration
{
    public int MaxClients { get; set; } = 10;
    
    public IPAddress Host { get; set; } = IPAddress.Loopback;

    public int Port { get; set; } = 1776;

    public TimeSpan ClientTimeout { get; set; } = TimeSpan.FromSeconds(5);

    public ServerConfiguration Clone() => (ServerConfiguration) MemberwiseClone();
}
