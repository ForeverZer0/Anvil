using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Describes the style, alignment, and general appearance of text to be displayed on a <see cref="Map"/>.
/// <para/>
/// Only used for <see cref="MapObject"/> instances that have a type of <see cref="ObjectType.Text"/>.
/// </summary>
[PublicAPI]
public class Text : TiledEntity
{
    /// <summary>
    /// Gets or sets the font-family to use for rendering.
    /// </summary>
    /// <remarks>Default: <c>"sans-serif"</c></remarks>
    public string FontFamily { get; set; } = "sans-serif";

    /// <summary>
    /// Gets or sets the pixel size of the rendered font.
    /// </summary>
    /// <remarks>Default: <c>16</c></remarks>
    public int PixelSize { get; set; } = 16;

    /// <summary>
    /// Gets or sets a flag indicating if automatic word wrapping is enabled.
    /// </summary>
    /// <remarks>Default: <c>false</c></remarks>
    public bool Wrap { get; set; } = false;

    /// <summary>
    /// Gets or sets the color used to render the font.
    /// </summary>
    /// <remarks>Default: <see cref="Colors.Black"/></remarks>
    public ColorF Color { get; set; } = Colors.Black;

    /// <summary>
    /// Gets or sets a bitfield indicating the style used to render the font.
    /// </summary>
    /// <remarks>Default: <see cref="TextStyle.None"/></remarks>
    public TextStyle Style { get; set; } = TextStyle.None;

    /// <summary>
    /// Gets or sets a bitfield indicating the alignment of the text within its bounding box.
    /// </summary>
    /// <remarks>Default: top-left</remarks>
    public TextAlign Alignment { get; set; } = TextAlign.Unspecified;

    /// <summary>
    /// Gets or sets a value indicating if kerning should be applied to the rendered text.
    /// <para/>
    /// Kerning is the process of altering the spacing of glyphs depending on their neighbors to create more aesthetic
    /// and readable displayed text.
    /// </summary>
    /// <remarks>Default: <c>true</c></remarks>
    public bool Kerning { get; set; } = true;

    /// <summary>
    /// Gets or sets the text value to display.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Creates a new default instance of the <see cref="Text"/> class.
    /// </summary>
    public Text() : base(Tag.Text)
    {
        Value = string.Empty;
    }

    internal Text(XmlReader reader) : base(reader, Tag.Text)
    {
        PixelSize = 16;
        Wrap = false;
        Color = Colors.Black;
        Kerning = true;

        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.FontFamily:
                    FontFamily = reader.ReadContentAsString();
                    break;
                case Tag.PixelSize:
                    PixelSize = reader.ReadContentAsInt();
                    break;
                case Tag.Wrap:
                    Wrap = reader.ReadContentAsBoolean();
                    break;
                case Tag.Color:
                    Color = ColorF.Parse(reader.Value);
                    break;
                case Tag.Bold:
                    if (reader.ReadContentAsBoolean())
                        Style |= TextStyle.Bold;
                    break;
                case Tag.Italic:
                    if (reader.ReadContentAsBoolean())
                        Style |= TextStyle.Italic;
                    break;
                case Tag.Underline:
                    if (reader.ReadContentAsBoolean())
                        Style |= TextStyle.Underline;
                    break;
                case Tag.Strikeout:
                    if (reader.ReadContentAsBoolean())
                        Style |= TextStyle.Strikeout;
                    break;
                case Tag.Kerning:
                    Kerning = reader.ReadContentAsBoolean();
                    break;
                case Tag.HorizontalAlign:
                    Alignment |= reader.Value switch
                    {
                        Tag.Left => TextAlign.Left,
                        Tag.Right => TextAlign.Right,
                        Tag.Center => TextAlign.CenterH,
                        Tag.Justify => TextAlign.Justify,
                        _ => TextAlign.Left
                    };
                    break;
                case Tag.VerticalAlign:
                    Alignment |= reader.Value switch
                    {
                        Tag.Top => TextAlign.Top,
                        Tag.Bottom => TextAlign.Bottom,
                        Tag.Center => TextAlign.CenterV,
                        _ => TextAlign.Top
                    };
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }

        reader.MoveToContent();
        Value = reader.ReadElementContentAsString();
        
        if (Alignment == TextAlign.Unspecified)
            Alignment = TextAlign.Top | TextAlign.Left;
    }
}