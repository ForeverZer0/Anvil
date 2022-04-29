using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Strongly-typed constants describing the buttons on X-Box/PlayStation style gamepad.
/// </summary>
[PublicAPI]
public enum GamepadButton
{
    /// <summary>The <c>A</c> button on an Xbox-style gamepad.</summary>
    A = 0,
    
    /// <summary>The <c>B</c> button on an Xbox-style gamepad.</summary>
    B = 1,
    
    /// <summary>The <c>X</c> button on an Xbox-style gamepad.</summary>
    X = 2,
    
    /// <summary>The <c>A</c> button on an Xbox-style gamepad.</summary>
    Y = 3,
    
    /// <summary>
    /// The left-bumper (i.e. bottom trigger or L2).
    /// </summary>
    LeftBumper = 4,
    
    /// <summary>
    /// The right-bumper (i.e. bottom trigger or R2).
    /// </summary>
    RightBumper = 5,
    
    /// <summary>
    /// The "back" button on an Xbox-style gamepad.
    /// </summary>
    Back = 6,
    
    /// <summary>
    /// The "start" button on an Xbox-style gamepad.
    /// </summary>
    Start = 7,
    
    /// <summary>
    /// The "guid" button on an Xbox-style gamepad.
    /// </summary>
    Guide = 8,
    
    /// <summary>
    /// The left thumb button (i.e. depressing left analog stick or L3).
    /// </summary>
    LeftThumb = 9,
    
    /// <summary>
    /// The right thumb button (i.e. depressing right analog stick or R3).
    /// </summary>
    RightThumb = 10,
    
    /// <summary>
    /// Up directional button on the D-pad.
    /// </summary>
    DPadUp = 11,
    
    /// <summary>
    /// Right directional button on the D-pad.
    /// </summary>
    DPadRight = 12,
    
    /// <summary>
    /// Down directional button on the D-pad.
    /// </summary>
    DPadDown = 13,
    
    /// <summary>
    /// Left directional button on the D-pad.
    /// </summary>
    DPadLeft = 14,
    
    /// <summary>
    /// The last valid valid in the enumeration.
    /// <para/>
    /// Alias for <see cref="A"/>.
    /// </summary>
    Last = DPadLeft,
    
    /// <summary>Alias for <see cref="A"/>.</summary>
    Cross = A,
    
    /// <summary>Alias for <see cref="B"/>.</summary>
    Circle = B,
    
    /// <summary>Alias for <see cref="X"/>.</summary>
    Square = X,
    
    /// <summary>Alias for <see cref="Y"/>.</summary>
    Triangle = Y
}