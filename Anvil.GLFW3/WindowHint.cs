using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Hints to be supplied prior to window creation to effect the behavior and configuration of the window, framebuffer,
/// and input modes.
/// </summary>
[PublicAPI]
public enum WindowHint
{
    /// <summary>
    /// Specifies the bit-depth of the red component in the default framebuffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    RedBits = 0x00021001,
	
    /// <summary>
    /// Specifies the bit-depth of the green component in the default framebuffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    GreenBits = 0x00021002,
	
    /// <summary>
    /// Specifies the blue-depth of the red component in the default framebuffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    BlueBits = 0x00021003,
	
    /// <summary>
    /// Specifies the bit-depth of the alpha component in the default framebuffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    AlphaBits = 0x00021004,
	
    /// <summary>
    /// Specifies the bit-depth of the depth in the default framebuffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    DepthBits = 0x00021005,
	
    /// <summary>
    /// Specifies the bit-depth of the stencil in the default framebuffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    StencilBits = 0x00021006,
	
    /// <summary>
    /// Specifies the bit-depth of the red component in the default accumulation buffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    AccumRedBits = 0x00021007,
	
    /// <summary>
    /// Specifies the bit-depth of the green component in the default accumulation buffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    AccumGreenBits = 0x00021008,
	
    /// <summary>
    /// Specifies the bit-depth of the blue component in the default accumulation buffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    AccumBlueBits = 0x00021009,
	
    /// <summary>
    /// Specifies the bit-depth of the alpha component in the default accumulation buffer.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    AccumAlphaBits = 0x0002100A,
	
    /// <summary>
    /// Specifies the desired number of auxiliary buffers.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    AuxBuffers = 0x0002100B,
	
    /// <summary>
    /// Specifies the desired number of samples to use for multisampling. Zero disables multisampling.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    Samples = 0x0002100D,
	
    /// <summary>
    /// Specifies the desired refresh rate for full screen windows. If set to zero, the highest available refresh rate
    /// will be used. This hint is ignored for windowed mode windows.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    RefreshRate = 0x0002100F,
	
    /// <summary>
    /// Specifies whether to use stereoscopic rendering.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    Stereo = 0x0002100C,
	
    /// <summary>
    /// Specifies whether the framebuffer should be sRGB capable.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    SrgbCapable = 0x0002100E,
	
    /// <summary>
    /// Specified whether to use double-buffering for the framebuffer.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    DoubleBuffer = 0x00021010,
	
    /// <summary>
    /// Specifies which client API to create the context for.
    /// </summary>
    /// <remarks><see cref="GLFW3.OpenGLApi"/> value.</remarks>
    ClientApi = 0x00022001,
	
    /// <summary>
    /// Specify the client API major version that the created context must be compatible with. The exact behavior of
    /// this hint depend on the requested client API.
    /// <para/>
    /// While there is no way to ask the driver for a context of the highest supported version, GLFW will attempt to
    /// provide this when you ask for a version <c>1.0</c> context, which is the default.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    ContextVersionMajor = 0x00022002,
	
    /// <summary>
    /// Specify the client API minor version that the created context must be compatible with. The exact behavior of
    /// this hint depend on the requested client API.
    /// <para/>
    /// While there is no way to ask the driver for a context of the highest supported version, GLFW will attempt to
    /// provide this when you ask for a version <c>1.0</c> context, which is the default.
    /// </summary>
    /// <remarks><see cref="int"/> value.</remarks>
    ContextVersionMinor = 0x00022003,
	
    /// <summary>
    /// Specifies the robustness strategy to be used by the context.
    /// </summary>
    /// <remarks><see cref="GLFW3.Robustness"/> value.</remarks>
    ContextRobustness = 0x00022005,

    /// <summary>
    /// Specified the release behavior for the context.
    /// </summary>
    /// <remarks><see cref="GLFW3.ReleaseBehavior"/> value.</remarks>
    ContextReleaseBehavior = 0x00022009,
	
    /// <summary>
    /// Indicates if window's context is an OpenGL forward-compatible one.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    OpenGLForwardCompat = 0x00022006,
	
    /// <summary>
    /// Flag indicating if the OpenGL context will be a debug context.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    OpenGLDebugContext = 0x00022007,
	
    /// <summary>
    /// Specifies the OpenGL profile.
    /// </summary>
    /// <remarks><see cref="OpenGLProfile"/> value.</remarks>
    OpenGLProfile = 0x00022008,
	
