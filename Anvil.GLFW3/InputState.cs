using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the state of an input device.
/// </summary>
[PublicAPI]
public enum InputState
{
    /// <summary>
    /// Not pressed or no input.
    /// </summary>
    Release = 0,
    
    /// <summary>
    /// Currently being pressed.
    /// </summary>
    Press = 1,
    
    /// <summary>
    /// Occurs while being pressed, emits at intervals based on system repeat rate for keys.
    /// </summary>
    Repeat = 2
}