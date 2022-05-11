namespace Anvil.Network.API;

public interface IPacket
{
    int MaximumSize { get; }

    void Serialize(IBinaryWriter writer);

    void Deserialize(IBinaryReader reader);
}