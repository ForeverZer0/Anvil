using System.Runtime.InteropServices;
using System.Xml;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Represents an image.
/// <para/>
/// Typically this is little more than a relative file path, which is what the Tiled application outputs, though
/// embedded data is supported according to the TMX specification.
/// </summary>
[PublicAPI]
public class Image : TiledEntity
{
    private Size size;
    
    /// <summary>
    /// Gets a value indicating the format of the data.
    /// </summary>
    /// <remarks>
    /// Only used for embedded images.
    /// <para>Embedded images are supported by the TMX format specification, but not possible via the Tiled application.</para>
    /// </remarks>
    public string? Format { get; private set; }
    
    /// <summary>
    /// Gets or sets a reference to the image file.
    /// </summary>
    public string? Source { get; set; }
    
    /// <summary>
    /// Gets or sets a color that should be interpreted as transparency when rendered, or <c>null</c> to ignore.
    /// </summary>
    public ColorF? TransparentColor { get; set; }

    /// <summary>
    /// Gets or sets the image width in pixels.
    /// </summary>
    /// <remarks>Optional, default is <c>0</c>.</remarks>
    public int Width
    {
        get => size.Width;
        set => size.Width = value;
    }
    
    /// <summary>
    /// Gets or sets the image height in pixels.
    /// </summary>
    /// <remarks>Optional, default is <c>0</c>.</remarks>
    public int Height
    {
        get => size.Height;
        set => size.Height = value;
    }
    
    /// <summary>
    /// Gets or sets the image size in pixels.
    /// </summary>
    /// <remarks>Optional, default is <c>{0,0}</c>.</remarks>
    public Size Size
    {
        get => size;
        set => size = value;
    }
    
    /// <summary>
    /// Gets the data payload of the image.
    /// </summary>
    /// <remarks>
    /// Only used for embedded images.
    /// <para>Embedded images are supported by the TMX format specification, but not possible via the Tiled application.</para>
    /// </remarks>
    public byte[]? Data { get; private set; }

    /// <summary>
    /// Sets the data for an embedded image.
    /// </summary>
    /// <param name="format">
    /// A string describing for the format of the <paramref name="payload"/>. Valid values include file extensions, such
    /// as <c>png</c>, <c>jpg</c>, <c>bmp</c>, etc.</param>
    /// <param name="payload">A byte array containing the image data. The data is copied internally before this method returns.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="format"/> or <paramref name="payload"/> is <c>null</c>.</exception>
    public void SetData(string format, byte[] payload)
    {
        Format = format ?? throw new ArgumentNullException(nameof(format));
        Data = payload;
    }
    
    /// <summary>
    /// Sets the data for an embedded image.
    /// </summary>
    /// <param name="format">
    /// A string describing for the format of the <paramref name="payload"/>. Valid values include file extensions, such
    /// as <c>png</c>, <c>jpg</c>, <c>bmp</c>, etc.</param>
    /// <param name="payload">A span containing the image data. The data is copied internally before this method returns.</param>
    /// <typeparam name="T">An unmanaged type.</typeparam>
    /// <exception cref="ArgumentNullException">When <paramref name="format"/> or <paramref name="payload"/> is <c>null</c>.</exception>
    public void SetData<T>(string format, ReadOnlySpan<T> payload) where T : unmanaged
    {
        Format = format ?? throw new ArgumentNullException(nameof(format));
        var buffer = MemoryMarshal.Cast<T, byte>(payload);
        Data = new byte[buffer.Length];
        buffer.CopyTo(Data);
    }

    /// <summary>
    /// Clears the <see cref="Data"/> and <see cref="Format"/> properties.
    /// </summary>
    public void Clear()
    {
        Format = string.Empty;
        Data = null;
    }

    /// <summary>
    /// Creates a new default instance of the <see cref="Image"/> class.
    /// </summary>
    public Image() : base(Tag.Image)
    {
    }

    internal Image(XmlReader reader) : base(reader, Tag.Image)
    {
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case Tag.Format:
                    Format = reader.Value;
                    break;
                case Tag.Id: 
                    continue;
                case Tag.Source:
                    Source = reader.Value;
                    break;
                case Tag.TransparencyColor:
                    TransparentColor = ColorF.Parse(reader.Value);
                    break;
                case Tag.Width:
                    Width = reader.ReadContentAsInt();
                    break;
                case Tag.Height:
                    Height = reader.ReadContentAsInt();
                    break;
                default:
                    UnhandledAttribute(reader.Name);
                    break;
            }
        }

        while (ReadChild(reader))
        {
            
            if (!string.Equals(reader.Name, Tag.Data, StringComparison.Ordinal))
            {
                UnhandledChild(reader.Name);
                continue;
            }

            while (ReadChild(reader))
            {
                if (string.Equals(reader.Name, Tag.Data, StringComparison.Ordinal))
                {
                    Data = DataReader.ReadRaw(reader);
                    if (Format is null)
                        Console.Error.WriteLine("Image contains data without specifying format.");
                }
                
                UnhandledChild(reader.Name);
            }
        }
    }
}