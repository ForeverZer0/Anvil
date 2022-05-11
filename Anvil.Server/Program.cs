// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using Anvil.Network.API;

namespace Anvil.Network;

public enum ClientBound
{
    
}

public enum ServerBound
{
    Test
}

public class TestServer : Server<ClientBound, ServerBound>
{
    /// <inheritdoc />
    protected override void SendPacket(IConnection connection, IPacket packet)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    protected override void OnClientConnecting(IConnection connection)
    {
        AddConnection(connection);
    }
}

public class TestPacket : IPacket
{
    /// <inheritdoc />
    public int MaximumSize => 256;

    /// <inheritdoc />
    public void Encode(IPacketWriter writer)
    {
        writer.WriteString(Value);
    }

    /// <inheritdoc />
    public void Decode(IPacketReader reader)
    {
        Value = reader.ReadString();
    }

    public TestPacket()
    {
        
    }

    public TestPacket(string value)
    {
        Value = value;
    }
    
    public string Value { get; set; }
}

internal static class ServerProgram
{

    private static async Task Main(string[] args)
    {

        var server = new TestServer();
        server.PacketReceived += ServerOnPacketReceived;
        Task.Run(server.StartAsync);


        var client = new TcpClient();
        await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 1776);

        server.PacketManager.Register(ClientState.Disconnected, ServerBound.Test, typeof(TestPacket));
        

        while (client.Connected)
        {
            var key = Console.ReadKey(true);
            Console.WriteLine(key.Key);
            switch (key.Key)
            {
                case ConsoleKey.A:
                {
                    using var memStream = new MemoryStream();
                    var packetWriter = new PacketWriter(memStream);
                    
                    var packet = new TestPacket("hello world");
                    packet.Encode(packetWriter);

                    var ns = client.GetStream();
                    var writer = new PacketWriter(ns);
                    writer.WriteTime(DateTime.UtcNow);
                    writer.WriteVarInt(ServerBound.Test);
                    writer.WriteVarInt(memStream.Length);
                    writer.WriteBytes(memStream.ToArray());
                    
                    await ns.FlushAsync();
                    
                    
                    break;
                }
                default: break;
            }
            
            Thread.Sleep(33);
        }
        
    }

    private static void ServerOnPacketReceived(IServer server, ClientPacketEventArgs<ServerBound> args)
    {
        Console.WriteLine(args);
    }
}