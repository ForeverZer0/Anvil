using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes standard cursor images.
/// </summary>
[PublicAPI]
public enum CursorShape
{
    /// <summary>
    /// Standard arrow pointer.
    /// </summary>
    Arrow = 0x00036001,    
	
    /// <summary>
    /// I-beam cursor for text selection.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    IBeam = 0x00036002,
	
    /// <summary>
    /// Crosshair/target cursor.
    /// </summary>
    Crosshair = 0x00036003,
	
    /// <summary>
    /// Hand cursor.
    /// </summary>
    Hand = 0x00036004,    
	
    /// <summary>
    /// Cursor used when resizing a window horizontally.
    /// </summary>
    HResize = 0x00036005,  
	
    /// <summary>
    /// Cursor used when resizing a window vertically.
    /// </summary>
    VResize = 0x00036006
}