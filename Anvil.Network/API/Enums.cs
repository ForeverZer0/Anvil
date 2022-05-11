namespace Anvil.Network.API;


public enum Endianness
{
    Little,
    Big
}


public enum DisconnectReason : byte
{
    Unspecified,
    Refused,
    InvalidResponse,
    UnsupportedProtocol,
    Timeout,
    ClientDisconnect,
    ServerDisconnect,
    ServerRestart,
    ServerClose,
    ServerFull
}

[Flags]
public enum Direction : byte
{
    NotSpecified,
    ServerBound,
    ClientBound,
    Both = ClientBound | ServerBound
}