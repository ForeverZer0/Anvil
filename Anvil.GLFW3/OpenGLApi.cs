using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the desired OpenGL API to use create for the window.
/// </summary>
/// <remarks>Used as window hint value when specifying the <see cref="WindowHint.ClientApi"/> parameter.</remarks>
/// <seealso cref="WindowHint.ClientApi"/>
/// <seealso cref="GLFW.WindowHint{TEnum}(WindowHint,TEnum)"/>
[PublicAPI]
public enum OpenGLApi
{
    /// <summary>
    /// No OpenGL API. This is a required hint when creating a Vulkan instance.
    /// </summary>
    None = 0,
	
    /// <summary>
    /// A standard OpenGL context.
    /// </summary>
    OpenGL = 0x00030001,
	
    /// <summary>
    /// OpenGL for embedded systems.
    /// </summary>
    OpenGLES = 0x00030002
}