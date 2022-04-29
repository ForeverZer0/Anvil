#if JSON_READING
using System.Text.Json;
#endif
using System.Runtime.CompilerServices;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Defines a Wang tile, by referring to a tile in the tileset and associating it with a certain Wang ID.
/// </summary>
[PublicAPI]
public class WangTile : TiledEntity
{
    /// <summary>
    /// Gets or sets the tile ID.
    /// </summary>
    public int TileId { get; set; }
    
    /// <summary>
    /// Gets or sets the indices used to refer to Wang colors within the set.
    /// </summary>
    public int[] WangIds { get; set; }

    /// <summary>
    /// Convenience method to retrieve the Wang ID using a strongly-typed indexer.
    /// </summary>
    /// <param name="index">The index to retrieve.</param>
    /// <returns>The Wang ID.</returns>
    public int Index(WangIndex index) => WangIds[Unsafe.As<WangIndex, int>(ref index)];
    
    /// <summary>
    /// Creates a new default instance of the <see cref="WangTile"/> class.
    /// </summary>
    public WangTile() : base(Tag.WangTile)
    {
        TileId = -1;
        WangIds = Array.Empty<int>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int[] ParseIds(string str)
    {
        const StringSplitOptions opts = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        var ids = str.Split(',', opts);
        return ids.Select(int.Parse).ToArray();
    }

#if JSON_READING
    internal WangTile(Utf8JsonReader reader) : base(Tag.WangTile)
    {
        while (ReadChild(reader, out var propertyName))
        {
            switch (propertyName)
            {
                case Tag.TileId:
                    TileId = reader.GetInt32();
                    break;
                case Tag.WangId:
                    WangIds = ParseIds(reader.GetString() ?? string.Empty);
                    break;
                default:
                    UnhandledProperty(propertyName);
                    break;
            }
        }
        
        WangIds ??= new int[Enum.GetValues<WangIndex>().Length];
    }
#endif

    internal WangTile(XmlReader reader) : base(reader, Tag.WangTile)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.TileId:
                    TileId = reader.ReadContentAsInt();
                    break;
                case Tag.WangId:
                    WangIds = ParseIds(reader.Value);
                    break;
                // Deprecated and unused, but do not emit debug messages for them
                case Tag.HFlip:
                case Tag.VFlip:
                case Tag.DFlip:
                    continue;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }

        WangIds ??= new int[Enum.GetValues<WangIndex>().Length];
    }
}