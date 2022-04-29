using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the behavior of a mouse cursor.
/// </summary>
[PublicAPI]
public enum CursorMode
{
    /// <summary>
    /// Makes the cursor visible and behaving normally.
    /// </summary>
    Normal = 0x00034001,
	
    /// <summary>
    /// Makes the cursor invisible when it is over the client area of the window but does not restrict the cursor from
    /// leaving. This is useful if you wish to render your own cursor or have no visible cursor at all.
    /// </summary>
    Hidden = 0x00034002,
	
    /// <summary>
    /// Hides and grabs the cursor, providing virtual and unlimited cursor movement. This is useful for implementing for
    /// example 3D camera controls.
    /// </summary>
    Disabled = 0x00034003
}