using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Represents a decoded image.
/// </summary>
[PublicAPI]
public interface IBitmap
{
    /// <summary>
    /// Gets the width of the image in pixel units.
    /// </summary>
    int Width { get; }
    
    /// <summary>
    /// Gets the height of the image in pixel units.
    /// </summary>
    int Height { get; }
    
    /// <summary>
    /// Gets a pointer to the raw pixel data.
    /// </summary>
    IntPtr Pixels { get; }
}

/// <summary>
/// Represents a decoded image.
/// </summary>
/// <typeparam name="TPixel">A blittable value type.</typeparam>
[PublicAPI]
public interface IBitmap<TPixel> : IBitmap where TPixel : unmanaged
{
    /// <summary>
    /// Gets a <see cref="Span{Byte}"/> containing the raw pixel data.
    /// </summary>
    Span<TPixel> PixelSpan { get; }
}

