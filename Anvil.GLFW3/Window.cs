using System.Runtime.InteropServices;
using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Opaque window object handle. 
/// </summary>
[StructLayout(LayoutKind.Sequential), PublicAPI]
public readonly struct Window : IEquatable<Window>, IHandle
{
    /// <summary>
    /// The singleton instance of a null/invalid <see cref="Window"/>.
    /// </summary>
    public static readonly Window None;

    /// <summary>
    /// Gets the current <see cref="Window"/> context on the calling thread, or <c>null</c> if none exist or are active.
    /// </summary>
    public static Window? Current => GLFW.GetCurrentContext();
    
    /// <inheritdoc />
    public bool Equals(Window other) => value.Equals(other.value);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Window other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => value.GetHashCode();

    /// <summary>
    /// Determines whether two specified windows have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Window"/> to compare.</param>
    /// <param name="right">The second <see cref="Window"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Window left, Window right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified windows have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Window"/> to compare.</param>
    /// <param name="right">The second <see cref="Window"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Window left, Window right) => !left.Equals(right);

    /// <summary>
    /// Occurs when a key is pressed, repeated or released.
    /// </summary>
    /// <remarks>
    /// The key functions deal with physical keys, with layout independent key tokens named after their values in the
    /// standard US keyboard layout. If you want to input text, use the <see cref="TextInput"/> event instead.
    /// <para/>
    /// When a window loses input focus, it will generate synthetic key release events for all pressed keys. You can
    /// tell these events from user-generated events by the fact that the synthetic ones are generated after the focus
    /// loss event has been processed, i.e. after the window focus callback has been called.
    /// <para/>
    /// The scancode of a key is specific to that platform or sometimes even to that machine. Scancodes are intended to
    /// allow users to bind keys that don't have a <see cref="GLFW3.Key"/>y token. Such keys have key set to
    /// <see cref="GLFW3.Key.Unknown"/>, their state is not saved and so it cannot be queried with
    /// <see cref="GLFW.GetKey"/>.
    /// <para/>
    /// Sometimes GLFW needs to generate synthetic key events, in which case the scancode may be zero.
    /// </remarks>
    public event KeyCallback? KeyInput
    {
        add => GLFW.SetKeyCallback(this, value, true);
        remove => GLFW.SetKeyCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when a Unicode character is input.
    /// </summary>
    /// <remarks>
    /// The character callback is intended for Unicode text input. As it deals with characters, it is keyboard layout
    /// dependent, whereas the key callback is not. Characters do not map 1:1 to physical keys, as a key may produce
    /// zero, one or more characters. If you want to know whether a specific physical key was pressed or released, see
    /// the <see cref="KeyInput"/> event instead.
    /// <para/>
    /// The character callback behaves as system text input normally does and will not be called if modifier keys are
    /// held down that would prevent normal text input on that platform, for example a Super (Command) key on macOS
    /// or Alt key on Windows.
    /// </remarks>
    public event CharCallback? TextInput
    {
        add => GLFW.SetCharCallback(this, value, true);
        remove => GLFW.SetCharCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs  when a mouse button is pressed or released.
    /// </summary>
    /// <remarks>
    /// When a window loses input focus, it will generate synthetic mouse button release events for all pressed mouse
    /// buttons. You can tell these events from user-generated events by the fact that the synthetic ones are generated
    /// after the focus loss event has been processed, i.e. after the <see cref="FocusChanged"/> event has been called.
    /// </remarks>
    public event MouseButtonCallback? MouseButtonInput
    {
        add =>  GLFW.SetMouseButtonCallback(this, value, true);
        remove =>  GLFW.SetMouseButtonCallback(this, value, false);
    }

    /// <summary>
    /// Occurs when the cursor is moved.
    /// </summary>
    /// <remarks>
    /// The callback is provided with the position, in screen coordinates, relative to the upper-left corner of the
    /// content area of the window.
    /// </remarks>
    public event CursorPositionCallback? CursorMoved
    {
        add => GLFW.SetCursorPosCallback(this, value, true);
        remove => GLFW.SetCursorPosCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when a scrolling device is used, such as a mouse wheel or scrolling area of a touchpad.
    /// </summary>
    /// <remarks>
    /// The scroll callback receives all scrolling input, like that from a mouse wheel or a touchpad scrolling area.
    /// </remarks>
    public event ScrollCallback? Scrolled
    {
        add => GLFW.SetScrollCallback(this, value, true);
        remove => GLFW.SetScrollCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when the cursor enters or leaves the content area of the window.
    /// </summary>
    public event CursorEnterCallback? CursorEnterChanged
    {
        add => GLFW.SetCursorEnterCallback(this, value, true);
        remove => GLFW.SetCursorEnterCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when one or more dragged paths are dropped on the window.
    /// </summary>
    public event FileDropCallback? FileDropped
    {
        add => GLFW.SetDropCallback(this, value, true);
        remove => GLFW.SetDropCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when the window is moved.
    /// </summary>
    /// <remarks>
    /// The callback is provided with the position, in screen coordinates, of the upper-left corner of the content area
    /// of the window.</remarks>
    public event WindowPositionCallback? Moved
    {
        add => GLFW.SetWindowPosCallback(this, value, true);
        remove => GLFW.SetWindowPosCallback(this, value, false);
    }

    /// <summary>
    /// Occurs when the window is resized.
    /// </summary>
    /// <remarks>
    /// The event is provided with the size, in screen coordinates, of the content area of the window. If you need
    /// notified for purpose of updating the viewport, projection matrix, etc, use <see cref="FramebufferResized"/>.
    /// </remarks>
    /// <seealso cref="FramebufferResized"/>
    public event WindowSizeCallback? Resized
    {
        add => GLFW.SetWindowSizeCallback(this, value, true);
        remove => GLFW.SetWindowSizeCallback(this, value, false);
    }

    /// <summary>
    /// Occurs when the user attempts to close the window, for example by clicking the close widget in the title bar.
    /// </summary>
    public event WindowCloseCallback? Closing
    {
        add => GLFW.SetWindowCloseCallback(this, value, true);
        remove => GLFW.SetWindowCloseCallback(this, value, false);
    }

    /// <summary>
    /// Occurs when the content area of the window needs to be redrawn, for example if the window has been exposed after
    /// having been covered by another window.
    /// </summary>
    /// <remarks>
    /// On compositing window systems such as Aero, Compiz, Aqua or Wayland, where the window contents are saved
    /// off-screen, this event may be called only very infrequently or never at all.
    /// </remarks>
    public event WindowRefreshCallback? Refreshed
    {
        add =>  GLFW.SetWindowRefreshCallback(this, value, true);
        remove =>  GLFW.SetWindowRefreshCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when the window gains or loses input focus.
    /// </summary>
    /// <remarks>
    /// After the focus callback is called for a window that lost input focus, synthetic key and mouse button release
    /// events will be generated for all such that had been pressed.
    /// </remarks>
    public event WindowFocusCallback? FocusChanged
    {
        add => GLFW.SetWindowFocusCallback(this, value, true);
        remove => GLFW.SetWindowFocusCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when the window is maximized or restored.
    /// </summary>
    public event WindowMaximizeCallback? Maximized
    {
        add => GLFW.SetWindowMaximizeCallback(this, value, true);
        remove => GLFW.SetWindowMaximizeCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs hen the window is minimized or restored.
    /// </summary>
    public event WindowMinimizeCallback? Minimized
    {
        add => GLFW.SetWindowIconifyCallback(this, value, true);
        remove => GLFW.SetWindowIconifyCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when the content scale of the window changes.
    /// </summary>
    public event WindowScaleCallback? ContentScaleChanged
    {
        add => GLFW.SetWindowContentScaleCallback(this, value, true);
        remove => GLFW.SetWindowContentScaleCallback(this, value, false);
    }
    
    /// <summary>
    /// Occurs when the framebuffer of the specified window is resized.
    /// </summary>
    /// <remarks>
    /// This is the preferred event to subscribe to over <see cref="Resized"/> when you need notified to update the
    /// viewport, projection matrix, etc, as the supplied arguments are in pixel coordinates with this event, and
    /// in screen coordinates in the other, which may differ on certain platforms.
    /// </remarks>
    public event FramebufferSizeCallback? FramebufferResized
    {
        add => GLFW.SetFramebufferSizeCallback(this, value, true);
        remove => GLFW.SetFramebufferSizeCallback(this, value, false);
    }

    /// <inheritdoc />
    public IntPtr Value => value;
    
    private readonly IntPtr value;
}