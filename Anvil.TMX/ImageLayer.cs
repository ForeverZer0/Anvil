using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// A layer consisting of a single image.
/// </summary>
[PublicAPI]
public class ImageLayer : Layer
{
    /// <summary>
    /// Gets or sets the image associated with this layer.
    /// </summary>
    public Image? Image { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating if the image will be repeated along the x-axis.
    /// </summary>
    public bool RepeatX { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating if the image will be repeated along the y-axis.
    /// </summary>
    public bool RepeatY { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="ImageLayer"/> class.
    /// </summary>
    /// <param name="map">The parent <see cref="Map"/> instance this layer is being created within.</param>
    public ImageLayer(Map map) : base(map, Tag.ImageLayer)
    {
    }

    internal ImageLayer(Map map, XmlReader reader) : base(map, reader, Tag.ImageLayer)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.RepeatX:
                    RepeatX = reader.ReadContentAsBoolean();
                    break;
                case Tag.RepeatY:
                    RepeatY = reader.ReadContentAsBoolean();
                    break;
                default:
                    ProcessAttribute(reader);
                    break;
            }
        }
        
        while (ReadChild(reader))
        {
            switch (reader.Name)
            {
                case Tag.Image:
                    Image = new Image(reader);
                    break;
                case Tag.Properties:
                    Properties = new PropertySet(reader);
                    break;
                default:
                    UnhandledChild(reader.Name);
                    break;
            }
            break;
        }
    }
}