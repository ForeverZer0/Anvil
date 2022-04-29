using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Read-only structure describing the supported video mode(s) of a monitor.
/// </summary>
/// <param name="Width">The width, in screen coordinates.</param>
/// <param name="Height">The height, in screen coordinates.</param>
/// <param name="RedBits">The bit depth of the red channel.</param>
/// <param name="GreenBits">The bit depth of the green channel.</param>
/// <param name="BlueBits">The bit depth of the blue channel.</param>
/// <param name="RefreshRate">The refresh rate, in Hz.</param>
[PublicAPI]
public record struct VideoMode(int Width, int Height, int RedBits, int GreenBits, int BlueBits, int RefreshRate)
{
    /// <summary>
    /// Gets a rectangle that describes the full-screen bounds of the video mode in screen coordinates.
    /// </summary>
    public Rectangle Bounds => new(0, 0, Width, Height);

    /// <summary>
    /// Gets the size of the video mode in screen coordinates.
    /// </summary>
    public Size Size => new(Width, Height);
}