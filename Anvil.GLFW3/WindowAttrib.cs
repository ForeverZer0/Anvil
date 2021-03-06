using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes parameters that can be used to query with <see cref="GLFW.GetWindowAttrib"/>.
/// </summary>
[PublicAPI]
public enum WindowAttrib
{
    /// <summary>
    /// Indicates whether the specified window has input focus.
    /// </summary>
    Focused = 0x00020001,
	
    /// <summary>
    /// Indicates whether the specified window is iconified.
    /// </summary>
    Iconified = 0x00020002,
	
    /// <summary>
    /// Indicates whether the specified window is maximized.
    /// </summary>
    Maximized = 0x00020008,
	
    /// <summary>
    /// Indicates whether the cursor is currently directly over the content area of the window, with no other windows
    /// between. 
    /// </summary>
    Hovered = 0x0002000B,
	
    /// <summary>
    /// Indicates whether the specified window is visible.
    /// </summary>
    Visible = 0x00020004,
	
    /// <summary>
    /// Indicates whether the specified window is resizable <i>by the user</i>.
    /// </summary>
    Resizable = 0x00020003,
	
    /// <summary>
    /// Indicates whether the specified window has decorations such as a border, a close widget, etc.
    /// </summary>
    Decorated = 0x00020005,
	
    /// <summary>
    /// Indicates whether the specified full screen window is iconified on focus loss, a close widget, etc.
    /// </summary>
    AutoIconify = 0x00020006,
	
    /// <summary>
    /// Indicates whether the specified window is floating, also called topmost or always-on-top.
    /// </summary>
    Floating = 0x00020007,
	
    /// <summary>
    /// Indicates whether the specified window has a transparent framebuffer, i.e. the window contents is composited
    /// with the background using the window framebuffer alpha channel.
    /// </summary>
    TransparentFramebuffer = 0x0002000A,
	
    /// <summary>
    /// Specifies whether the window will be given input focus when <see cref="GLFW.ShowWindow"/> is called. 
    /// </summary>
    FocusOnShow = 0x0002000C,

    /// <summary>
    /// Indicates the client API provided by the window's context.
    /// </summary>
    ClientApi = 0x00022001,
	
    /// <summary>
    /// Indicates the context creation API used to create the window's context.
    /// </summary>
    ContextCreationApi = 0x0002200B,
	
    /// <summary>
    /// Indicate the client API major version of the window's context.
    /// </summary>
    ContextVersionMajor = 0x00022002,
	
    /// <summary>
    /// Indicate the client API minor version of the window's context.
    /// </summary>
    ContextVersionMinor = 0x00022003,
	
    /// <summary>
    /// Indicate the client API revision version of the window's context.
    /// </summary>
    ContextRevision = 0x00022004,
	
    /// <summary>
    /// Indicates the robustness strategy used by the context.
    /// </summary>
    ContextRobustness = 0x00022005,
	
    /// <summary>
    /// Indicates if the window's context is an OpenGL forward-compatible one.
    /// </summary>
    OpenGLForwardCompat = 0x00022006,
	
    /// <summary>
    /// Indicates the OpenGL profile used by the context.
    /// </summary>
    OpenGLProfile = 0x00022008,
	
    /// <summary>
    /// Indicates the release used by the context.
    /// </summary>
    ContextReleaseBehavior = 0x00022009,
	
    /// <summary>
    /// Indicates whether errors are generated by the context.
    /// </summary>
    ContextNoError = 0x0002200A,
	
    /// <summary>
    /// Indicates  if the window's context is in debug mode.
    /// </summary>
    OpenGLDebugContext = 0x00022007,
}