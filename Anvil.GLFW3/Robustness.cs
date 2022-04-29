using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the robustness strategy to be used by the context.
/// </summary>
/// <remarks>Used as window hint value when specifying the <see cref="WindowHint.ContextRobustness"/> parameter.</remarks>
/// <seealso cref="WindowHint.ContextRobustness"/>
/// <seealso cref="GLFW.WindowHint{TEnum}(WindowHint,TEnum)"/>
[PublicAPI]
public enum Robustness
{
    /// <summary>
    /// None specified.
    /// </summary>
    None = 0,
	
    /// <summary>
    /// No reset notification.
    /// </summary>
    NoResetNotification = 0x00031001,
	
    /// <summary>
    /// Lose context on reset.
    /// </summary>
    LoseContextOnReset = 0x00031002
}