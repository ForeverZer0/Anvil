using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace Anvil.GLFW3;

/// <summary>
/// Handler for key callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="key">The keyboard key that was pressed or released.</param>
/// <param name="scancode">The system-specific scancode of the key.</param>
/// <param name="state">A value indicating the state change that occurred.</param>
/// <param name="mods">Flags describing which modifier keys were held down.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void KeyCallback(Window window, Key key, int scancode, InputState state, ModKey mods);
    
/// <summary>
/// Handler for mouse button callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="button">The mouse button that was pressed or released.</param>
/// <param name="state">A value indicating the state change that occurred.</param>
/// <param name="mods">Flags describing which modifier keys were held down.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void MouseButtonCallback(Window window, MouseButton button, InputState state, ModKey mods);
    
/// <summary>
/// Handler for cursor movement callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="x">The new cursor x-coordinate, relative to the left edge of the content area.</param>
/// <param name="y">The new cursor y-coordinate, relative to the left edge of the content area.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void CursorPositionCallback(Window window, double x, double y);

/// <summary>
/// Handler for cursor enter/leave callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="entered"><c>true</c> if the cursor entered the window's content area, or <c>false</c> if it left it.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void CursorEnterCallback(Window window, [MarshalAs(UnmanagedType.Bool)] bool entered);
  
/// <summary>
/// Handler for cursor scroll callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="xScroll">The scroll offset along the x-axis.</param>
/// <param name="yScroll">The scroll offset along the y-axis.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void ScrollCallback(Window window, double xScroll, double yScroll);

/// <summary>
/// Handler for text input callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="codepoint">The Unicode code point of the character.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void CharCallback(Window window, int codepoint);

/// <summary>
/// Handler for path drop callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="paths">An array containing the full path to the dropped files/directories.</param>
[PublicAPI]
public delegate void FileDropCallback(Window window, string[] paths);

/// <summary>
/// Handler for window position callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="x">The new x-coordinate, in screen coordinates, of the upper-left corner of the content area of the window.</param>
/// <param name="y">The new y-coordinate, in screen coordinates, of the upper-left corner of the content area of the window.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void WindowPositionCallback(Window window, int x, int y);

/// <summary>
/// Handler for window size callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="width">The new width, in screen coordinates, of the window.</param>
/// <param name="height">The new height, in screen coordinates, of the window.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void WindowSizeCallback(Window window, int width, int height);

/// <summary>
/// Handler for window closing callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="args">Argument that provide a mechanism to cancel the closing action.</param>
[PublicAPI]
public delegate void WindowCloseCallback(Window window, CancelEventArgs args);

/// <summary>
/// Handler for window refresh callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void WindowRefreshCallback(Window window);
    
/// <summary>
/// Handler for window refresh callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="focused"><c>true</c> if the window was given input focus, or <c>false</c> if it lost it.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void WindowFocusCallback(Window window, [MarshalAs(UnmanagedType.Bool)] bool focused);

/// <summary>
/// Handler for window minimize callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="minimized"><c>true</c> if the window was iconified, or <c>false</c> if it was restored.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void WindowMinimizeCallback(Window window, [MarshalAs(UnmanagedType.Bool)] bool minimized);
    
/// <summary>
/// Handler for window maximize callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="maximized"><c>true</c> if the window was maximized, or <c>false</c> if it was restored.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void WindowMaximizeCallback(Window window, [MarshalAs(UnmanagedType.Bool)] bool maximized);
    
/// <summary>
/// Handler for framebuffer resize callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="width">The new width, in pixels, of the framebuffer.</param>
/// <param name="height">The new height, in pixels, of the framebuffer.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void FramebufferSizeCallback(Window window, int width, int height);
    
/// <summary>
/// Handler for window content scale callbacks.
/// </summary>
/// <param name="window">The <see cref="Window"/> instance that raised the event.</param>
/// <param name="xScale">The new x-axis content scale of the window.</param>
/// <param name="yScale">The new y-axis content scale of the window.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void WindowScaleCallback(Window window, float xScale, float yScale);
    
/// <summary>
/// Handler for monitor state callbacks.
/// </summary>
/// <param name="monitor">The monitor whose state was changed.</param>
/// <param name="state">The new state of the monitor.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void MonitorCallback(Monitor monitor, ConnectionState state);
    
/// <summary>
/// Handler for joystick state callbacks.
/// </summary>
/// <param name="jid">The joystick index whose state was changed.</param>
/// <param name="state">The new state of the joystick.</param>
[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, PublicAPI]
public delegate void JoystickCallback(int jid, ConnectionState state);

