using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes various input modes that can be configured to determine the behavior of how input is handled and reported.
/// </summary>
[PublicAPI]
public enum InputMode
{
    /// <summary>
    /// The input mode of a cursor.
    /// </summary>
    /// <seealso cref="CursorMode"/>
    Cursor = 0x00033001,
	
    /// <summary>
    /// Indicates whether key states once pressed will maintain that state until the next time it is queried with
    /// <see cref="GLFW.GetKey"/>. This is useful for when implementing a polling based input system, as it avoids
    /// missing state changed between frames.
    /// </summary>
    StickyKeys = 0x00033002,
	
    /// <summary>
    /// Indicates whether mouse button states once pressed will maintain that state until the next time it is queried
    /// with <see cref="GLFW.GetKey"/>. This is useful for when implementing a polling based input system, as it avoids
    /// missing state changed between frames.
    /// </summary>
    StickyMouseButtons = 0x00033003,
	
    /// <summary>
    /// Specifies whether the state of the <c>CapsLock</c> and <c>NumLock</c> keys will be reported with events.
    /// </summary>
    LockKeyMods = 0x00033004,
	
    /// <summary>
    /// Indicates whether (when supported) raw (unscaled and unaccelerated) mouse motion is enabled.
    /// </summary>
    /// <seealso cref="GLFW.RawMouseMotionSupported"/>
    RawMouseMotion = 0x00033005
}