using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the API used to create the context.
/// </summary>
/// <seealso cref="WindowHint.ContextCreationApi"/>
/// <seealso cref="GLFW.WindowHint{TEnum}(WindowHint,TEnum)"/>
[PublicAPI]
public enum ContextApi
{
    /// <summary>
    /// The native API for the platform.
    /// </summary>
    Native = 0x00036001,
	
    /// <summary>
    /// EGL
    /// </summary>
    Egl = 0x00036002,
	
    /// <summary>
    /// Mesa
    /// </summary>
    OsMesa = 0x00036003
}