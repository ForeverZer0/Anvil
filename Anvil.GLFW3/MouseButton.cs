using JetBrains.Annotations;
#pragma warning disable CS1591

namespace Anvil.GLFW3;

/// <summary>
/// Strongly-typed constants describing the buttons on a mouse.
/// </summary>
[PublicAPI]
public enum MouseButton
{
    /// <summary>Button 1</summary>
    Button1 = 0,
    
    /// <summary>Button 2</summary>
    Button2 = 1,
    
    /// <summary>Button 3</summary>
    Button3 = 2,
    
    /// <summary>Button 4</summary>
    Button4 = 3,
    
    /// <summary>Button 5</summary>
    Button5 = 4,
    
    /// <summary>Button 6</summary>
    Button6 = 5,
    
    /// <summary>Button 7</summary>
    Button7 = 6,
    
    /// <summary>Button 8</summary>
    Button8 = 7,
    
    /// <summary>
    /// The last valid button value.
    /// <para/>
    /// Alias for <see cref="Button8"/>.
    /// </summary>
    Last = Button8,
    
    /// <summary>Alias for <see cref="Button1"/>.</summary>
    Left = Button1,
    
    /// <summary>Alias for <see cref="Button2"/>.</summary>
    Right = Button2,
    
    /// <summary>Alias for <see cref="Button3"/>.</summary>
    Middle = Button3,
}