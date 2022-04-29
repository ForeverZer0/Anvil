using System.Diagnostics;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Used in case of maps with isometric orientation, and determines how tile overlays for terrain and collision
/// information are rendered.
/// </summary>
[PublicAPI]
public sealed class Grid : TiledEntity
{
    private Size size;
    
    /// <summary>
    /// Gets or sets the orientation of the grid for the tiles in this tileset.
    /// <para/>
    /// Valid values include either <see cref="TMX.Orientation.Orthogonal"/> or <see cref="TMX.Orientation.Isometric"/>.
    /// </summary>
    public Orientation Orientation { get; set; }

    /// <summary>
    /// Gets or sets the size of a grid cell, in pixels.
    /// </summary>
    public Size Size
    {
        get => size;
        set => size = value;
    }

    /// <summary>
    /// Gets or sets the width of a grid cell, in pixels.
    /// </summary>
    public int Width
    {
        get => size.Width;
        set => size.Width = value;
    }
    
    /// <summary>
    /// Gets or sets the height of a grid cell, in pixels.
    /// </summary>
    public int Height
    {
        get => size.Height;
        set => size.Height = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public Grid() : base(Tag.Grid)
    {
    }

    internal Grid(XmlReader reader) : base(reader, Tag.Grid)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Orientation:
                    Orientation = ParseEnum<Orientation>(reader.Value);
                    break;
                case Tag.Width:
                    Width = reader.ReadContentAsInt();
                    break;
                case Tag.Height:
                    Height = reader.ReadContentAsInt();
                    break;
                default:
                    Debug.WriteLine($"Unhandled attribute \"{reader.Name}\" in <grid> element.");
                    break;
            }
        }
    }
    
}