using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes hints that can be supplied to GLFW to determine initialization behavior.
/// </summary>
[PublicAPI]
public enum InitHint
{
    /// <summary>
    /// Specifies whether to also expose joystick hats as buttons, for compatibility with earlier versions of GLFW that
    /// did not have <see cref="GLFW.GetJoystickHats(int)"/>.
    /// </summary>
    JoystickHatButtons = 0x00050001,
	
    /// <summary>
    /// Specifies whether to set the current directory to the application to the "Contents/Resources" subdirectory of
    /// the application's bundle, if present.
    /// </summary>
    CocoaChdirResources = 0x00051001,
	
    /// <summary>
    /// Specifies whether to create a basic menu bar, either from a nib or manually, when the first window is created,
    /// which is when AppKit is initialized.
    /// </summary>
    CocoaMenuBar = 0x00051002
}