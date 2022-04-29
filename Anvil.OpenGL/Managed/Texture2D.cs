using JetBrains.Annotations;

namespace Anvil.OpenGL.Managed;

/// <summary>
/// Object-oriented abstraction for a 2D OpenGL texture.
/// </summary>
[PublicAPI]
public sealed class Texture2D : IDisposable, IEquatable<Texture2D>
{
    private readonly Texture id;

    /// <summary>
    /// Gets the native texture name for this instance.
    /// </summary>
    public Texture Name => id;
    
    /// <summary>
    /// Gets the width of the texture in pixel units.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the texture in pixel units.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the size of the texture in pixel units.
    /// </summary>
    public Size Size => new (Width, Height);
    
    /// <summary>
    /// Creates a new empty texture with the specified <paramref name="width"/> and <paramref name="height"/> using the
    /// default options.
    /// </summary>
    /// <param name="width">The desired width of the texture in pixel units.</param>
    /// <param name="height">The desired height of the texture in pixel units.</param>
    public Texture2D(int width, int height) : this(new Bitmap(width, height, IntPtr.Zero), TextureOptions.Default)
    {
    }

    /// <summary>
    /// Creates a new empty texture with the specified <paramref name="width"/> and <paramref name="height"/>.
    /// </summary>
    /// <param name="width">The desired width of the texture in pixel units.</param>
    /// <param name="height">The desired height of the texture in pixel units.</param>
    /// <param name="options">Options for configuring texture behavior and pixel format.</param>
    public Texture2D(int width, int height, TextureOptions options) : this(new Bitmap(width, height, IntPtr.Zero), options)
    {

    }

    /// <summary>
    /// Creates a new <see cref="Texture2D"/> from the specified pixel data using the default options.
    /// </summary>
    /// <param name="bitmap">An object that represents image data.</param>
    public Texture2D(IBitmap bitmap) : this(bitmap, TextureOptions.Default)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Texture2D"/> from the specified pixel data using the default options.
    /// </summary>
    /// <param name="bitmap">An object that represents image data.</param>
    /// <param name="options">Options for configuring texture behavior and pixel format.</param>
    public Texture2D(IBitmap bitmap, TextureOptions options)
    {
        Width = bitmap.Width;
        Height = bitmap.Height;

        id = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, id);
        GL.TexImage2D(TextureTarget.Texture2D, options.Level, options.InternalFormat, Width, Height, 0, options.Format, options.Type, bitmap.Pixels);
        
        if (options.WrapS.HasValue)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameter.WrapS, options.WrapS.Value);
        if (options.WrapT.HasValue)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameter.WrapT, options.WrapT.Value);
        if (options.WrapR.HasValue)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameter.WrapR, options.WrapR.Value);
        if (options.MagFilter.HasValue)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameter.MagFilter, options.MagFilter.Value);
        if (options.MinFilter.HasValue)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameter.MinFilter, options.MinFilter.Value);
        if (options.GenerateMipmaps)
            GL.GenerateMipmap(TextureTarget.Texture2D);
        
        GL.BindTexture(TextureTarget.Texture2D, default);
    }

    /// <summary>
    /// Binds the texture to the currently active texture unit.
    /// </summary>
    public void Bind() => GL.BindTexture(TextureTarget.Texture2D, id);

    /// <summary>
    /// Binds the texture to the specified texture <paramref name="unit"/>.
    /// </summary>
    /// <param name="unit">The texture unit to make activate before binding.</param>
    /// /// <remarks>
    /// The number of units available is implementation-defined, but guaranteed to be at least <c>80</c>.
    /// </remarks>
    public void Bind(TextureUnit unit)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, id);
    }

    /// <summary>
    /// Binds the texture to the texture unit at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The zero-based index of the texture unit to bind to.</param>
    /// <remarks>
    /// The number of units available is implementation-defined, but guaranteed to be at least <c>80</c>.
    /// </remarks>
    public void Bind(int index)
    {
        GL.ActiveTexture(TextureUnit.Texture0 + index);
        GL.BindTexture(TextureTarget.Texture2D, id);
    }

    /// <inheritdoc />
    public override int GetHashCode() => id.GetHashCode();

    /// <inheritdoc />
    public void Dispose() => GL.DeleteTexture(id);

    /// <summary>
    /// Copies the texture pixels into a buffer.
    /// </summary>
    /// <param name="buffer">A buffer to receive the data.</param>
    /// <param name="format">Specifies a pixel format for the returned data.</param>
    /// <param name="type">Specifies a pixel type for the returned data.</param>
    /// <param name="level">
    /// Specifies the level-of-detail number of the desired image. Level <c>0</c> is the base image
    /// level. Level <i>n</i> is the <i>n</i>th mipmap reduction image.
    /// </param>
    /// <typeparam name="TPixel">A primitive type compatible with the specified format and type.</typeparam>
    public void GetPixels<TPixel>(TPixel[] buffer, PixelFormat format, PixelType type, int level = 0) where TPixel : unmanaged
    {
        var span = new Span<TPixel>(buffer);
        GL.GetTexImage(TextureTarget.Texture2D, level, format, type, span);
    }
    
    /// <summary>
    /// Copies the texture pixels into a buffer.
    /// </summary>
    /// <param name="buffer">A buffer to receive the data.</param>
    /// <param name="format">Specifies a pixel format for the returned data.</param>
    /// <param name="type">Specifies a pixel type for the returned data.</param>
    /// <param name="level">
    /// Specifies the level-of-detail number of the desired image. Level <c>0</c> is the base image
    /// level. Level <i>n</i> is the <i>n</i>th mipmap reduction image.
    /// </param>
    /// <typeparam name="TPixel">A primitive type compatible with the specified format and type.</typeparam>
    public void GetPixels<TPixel>(Span<TPixel> buffer, PixelFormat format, PixelType type, int level = 0) where TPixel : unmanaged
    {
        GL.GetTexImage(TextureTarget.Texture2D, level, format, type, buffer);
    }

    /// <summary>
    /// Copies the texture pixels into a buffer.
    /// </summary>
    /// <param name="buffer">A buffer to receive the data.</param>
    /// <param name="level">
    /// Specifies the level-of-detail number of the desired image. Level <c>0</c> is the base image
    /// level. Level <i>n</i> is the <i>n</i>th mipmap reduction image.
    /// </param>
    public void GetPixels(ColorF[] buffer, int level = 0)
    {
        var span = new Span<ColorF>(buffer);
        GL.GetTexImage(TextureTarget.Texture2D, level, PixelFormat.Rgba, PixelType.Float, span);
    }
    
    /// <summary>
    /// Copies the texture pixels into a buffer.
    /// </summary>
    /// <param name="buffer">A buffer to receive the data.</param>
    /// <param name="level">
    /// Specifies the level-of-detail number of the desired image. Level <c>0</c> is the base image
    /// level. Level <i>n</i> is the <i>n</i>th mipmap reduction image.
    /// </param>
    public void GetPixels(Span<ColorF> buffer, int level = 0)
    {
        GL.GetTexImage(TextureTarget.Texture2D, level, PixelFormat.Rgba, PixelType.Float, buffer);   
    }

    /// <inheritdoc />
    public bool Equals(Texture2D? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || id.Equals(other.id);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is Texture2D other && Equals(other);

    /// <summary>
    /// Determines whether two specified textures have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Texture2D"/> to compare.</param>
    /// <param name="right">The second <see cref="Texture2D"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Texture2D? left, Texture2D? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified textures have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Texture2D"/> to compare.</param>
    /// <param name="right">The second <see cref="Texture2D"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Texture2D? left, Texture2D? right) => !Equals(left, right);
}