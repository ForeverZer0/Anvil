using System.Runtime.InteropServices;

namespace Anvil;

/// <summary>
/// Concrete implementation of a <see cref="IBitmap"/> 
/// </summary>
/// <param name="Width">Gets the width of the image in pixels.</param>
/// <param name="Height">Gets the height of the image in pixels.</param>
/// <param name="Pixels">A pointer to the image data.</param>
[StructLayout(LayoutKind.Sequential)]
public record struct Bitmap(int Width, int Height, IntPtr Pixels) : IBitmap;