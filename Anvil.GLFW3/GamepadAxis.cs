using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Strongly-typed constants describing the axes on a gamepad.
/// </summary>
[PublicAPI]
public enum GamepadAxis
{
    /// <summary>
    /// The left value of the x-axis.
    /// </summary>
    LeftX = 0,

    /// <summary>
    /// The left value of the y-axis.
    /// </summary>
    LeftY = 1,

    /// <summary>
    /// The right value of the x-axis.
    /// </summary>
    RightX = 2,

    /// <summary>
    /// The right value of the y-axis.
    /// </summary>
    RightY = 3,

    /// <summary>
    /// The value of the left trigger.
    /// </summary>
    LeftTrigger = 4,

    /// <summary>
    /// The value of the right trigger.
    /// </summary>
    RightTrigger = 5
}