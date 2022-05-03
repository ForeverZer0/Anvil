using System.Net;
using System.Net.Sockets;

namespace Anvil.Network.API;

/// <summary>
/// Represents a "connection" between a server and a client.
/// </summary>
/// <remarks>
/// While some protocols (e.g. UDP) have no concept of "connection", they may still implement this interface as a
/// "virtual connection" to a client. 
/// </remarks>
public interface IConnection
{
    /// <summary>
    /// Gets or sets the UTC time data was last received from this <see cref="IConnection"/>.
    /// </summary>
    DateTime LastSeen { get; set; }
    
    /// <summary>
    /// Gets or sets the expected state of the client connection.
    /// </summary>
    ClientState State { get; set; }
    
    /// <summary>
    /// Gets the <see cref="Socket"/> for the connection.
    /// </summary>
    Socket Socket { get; }

    /// <summary>
    /// Gets the protocol type used by the client.
    /// </summary>
    public ProtocolType ProtocolType => Socket.ProtocolType;

    /// <summary>
    /// Gets the <see cref="EndPoint"/> for this connection to communicate with. 
    /// </summary>
    /// <exception cref="SocketException">The socket has not been accepted and/or connected yet.</exception>
    public EndPoint EndPoint => Socket.RemoteEndPoint ?? throw new SocketException();

    /// <inheritdoc cref="System.Net.Sockets.Socket.Available"/>
    public int Available => Socket.Available;

    /// <summary>
    /// Gets a value indicating if there is pending data to be consumed from this <see cref="IConnection"/>.
    /// </summary>
    /// <exception cref="SocketException">An error occurred while attempting to access the socket.</exception>
    /// <exception cref="ObjectDisposedException">The socket has been disposed.</exception>
    public bool HasData => Socket.Available > 0;
}