    /// <summary>
    /// Specified the API used to create the context. 
    /// </summary>
    /// <remarks><see cref="GLFW3.ContextApi"/></remarks>
    ContextCreationApi = 0x0002200B,
	
    /// <summary>
    /// Specifies whether to create the context with a transparent framebuffer or not.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    TransparentFramebuffer = 0x0002000A,
	
    /// <summary>
    /// Indicates if the window will be created as user-resizeable window.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    Resizable = 0x00020003,
	
    /// <summary>
    /// Indicates if the window will be initially hidden or not.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    Visible = 0x00020004,
	
    /// <summary>
    /// Indicates if the window will be decorated with platform specific decorators (i.e. titlebar, close widget, etc.)
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    Decorated = 0x00020005,
	
    /// <summary>
    /// Specifies whether the windowed mode window will be given input focus when created.
    /// <para/>
    /// This hint is ignored for full screen and initially hidden windows.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    Focused = 0x00020001,
	
    /// <summary>
    /// Specifies whether the full screen window will automatically iconify and restore the previous video mode on input
    /// focus loss.
    /// <para/>
    /// This hint is ignored for windowed mode windows.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    AutoIconify = 0x00020006,
	
    /// <summary>
    /// Specifies whether the windowed mode window will be floating above other regular windows, also called topmost
    /// or always-on-top. This is intended primarily for debugging purposes and cannot be used to implement proper full
    /// screen windows. 
    /// <para/>
    /// This hint is ignored for full screen windows.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    Floating = 0x00020007,
	
    /// <summary>
    /// Specifies whether the windowed mode window will be maximized when created.
    /// <para/>
    /// This hint is ignored for full screen windows.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    Maximized = 0x00020008,
	
    /// <summary>
    /// Specifies whether the cursor should be centered over newly created full screen windows.
    /// <para/>
    /// This hint is ignored for windowed mode windows.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    CenterCursor = 0x00020009,
	
    /// <summary>
    /// Specifies whether the window will be given input focus when <see cref="GLFW.ShowWindow"/> is called.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    FocusOnShow = 0x0002000C,
	
    /// <summary>
    /// Specifies whether the window content area should be resized based on the monitor content scale of any monitor
    /// it is placed on. This includes the initial placement when the window is created
    /// <para/>
    /// This hint only has an effect on platforms where screen coordinates and pixels always map 1:1 such as Windows
    /// and X11. On platforms like macOS the resolution of the framebuffer is changed independently of the window size.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    ScaleToMonitor = 0x0002200C,
	
    /// <summary>
    /// Specifies whether to use full resolution framebuffers on Retina displays.
    /// <para/>
    /// Ignored on non-macOS platforms.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    CocoaRetinaFramebuffer = 0x00023001,
	
    /// <summary>
    /// Specifies the UTF-8 encoded name to use for auto-saving the window frame, or if empty disables frame auto-saving
    /// for the window.
    /// <para/>
    /// Ignored on non-macOS platforms.
    /// </summary>
    /// <remarks><see cref="string"/> value.</remarks>
    CocoaFrameName = 0x00023002,
	
    /// <summary>
    /// Specifies whether to in Automatic Graphics Switching, i.e. to allow the system to choose the integrated GPU for
    /// the OpenGL context and move it between GPUs if necessary or whether to force it to always run on the discrete
    /// GPU. This only affects systems with both integrated and discrete GPUs.
    /// <para/>
    /// Simpler programs and tools may want to enable this to save power, while games and other applications performing
    /// advanced rendering will want to leave it disabled.
    /// <para/>
    /// A bundled application that wishes to participate in Automatic Graphics Switching should also declare this in its
    /// <c>Info.plist</c> by setting the <c>NSSupportsAutomaticGraphicsSwitching</c> key to true.
    /// <para/>
    /// Ignored on non-macOS platforms.
    /// </summary>
    /// <remarks><see cref="bool"/> value.</remarks>
    CocoaGraphicsSwitching = 0x00023003,
	
    /// <summary>
    /// Specifies the desired ASCII encoded class of the ICCCM <c>WM_CLASS</c> window property.
    /// </summary>
    /// <remarks><see cref="string"/> value.</remarks>
    X11ClassName = 0x00024001,
	
    /// <summary>
    /// Specifies the desired ASCII encoded instance parts of the ICCCM <c>WM_CLASS</c> window property.
    /// </summary>
    /// <remarks><see cref="string"/> value.</remarks>
    X11InstanceName = 0x00024002
}