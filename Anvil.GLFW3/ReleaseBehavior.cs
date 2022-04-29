using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the release behavior of a context.
/// </summary>
/// <seealso cref="WindowHint.ContextReleaseBehavior"/>
/// <seealso cref="GLFW.WindowHint{TEnum}(WindowHint,TEnum)"/>
[PublicAPI]
public enum ReleaseBehavior
{
    /// <summary>
    /// Default for context.
    /// </summary>
    Any = 0,
	
    /// <summary>
    /// Flush all operations.
    /// </summary>
    Flush = 0x00035001,
	
    /// <summary>
    /// Do nothing.
    /// </summary>
    None = 0x00035002
}