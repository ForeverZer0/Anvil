using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Anvil.Network;
using Anvil.Network.API;
using Anvil.OpenAL;
using Anvil.OpenAL.Managed;
using Anvil.SndFile;
using Buffer = Anvil.OpenAL.Buffer;
using SampleType = Anvil.SndFile.SampleType;

namespace Anvil;



internal static class Program
{

    private static int FillBuffer(byte[] data, AudioStream stream, AudioBuffer buffer)
    {
        var bytesRead = stream.Read(data, 0, data.Length);
        if (bytesRead > 0) 
            buffer.Data(data, 0, bytesRead, AudioFormat.Stereo16, stream.Frequency);
        return bytesRead;
    }
    
    static async Task Main(string[] args)
    {


        IPacket<AudioFormat> a;

        var device = ALC.OpenDevice();
        var context = ALC.CreateContext(device);

        var source = new StreamingSource();
        var buffers = AudioBuffer.Create(2);
        var effect = AudioEffect.Factory<Chorus>();
        var slot = new AudioEffectSlot(effect);
        source.SendFilter(0, slot, null);
        var sound = AudioStream.OpenRead("/home/eric/Music/Unorganized/Alan Walker - Faded.flac", SampleType.Short);
        var data = new byte[sound.Channels * sound.Frequency * sizeof(short)]; // 1 second of audio
        

        foreach (var buffer in buffers)
            FillBuffer(data, sound, buffer);
        source.Queue(buffers);
        source.Play();

        while (source.IsPlaying)
        {
            Thread.Sleep(100);
            foreach (var buffer in source.GetProcessed())
            {
                if (FillBuffer(data, sound, buffer) > 0)
                    source.Queue(buffer);
            }
        }
        
        source.Dispose();
        AudioBuffer.Destroy(buffers);
        slot.Dispose();
        effect.Dispose();
        ALC.DestroyContext(context);
        ALC.CloseDevice(device);
        sound.Dispose();
    }

}