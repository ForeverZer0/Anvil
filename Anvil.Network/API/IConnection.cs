using System.Net;
using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represents an abstract relationship between two network-connected devices.
/// </summary>
[PublicAPI]
public interface IConnection : IEquatable<IConnection>
{
    /// <summary>
    /// Gets the remote end-point of the <see cref="IConnection"/> used for communication.
    /// </summary>
    IPEndPoint EndPoint { get; }
    
    /// <summary>
    /// Gets or sets the time (UTC) of the last received data from this <see cref="IConnection"/>.
    /// </summary>
    DateTime LastReceive { get; set; }

    /// <summary>
    /// Gets or sets the client salt for this <see cref="IConnection"/>.
    /// </summary>
    int ClientSalt { get; set; }
    
    /// <summary>
    /// Gets or sets the server salt for this <see cref="IConnection"/>.
    /// </summary>
    int ServerSalt { get; set; }

    /// <summary>
    /// Gets a unique value for this connection based on the <see cref="ClientSalt"/> and <see cref="ServerSalt"/>.
    /// </summary>
    public int Salt => ServerSalt ^ ClientSalt;

    /// <inheritdoc cref="Object.GetHashCode"/>
    /// <remarks>Default implementation.</remarks>
    public int GetHashCode() => HashCode.Combine(ServerSalt ^ ClientSalt, EndPoint);
    
    /// <inheritdoc />
    bool IEquatable<IConnection>.Equals(IConnection? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EndPoint.Equals(other.EndPoint) && Salt.Equals(other.Salt);
    }
}