using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the state of a gamepad.
/// </summary>
[StructLayout(LayoutKind.Sequential), PublicAPI]
public unsafe struct GamepadState
{
    private fixed bool buttons[15];
    private fixed float axes[6];

    /// <summary>
    /// Gets the state of the specified <paramref name="button"/>, where <c>true</c> is pressed, and <c>false</c> is
    /// not pressed.
    /// </summary>
    /// <param name="button">The gamepad button to query.</param>
    public bool this[GamepadButton button] => buttons[Unsafe.As<GamepadButton, int>(ref button)];

    /// <summary>
    /// Gets the state of the specified <paramref name="axis"/>, a value between <c>-1.0</c> and <c>1.0</c>, where
    /// <c>0.0</c> is no input.
    /// </summary>
    /// <param name="axis">The gamepad axis to query.</param>
    public float this[GamepadAxis axis] => axes[Unsafe.As<GamepadAxis, int>(ref axis)];
}