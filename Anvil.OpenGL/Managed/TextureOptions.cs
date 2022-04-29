using JetBrains.Annotations;

namespace Anvil.OpenGL.Managed;

/// <summary>
/// Provides options to be used with texture creation.
/// </summary>
[PublicAPI]
public sealed class TextureOptions
{
    /// <summary>
    /// Specifies the format of the pixel data. 
    /// </summary>
    /// <remarks>Default: <see cref="Anvil.OpenGL.PixelFormat.Rgba"/></remarks>
    public PixelFormat Format { get; set; }
    
    /// <summary>
    /// Specifies the data type of the pixel data.
    /// </summary>
    /// <remarks>Default: <see cref="Anvil.OpenGL.PixelType.UnsignedByte"/></remarks>
    public PixelType Type { get; set; }
    
    /// <summary>
    /// Specifies the number of color components in the texture.
    /// </summary>
    /// <remarks>Default: <see cref="Anvil.OpenGL.InternalFormat.Rgba"/></remarks>
    public InternalFormat InternalFormat { get; set; }
    
    /// <summary>
    /// Sets the wrap parameter for texture coordinate <i>s</i>.
    /// </summary>
    /// <remarks>Default: <see cref="Anvil.OpenGL.TextureWrapMode.ClampToEdge"/></remarks>
    public TextureWrapMode? WrapS { get; set; }
    
    /// <summary>
    /// Sets the wrap parameter for texture coordinate <i>t</i>.
    /// </summary>
    /// <remarks>Default: <see cref="Anvil.OpenGL.TextureWrapMode.ClampToEdge"/></remarks>
    public TextureWrapMode? WrapT { get; set; }
    
    /// <summary>
    /// Sets the wrap parameter for texture coordinate <i>r</i>.
    /// </summary>
    /// <remarks>Default: <c>null</c></remarks>
    public TextureWrapMode? WrapR { get; set; }

    /// <summary>
    /// Specifies the e level-of-detail function used when sampling from the texture determines that the texture should
    /// be magnified.
    /// </summary>
    /// <remarks>Default: <see cref="Anvil.OpenGL.TextureMagFilter.Linear"/></remarks>
    public TextureMagFilter? MagFilter { get; set; }
    
    /// <summary>
    /// Specifies the e level-of-detail function used when sampling from the texture determines that the texture should
    /// be minified.
    /// </summary>
    /// <remarks>Default: <see cref="Anvil.OpenGL.TextureMinFilter.LinearMipmapNearest"/></remarks>
    public TextureMinFilter? MinFilter { get; set; }
    
    /// <summary>
    /// Specifies if mipmaps will be generated.
    /// </summary>
    /// <remarks>Default: <c>true</c></remarks>
    public bool GenerateMipmaps { get; set; }
    
    /// <summary>
    /// Specifies the level-of-detail number. Level <c>0</c> is the base image level.
    /// <para>Level <i>n</i> is the <i>n</i>th mipmap reduction image.</para>
    /// </summary>
    /// <remarks>Default: <c>0</c></remarks>
    public int Level { get; set; }

    /// <summary>
    /// Gets a new instance that represents the default configuration.
    /// </summary>    
    public static TextureOptions Default => new()
    {
        Format = PixelFormat.Rgba,
        Type = PixelType.UnsignedByte,
        InternalFormat = InternalFormat.Rgba,
        WrapS = TextureWrapMode.ClampToEdge,
        WrapT = TextureWrapMode.ClampToEdge,
        WrapR = null,
        MagFilter = TextureMagFilter.Linear,
        MinFilter = TextureMinFilter.LinearMipmapNearest,
        GenerateMipmaps = true,
        Level = 0
    };
}