/// <summary>
/// Handler for native GLFW error.
/// </summary>
/// <param name="code">An error code describing the type of the error.</param>
/// <param name="message">A string describing the error.</param>
[PublicAPI]
public delegate void ErrorCallback(ErrorCode code, string message);

[SuppressUnmanagedCodeSecurity]
public static unsafe partial class GLFW
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private delegate void ErrorCallbackImpl(ErrorCode code, IntPtr message);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private delegate void FileDropCallbackImpl(Window window, int numPaths, IntPtr paths);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private delegate void WindowCloseCallbackImpl(Window window);
    
    private sealed class WindowCallbacks : IDisposable
    {
        public KeyCallback? KeyCallback;
        public MouseButtonCallback? MouseButtonCallback;
        public CursorPositionCallback? CursorPositionCallback;
        public CursorEnterCallback? CursorEnterCallback;
        public WindowPositionCallback? WindowPositionCallback;
        public CharCallback? CharCallback;
        public ScrollCallback? ScrollCallback;
        public FileDropCallback? FileDropCallback;
        public WindowSizeCallback? WindowSizeCallback;
        public WindowCloseCallback? WindowCloseCallback;
        public WindowRefreshCallback? WindowRefreshCallback;
        public WindowFocusCallback? WindowFocusCallback;
        public WindowMinimizeCallback? WindowMinimizeCallback;
        public WindowMaximizeCallback? WindowMaximizeCallback;
        public FramebufferSizeCallback? FramebufferSizeCallback;
        public WindowScaleCallback? WindowScaleCallback;

        public void Dispose()
        {
            KeyCallback = null;
            MouseButtonCallback = null;
            CursorPositionCallback = null;
            CursorEnterCallback = null;
            WindowPositionCallback = null;
            CharCallback = null;
            ScrollCallback = null;
            FileDropCallback = null;
            WindowSizeCallback = null;
            WindowCloseCallback = null;
            WindowRefreshCallback = null;
            WindowFocusCallback = null;
            WindowMinimizeCallback = null;
            WindowMaximizeCallback = null;
            FramebufferSizeCallback = null;
            WindowScaleCallback = null;
        }
    }
    
    /// <summary>
    /// Raised when GLFW reports an error.
    /// </summary>
    public static event ErrorCallback? ErrorOccurred;

    /// <summary>
    /// Occurs when a joystick's connection state is changed.
    /// </summary>
    public static event JoystickCallback? JoystickStateChanged;

    /// <summary>
    /// Occurs when a monitor's connection state is changed.
    /// </summary>
    public static event MonitorCallback? MonitorStateChanged;

    private static void OnError(ErrorCode code, IntPtr message)
    {
        if (code == ErrorCode.None || ErrorOccurred is null)
            return;
        ErrorOccurred.Invoke(code, Marshal.PtrToStringUTF8(message) ?? string.Empty);
    }
    
    private static void OnJoystickConnection(int jid, ConnectionState state)
    {
        JoystickStateChanged?.Invoke(jid, state);
    }

    private static void OnMonitorConnection(Monitor monitor, ConnectionState state)
    {
        MonitorStateChanged?.Invoke(monitor, state);
    }
    
    private static WindowCallbacks GetEventHandlers(Window window)
    {
        if (windowCallbacksMap.TryGetValue(window, out var map)) 
            return map;
        
        map = new WindowCallbacks();
        windowCallbacksMap[window] = map;
        return map;
    }
    
    private static bool IsNullOrEmpty<TDelegate>(TDelegate? handler) where TDelegate : MulticastDelegate
    {
        return handler is null || handler.GetInvocationList().Length == 0;
    }

    internal static void SetWindowPosCallback(Window window, WindowPositionCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowPositionCallback += callback;
        else
            map.WindowPositionCallback -= callback;
        glfwSetWindowPosCallback(window, IsNullOrEmpty(map.WindowPositionCallback) ? null : map.WindowPositionCallback);
    }
    
    internal static void SetKeyCallback(Window window, KeyCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.KeyCallback += callback;
        else
            map.KeyCallback -= callback;
        glfwSetKeyCallback(window, IsNullOrEmpty(map.KeyCallback) ? null : map.KeyCallback);
    }
    
    internal static void SetMouseButtonCallback(Window window, MouseButtonCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.MouseButtonCallback += callback;
        else
            map.MouseButtonCallback -= callback;
        glfwSetMouseButtonCallback(window, IsNullOrEmpty(map.MouseButtonCallback) ? null : map.MouseButtonCallback);
    }

    internal static void SetCursorPosCallback(Window window, CursorPositionCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.CursorPositionCallback += callback;
        else
            map.CursorPositionCallback -= callback;
        glfwSetCursorPosCallback(window, IsNullOrEmpty(map.CursorPositionCallback) ? null : map.CursorPositionCallback);
    }

    internal static void SetCursorEnterCallback(Window window, CursorEnterCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.CursorEnterCallback += callback;
        else
            map.CursorEnterCallback -= callback;
        glfwSetCursorEnterCallback(window, IsNullOrEmpty(map.CursorEnterCallback) ? null : map.CursorEnterCallback);
    }

    internal static void SetCharCallback(Window window, CharCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.CharCallback += callback;
        else
            map.CharCallback -= callback;
        glfwSetCharCallback(window, IsNullOrEmpty(map.CharCallback) ? null : map.CharCallback);
    }

    internal static void SetScrollCallback(Window window, ScrollCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.ScrollCallback += callback;
        else
            map.ScrollCallback -= callback;
        glfwSetScrollCallback(window, IsNullOrEmpty(map.ScrollCallback) ? null : map.ScrollCallback);
    }

    internal static void SetWindowSizeCallback(Window window, WindowSizeCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowSizeCallback += callback;
        else
            map.WindowSizeCallback -= callback;
        glfwSetWindowSizeCallback(window, IsNullOrEmpty(map.WindowSizeCallback) ? null : map.WindowSizeCallback);
    }

    internal static void SetWindowRefreshCallback(Window window, WindowRefreshCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowRefreshCallback += callback;
        else
            map.WindowRefreshCallback -= callback;
        glfwSetWindowRefreshCallback(window, IsNullOrEmpty(map.WindowRefreshCallback) ? null : map.WindowRefreshCallback);
    }

    internal static void SetWindowFocusCallback(Window window, WindowFocusCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowFocusCallback += callback;
        else
            map.WindowFocusCallback -= callback;
        glfwSetWindowFocusCallback(window, IsNullOrEmpty(map.WindowFocusCallback) ? null : map.WindowFocusCallback);
    }

    internal static void SetWindowIconifyCallback(Window window, WindowMinimizeCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowMinimizeCallback += callback;
        else
            map.WindowMinimizeCallback -= callback;
        glfwSetWindowIconifyCallback(window, IsNullOrEmpty(map.WindowMinimizeCallback) ? null : map.WindowMinimizeCallback);
    }

    internal static void SetWindowMaximizeCallback(Window window, WindowMaximizeCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowMaximizeCallback += callback;
        else
            map.WindowMaximizeCallback -= callback;
        glfwSetWindowMaximizeCallback(window, IsNullOrEmpty(map.WindowMaximizeCallback) ? null : map.WindowMaximizeCallback);
    }

    internal static void SetFramebufferSizeCallback(Window window, FramebufferSizeCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.FramebufferSizeCallback += callback;
        else
            map.FramebufferSizeCallback -= callback;
        glfwSetFramebufferSizeCallback(window, IsNullOrEmpty(map.FramebufferSizeCallback) ? null : map.FramebufferSizeCallback);
    }

    internal static void SetWindowContentScaleCallback(Window window, WindowScaleCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowScaleCallback += callback;
        else
            map.WindowScaleCallback -= callback;
        glfwSetWindowContentScaleCallback(window, IsNullOrEmpty(map.WindowScaleCallback) ? null : map.WindowScaleCallback);
    }
    
    internal static void SetDropCallback(Window window, FileDropCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.FileDropCallback += callback;
        else
            map.FileDropCallback -= callback;
        glfwSetDropCallback(window, IsNullOrEmpty(map.FileDropCallback) ? null : onFileDrop);
    }
    
    private static void OnFileDrop(Window window, int numPaths, IntPtr paths)
    {
        var map = GetEventHandlers(window);
        if (IsNullOrEmpty(map.FileDropCallback))
            return;

        var pathArray = new string[numPaths];
        for (var i = 0; i < numPaths; i++)
        {
            var ptr = Marshal.ReadIntPtr(paths, i * IntPtr.Size);
            pathArray[i] = Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
        }
        map.FileDropCallback?.Invoke(window, pathArray);
    }
    
    internal static void SetWindowCloseCallback(Window window, WindowCloseCallback? callback, bool add)
    {
        var map = GetEventHandlers(window);
        if (add)
            map.WindowCloseCallback += callback;
        else
            map.WindowCloseCallback -= callback;
        glfwSetWindowCloseCallback(window, IsNullOrEmpty(map.WindowCloseCallback) ? null : onWindowClose);
    }
    
    private static void OnWindowClose(Window window)
    {
        var map = GetEventHandlers(window);
        if (IsNullOrEmpty(map.WindowCloseCallback))
            return;

        var args = new CancelEventArgs();
        map.WindowCloseCallback?.Invoke(window, args);
        if (args.Cancel)
            glfwSetWindowShouldClose(window, FALSE);
    }
}

