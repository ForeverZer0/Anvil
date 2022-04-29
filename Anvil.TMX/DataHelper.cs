using System.Buffers.Text;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Xml;

namespace Anvil.TMX;

/// <summary>
/// Helper class for reading data elements that may be found within a TMX file.
/// </summary>
internal static class DataReader
{
    /// <summary>
    /// Converts the inner text contents of a data element into tile data.
    /// </summary>
    /// <param name="contents">The string contents of the data element.</param>
    /// <param name="compression">The compression algorithm used on the data.</param>
    /// <param name="encoding">The encoding used on the data.</param>
    /// <returns>An array of tile IDs.</returns>
    /// <exception cref="ArgumentOutOfRangeException">An invalid compression/encoding configuration was specified.</exception>
    public static List<Gid> ReadTileData(string contents, DataCompression compression, DataEncoding encoding)
    {
        switch (encoding)
        {
            case DataEncoding.Base64:
                var buffer = System.Text.Encoding.UTF8.GetBytes(contents);
                Base64.DecodeFromUtf8InPlace(buffer, out var size);
                return ReadTileData(buffer[..size], compression);
            case DataEncoding.Csv:
                const StringSplitOptions opts = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                var items = contents.Split(',', opts);
                return items.Select(item => new Gid(uint.Parse(item))).ToList();
            default:
                throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null);
        }
    }
    
    /// <summary>
    /// Reads a data tag whose payload is raw binary data (i.e. image data).
    /// </summary>
    /// <param name="reader">The reader containing the data, positioned at the start of the data element.</param>
    /// <returns>A byte array of the decoded and decompressed payload.</returns>
    public static byte[] ReadRaw(XmlReader reader)
    {
        ReadAttributes(reader, out var compression, out var encoding);
        
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Tag.Data)
                break;
            if (reader.NodeType == XmlNodeType.Text)
                return ReadRawData(reader.Value, compression, encoding);
        }

        return Array.Empty<byte>();
    }
    
    /// <summary>
    /// Reads a data tah whose payload consists of tile/chunk data.
    /// </summary>
    /// <param name="reader">The reader containing the data, positioned at the start of the data element.</param>
    /// <returns>A tuple containing the respective tile IDs and chunks.</returns>
    public static (List<Gid>, List<Chunk>) ReadTileData(XmlReader reader)
    {
        ReadAttributes(reader, out var compression, out var encoding);
        
        var tiles = new List<Gid>();
        var chunks = new List<Chunk>();

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Tag.Data)
                break;
            
            if (reader.NodeType == XmlNodeType.Text)
            {
                var payload = reader.Value.Trim();
                tiles = ReadTileData(payload, compression, encoding);
                break;
            }
            
            if (reader.NodeType != XmlNodeType.Element)
                continue;

            if (reader.Name == Tag.Chunk)
            {
                do
                {
                    chunks.Add(new Chunk(reader, compression, encoding));
                } while (ReadChild(reader));
                break;
            }

            if (reader.Name == Tag.Tile)
            {
                do
                {
                    tiles.Add(new Gid(uint.Parse(reader.Value)));
                } while (ReadChild(reader));
                break;
            }
        }
        
        return (tiles, chunks);
    }


    private static void ReadAttributes(XmlReader reader, out DataCompression compression, out DataEncoding encoding)
    {
        compression = DataCompression.None;
        encoding = DataEncoding.None;

        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Compression:
                    compression = Enum.Parse<DataCompression>(reader.Value, true);
                    break;
                case Tag.Encoding:
                    encoding = Enum.Parse<DataEncoding>(reader.Value, true);
                    break;
            }
        }
    }

    private static bool ReadChild(XmlReader reader)
    {
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Tag.Data)
                return false;

            if (reader.NodeType == XmlNodeType.Element)
                return true;
        }

        return false;
    }
    
    private static byte[] ReadRawData(string payload, DataCompression compression, DataEncoding encoding)
    {
        int size;
        var buffer = System.Text.Encoding.UTF8.GetBytes(payload);
        switch (encoding)
        {
            case DataEncoding.None:
                size = buffer.Length;
                break;
            case DataEncoding.Base64:
                Base64.DecodeFromUtf8InPlace(buffer, out size);
                break;
            case DataEncoding.Csv:
                if (compression != DataCompression.None)
                    throw new ArgumentException("Cannot decompress CSV encoded data.");
                size = buffer.Length;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null);
        }
        
        return Decompress(buffer[..size], compression);
    }

    private static byte[] Decompress(byte[] buffer, DataCompression compression)
    {
        if (compression == DataCompression.None)
            return buffer;
        
        using var outputStream = new MemoryStream(buffer.Length);
        Stream bufferStream;
        Stream compressStream;
        
        switch (compression)
        {
            case DataCompression.Gzip:
            {
                bufferStream = new MemoryStream(buffer);
                compressStream = new GZipStream(bufferStream, CompressionMode.Decompress, true);
                break;
            }
            case DataCompression.Zlib:
            {
                // Remove the Zlib header by omitting first 2 bytes, as the .NET DEFLATE implementation will explode
                // if they are present.
                bufferStream = new MemoryStream(buffer[2..], false);
                compressStream = new DeflateStream(bufferStream, CompressionMode.Decompress, true);
                break;
            }
            default:
                throw new NotSupportedException("Unsupported compression specified.");
        }
        
        compressStream.CopyTo(outputStream);
        bufferStream.Dispose();
        compressStream.Dispose();

        return outputStream.ToArray();
    }
    
    private static List<Gid> ReadTileData(byte[] buffer, DataCompression compression)
    {
        var span = MemoryMarshal.Cast<byte, Gid>(Decompress(buffer, compression));
        var list = new List<Gid>(span.Length);
        foreach (var gid in span)
            list.Add(gid);
        return list;
    }
}