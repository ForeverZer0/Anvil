using System.Net;
using Anvil.Network.API;

namespace Anvil.Network;

internal class Connection : IConnection
{
    /// <inheritdoc />
    public IPEndPoint EndPoint { get; }
    
    /// <inheritdoc />
    public DateTime LastReceive { get; set; }
    
    /// <inheritdoc />
    public int ClientSalt { get; set; }

    /// <inheritdoc />
    public int ServerSalt { get; set; }

    public Connection(IPEndPoint endPoint, int serverSalt, int clientSalt)
    {
        EndPoint = endPoint;
        ServerSalt = serverSalt;
        ClientSalt = clientSalt;
        LastReceive = DateTime.UtcNow;
    }

    /// <inheritdoc />
    public override string ToString() => EndPoint.ToString();
}