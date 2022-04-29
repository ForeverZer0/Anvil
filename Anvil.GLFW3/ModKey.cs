using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Flags describing the state of modifier keys.
/// </summary>
[PublicAPI, Flags]
public enum ModKey
{
    /// <summary>
    /// No modifier key input detected.
    /// </summary>
    None = 0x0000,
    
    /// <summary>
    /// Any of the <c>Shift</c> keys are depressed.
    /// </summary>
    Shift = 0x0001,
    
    /// <summary>
    /// Any of the <c>Ctrl</c> keys are depressed.
    /// </summary>
    Control = 0x0002,
    
    /// <summary>
    /// Any of the <c>Alt</c> keys are depressed.
    /// </summary>
    Alt = 0x0004,
    
    /// <summary>
    /// Any of the <c>Super</c> (i.e. "Windows key") keys are depressed.
    /// </summary>
    Super = 0x0008,
    
    /// <summary>
    /// <c>CapsLock</c> is enabled.
    /// </summary>
    CapsLock = 0x0010,
    
    /// <summary>
    /// <c>NumLock</c> is enabled.
    /// </summary>
    NumLock = 0x0020
}