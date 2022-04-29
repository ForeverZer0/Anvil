using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Sets the desired OpenGL profile for the window context.
/// </summary>
/// <seealso cref="WindowHint.OpenGLProfile"/>
/// <seealso cref="GLFW.WindowHint{TEnum}(WindowHint,TEnum)"/>
[PublicAPI]
public enum OpenGLProfile
{
    /// <summary>
    /// Any available, or early OpenGL versions before the concept of a "profile" existed.
    /// </summary>
    Any = 0,
	
    /// <summary>
    /// Core profile with all deprecated fixed-pipeline functions omitted. (i.e. "modern" OpenGL)
    /// </summary>
    Core = 0x00032001,
	
    /// <summary>
    /// Compatibility profile that includes deprecated fixed-pipeline functions.
    /// </summary>
    Compatibility = 0x00032002
}