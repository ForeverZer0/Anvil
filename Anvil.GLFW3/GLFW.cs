using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Anvil.GLFW3.Vulkan;
using Anvil.Native;
using JetBrains.Annotations;
// ReSharper disable InconsistentNaming

[assembly: CLSCompliant(true)]

namespace Anvil.GLFW3;

/// <summary>
/// Provides an interface to the native GLFW library.
/// </summary>
[PublicAPI, SuppressUnmanagedCodeSecurity]
public static unsafe partial class GLFW
{
    private const int TRUE = 1;
    private const int FALSE = 0;
    private const int DONT_CARE = -1;

    private static readonly delegate *unmanaged[Cdecl]<int> glfwInit;
    private static readonly delegate *unmanaged[Cdecl]<void> glfwTerminate;
    private static readonly delegate *unmanaged[Cdecl]<InitHint,int,void> glfwInitHint;
    private static readonly delegate *unmanaged[Cdecl]<int*,int*,int*,void> glfwGetVersion;
    private static readonly delegate *unmanaged[Cdecl]<IntPtr> glfwGetVersionString;
    private static readonly delegate *unmanaged[Cdecl]<byte**,ErrorCode> glfwGetError;
    private static readonly delegate *unmanaged[Cdecl]<ErrorCallbackImpl?,ErrorCallbackImpl?> glfwSetErrorCallback;
    private static readonly delegate *unmanaged[Cdecl]<int*,Monitor*> glfwGetMonitors;
    private static readonly delegate *unmanaged[Cdecl]<Monitor> glfwGetPrimaryMonitor;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,int*,int*,void> glfwGetMonitorPos;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,int*,int*,int*,int*,void> glfwGetMonitorWorkarea;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,int*,int*,void> glfwGetMonitorPhysicalSize;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,float*,float*,void> glfwGetMonitorContentScale;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,IntPtr> glfwGetMonitorName;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,IntPtr,void> glfwSetMonitorUserPointer;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,IntPtr> glfwGetMonitorUserPointer;
    private static readonly delegate *unmanaged[Cdecl]<MonitorCallback?,MonitorCallback?> glfwSetMonitorCallback;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,int*,VideoMode*> glfwGetVideoModes;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,VideoMode*> glfwGetVideoMode;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,float,void> glfwSetGamma;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,GammaRamp*> glfwGetGammaRamp;
    private static readonly delegate *unmanaged[Cdecl]<Monitor,GammaRamp*,void> glfwSetGammaRamp;
    private static readonly delegate *unmanaged[Cdecl]<void> glfwDefaultWindowHints;
    private static readonly delegate *unmanaged[Cdecl]<WindowHint,int,void> glfwWindowHint;
    private static readonly delegate *unmanaged[Cdecl]<WindowHint,byte*,void> glfwWindowHintString;
    private static readonly delegate *unmanaged[Cdecl]<int,int,byte*,Monitor,Window,Window> glfwCreateWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwDestroyWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,int> glfwWindowShouldClose;
    private static readonly delegate *unmanaged[Cdecl]<Window,int,void> glfwSetWindowShouldClose;
    private static readonly delegate *unmanaged[Cdecl]<Window,byte*,void> glfwSetWindowTitle;
    private static readonly delegate *unmanaged[Cdecl]<Window,int,Bitmap*,void> glfwSetWindowIcon;
    private static readonly delegate *unmanaged[Cdecl]<Window,int*,int*,void> glfwGetWindowPos;
    private static readonly delegate *unmanaged[Cdecl]<Window,int,int,void> glfwSetWindowPos;
    private static readonly delegate *unmanaged[Cdecl]<Window,int*,int*,void> glfwGetWindowSize;
    private static readonly delegate *unmanaged[Cdecl]<Window,int,int,int,int,void> glfwSetWindowSizeLimits;
    private static readonly delegate *unmanaged[Cdecl]<Window,int,int,void> glfwSetWindowAspectRatio;
    private static readonly delegate *unmanaged[Cdecl]<Window,int,int,void> glfwSetWindowSize;
    private static readonly delegate *unmanaged[Cdecl]<Window,int*,int*,void> glfwGetFramebufferSize;
    private static readonly delegate *unmanaged[Cdecl]<Window,int*,int*,int*,int*,void> glfwGetWindowFrameSize;
    private static readonly delegate *unmanaged[Cdecl]<Window,float*,float*,void> glfwGetWindowContentScale;
    private static readonly delegate *unmanaged[Cdecl]<Window,float> glfwGetWindowOpacity;
    private static readonly delegate *unmanaged[Cdecl]<Window,float,void> glfwSetWindowOpacity;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwIconifyWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwRestoreWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwMaximizeWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwShowWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwHideWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwFocusWindow;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwRequestWindowAttention;
    private static readonly delegate *unmanaged[Cdecl]<Window,Monitor> glfwGetWindowMonitor;
    private static readonly delegate *unmanaged[Cdecl]<Window,Monitor,int,int,int,int,int,void> glfwSetWindowMonitor;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowAttrib,int> glfwGetWindowAttrib;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowAttrib,int,void> glfwSetWindowAttrib;
    private static readonly delegate *unmanaged[Cdecl]<Window,IntPtr,void> glfwSetWindowUserPointer;
    private static readonly delegate *unmanaged[Cdecl]<Window,IntPtr> glfwGetWindowUserPointer;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowPositionCallback?,WindowPositionCallback?> glfwSetWindowPosCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowSizeCallback?,WindowSizeCallback?> glfwSetWindowSizeCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowCloseCallbackImpl?,WindowCloseCallbackImpl?> glfwSetWindowCloseCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowRefreshCallback?,WindowRefreshCallback?> glfwSetWindowRefreshCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowFocusCallback?,WindowFocusCallback?> glfwSetWindowFocusCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowMinimizeCallback?,WindowMinimizeCallback?> glfwSetWindowIconifyCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowMaximizeCallback?,WindowMaximizeCallback?> glfwSetWindowMaximizeCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,FramebufferSizeCallback?,FramebufferSizeCallback?> glfwSetFramebufferSizeCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,WindowScaleCallback?,WindowScaleCallback?> glfwSetWindowContentScaleCallback;
    private static readonly delegate *unmanaged[Cdecl]<void> glfwPollEvents;
    private static readonly delegate *unmanaged[Cdecl]<void> glfwWaitEvents;
    private static readonly delegate *unmanaged[Cdecl]<double,void> glfwWaitEventsTimeout;
    private static readonly delegate *unmanaged[Cdecl]<void> glfwPostEmptyEvent;
    private static readonly delegate *unmanaged[Cdecl]<Window,InputMode,int> glfwGetInputMode;
    private static readonly delegate *unmanaged[Cdecl]<Window,InputMode,int,void> glfwSetInputMode;
    private static readonly delegate *unmanaged[Cdecl]<int> glfwRawMouseMotionSupported;
    private static readonly delegate *unmanaged[Cdecl]<Key,int,IntPtr> glfwGetKeyName;
    private static readonly delegate *unmanaged[Cdecl]<Key,int> glfwGetKeyScancode;
    private static readonly delegate *unmanaged[Cdecl]<Window,Key,int> glfwGetKey;
    private static readonly delegate *unmanaged[Cdecl]<Window,MouseButton,int> glfwGetMouseButton;
    private static readonly delegate *unmanaged[Cdecl]<Window,double*,double*,void> glfwGetCursorPos;
    private static readonly delegate *unmanaged[Cdecl]<Window,double,double,void> glfwSetCursorPos;
    private static readonly delegate *unmanaged[Cdecl]<Bitmap*,int,int,Cursor> glfwCreateCursor;
    private static readonly delegate *unmanaged[Cdecl]<CursorShape,Cursor> glfwCreateStandardCursor;
    private static readonly delegate *unmanaged[Cdecl]<Cursor,void> glfwDestroyCursor;
    private static readonly delegate *unmanaged[Cdecl]<Window,Cursor,void> glfwSetCursor;
    private static readonly delegate *unmanaged[Cdecl]<Window,KeyCallback?,KeyCallback?> glfwSetKeyCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,CharCallback?,CharCallback?> glfwSetCharCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,MouseButtonCallback?,MouseButtonCallback?> glfwSetMouseButtonCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,CursorPositionCallback?,CursorPositionCallback?> glfwSetCursorPosCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,CursorEnterCallback?,CursorEnterCallback?> glfwSetCursorEnterCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,ScrollCallback?,ScrollCallback?> glfwSetScrollCallback;
    private static readonly delegate *unmanaged[Cdecl]<Window,FileDropCallbackImpl?,FileDropCallbackImpl?> glfwSetDropCallback;
    private static readonly delegate *unmanaged[Cdecl]<int,int> glfwJoystickPresent;
    private static readonly delegate *unmanaged[Cdecl]<int,int*,float*> glfwGetJoystickAxes;
    private static readonly delegate *unmanaged[Cdecl]<int,int*,bool*> glfwGetJoystickButtons;
    private static readonly delegate *unmanaged[Cdecl]<int,int*,bool*> glfwGetJoystickHats;
    private static readonly delegate *unmanaged[Cdecl]<int,IntPtr> glfwGetJoystickName;
    private static readonly delegate *unmanaged[Cdecl]<int,IntPtr> glfwGetJoystickGUID;
    private static readonly delegate *unmanaged[Cdecl]<int,IntPtr,void> glfwSetJoystickUserPointer;
    private static readonly delegate *unmanaged[Cdecl]<int,IntPtr> glfwGetJoystickUserPointer;
    private static readonly delegate *unmanaged[Cdecl]<int,int> glfwJoystickIsGamepad;
    private static readonly delegate *unmanaged[Cdecl]<JoystickCallback?,JoystickCallback?> glfwSetJoystickCallback;
    private static readonly delegate *unmanaged[Cdecl]<byte*,int> glfwUpdateGamepadMappings;
    private static readonly delegate *unmanaged[Cdecl]<int,IntPtr> glfwGetGamepadName;
    private static readonly delegate *unmanaged[Cdecl]<int,GamepadState*,int> glfwGetGamepadState;
    private static readonly delegate *unmanaged[Cdecl]<Window,byte*,void> glfwSetClipboardString;
    private static readonly delegate *unmanaged[Cdecl]<Window,IntPtr> glfwGetClipboardString;
    private static readonly delegate *unmanaged[Cdecl]<double> glfwGetTime;
    private static readonly delegate *unmanaged[Cdecl]<double,void> glfwSetTime;
    private static readonly delegate *unmanaged[Cdecl]<ulong> glfwGetTimerValue;
    private static readonly delegate *unmanaged[Cdecl]<ulong> glfwGetTimerFrequency;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwMakeContextCurrent;
    private static readonly delegate *unmanaged[Cdecl]<Window> glfwGetCurrentContext;
    private static readonly delegate *unmanaged[Cdecl]<Window,void> glfwSwapBuffers;
    private static readonly delegate *unmanaged[Cdecl]<int,void> glfwSwapInterval;
    private static readonly delegate *unmanaged[Cdecl]<byte*,int> glfwExtensionSupported;
    private static readonly delegate *unmanaged[Cdecl]<byte*,IntPtr> glfwGetProcAddress;
    private static readonly delegate *unmanaged[Cdecl]<int> glfwVulkanSupported;
    private static readonly delegate *unmanaged[Cdecl]<uint*, IntPtr*> glfwGetRequiredInstanceExtensions;
    private static readonly delegate *unmanaged[Cdecl]<Instance, byte*, IntPtr> glfwGetInstanceProcAddress;
    private static readonly delegate *unmanaged[Cdecl]<Instance, PhysicalDevice, uint, int> glfwGetPhysicalDevicePresentationSupport;
    private static readonly delegate *unmanaged[Cdecl]<Instance,Window,IntPtr*,SurfaceKHR*,Result> glfwCreateWindowSurface;
    private static readonly delegate *unmanaged[Cdecl]<Window, IntPtr> glfwGetWGLContext;
    private static readonly delegate *unmanaged[Cdecl]<Window, IntPtr> glfwGetGLXContext;
    private static readonly delegate *unmanaged[Cdecl]<Window, IntPtr> glfwGetEGLContext;
    private static readonly delegate *unmanaged[Cdecl]<Window, int> glfwGetNSGLContext;
    
    private static readonly ErrorCallbackImpl onError;
    private static readonly MonitorCallback? monitorCallback;
    private static readonly JoystickCallback? joystickCallback;
    private static readonly FileDropCallbackImpl onFileDrop;
    private static readonly WindowCloseCallbackImpl onWindowClose;
    private static readonly Dictionary<Window, WindowCallbacks> windowCallbacksMap;
    private static readonly UnmanagedLibrary Library;
    
    [StructLayout(LayoutKind.Sequential)]
    private struct GammaRamp
    {
        public ushort* R;
        public ushort* G;
        public ushort* B;
        public int Size;
    }

    static GLFW()
    {
        // ReSharper disable StringLiteralTypo
        Library = new UnmanagedLibrary("/usr/lib/libglfw.so");
        glfwInit = (delegate *unmanaged[Cdecl]<int>)Library.Import("glfwInit");
        glfwTerminate = (delegate *unmanaged[Cdecl]<void>)Library.Import("glfwTerminate");
        glfwInitHint = (delegate *unmanaged[Cdecl]<InitHint, int, void>)Library.Import("glfwInitHint");
        glfwGetVersion = (delegate *unmanaged[Cdecl]<int*, int*, int*, void>)Library.Import("glfwGetVersion");
        glfwGetVersionString = (delegate *unmanaged[Cdecl]<IntPtr>)Library.Import("glfwGetVersionString");
        glfwGetError = (delegate *unmanaged[Cdecl]<byte**, ErrorCode>)Library.Import("glfwGetError");
        glfwSetErrorCallback = (delegate *unmanaged[Cdecl]<ErrorCallbackImpl?, ErrorCallbackImpl?>)Library.Import("glfwSetErrorCallback");
        glfwGetMonitors = (delegate *unmanaged[Cdecl]<int*, Monitor*>)Library.Import("glfwGetMonitors");
        glfwGetPrimaryMonitor = (delegate *unmanaged[Cdecl]<Monitor>)Library.Import("glfwGetPrimaryMonitor");
        glfwGetMonitorPos = (delegate *unmanaged[Cdecl]<Monitor, int*, int*, void>)Library.Import("glfwGetMonitorPos");
        glfwGetMonitorWorkarea = (delegate *unmanaged[Cdecl]<Monitor, int*, int*, int*, int*, void>)Library.Import("glfwGetMonitorWorkarea");
        glfwGetMonitorPhysicalSize = (delegate *unmanaged[Cdecl]<Monitor, int*, int*, void>)Library.Import("glfwGetMonitorPhysicalSize");
        glfwGetMonitorContentScale = (delegate *unmanaged[Cdecl]<Monitor, float*, float*, void>)Library.Import("glfwGetMonitorContentScale");
        glfwGetMonitorName = (delegate *unmanaged[Cdecl]<Monitor, IntPtr>)Library.Import("glfwGetMonitorName");
        glfwSetMonitorUserPointer = (delegate *unmanaged[Cdecl]<Monitor, IntPtr, void>)Library.Import("glfwSetMonitorUserPointer");
        glfwGetMonitorUserPointer = (delegate *unmanaged[Cdecl]<Monitor, IntPtr>)Library.Import("glfwGetMonitorUserPointer");
        glfwSetMonitorCallback = (delegate *unmanaged[Cdecl]<MonitorCallback?, MonitorCallback?>)Library.Import("glfwSetMonitorCallback");
        glfwGetVideoModes = (delegate *unmanaged[Cdecl]<Monitor, int*, VideoMode*>)Library.Import("glfwGetVideoModes");
        glfwGetVideoMode = (delegate *unmanaged[Cdecl]<Monitor, VideoMode*>)Library.Import("glfwGetVideoMode");
        glfwSetGamma = (delegate *unmanaged[Cdecl]<Monitor, float, void>)Library.Import("glfwSetGamma");
        glfwGetGammaRamp = (delegate *unmanaged[Cdecl]<Monitor, GammaRamp*>)Library.Import("glfwGetGammaRamp");
        glfwSetGammaRamp = (delegate *unmanaged[Cdecl]<Monitor, GammaRamp*, void>)Library.Import("glfwSetGammaRamp");
        glfwDefaultWindowHints = (delegate *unmanaged[Cdecl]<void>)Library.Import("glfwDefaultWindowHints");
        glfwWindowHint = (delegate *unmanaged[Cdecl]<WindowHint, int, void>)Library.Import("glfwWindowHint");
        glfwWindowHintString = (delegate *unmanaged[Cdecl]<WindowHint, byte*, void>)Library.Import("glfwWindowHintString");
        glfwCreateWindow = (delegate *unmanaged[Cdecl]<int, int, byte*, Monitor, Window, Window>)Library.Import("glfwCreateWindow");
        glfwDestroyWindow = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwDestroyWindow");
        glfwWindowShouldClose = (delegate *unmanaged[Cdecl]<Window, int>)Library.Import("glfwWindowShouldClose");
        glfwSetWindowShouldClose = (delegate *unmanaged[Cdecl]<Window, int, void>)Library.Import("glfwSetWindowShouldClose");
        glfwSetWindowTitle = (delegate *unmanaged[Cdecl]<Window, byte*, void>)Library.Import("glfwSetWindowTitle");
        glfwSetWindowIcon = (delegate *unmanaged[Cdecl]<Window, int, Bitmap*, void>)Library.Import("glfwSetWindowIcon");
        glfwGetWindowPos = (delegate *unmanaged[Cdecl]<Window, int*, int*, void>)Library.Import("glfwGetWindowPos");
        glfwSetWindowPos = (delegate *unmanaged[Cdecl]<Window, int, int, void>)Library.Import("glfwSetWindowPos");
        glfwGetWindowSize = (delegate *unmanaged[Cdecl]<Window, int*, int*, void>)Library.Import("glfwGetWindowSize");
        glfwSetWindowSizeLimits = (delegate *unmanaged[Cdecl]<Window, int, int, int, int, void>)Library.Import("glfwSetWindowSizeLimits");
        glfwSetWindowAspectRatio = (delegate *unmanaged[Cdecl]<Window, int, int, void>)Library.Import("glfwSetWindowAspectRatio");
        glfwSetWindowSize = (delegate *unmanaged[Cdecl]<Window, int, int, void>)Library.Import("glfwSetWindowSize");
        glfwGetFramebufferSize = (delegate *unmanaged[Cdecl]<Window, int*, int*, void>)Library.Import("glfwGetFramebufferSize");
        glfwGetWindowFrameSize = (delegate *unmanaged[Cdecl]<Window, int*, int*, int*, int*, void>)Library.Import("glfwGetWindowFrameSize");
        glfwGetWindowContentScale = (delegate *unmanaged[Cdecl]<Window, float*, float*, void>)Library.Import("glfwGetWindowContentScale");
        glfwGetWindowOpacity = (delegate *unmanaged[Cdecl]<Window, float>)Library.Import("glfwGetWindowOpacity");
        glfwSetWindowOpacity = (delegate *unmanaged[Cdecl]<Window, float, void>)Library.Import("glfwSetWindowOpacity");
        glfwIconifyWindow = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwIconifyWindow");
        glfwRestoreWindow = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwRestoreWindow");
        glfwMaximizeWindow = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwMaximizeWindow");
        glfwShowWindow = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwShowWindow");
        glfwHideWindow = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwHideWindow");
        glfwFocusWindow = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwFocusWindow");
        glfwRequestWindowAttention = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwRequestWindowAttention");
        glfwGetWindowMonitor = (delegate *unmanaged[Cdecl]<Window, Monitor>)Library.Import("glfwGetWindowMonitor");
        glfwSetWindowMonitor = (delegate *unmanaged[Cdecl]<Window, Monitor, int, int, int, int, int, void>)Library.Import("glfwSetWindowMonitor");
        glfwGetWindowAttrib = (delegate *unmanaged[Cdecl]<Window, WindowAttrib, int>)Library.Import("glfwGetWindowAttrib");
        glfwSetWindowAttrib = (delegate *unmanaged[Cdecl]<Window, WindowAttrib, int, void>)Library.Import("glfwSetWindowAttrib");
        glfwSetWindowUserPointer = (delegate *unmanaged[Cdecl]<Window, IntPtr, void>)Library.Import("glfwSetWindowUserPointer");
        glfwGetWindowUserPointer = (delegate *unmanaged[Cdecl]<Window, IntPtr>)Library.Import("glfwGetWindowUserPointer");
        glfwSetWindowPosCallback = (delegate *unmanaged[Cdecl]<Window, WindowPositionCallback?, WindowPositionCallback?>)Library.Import("glfwSetWindowPosCallback");
        glfwSetWindowSizeCallback = (delegate *unmanaged[Cdecl]<Window, WindowSizeCallback?, WindowSizeCallback?>)Library.Import("glfwSetWindowSizeCallback");
        glfwSetWindowCloseCallback = (delegate *unmanaged[Cdecl]<Window, WindowCloseCallbackImpl?, WindowCloseCallbackImpl?>)Library.Import("glfwSetWindowCloseCallback");
        glfwSetWindowRefreshCallback = (delegate *unmanaged[Cdecl]<Window, WindowRefreshCallback?, WindowRefreshCallback?>)Library.Import("glfwSetWindowRefreshCallback");
        glfwSetWindowFocusCallback = (delegate *unmanaged[Cdecl]<Window, WindowFocusCallback?, WindowFocusCallback?>)Library.Import("glfwSetWindowFocusCallback");
        glfwSetWindowIconifyCallback = (delegate *unmanaged[Cdecl]<Window, WindowMinimizeCallback?, WindowMinimizeCallback?>)Library.Import("glfwSetWindowIconifyCallback");
        glfwSetWindowMaximizeCallback = (delegate *unmanaged[Cdecl]<Window, WindowMaximizeCallback?, WindowMaximizeCallback?>)Library.Import("glfwSetWindowMaximizeCallback");
        glfwSetFramebufferSizeCallback = (delegate *unmanaged[Cdecl]<Window, FramebufferSizeCallback?, FramebufferSizeCallback?>)Library.Import("glfwSetFramebufferSizeCallback");
        glfwSetWindowContentScaleCallback = (delegate *unmanaged[Cdecl]<Window, WindowScaleCallback?, WindowScaleCallback?>)Library.Import("glfwSetWindowContentScaleCallback");
        glfwPollEvents = (delegate *unmanaged[Cdecl]<void>)Library.Import("glfwPollEvents");
        glfwWaitEvents = (delegate *unmanaged[Cdecl]<void>)Library.Import("glfwWaitEvents");
        glfwWaitEventsTimeout = (delegate *unmanaged[Cdecl]<double, void>)Library.Import("glfwWaitEventsTimeout");
        glfwPostEmptyEvent = (delegate *unmanaged[Cdecl]<void>)Library.Import("glfwPostEmptyEvent");
        glfwGetInputMode = (delegate *unmanaged[Cdecl]<Window, InputMode, int>)Library.Import("glfwGetInputMode");
        glfwSetInputMode = (delegate *unmanaged[Cdecl]<Window, InputMode, int, void>)Library.Import("glfwSetInputMode");
        glfwRawMouseMotionSupported = (delegate *unmanaged[Cdecl]<int>)Library.Import("glfwRawMouseMotionSupported");
        glfwGetKeyName = (delegate *unmanaged[Cdecl]<Key, int, IntPtr>)Library.Import("glfwGetKeyName");
        glfwGetKeyScancode = (delegate *unmanaged[Cdecl]<Key, int>)Library.Import("glfwGetKeyScancode");
        glfwGetKey = (delegate *unmanaged[Cdecl]<Window, Key, int>)Library.Import("glfwGetKey");
        glfwGetMouseButton = (delegate *unmanaged[Cdecl]<Window, MouseButton, int>)Library.Import("glfwGetMouseButton");
        glfwGetCursorPos = (delegate *unmanaged[Cdecl]<Window, double*, double*, void>)Library.Import("glfwGetCursorPos");
        glfwSetCursorPos = (delegate *unmanaged[Cdecl]<Window, double, double, void>)Library.Import("glfwSetCursorPos");
        glfwCreateCursor = (delegate *unmanaged[Cdecl]<Bitmap*, int, int, Cursor>)Library.Import("glfwCreateCursor");
        glfwCreateStandardCursor = (delegate *unmanaged[Cdecl]<CursorShape, Cursor>)Library.Import("glfwCreateStandardCursor");
        glfwDestroyCursor = (delegate *unmanaged[Cdecl]<Cursor, void>)Library.Import("glfwDestroyCursor");
        glfwSetCursor = (delegate *unmanaged[Cdecl]<Window, Cursor, void>)Library.Import("glfwSetCursor");
        glfwSetKeyCallback = (delegate *unmanaged[Cdecl]<Window, KeyCallback?, KeyCallback?>)Library.Import("glfwSetKeyCallback");
        glfwSetCharCallback = (delegate *unmanaged[Cdecl]<Window, CharCallback?, CharCallback?>)Library.Import("glfwSetCharCallback");
        glfwSetMouseButtonCallback = (delegate *unmanaged[Cdecl]<Window, MouseButtonCallback?, MouseButtonCallback?>)Library.Import("glfwSetMouseButtonCallback");
        glfwSetCursorPosCallback = (delegate *unmanaged[Cdecl]<Window, CursorPositionCallback?, CursorPositionCallback?>)Library.Import("glfwSetCursorPosCallback");
        glfwSetCursorEnterCallback = (delegate *unmanaged[Cdecl]<Window, CursorEnterCallback?, CursorEnterCallback?>)Library.Import("glfwSetCursorEnterCallback");
        glfwSetScrollCallback = (delegate *unmanaged[Cdecl]<Window, ScrollCallback?, ScrollCallback?>)Library.Import("glfwSetScrollCallback");
        glfwSetDropCallback = (delegate *unmanaged[Cdecl]<Window, FileDropCallbackImpl?, FileDropCallbackImpl?>)Library.Import("glfwSetDropCallback");
        glfwJoystickPresent = (delegate *unmanaged[Cdecl]<int, int>)Library.Import("glfwJoystickPresent");
        glfwGetJoystickAxes = (delegate *unmanaged[Cdecl]<int, int*, float*>)Library.Import("glfwGetJoystickAxes");
        glfwGetJoystickButtons = (delegate *unmanaged[Cdecl]<int, int*, bool*>)Library.Import("glfwGetJoystickButtons");
        glfwGetJoystickHats = (delegate *unmanaged[Cdecl]<int, int*, bool*>)Library.Import("glfwGetJoystickHats");
        glfwGetJoystickName = (delegate *unmanaged[Cdecl]<int, IntPtr>)Library.Import("glfwGetJoystickName");
        glfwGetJoystickGUID = (delegate *unmanaged[Cdecl]<int, IntPtr>)Library.Import("glfwGetJoystickGUID");
        glfwSetJoystickUserPointer = (delegate *unmanaged[Cdecl]<int, IntPtr, void>)Library.Import("glfwSetJoystickUserPointer");
        glfwGetJoystickUserPointer = (delegate *unmanaged[Cdecl]<int, IntPtr>)Library.Import("glfwGetJoystickUserPointer");
        glfwJoystickIsGamepad = (delegate *unmanaged[Cdecl]<int, int>)Library.Import("glfwJoystickIsGamepad");
        glfwSetJoystickCallback = (delegate *unmanaged[Cdecl]<JoystickCallback?, JoystickCallback?>)Library.Import("glfwSetJoystickCallback");
        glfwUpdateGamepadMappings = (delegate *unmanaged[Cdecl]<byte*, int>)Library.Import("glfwUpdateGamepadMappings");
        glfwGetGamepadName = (delegate *unmanaged[Cdecl]<int, IntPtr>)Library.Import("glfwGetGamepadName");
        glfwGetGamepadState = (delegate *unmanaged[Cdecl]<int, GamepadState*, int>)Library.Import("glfwGetGamepadState");
        glfwSetClipboardString = (delegate *unmanaged[Cdecl]<Window, byte*, void>)Library.Import("glfwSetClipboardString");
        glfwGetClipboardString = (delegate *unmanaged[Cdecl]<Window, IntPtr>)Library.Import("glfwGetClipboardString");
        glfwGetTime = (delegate *unmanaged[Cdecl]<double>)Library.Import("glfwGetTime");
        glfwSetTime = (delegate *unmanaged[Cdecl]<double, void>)Library.Import("glfwSetTime");
        glfwGetTimerValue = (delegate *unmanaged[Cdecl]<ulong>)Library.Import("glfwGetTimerValue");
        glfwGetTimerFrequency = (delegate *unmanaged[Cdecl]<ulong>)Library.Import("glfwGetTimerFrequency");
        glfwMakeContextCurrent = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwMakeContextCurrent");
        glfwGetCurrentContext = (delegate *unmanaged[Cdecl]<Window>)Library.Import("glfwGetCurrentContext");
        glfwSwapBuffers = (delegate *unmanaged[Cdecl]<Window, void>)Library.Import("glfwSwapBuffers");
        glfwSwapInterval = (delegate *unmanaged[Cdecl]<int, void>)Library.Import("glfwSwapInterval");
        glfwExtensionSupported = (delegate *unmanaged[Cdecl]<byte*, int>)Library.Import("glfwExtensionSupported");
        glfwGetProcAddress = (delegate *unmanaged[Cdecl]<byte*, IntPtr>)Library.Import("glfwGetProcAddress");
        
        glfwVulkanSupported = (delegate *unmanaged[Cdecl]<int>) Library.Import("glfwVulkanSupported");
        glfwGetRequiredInstanceExtensions = (delegate *unmanaged[Cdecl]<uint*, IntPtr*>) Library.Import("glfwGetRequiredInstanceExtensions");
        glfwGetInstanceProcAddress = (delegate *unmanaged[Cdecl]<Instance, byte*, IntPtr>) Library.Import("glfwGetInstanceProcAddress");
        glfwGetPhysicalDevicePresentationSupport = (delegate *unmanaged[Cdecl]<Instance, PhysicalDevice, uint, int>) Library.Import("glfwGetPhysicalDevicePresentationSupport");
        glfwCreateWindowSurface = (delegate *unmanaged[Cdecl]<Instance,Window,IntPtr*,SurfaceKHR*,Result>) Library.Import("glfwCreateWindowSurface");
        glfwGetWGLContext = (delegate *unmanaged[Cdecl]<Window, IntPtr>)Library.Import("glfwGetWGLContext", false);
        glfwGetGLXContext = (delegate *unmanaged[Cdecl]<Window, IntPtr>)Library.Import("glfwGetGLXContext", false);
        glfwGetEGLContext = (delegate *unmanaged[Cdecl]<Window, IntPtr>)Library.Import("glfwGetEGLContext", false);
        glfwGetNSGLContext = (delegate *unmanaged[Cdecl]<Window, int>)Library.Import("glfwGetNSGLContext", false);
            
        // ReSharper restore StringLiteralTypo

        // Store a reference, as the garbage collector is not aware the delegate is still being referenced natively.
        onError = OnError;
        monitorCallback = OnMonitorConnection;
        joystickCallback = OnJoystickConnection;
        onFileDrop = OnFileDrop;
        onWindowClose = OnWindowClose;
        
        glfwSetErrorCallback(onError);
        glfwSetMonitorCallback(monitorCallback);
        glfwSetJoystickCallback(joystickCallback);
        windowCallbacksMap = new Dictionary<Window, WindowCallbacks>();
    }
    
    /// <summary>
    /// Gets the maximum number of supported joysticks.
    /// <para/>
    /// Joysticks are identified using a zero-based index.
    /// </summary>
    /// <example>
    /// Enumerate through each index and check is joystick is present.
    /// <code>
    ///     for (var jid = 0; jid &lt; GLFW.MaxJoysticks; jid++)
    ///     {
    ///         yield return GLFW.JoystickPresent(jid);
    ///     }
    /// </code>
    /// </example>
    public const int MaxJoysticks = 16;
    
    /// <summary>
    /// Gets the current GLFW time as a <see cref="TimeSpan"/>. Unless the time has been set using
    /// <see cref="SetTime(double)"/> it measures time elapsed since GLFW was initialized.
    /// <para/>
    /// The resolution of the timer is system dependent, but is usually on the order of a few micro- or nanoseconds.
    /// It uses the highest-resolution monotonic time source on each supported platform.
    /// </summary>
    public static TimeSpan Time => TimeSpan.FromSeconds(glfwGetTime());
    
    /// <summary>
    /// Gets the current value of the raw timer, measured in <c>1 / TimerFrequency</c> seconds.
    /// </summary>
    public static long TimerValue => unchecked((long)glfwGetTimerValue());

    /// <summary>
    /// Gets the frequency, in Hz, of the raw timer.
    /// </summary>
    public static long TimerFrequency => unchecked((long)glfwGetTimerFrequency());

    /// <summary>
    /// Initializes the GLFW library. Before most GLFW functions can be used, GLFW must be initialized, and before an application terminates GLFW should be
    /// terminated in order to free any resources allocated during or after initialization.
    /// <para/>
    /// If this function fails, it calls <see cref="Terminate"/> before returning. If it succeeds, you should call <see cref="Terminate"/> before the
    /// application exits.
    /// <para/>
    /// Additional calls to this function after successful initialization but before termination will return <see langword="true"/> immediately.
    /// </summary>
    /// <returns><see langword="true"/> if GLFW was successfully initialized, otherwise <see langword="false"/>.</returns>
    /// <seealso cref="Terminate"/>
    /// <exception cref="PlatformNotSupportedException">Host system is not supported.</exception>
    [NativeMethod("glfwInit")]
    public static bool Init() => glfwInit() != FALSE;

    /// <summary>
    /// Destroys all remaining windows and cursors, restores any modified gamma ramps and frees any other allocated resources. Once this function is called,
    /// you must again call <see cref="Init"/> successfully before you will be able to use most GLFW functions.
    /// <para/>
    /// If GLFW has been successfully initialized, this function should be called before the application exits. If initialization fails, there is no need to
    /// call this function, as it is called by <see cref="Init"/> before it returns failure.
    /// <para/>
    /// This method has no effect if GLFW is not initialized.
    /// </summary>
    /// <exception cref="PlatformNotSupportedException">Host system is not supported.</exception>
    [NativeMethod("glfwTerminate")]
    public static void Terminate()
    {
        foreach (var cb in windowCallbacksMap.Values)
            cb.Dispose();
        windowCallbacksMap.Clear();
        glfwTerminate();
    }

    /// <summary>
    /// Sets hints for the next initialization of GLFW.
    /// <para/>
    /// The values you set hints to are never reset by GLFW, but they only take effect during initialization. Once GLFW has been initialized, any values
    /// you set will be ignored until the library is terminated and initialized again.
    /// <para/>
    /// Some hints are platform specific. These may be set on any platform but they will only affect their specific platform. Other platforms will ignore
    /// them. Setting these hints requires no platform specific headers or functions.
    /// </summary>
    /// <param name="hint">The initialization hint to set.</param>
    /// <param name="value">The new value of the hint.</param>
    /// <exception cref="ArgumentException">When an invalid hint or value is specified.</exception>
    /// <remarks>This method may be called before <see cref="Init"/>.</remarks>
    [NativeMethod("glfwInitHint")]
    public static void InitHint(InitHint hint, bool value) => glfwInitHint(hint, value ? TRUE : FALSE);

    /// <summary>
    /// Retrieves the major, minor and revision numbers of the GLFW library. It is intended for when you are using GLFW as a shared library and want to
    /// ensure that you are using the minimum required version.
    /// </summary>
    /// <returns>The native GLFW version.</returns>
    /// <remarks>This method may be called before <see cref="Init"/>.</remarks>
    [NativeMethod("glfwGetVersion")]
    public static Version GetVersion()
    {
        int major, minor, revision;
        glfwGetVersion(&major, &minor, &revision);
        return new Version(major, minor, revision);
    }

    /// <summary>
    /// Returns the compile-time generated version string of the GLFW library binary. It describes the version, platform, compiler and any platform-specific
    /// compile-time options. 
    /// </summary>
    /// <returns>The ASCII encoded GLFW version string.</returns>
    /// <remarks>This method may be called before <see cref="Init"/>.</remarks>
    [NativeMethod("glfwGetVersionString")]
    public static string? GetVersionString() => Marshal.PtrToStringUTF8(glfwGetVersionString());

    /// <summary>
    /// Returns and clears the error code of the last error that occurred on the calling thread, and a human-readable description of it. If no error has
    /// occurred since the last call, it returns <see cref="ErrorCode.None"/> and <paramref name="description"/> will be set to <see langword="null"/>.
    /// </summary>
    /// <param name="description">A variable where the error description will be stored, or <see langword="null"/> when there is no error.</param>
    /// <returns>The last error code for the calling thread, or <see cref="ErrorCode.None"/> when no error has occurred.</returns>
    /// <remarks>This method may be called before <see cref="Init"/>.</remarks>
    [NativeMethod("glfwGetError")]
    public static ErrorCode GetError(out string? description)
    {
        byte* desc;
        var error = glfwGetError(&desc);
        description = Marshal.PtrToStringUTF8(new IntPtr(desc));
        return error;
    }

    /// <summary>
    /// Returns a <see cref="ReadOnlySpan{T}"/> of handles for all currently connected monitors. The primary monitor is always first in the returned
    /// collection.
    /// </summary>
    /// <returns>A span containing the handles of all connected monitors.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwGetMonitors")]
    public static ReadOnlySpan<Monitor> GetMonitors()
    {
        int count;
        var ptr = glfwGetMonitors(&count);
        return new ReadOnlySpan<Monitor>(ptr, count);
    }

    /// <summary>
    /// Returns the primary monitor. This is usually the monitor where elements like the task bar or global menu bar are located.
    /// </summary>
    /// <returns>The primary monitor, or <see langword="null"/></returns> when no monitors were found or an error occured.
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwGetPrimaryMonitor")]
    public static Monitor? GetPrimaryMonitor()
    {
        var monitor = glfwGetPrimaryMonitor();
        return monitor == default ? null : monitor;
    }

    /// <summary>
    /// Returns the position, in screen coordinates, of the upper-left corner of the specified <paramref name="monitor"/>.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="xPos">Where to store the monitor x-coordinate.</param>
    /// <param name="yPos">Where to store the monitor y-coordinate.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [CLSCompliant(false), NativeMethod("glfwGetMonitorPos")]
    public static void GetMonitorPos(Monitor monitor, int* xPos, int* yPos) => glfwGetMonitorPos(monitor, xPos, yPos);

    /// <summary>
    /// Returns the position, in screen coordinates, of the upper-left corner of the specified <paramref name="monitor"/>.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="xPos">A variable to store the monitor x-coordinate.</param>
    /// <param name="yPos">A variable to store the monitor y-coordinate.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetMonitorPos")]
    public static void GetMonitorPos(Monitor monitor, out int xPos, out int yPos)
    {
        int x, y;
        glfwGetMonitorPos(monitor, &x, &y);
        xPos = x;
        yPos = y;
    }

    /// <summary>
    /// Returns the position, in screen coordinates, of the upper-left corner of the specified <paramref name="monitor"/>.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>A <see cref="Point"/> structure containing the monitor coordinates.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetMonitorPos")]
    public static Point GetMonitorPos(Monitor monitor)
    {
        int x, y;
        glfwGetMonitorPos(monitor, &x, &y);
        return new Point(x, y);
    }

    /// <summary>
    /// Returns the position, in screen coordinates, of the upper-left corner of the work area of the specified monitor along with the work area size in
    /// screen coordinates. The work area is defined as the area of the monitor not occluded by the operating system task bar where present. If no task bar
    /// exists then the work area is the monitor resolution in screen coordinates.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="xPos">Where to store the monitor x-coordinate.</param>
    /// <param name="yPos">Where to store the monitor y-coordinate.</param>
    /// <param name="width">Where to store the monitor width.</param>
    /// <param name="height">Where to store the monitor height.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [CLSCompliant(false), NativeMethod("glfwGetMonitorWorkarea")]
    public static void GetMonitorWorkArea(Monitor monitor, int* xPos, int* yPos, int* width, int* height) => glfwGetMonitorWorkarea(monitor, xPos, yPos, width, height);

    /// <summary>
    /// Returns the position, in screen coordinates, of the upper-left corner of the work area of the specified monitor along with the work area size in
    /// screen coordinates. The work area is defined as the area of the monitor not occluded by the operating system task bar where present. If no task bar
    /// exists then the work area is the monitor resolution in screen coordinates.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="xPos">A variable to store the monitor x-coordinate.</param>
    /// <param name="yPos">A variable to store the monitor y-coordinate.</param>
    /// <param name="width">A variable to store the monitor width.</param>
    /// <param name="height">A variable to store the monitor height.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetMonitorWorkarea")]
    public static void GetMonitorWorkArea(Monitor monitor, out int xPos, out int yPos, out int width, out int height)
    {
        int x, y, w, h;
        glfwGetMonitorWorkarea(monitor, &x, &y, &w, &h);
        xPos = x;
        yPos = y;
        width = w;
        height = h;
    }

    /// <summary>
    /// Returns the position, in screen coordinates, of the upper-left corner of the work area of the specified monitor along with the work area size in
    /// screen coordinates. The work area is defined as the area of the monitor not occluded by the operating system task bar where present. If no task bar
    /// exists then the work area is the monitor resolution in screen coordinates.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>A <see cref="Rectangle"/> structure containing the location and size of the monitor work area.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetMonitorWorkarea")]
    public static Rectangle GetMonitorWorkArea(Monitor monitor)
    {
        int x, y, w, h;
        glfwGetMonitorWorkarea(monitor, &x, &y, &w, &h);
        return new Rectangle(x, y, w, h);
    }

    /// <summary>
    /// Returns the size, in millimetres, of the display area of the specified monitor.
    /// <para/>
    /// Some systems do not provide accurate monitor size information, either because the monitor EDID data is incorrect or because the driver does not
    /// report it accurately.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="widthMM">Where to store the width of the monitor's display area, in millimeter units.</param>
    /// <param name="heightMM">Where to store the height of the monitor's display area, in millimeter units</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [CLSCompliant(false), NativeMethod("glfwGetMonitorPhysicalSize")]
    public static void GetMonitorPhysicalSize(Monitor monitor, int* widthMM, int* heightMM) => glfwGetMonitorPhysicalSize(monitor, widthMM, heightMM);

    /// <summary>
    /// Returns the size, in millimetres, of the display area of the specified monitor.
    /// <para/>
    /// Some systems do not provide accurate monitor size information, either because the monitor EDID data is incorrect or because the driver does not
    /// report it accurately.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="widthMM">A variable to store the width of the monitor's display area, in millimeter units.</param>
    /// <param name="heightMM">A variable to store the height of the monitor's display area, in millimeter units</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwGetMonitorPhysicalSize")]
    public static void GetMonitorPhysicalSize(Monitor monitor, out int widthMM, out int heightMM)
    {
        int width, height;
        glfwGetMonitorPhysicalSize(monitor, &width, &height);
        widthMM = width;
        heightMM = height;
    }

    /// <summary>
    /// Returns the size, in millimetres, of the display area of the specified monitor.
    /// <para/>
    /// Some systems do not provide accurate monitor size information, either because the monitor EDID data is incorrect or because the driver does not
    /// report it accurately.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>A <see cref="Size"/> structure containing the physical size of the monitor, in millimeter units.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwGetMonitorPhysicalSize")]
    public static Size GetMonitorPhysicalSize(Monitor monitor)
    {
        int width, height;
        glfwGetMonitorPhysicalSize(monitor, &width, &height);
        return new Size(width, height);
    }

    /// <summary>
    /// Retrieves the content scale for the specified monitor. The content scale is the ratio between the current DPI and the platform's default DPI. This
    /// is especially Library.Important for text and any UI elements. If the pixel dimensions of your UI scaled by this look appropriate on your machine then it
    /// should appear at a reasonable size on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling
    /// settings being somewhat correct.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="xScale">Where to store the x-axis content scale.</param>
    /// <param name="yScale">Where to store the y-axis content scale</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [CLSCompliant(false), NativeMethod("glfwGetMonitorContentScale")]
    public static void GetMonitorContentScale(Monitor monitor, float* xScale, float* yScale) => glfwGetMonitorContentScale(monitor, xScale, yScale);

    /// <summary>
    /// Retrieves the content scale for the specified monitor. The content scale is the ratio between the current DPI and the platform's default DPI. This
    /// is especially Library.Important for text and any UI elements. If the pixel dimensions of your UI scaled by this look appropriate on your machine then it
    /// should appear at a reasonable size on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling
    /// settings being somewhat correct.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <param name="xScale">A variable to store the x-axis content scale.</param>
    /// <param name="yScale">A variable to store the y-axis content scale</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetMonitorContentScale")]
    public static void GetMonitorContentScale(Monitor monitor, out float xScale, out float yScale)
    {
        float x, y;
        glfwGetMonitorContentScale(monitor, &x, &y);
        xScale = x;
        yScale = y;
    }

    /// <summary>
    /// Retrieves the content scale for the specified monitor. The content scale is the ratio between the current DPI and the platform's default DPI. This
    /// is especially Library.Important for text and any UI elements. If the pixel dimensions of your UI scaled by this look appropriate on your machine then it
    /// should appear at a reasonable size on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling
    /// settings being somewhat correct.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>A <see cref="Vector2"/> structure containing the monitor content scale.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetMonitorContentScale")]
    public static Vector2 GetMonitorContentScale(Monitor? monitor)
    {
        if (monitor is null)
            return Vector2.Zero;
            
        float x, y;
        glfwGetMonitorContentScale(monitor.Value, &x, &y);
        return new Vector2(x, y);
    }

    /// <summary>
    /// Returns a human-readable name of the specified <paramref name="monitor"/>. The name typically reflects the make and model of the monitor and is not
    /// guaranteed to be unique among the connected monitors.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>The name of the monitor, or <see lamgword="null"/> if an error occurred.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwGetMonitorName")]
    public static string? GetMonitorName(Monitor monitor) => Marshal.PtrToStringUTF8(glfwGetMonitorName(monitor));

    /// <summary>
    /// Sets the user-defined pointer of the specified <paramref name="monitor"/>. The current value is retained until the monitor is disconnected.
    /// </summary>
    /// <param name="monitor">The monitor whose pointer to set.</param>
    /// <param name="pointer">The new value.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwSetMonitorUserPointer")]
    public static void SetMonitorUserPointer(Monitor monitor, IntPtr pointer) => glfwSetMonitorUserPointer(monitor, pointer);

    /// <summary>
    /// Returns the current value of the user-defined pointer of the specified monitor.
    /// </summary>
    /// <param name="monitor">The monitor whose pointer to return.</param>
    /// <returns>The pointer value.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwGetMonitorUserPointer")]
    public static IntPtr GetMonitorUserPointer(Monitor monitor) => glfwGetMonitorUserPointer(monitor);

    /// <summary>
    /// Returns a <see cref="ReadOnlySpan{T}"/> of all video modes supported by the specified <paramref name="monitor"/>. The returned collection is sorted
    /// in ascending order, first by color bit depth (the sum of all channel depths), then by resolution area (the product of width and height), then
    /// resolution width and finally by refresh rate.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>A span containing the supported video modes.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetVideoModes")]
    public static ReadOnlySpan<VideoMode> GetVideoModes(Monitor monitor)
    {
        int count;
        var modes = glfwGetVideoModes(monitor, &count);
        return new ReadOnlySpan<VideoMode>(modes, count);
    }

    /// <summary>
    /// Returns the current video mode of the specified <paramref name="monitor"/>. If you have created a full screen window for that monitor, the return
    /// value will depend on whether that window is iconified.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>The current video mode.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetVideoMode")]
    public static VideoMode GetVideoMode(Monitor monitor) => *glfwGetVideoMode(monitor);

    /// <summary>
    /// Generates an appropriately sized gamma ramp from the specified exponent and then calls <see cref="SetGammaRamp"/>
    /// with it. The value must be a finite number greater than zero.
    /// <para/>
    /// The software controlled gamma ramp is applied in addition to the hardware gamma correction, which today is usually an approximation of sRGB gamma.
    /// This means that setting a perfectly linear ramp, or gamma 1.0, will produce the default (usually sRGB-like) behavior.
    /// </summary>
    /// <param name="monitor">The monitor whose gamma ramp to set.</param>
    /// <param name="gamma">The desired exponent.</param>
    /// <exception cref="ArgumentException">An invalid <paramref name="gamma"/> value was specified.</exception>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    /// <remarks>On Wayland systems, gamma handling is a privileged protocol, thus a <see cref="PlatformNotSupportedException"/> exception is always emitted.</remarks>
    [NativeMethod("glfwSetGamma")]
    public static void SetGamma(Monitor monitor, float gamma) => glfwSetGamma(monitor, gamma);

    /// <summary>
    /// Returns the current gamma ramp of the specified <paramref name="monitor"/>.
    /// </summary>
    /// <param name="monitor">The monitor to query.</param>
    /// <returns>The current gamma ramp.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwGetGammaRamp")]
    public static ReadOnlySpan<(int, int, int)> GetGammaRamp(Monitor monitor)
    {
        var ramp = *glfwGetGammaRamp(monitor);
        var rgb = new (int, int, int)[ramp.Size];
        for (var i = 0; i < ramp.Size; i++)
            rgb[i] = (ramp.R[i], ramp.G[i], ramp.B[i]);
        return new ReadOnlySpan<(int, int, int)>(rgb);
    }

    /// <summary>
    /// Sets the current gamma ramp for the specified <paramref name="monitor"/>. The original gamma ramp for that monitor is saved by GLFW the first time
    /// this function is called and is restored by <see cref="Terminate"/>.
    /// <para/>
    /// The software controlled gamma ramp is applied in addition to the hardware gamma correction, which today is usually an approximation of sRGB gamma.
    /// This means that setting a perfectly linear ramp, or gamma 1.0, will produce the default (usually sRGB-like) behavior.
    /// </summary>
    /// <param name="monitor">The monitor whose gamma ramp to set.</param>
    /// <param name="gammaRamp">The gamma ramp to use.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">Host system does not support the query.</exception>
    [NativeMethod("glfwSetGammaRamp")]
    public static void SetGammaRamp(Monitor monitor, ReadOnlySpan<(int, int, int)> gammaRamp)
    {
        // Ugly code ahead. Perhaps just remove and enforce the use of GLFW.SetGamma
        
        GammaRamp temp = default;
        temp.R = (ushort*) Marshal.AllocHGlobal(gammaRamp.Length * sizeof(ushort));
        temp.G = (ushort*) Marshal.AllocHGlobal(gammaRamp.Length * sizeof(ushort));
        temp.B = (ushort*) Marshal.AllocHGlobal(gammaRamp.Length * sizeof(ushort));
        temp.Size = gammaRamp.Length;
        try
        {
            for (var i = 0; i < gammaRamp.Length; i++)
            {
                if ((gammaRamp[i].Item1 is < ushort.MinValue or > ushort.MaxValue) ||
                    (gammaRamp[i].Item2 is < ushort.MinValue or > ushort.MaxValue) ||
                    (gammaRamp[i].Item3 is < ushort.MinValue or > ushort.MaxValue)
                   )
                {
                    var msg = $"Gamma values must be between {ushort.MinValue} and {ushort.MaxValue} inclusive.";
                    throw new ArgumentOutOfRangeException(nameof(gammaRamp), msg);
                }

                temp.R[i] = (ushort) gammaRamp[i].Item1;
                temp.G[i] = (ushort) gammaRamp[i].Item2;
                temp.B[i] = (ushort) gammaRamp[i].Item3;
            }
            
            glfwSetGammaRamp(monitor, &temp);
        }
        finally
        {
            Marshal.FreeHGlobal(new IntPtr(temp.R));
            Marshal.FreeHGlobal(new IntPtr(temp.G));
            Marshal.FreeHGlobal(new IntPtr(temp.B));
        }
    }

    /// <summary>
    /// Resets all window hints to their default values.
    /// </summary>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwDefaultWindowHints")]
    public static void DefaultWindowHints() => glfwDefaultWindowHints();

    /// <summary>
    /// Sets hints for the next call to <see cref="CreateWindow"/>. The hints, once set, retain their values until
    /// changed by a call to this function or <see cref="DefaultWindowHints"/>, or until the library is terminated.
    /// <para/>
    /// This method does not check whether the specified hint values are valid. If you set hints to invalid values this
    /// will instead be reported by the next call to <see cref="CreateWindow"/>
    /// <para/>
    /// Some hints are platform specific. These may be set on any platform but they will only affect their specific
    /// platform. Other platforms will ignore them. Setting these hints requires no platform specific headers or
    /// functions..
    /// </summary>
    /// <param name="hint">The window hint to set.</param>
    /// <param name="value">The new value of the window hint.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="ArgumentException">An invalid <paramref name="hint"/> value is specified.</exception>
    [NativeMethod("glfwWindowHint")]
    public static void WindowHint(WindowHint hint, int value) => glfwWindowHint(hint, value);

    /// <summary>
    /// Sets hints for the next call to <see cref="CreateWindow"/>. The hints, once set, retain their values until
    /// changed by a call to this function or <see cref="DefaultWindowHints"/>, or until the library is terminated.
    /// <para/>
    /// This method does not check whether the specified hint values are valid. If you set hints to invalid values this
    /// will instead be reported by the next call to <see cref="CreateWindow"/>
    /// <para/>
    /// Some hints are platform specific. These may be set on any platform but they will only affect their specific
    /// platform. Other platforms will ignore them. Setting these hints requires no platform specific headers or
    /// functions..
    /// </summary>
    /// <param name="hint">The window hint to set.</param>
    /// <param name="value">The new value of the window hint.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="ArgumentException">An invalid <paramref name="hint"/> value is specified.</exception>
    [NativeMethod("glfwWindowHintString")]
    public static void WindowHint(WindowHint hint, string? value)
    {
        fixed (byte* ptr = &UTF8String.Pin(value))
        {
            glfwWindowHintString(hint, ptr);   
        }
    }

    /// <summary>
    /// Sets hints for the next call to <see cref="CreateWindow"/>. The hints, once set, retain their values until
    /// changed by a call to this function or <see cref="DefaultWindowHints"/>, or until the library is terminated.
    /// <para/>
    /// This method does not check whether the specified hint values are valid. If you set hints to invalid values this
    /// will instead be reported by the next call to <see cref="CreateWindow"/>
    /// <para/>
    /// Some hints are platform specific. These may be set on any platform but they will only affect their specific
    /// platform. Other platforms will ignore them. Setting these hints requires no platform specific headers or
    /// functions..
    /// </summary>
    /// <param name="hint">The window hint to set.</param>
    /// <param name="value">The new value of the window hint.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="ArgumentException">An invalid <paramref name="hint"/> value is specified.</exception>
    [NativeMethod("glfwWindowHint")]
    public static void WindowHint(WindowHint hint, bool value) => glfwWindowHint(hint, value ? TRUE : FALSE);

    /// <summary>
    /// Sets hints for the next call to <see cref="CreateWindow"/>. The hints, once set, retain their values until
    /// changed by a call to this function or <see cref="DefaultWindowHints"/>, or until the library is terminated.
    /// <para/>
    /// This method does not check whether the specified hint values are valid. If you set hints to invalid values this
    /// will instead be reported by the next call to <see cref="CreateWindow"/>
    /// <para/>
    /// Some hints are platform specific. These may be set on any platform but they will only affect their specific
    /// platform. Other platforms will ignore them. Setting these hints requires no platform specific headers or
    /// functions..
    /// </summary>
    /// <param name="hint">The window hint to set.</param>
    /// <param name="value">The new value of the window hint.</param>
    /// <typeparam name="TEnum32">An enum type that <b>must</b> be backed by a 32-bit integer type.</typeparam>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="ArgumentException">An invalid <paramref name="hint"/> value is specified.</exception>
    [NativeMethod("glfwWindowHint")]
    public static void WindowHint<TEnum32>(WindowHint hint, TEnum32 value) where TEnum32 : Enum
    {
        var size = Marshal.SizeOf(typeof(TEnum32).GetEnumUnderlyingType());
        if (size != sizeof(int))
            throw new ArgumentException("Underlying integer type of enum must be 32-bits.", nameof(TEnum32));
        
        glfwWindowHint(hint, Unsafe.As<TEnum32, int>(ref value));
    }

    /// <summary>
    /// Creates a window and its associated OpenGL or OpenGL ES context. Most of the options controlling how the window and its context should be created
    /// are specified with window hints.
    /// <para/>
    /// Successful creation does not change which context is current. Before you can use the newly created context, you need to make it current.
    /// <para/>
    /// To create a full screen window, you need to specify the monitor the window will cover. If no monitor is specified, the window will be windowed mode.
    /// Unless you have a way for the user to choose a specific monitor, it is recommended that you pick the primary monitor. For more information on how to
    /// query connected monitors, see Retrieving monitors.
    /// <para/>
    /// For full screen windows, the specified size becomes the resolution of the window's desired video mode. As long as a full screen window is not
    /// iconified, the supported video mode most closely matching the desired video mode is set for the specified monitor. For more information about full
    /// screen windows, including the creation of so called windowed full screen or borderless full screen windows.
    /// <para/>
    /// The swap interval is not set during window creation and the initial value may vary depending on driver settings and defaults.
    /// </summary>
    /// <param name="width">The desired width, in screen coordinates, of the window. This must be greater than zero.</param>
    /// <param name="height">The desired height, in screen coordinates, of the window. This must be greater than zero.</param>
    /// <param name="title">The initial title of the window.</param>
    /// <param name="monitor">The monitor to use for full screen mode, or <see langword="null"/> for windowed mode.</param>
    /// <param name="share">The window whose context to share resources with, or <see langword="null"/> to not share resources.</param>
    /// <returns>The handle of the created window, or <see langword="null"/> if an error occurred.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="ArgumentException">An invalid hint/value was specified.</exception>
    /// <exception cref="NotSupportedException">The specified API/profile is not unavailable.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    /// <exception cref="FormatException">An invalid format was specified.</exception>
    [Pure, NativeMethod("glfwCreateWindow")]
    public static Window? CreateWindow(int width, int height, string? title, Monitor? monitor = null, Window? share = null)
    {
        fixed (byte* ptr = &UTF8String.Pin(title))
        {
            var window = glfwCreateWindow(width, height, ptr, monitor ?? default, share ?? default);
            return window == default ? null : window;
        }
    }

    /// <summary>
    /// Destroys the specified <paramref name="window"/> and its context. On calling this function, no further callbacks will be called for that window.
    /// <para/>
    /// If the context of the specified window is current on the main thread, it is detached before being destroyed.
    /// </summary>
    /// <param name="window">The window to destroy.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("glfwDestroyWindow")]
    public static void DestroyWindow(Window window)
    {
        if (windowCallbacksMap.TryGetValue(window, out var map))
            map.Dispose();
        windowCallbacksMap.Remove(window);
        glfwDestroyWindow(window);
    }

    /// <summary>
    /// Returns the value of the close flag of the specified <paramref name="window"/>.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The value of the close flag.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwWindowShouldClose")]
    public static bool WindowShouldClose(Window window) => glfwWindowShouldClose(window) != FALSE;

    /// <summary>
    /// Sets the value of the close flag of the specified <paramref name="window"/>. This can be used to override the user's attempt to close the window,
    /// or to signal that it should be closed.
    /// </summary>
    /// <param name="window">TThe window whose flag to change.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    [NativeMethod("glfwSetWindowShouldClose")]
    public static void SetWindowShouldClose(Window window, bool value = true) => glfwSetWindowShouldClose(window, value ? TRUE : FALSE);

    /// <summary>
    /// Sets the window title of the specified <paramref name="window"/>.
    /// </summary>
    /// <param name="window">TThe window whose title to change.</param>
    /// <param name="title">The new window title to set.</param>
    /// <remarks>On macOS, the window title will not be updated until the next time events are processed.</remarks>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("SetWindowTitle")]
    public static void SetWindowTitle(Window window, string? title)
    {
        fixed (byte* ptr = &UTF8String.Pin(title))
        {
            glfwSetWindowTitle(window, ptr);
        }
    }

    /// <summary>
    /// Sets the icon of the specified <paramref name="window"/>. If passed an array of candidate images, those of or closest to the sizes desired by the
    /// system are selected. If no images are specified, the window reverts to its default icon.
    /// <para/>
    /// The pixels are 32-bit, little-endian, non-premultiplied RGBA, i.e. eight bits per channel with the red channel first. They are arranged
    /// canonically as packed sequential rows, starting from the top-left corner.
    /// <para/>
    /// The desired image sizes varies depending on platform and system settings. The selected images will be rescaled as needed. Good sizes include
    /// 16x16, 32x32 and 48x48.
    /// </summary>
    /// <param name="window">The window whose icon to set.</param>
    /// <param name="images">The image(s) to create the icon from.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    /// <remarks>The image data is copied internally, and is safe to free once the method has returned.</remarks>
    [NativeMethod("glfwSetWindowIcon")]
    public static void SetWindowIcon(Window window, params IBitmap[] images)
    {
        var buffers = stackalloc Bitmap[images.Length];
        for (var i = 0; i < images.Length; i++)
        {
            buffers[i].Width =  images[i].Width;
            buffers[i].Height = images[i].Height;
            buffers[i].Pixels = images[i].Pixels;
        }
        glfwSetWindowIcon(window, images.Length, buffers);
    }

    /// <inheritdoc cref="SetWindowIcon(Anvil.GLFW3.Window,Anvil.IBitmap[])"/>
    [NativeMethod("glfwSetWindowIcon")]
    public static void SetWindowIcon(Window window, IBitmap? image)
    {
        if (image is null)
        {
            glfwSetWindowIcon(window, 0, (Bitmap*)0);
            return;
        }

        var buffer = new Bitmap(image.Width, image.Height, image.Pixels);
        glfwSetWindowIcon(window, 1, &buffer);
    }

    /// <summary>
    /// Retrieves the position, in screen coordinates, of the upper-left corner of the content area of the specified window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="xPos">Where to store the x-coordinate of the upper-left corner of the content area.</param>
    /// <param name="yPos">Where to store the y-coordinate of the upper-left corner of the content area.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [CLSCompliant(false), NativeMethod("glfwGetWindowPos")]
    public static void GetWindowPos(Window window, int* xPos, int* yPos) => glfwGetWindowPos(window, xPos, yPos);

    /// <summary>
    /// Retrieves the position, in screen coordinates, of the upper-left corner of the content area of the specified window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="xPos">A variable to store the x-coordinate of the upper-left corner of the content area.</param>
    /// <param name="yPos">A variable to store the y-coordinate of the upper-left corner of the content area.</param>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("glfwGetWindowPos")]
    public static void GetWindowPos(Window window, out int xPos, out int yPos)
    {
        int x, y;
        glfwGetWindowPos(window, &x, &y);
        xPos = x;
        yPos = y;
    }

    /// <summary>
    /// Retrieves the position, in screen coordinates, of the upper-left corner of the content area of the specified window.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>A <see cref="Point"/> structure containing the window coordinates of the upper-left corner of the content area.</returns>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [Pure, NativeMethod("glfwGetWindowPos")]
    public static Point GetWindowPos(Window window)
    {
        int x, y;
        glfwGetWindowPos(window, &x, &y);
        return new Point(x, y);
    }

    /// <summary>
    /// Sets the position, in screen coordinates, of the upper-left corner of the content area of the specified windowed mode <paramref name="window"/>.
    /// If the window is a full screen window, this function does nothing.
    /// <para/>
    /// The window manager may put limits on what positions are allowed. GLFW cannot and should not override these limits.
    /// </summary>
    /// <param name="window">The window to set the position of.</param>
    /// <param name="xPos">The x-coordinate of the upper-left corner of the content area.</param>
    /// <param name="yPos">The y-coordinate of the upper-left corner of the content area.</param>
    /// <remarks>Do not use this function to move an already visible window unless you have very good reasons for doing so, as it will confuse and annoy the user.</remarks>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("glfwSetWindowPos")]
    public static void SetWindowPos(Window window, int xPos, int yPos) => glfwSetWindowPos(window, xPos, yPos);

    /// <summary>
    /// Sets the position, in screen coordinates, of the upper-left corner of the content area of the specified windowed mode <paramref name="window"/>.
    /// If the window is a full screen window, this function does nothing.
    /// <para/>
    /// The window manager may put limits on what positions are allowed. GLFW cannot and should not override these limits.
    /// </summary>
    /// <param name="window">The window to set the position of.</param>
    /// <param name="pos">A <see cref="Point"/> structure containing the coordinates of the upper-left corner of the content area.</param>
    /// <remarks>Do not use this function to move an already visible window unless you have very good reasons for doing so, as it will confuse and annoy the user.</remarks>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("glfwSetWindowPos")]
    public static void SetWindowPos(Window window, Point pos) => glfwSetWindowPos(window, pos.X, pos.Y);

    /// <summary>
    /// Centers the specified window-mode <paramref name="window"/> within the work-area of the monitor it is displayed on. This method has no effect on
    /// full-screen windows.
    /// </summary>
    /// <param name="window">The window to set the position of.</param>
    /// <remarks>Do not use this function to move an already visible window unless you have very good reasons for doing so, as it will confuse and annoy the user.</remarks>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("glfwGetMonitorWorkarea"), NativeMethod("glfwGetWindowSize"), NativeMethod("glfwSetWindowPos")]
    public static void CenterWindow(Window window)
    {
        var monitor = GetWindowMonitor(window);
        if (monitor.HasValue)
            return;

        monitor = GetPrimaryMonitor();
        if (!monitor.HasValue)
            return;

        int mx, my, mw, mh;
        glfwGetMonitorWorkarea(monitor.Value, &mx, &my, &mw, &mh);
            
        int w, h;
        glfwGetWindowSize(window, &w, &h);
        var x = mx + ((mw - w) / 2);
        var y = my + ((mh - h) / 2);
        glfwSetWindowPos(window, x, y);
    }

    /// <summary>
    /// Retrieves the size, in screen coordinates, of the content area of the specified <paramref name="window"/>.
    /// If you wish to retrieve the size of the framebuffer of the window in pixels, see
    /// <see cref="GetFramebufferSize(GLFW3.Window,int*,int*)"/>.
    /// </summary>
    /// <param name="window">The window whose size to retrieve.</param>
    /// <param name="width">Where to store the width, in screen coordinates, of the content area.</param>
    /// <param name="height">Where to store the height, in screen coordinates, of the content area.</param>
    /// <seealso cref="GetFramebufferSize(GLFW3.Window,int*,int*)"/>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [CLSCompliant(false), NativeMethod("glfwGetWindowSize")]
    public static void GetWindowSize(Window window, int* width, int* height) => glfwGetWindowSize(window, width, height);

    /// <summary>
    /// Retrieves the size, in screen coordinates, of the content area of the specified <paramref name="window"/>.
    /// If you wish to retrieve the size of the framebuffer of the window in pixels,
    /// see <see cref="GetFramebufferSize(GLFW3.Window,int*,int*)"/>.
    /// </summary>
    /// <param name="window">The window whose size to retrieve.</param>
    /// <param name="width">A variable to store the width, in screen coordinates, of the content area.</param>
    /// <param name="height">A variable to store the height, in screen coordinates, of the content area.</param>
    /// <seealso cref="GetFramebufferSize(GLFW3.Window,int*,int*)"/>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("glfwGetWindowSize")]
    public static void GetWindowSize(Window window, out int width, out int height)
    {
        int w, h;
        glfwGetWindowSize(window, &w, &h);
        width = w;
        height = h;
    }

    /// <summary>
    /// Retrieves the size, in screen coordinates, of the content area of the specified <paramref name="window"/>. If you wish to retrieve the size of the
    /// framebuffer of the window in pixels, see <see cref="GetFramebufferSize(GLFW3.Window,int*,int*)"/>.
    /// </summary>
    /// <param name="window">The window whose size to retrieve.</param>
    /// <returns>A <see cref="Size"/> structure containing the size of the content area in screen coordinates.</returns>
    /// <seealso cref="GetFramebufferSize(GLFW3.Window,int*,int*)"/>
    /// <exception cref="InvalidOperationException">GLFW has not been initialized.</exception>
    /// <exception cref="PlatformNotSupportedException">A platform error occurred.</exception>
    [NativeMethod("glfwGetWindowSize")]
    public static Size GetWindowSize(Window window)
    {
        int w, h;
        glfwGetWindowSize(window, &w, &h);
        return new Size(w, h);
    }

    /// <summary>
    /// Gets a rectangle describing the position and size of a window's content area.
    /// </summary>
    /// <param name="window">The window whose bounds to retrieve.</param>
    /// <returns>The position and size of the window content area.</returns>
    [Pure, NativeMethod("glfwGetWindowPos"), NativeMethod("glfwGetWindowSize")]
    public static Rectangle GetWindowBounds(Window window)
    {
        int x, y, w, h;
        glfwGetWindowPos(window, &x, &y);
        glfwGetWindowSize(window, &w, &h);
        return new Rectangle(x, y, w, h);
    }

    /// <summary>
    /// Convenience method to set the window position and size simultaneously with a single call.
    /// </summary>
    /// <param name="window">The window whose size and position to set.</param>
    /// <param name="x">The x-coordinate of the upper-left corner of the content area.</param>
    /// <param name="y">The y-coordinate of the upper-left corner of the content area.</param>
    /// <param name="width">The desired width, in screen coordinates, of the window content area.</param>
    /// <param name="height">The desired height, in screen coordinates, of the window content area.</param>
    [NativeMethod("glfwSetWindowPos"), NativeMethod("glfwSetWindowSize")]
    public static void SetWindowBounds(Window window, int x, int y, int width, int height)
    {
        glfwSetWindowPos(window, x, y);
        glfwSetWindowSize(window, width, height);
    }

    /// <summary>
    /// Convenience method to set the window position and size simultaneously with a single call.
    /// </summary>
    /// <param name="window">The window whose size and position to set.</param>
    /// <param name="bounds">The desired position and size of the window, in screen coordinates.</param>
    [NativeMethod("glfwSetWindowPos"), NativeMethod("glfwSetWindowSize")]
    public static void SetWindowBounds(Window window, Rectangle bounds)
    {
        glfwSetWindowPos(window, bounds.X, bounds.Y);
        glfwSetWindowSize(window, bounds.Width, bounds.Height);
    }

    /// <summary>
    /// Convenience method to set the window position and size simultaneously with a single call.
    /// </summary>
    /// <param name="window">The window whose size and position to set.</param>
    /// <param name="location">The desired position of the window, in screen coordinates.</param>
    /// <param name="size">The desired position of the window, in screen coordinates.</param>
    [NativeMethod("glfwSetWindowPos"), NativeMethod("glfwSetWindowSize")]
    public static void SetWindowBounds(Window window, Point location, Size size)
    {
        glfwSetWindowPos(window, location.X, location.Y);
        glfwSetWindowSize(window, size.Width, size.Height);
    }

    /// <summary>
    /// Sets the size limits of the content area of the specified window. If the window is full screen, the size
    /// limits only take effect once it is made windowed. If the window is not resizable, this function does nothing.
    /// <para/>
    /// The size limits are applied immediately to a windowed mode window and may cause it to be resized.
    /// <para/>
    /// The maximum dimensions must be greater than or equal to the minimum dimensions and all must be greater than
    /// or equal to zero.
    /// </summary>
    /// <param name="window">The window to set limits for.</param>
    /// <param name="minWidth">The minimum width, in screen coordinates, of the content area, or <c>-1</c> to not set.</param>
    /// <param name="minHeight">The minimum height, in screen coordinates, of the content area, or <c>-1</c> to not set.</param>
    /// <param name="maxWidth">The maximum width, in screen coordinates, of the content area, or <c>-1</c> to not set.</param>
    /// <param name="maxHeight">The maximum width, in screen coordinates, of the content area, or <c>-1</c> to not set.</param>
    [NativeMethod("glfwSetWindowSizeLimits")]
    public static void SetWindowSizeLimits(Window window, int minWidth, int minHeight, int maxWidth, int maxHeight)
    {
        glfwSetWindowSizeLimits(window, minWidth, minHeight, maxWidth, maxHeight);
    }

    /// <summary>
    /// Sets the size limits of the content area of the specified window. If the window is full screen, the size
    /// limits only take effect once it is made windowed. If the window is not resizable, this function does nothing.
    /// <para/>
    /// The size limits are applied immediately to a windowed mode window and may cause it to be resized.
    /// <para/>
    /// The maximum dimensions must be greater than or equal to the minimum dimensions and all must be greater than
    /// or equal to zero.
    /// </summary>
    /// <param name="window">The window to set limits for.</param>
    /// <param name="min">The minimum size, in screen coordinates, of the content area.</param>
    /// <param name="max">The maximum size, in screen coordinates, of the content area.</param>
    [NativeMethod("glfwSetWindowSizeLimits")]
    public static void SetWindowSizeLimits(Window window, Size min, Size max)
    {
        glfwSetWindowSizeLimits(window, min.Width, min.Height, max.Width, max.Height);
    }

    /// <summary>
    /// Sets the required aspect ratio of the content area of the specified window. If the window is full screen, the
    /// aspect ratio only takes effect once it is made windowed. If the window is not resizable, this function doe
    /// s nothing.
    /// <para/>
    /// The aspect ratio is specified as a numerator and a denominator and both values must be greater than zero.
    /// For example, the common 16:9 aspect ratio is specified as 16 and 9, respectively.
    /// <para/>
    /// If the numerator and denominator is set to <c>-1</c> then the aspect ratio limit is disabled.
    /// <para/>
    /// The aspect ratio is applied immediately to a windowed mode window and may cause it to be resized.
    /// </summary>
    /// <param name="window">The window to set limits for.</param>
    /// <param name="numerator">The numerator of the desired aspect ratio, or <c>-1</c>.</param>
    /// <param name="denominator">The denominator of the desired aspect ratio, or <c>-1</c>.</param>
    [NativeMethod("glfwSetWindowAspectRatio")]
    public static void SetWindowAspectRatio(Window window, int numerator, int denominator) => glfwSetWindowAspectRatio(window, numerator, denominator);

    /// <summary>
    /// Sets the size, in screen coordinates, of the content area of the specified window.
    /// <para/>
    /// For full screen windows, this function updates the resolution of its desired video mode and switches to the
    /// video mode closest to it, without affecting the window's context. As the context is unaffected, the bit depths
    /// of the framebuffer remain unchanged.
    /// <para/>
    /// If you wish to update the refresh rate of the desired video mode in addition to its resolution, see
    /// <see cref="glfwSetWindowMonitor"/>.
    /// <para/>
    /// The window manager may put limits on what sizes are allowed. GLFW cannot and should not override these limits.
    /// </summary>
    /// <param name="window">The window to resize.</param>
    /// <param name="width">The desired width, in screen coordinates, of the window content area.</param>
    /// <param name="height">The desired height, in screen coordinates, of the window content area.</param>
    [NativeMethod("glfwSetWindowSize")]
    public static void SetWindowSize(Window window, int width, int height) => glfwSetWindowSize(window, width, height);

    /// <summary>
    /// Sets the size, in screen coordinates, of the content area of the specified window.
    /// <para/>
    /// For full screen windows, this function updates the resolution of its desired video mode and switches to the
    /// video mode closest to it, without affecting the window's context. As the context is unaffected, the bit depths
    /// of the framebuffer remain unchanged.
    /// <para/>
    /// If you wish to update the refresh rate of the desired video mode in addition to its resolution, see
    /// <see cref="glfwSetWindowMonitor"/>.
    /// <para/>
    /// The window manager may put limits on what sizes are allowed. GLFW cannot and should not override these limits.
    /// </summary>
    /// <param name="window">The window to resize.</param>
    /// <param name="size">The desired size, in screen coordinates, of the window content area.</param>
    [NativeMethod("glfwSetWindowSize")]
    public static void SetWindowSize(Window window, Size size) => glfwSetWindowSize(window, size.Width, size.Height);

    /// <summary>
    /// Retrieves the size, in pixels, of the framebuffer of the specified window. If you wish to retrieve the size of
    /// the window in screen coordinates, see <see cref="GetWindowSize(Anvil.GLFW3.Window,int*,int*)"/>.
    /// </summary>
    /// <param name="window">The window whose framebuffer to query.</param>
    /// <param name="width">Where to store the width, in pixels, of the framebuffer.</param>
    /// <param name="height">Where to store the height, in pixels, of the framebuffer.</param>
    [CLSCompliant(false), NativeMethod("glfwGetFramebufferSize")]
    public static void GetFramebufferSize(Window window, int* width, int* height) => glfwGetFramebufferSize(window, width, height);

    /// <inheritdoc cref="GetFramebufferSize(Anvil.GLFW3.Window,int*,int*)"/>
    [NativeMethod("glfwGetFramebufferSize")]
    public static void GetFramebufferSize(Window window, out int width, out int height)
    {
        int w, h;
        glfwGetFramebufferSize(window, &w, &h);
        width = w;
        height = h;
    }

    /// <summary>
    /// Retrieves the size, in pixels, of the framebuffer of the specified window. If you wish to retrieve the size of
    /// the window in screen coordinates, see <see cref="GetWindowSize(Anvil.GLFW3.Window,int*,int*)"/>.
    /// </summary>
    /// <param name="window">The window whose framebuffer to query.</param>
    /// <returns>The size of the window framebuffer.</returns>
    [Pure, NativeMethod("glfwGetFramebufferSize")]
    public static Size GetFramebufferSize(Window window)
    {
        int w, h;
        glfwGetFramebufferSize(window, &w, &h);
        return new Size(w, h);
    }

    /// <summary>
    /// Retrieves the size, in pixels, of the framebuffer of the specified window. If you wish to retrieve the size of
    /// the window in screen coordinates, see <see cref="GetWindowSize(Anvil.GLFW3.Window,int*,int*)"/>.
    /// </summary>
    /// <param name="window">The window whose framebuffer to query.</param>
    /// <returns>The size of the window framebuffer.</returns>
    /// <remarks>Convenience method to retrieve the values as a float.</remarks>
    [Pure, NativeMethod("glfwGetFramebufferSize")]
    public static Vector2 GetFramebufferSizeF(Window? window)
    {
        if (window is null)
            return Vector2.Zero;
            
        int w, h;
        glfwGetFramebufferSize(window.Value, &w, &h);
        return new Vector2(w, h);
    }

    /// <inheritdoc cref="GetFramebufferSize(Anvil.GLFW3.Window,int*,int*)"/>
    /// <remarks>Convenience method to retrieve the values as a float.</remarks>
    [NativeMethod("glfwGetFramebufferSize")]
    public static void GetFramebufferSizeF(Window? window, out float width, out float height)
    {
        if (window is null)
        {
            width = 0.0f;
            height = 0.0f;
            return;
        }
            
        int w, h;
        glfwGetFramebufferSize(window.Value, &w, &h);
        width = w;
        height = h;
    }
    
    /// <summary>
    /// Retrieves the size, in screen coordinates, of each edge of the frame of the specified window. This size includes
    /// the title bar, if the window has one. The size of the frame may vary depending on the window-related hints
    /// used to create it.
    /// <para/>
    /// Because this function retrieves the size of each window frame edge and not the offset along a particular
    /// coordinate axis, the retrieved values will always be zero or positive.
    /// </summary>
    /// <param name="window">The window whose frame size to query.</param>
    /// <param name="left">Where to store the size, in screen coordinates.</param>
    /// <param name="top">Where to store the size, in screen coordinates.</param>
    /// <param name="right">Where to store the size, in screen coordinates.</param>
    /// <param name="bottom">Where to store the size, in screen coordinates.</param>
    [CLSCompliant(false), NativeMethod("glfwGetWindowFrameSize")]
    public static void GetWindowFrameSize(Window window, int* left, int* top, int* right, int* bottom)
    {
        glfwGetWindowFrameSize(window, left, top, right, bottom);
    }
    
    /// <inheritdoc cref="GetWindowFrameSize(Anvil.GLFW3.Window,int*,int*,int*,int*)"/>
    [NativeMethod("glfwGetWindowFrameSize")]
    public static void GetWindowFrameSize(Window window, out int left, out int top, out int right, out int bottom)
    {
        int l, t, r, b;
        glfwGetWindowFrameSize(window, &l, &t, &r, &b);
        left = l;
        top = t;
        right = r;
        bottom = b;
    }

    /// <summary>
    /// Retrieves the content scale for the specified window. The content scale is the ratio between the current DPI
    /// and the platform's default DPI. This is especially important for text and any UI elements. If the pixel
    /// dimensions of your UI scaled by this look appropriate on your machine then it should appear at a reasonable siz
    /// on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling
    /// settings being somewhat correct.
    /// <para/>
    /// On systems where each monitors can have its own content scale, the window content scale will depend on which
    /// monitor the system considers the window to be on.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="xScale">Where to store the x-axis content scale.</param>
    /// <param name="yScale">Where to store the y-axis content scale.</param>
    [CLSCompliant(false), NativeMethod("glfwGetWindowContentScale")]
    public static void GetWindowContentScale(Window window, float* xScale, float* yScale) => glfwGetWindowContentScale(window, xScale, yScale);

    /// <summary>
    /// Retrieves the content scale for the specified window. The content scale is the ratio between the current DPI
    /// and the platform's default DPI. This is especially important for text and any UI elements. If the pixel
    /// dimensions of your UI scaled by this look appropriate on your machine then it should appear at a reasonable siz
    /// on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling
    /// settings being somewhat correct.
    /// <para/>
    /// On systems where each monitors can have its own content scale, the window content scale will depend on which
    /// monitor the system considers the window to be on.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="xScale">Where to store the x-axis content scale.</param>
    /// <param name="yScale">Where to store the y-axis content scale.</param>
    [NativeMethod("glfwGetWindowContentScale")]
    public static void GetWindowContentScale(Window window, out float xScale, out float yScale)
    {
        float x, y;
        glfwGetWindowContentScale(window, &x, &y);
        xScale = x;
        yScale = y;
    }

    /// <summary>
    /// Retrieves the content scale for the specified window. The content scale is the ratio between the current DPI
    /// and the platform's default DPI. This is especially important for text and any UI elements. If the pixel
    /// dimensions of your UI scaled by this look appropriate on your machine then it should appear at a reasonable siz
    /// on other machines regardless of their DPI and scaling settings. This relies on the system DPI and scaling
    /// settings being somewhat correct.
    /// <para/>
    /// On systems where each monitors can have its own content scale, the window content scale will depend on which
    /// monitor the system considers the window to be on.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The window content scale.</returns>
    [NativeMethod("glfwGetWindowContentScale")]
    public static Vector2 GetWindowContentScale(Window window)
    {
        float x, y;
        glfwGetWindowContentScale(window, &x, &y);
        return new Vector2(x, y);
    }

    /// <summary>
    /// Returns the opacity of the window, including any decorations.
    /// <para/>
    /// The opacity (or alpha) value is a positive finite number between <c>0.0</c> and <c>1.0</c>, where <c>0.0</c>
    /// is fully transparent and <c>1.0</c> is fully opaque. If the system does not support whole window transparency,
    /// this function always returns <c>1.0</c>.
    /// <para/>
    /// The initial opacity value for newly created windows is one.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The opacity value of the specified window.</returns>
    [NativeMethod("glfwGetWindowOpacity")]
    public static float GetWindowOpacity(Window window) => glfwGetWindowOpacity(window);

    /// <summary>
    /// Sets the opacity of the window, including any decorations.
    /// <para/>
    /// The opacity (or alpha) value is a positive finite number between <c>0.0</c> and <c>1.0</c>, where <c>0.0</c>
    /// is fully transparent and <c>1.0</c> is fully opaque.
    /// <para/>
    /// A window created with framebuffer transparency may not use whole window transparency. The results of doing this
    /// are undefined.
    /// </summary>
    /// <param name="window">The window to set the opacity for.</param>
    /// <param name="opacity">The desired opacity of the specified window.</param>
    [NativeMethod("glfwSetWindowOpacity")]
    public static void SetWindowOpacity(Window window, float opacity) => glfwSetWindowOpacity(window, opacity);

    /// <summary>
    /// Iconifies (minimizes) the specified window if it was previously restored. If the window is already iconified,
    /// this function does nothing.
    /// <para/>
    /// If the specified window is a full screen window, the original monitor resolution is restored until the window
    /// is restored.
    /// </summary>
    /// <param name="window">The window to iconify.</param>
    [NativeMethod("glfwIconifyWindow")]
    public static void IconifyWindow(Window window) => glfwIconifyWindow(window);

    /// <summary>
    /// Restores the specified window if it was previously iconified (minimized) or maximized. If the window is already
    /// restored, this function does nothing.
    /// <para/>
    /// If the specified window is a full screen window, the resolution chosen for the window is restored on the
    /// selected monitor.
    /// </summary>
    /// <param name="window">The window to restore.</param>
    [NativeMethod("glfwRestoreWindow")]
    public static void RestoreWindow(Window window) => glfwRestoreWindow(window);

    /// <summary>
    /// Maximizes the specified window if it was previously not maximized. If the window is already maximized, this
    /// function does nothing.
    /// <para/>
    /// If the specified window is a full screen window, this function does nothing.
    /// </summary>
    /// <param name="window">The window to maximize.</param>
    [NativeMethod("glfwMaximizeWindow")]
    public static void MaximizeWindow(Window window) => glfwMaximizeWindow(window);

    /// <summary>
    /// Makes the specified <paramref name="window"/> visible if it was previously hidden. If the window is already
    /// visible or is in full screen mode, this function does nothing.
    /// <para/>
    /// By default, windowed mode windows are focused when shown Set the <see cref="GLFW3.WindowHint.FocusOnShow"/>
    /// window hint to change this behavior for all newly created windows, or change the behavior for an existing window
    /// with <see cref="SetWindowAttrib(Anvil.GLFW3.Window,Anvil.GLFW3.WindowAttrib,bool)"/>.
    /// </summary>
    /// <param name="window">The window to make visible.</param>
    [NativeMethod("glfwShowWindow")]
    public static void ShowWindow(Window window) => glfwShowWindow(window);

    /// <summary>
    /// Hides the specified window if it was previously visible. If the window is already hidden or is in full screen
    /// mode, this function does nothing.
    /// </summary>
    /// <param name="window">The window to hide.</param>
    [NativeMethod("glfwHideWindow")]
    public static void HideWindow(Window window) => glfwHideWindow(window);

    /// <summary>
    /// Brings the specified window to front and sets input focus. The window should already be visible and not
    /// iconified.
    /// <para/>
    /// By default, both windowed and full screen mode windows are focused when initially created. Set the
    /// <see cref="GLFW3.WindowHint.Focused"/> to disable this behavior.
    /// </summary>
    /// <param name="window">The window to give input focus.</param>
    [NativeMethod("glfwFocusWindow")]
    public static void FocusWindow(Window window) => glfwFocusWindow(window);

    /// <summary>
    /// Requests user attention to the specified window. On platforms where this is not supported, attention is
    /// requested to the application as a whole.
    /// <para/>
    /// Once the user has given attention, usually by focusing the window or application, the system will end the
    /// request automatically.
    /// </summary>
    /// <param name="window">The window to request attention to.</param>
    [NativeMethod("glfwRequestWindowAttention")]
    public static void RequestWindowAttention(Window window) => glfwRequestWindowAttention(window);

    /// <summary>
    /// Returns the handle of the monitor that the specified window is in full screen on.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The monitor, or <c>null</c> if an error occurred.</returns>
    [ContractAnnotation("window:null => null"), NativeMethod("glfwGetWindowMonitor")]
    public static Monitor? GetWindowMonitor(Window? window)
    {
        if (window is null)
            return null;
            
        var monitor = glfwGetWindowMonitor(window.Value);
        return monitor == default ? null : monitor;
    }

    /// <summary>
    /// Sets the monitor that the window uses for full screen mode or, if the monitor is <c>null</c>, makes it windowed
    /// mode.
    /// <para/>
    /// When setting a monitor, this function updates the width, height and refresh rate of the desired video mode and
    /// switches to the video mode closest to it. The window position is ignored when setting a monitor.
    /// <para/>
    /// When the monitor is <c>null</c>L, the position, width and height are used to place the window content area. The
    /// refresh rate is ignored when no monitor is specified.
    /// <para/>
    /// If you only wish to update the resolution of a full screen window or the size of a windowed mode window, see
    /// <see cref="SetWindowSize(Anvil.GLFW3.Window,int,int)"/>.
    /// <para/>
    /// When a window transitions from full screen to windowed mode, this function restores any previous window settings
    /// such as whether it is decorated, floating, resizable, has size or aspect ratio limits, etc.
    /// </summary>
    /// <param name="window">The window whose monitor, size or video mode to set.</param>
    /// <param name="monitor">The desired monitor, or <c>null</c> to set windowed mode.</param>
    /// <param name="xPos">The desired x-coordinate of the upper-left corner of the content area.</param>
    /// <param name="yPos">The desired y-coordinate of the upper-left corner of the content area.</param>
    /// <param name="width">The desired width, in screen coordinates, of the content area or video mode.</param>
    /// <param name="height">The desired height, in screen coordinates, of the content area or video mode.</param>
    /// <param name="refreshRate">The desired refresh rate, in Hz, of the video mode, or <c>-1</c> to ignore.</param>
    [NativeMethod("glfwSetWindowMonitor")]
    public static void SetWindowMonitor(Window window, Monitor? monitor, int xPos, int yPos, int width, int height, int refreshRate = DONT_CARE)
    {
        glfwSetWindowMonitor(window, monitor ?? default, xPos, yPos, width, height, refreshRate);
    }

    /// <summary>
    /// Sets the monitor that the window uses for full screen mode or, if the monitor is <c>null</c>, makes it windowed
    /// mode.
    /// <para/>
    /// When setting a monitor, this function updates the width, height and refresh rate of the desired video mode and
    /// switches to the video mode closest to it. The window position is ignored when setting a monitor.
    /// <para/>
    /// When the monitor is <c>null</c>L, the position, width and height are used to place the window content area. The
    /// refresh rate is ignored when no monitor is specified.
    /// <para/>
    /// If you only wish to update the resolution of a full screen window or the size of a windowed mode window, see
    /// <see cref="SetWindowSize(Anvil.GLFW3.Window,int,int)"/>.
    /// <para/>
    /// When a window transitions from full screen to windowed mode, this function restores any previous window settings
    /// such as whether it is decorated, floating, resizable, has size or aspect ratio limits, etc.
    /// </summary>
    /// <param name="window">The window whose monitor, size or video mode to set.</param>
    /// <param name="monitor">The desired monitor, or <c>null</c> to set windowed mode.</param>
    /// <param name="location">The desired location of the upper-left corner of the content area.</param>
    /// <param name="size">The desired size, in screen coordinates, of the content area or video mode.</param>
    /// <param name="refreshRate">The desired refresh rate, in Hz, of the video mode, or <c>-1</c> to ignore.</param>
    [NativeMethod("glfwSetWindowMonitor")]
    public static void SetWindowMonitor(Window window, Monitor? monitor, Point location, Size size, int refreshRate = DONT_CARE)
    {
        glfwSetWindowMonitor(window, monitor ?? default, location.X, location.Y, size.Width, size.Height, refreshRate);
    }

    /// <summary>
    /// Sets the monitor that the window uses for full screen mode or, if the monitor is <c>null</c>, makes it windowed
    /// mode.
    /// <para/>
    /// When setting a monitor, this function updates the width, height and refresh rate of the desired video mode and
    /// switches to the video mode closest to it. The window position is ignored when setting a monitor.
    /// <para/>
    /// When the monitor is <c>null</c>L, the position, width and height are used to place the window content area. The
    /// refresh rate is ignored when no monitor is specified.
    /// <para/>
    /// If you only wish to update the resolution of a full screen window or the size of a windowed mode window, see
    /// <see cref="SetWindowSize(Anvil.GLFW3.Window,int,int)"/>.
    /// <para/>
    /// When a window transitions from full screen to windowed mode, this function restores any previous window settings
    /// such as whether it is decorated, floating, resizable, has size or aspect ratio limits, etc.
    /// </summary>
    /// <param name="window">The window whose monitor, size or video mode to set.</param>
    /// <param name="monitor">The desired monitor, or <c>null</c> to set windowed mode.</param>
    /// <param name="bounds">The desired location and size, in screen coordinates, of the content area or video mode.</param>
    /// <param name="refreshRate">The desired refresh rate, in Hz, of the video mode, or <c>-1</c> to ignore.</param>
    [NativeMethod("glfwSetWindowMonitor")]
    public static void SetWindowMonitor(Window window, Monitor? monitor, Rectangle bounds, int refreshRate = DONT_CARE)
    {
        glfwSetWindowMonitor(window, monitor ?? default, bounds.X, bounds.Y, bounds.Width, bounds.Height, refreshRate);
    }

    /// <summary>
    /// Returns the value of an attribute of the specified window or its OpenGL or OpenGL ES context.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="attrib">The window attribute whose value to return.</param>
    /// <returns>The value of the attribute, or zero if an error occurred.</returns>
    [NativeMethod("glfwGetWindowAttrib")]
    public static int GetWindowAttrib(Window window, WindowAttrib attrib) => glfwGetWindowAttrib(window, attrib);

    /// <summary>
    /// Returns the value of an attribute of the specified window or its OpenGL or OpenGL ES context.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="attrib">The window attribute whose value to return.</param>
    /// <returns>The value of the attribute.</returns>
    [NativeMethod("glfwGetWindowAttrib")]
    public static bool GetWindowAttribBool(Window window, WindowAttrib attrib) => glfwGetWindowAttrib(window, attrib) != FALSE;

    /// <summary>
    /// Returns the value of an attribute of the specified window or its OpenGL or OpenGL ES context.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="attrib">The window attribute whose value to return.</param>
    /// <typeparam name="TEnum32">An enum type that <b>must</b> be backed by a 32-bit integer type.</typeparam>
    /// <returns>The value of the attribute, or  a "zero" value if an error occurred.</returns>
    [NativeMethod("glfwGetWindowAttrib")]
    public static TEnum32 GetWindowAttrib<TEnum32>(Window window, WindowAttrib attrib) where TEnum32 : Enum
    {
        var value = glfwGetWindowAttrib(window, attrib);
        return Unsafe.As<int, TEnum32>(ref value);
    }

    /// <summary>
    /// Sets the value of an attribute of the specified window.
    /// <para/>
    /// Supported values include <see cref="WindowAttrib.AutoIconify"/>, <see cref="WindowAttrib.Decorated"/>, <see cref="WindowAttrib.Floating"/>. and
    /// <see cref="WindowAttrib.FocusOnShow"/>.
    /// </summary>
    /// <param name="window">The window to set the attribute for.</param>
    /// <param name="attrib">A supported window attribute.</param>
    /// <param name="value">The attribute value to set.</param>
    [NativeMethod("glfwSetWindowAttrib")]
    public static void SetWindowAttrib(Window window, WindowAttrib attrib, bool value) => glfwSetWindowAttrib(window, attrib, value ? TRUE : FALSE);

    /// <summary>
    /// Sets the value of an attribute of the specified window.
    /// <para/>
    /// Supported values include <see cref="WindowAttrib.AutoIconify"/>, <see cref="WindowAttrib.Decorated"/>, <see cref="WindowAttrib.Floating"/>. and
    /// <see cref="WindowAttrib.FocusOnShow"/>.
    /// </summary>
    /// <param name="window">The window to set the attribute for.</param>
    /// <param name="attrib">A supported window attribute.</param>
    /// <param name="value">The attribute value to set.</param>
    [NativeMethod("glfwSetWindowAttrib")]
    public static void SetWindowAttrib(Window window, WindowAttrib attrib, int value) => glfwSetWindowAttrib(window, attrib, value);

    /// <summary>
    /// Sets the value of an attribute of the specified window.
    /// <para/>
    /// Supported values include <see cref="WindowAttrib.AutoIconify"/>, <see cref="WindowAttrib.Decorated"/>, <see cref="WindowAttrib.Floating"/>. and
    /// <see cref="WindowAttrib.FocusOnShow"/>.
    /// </summary>
    /// <param name="window">The window to set the attribute for.</param>
    /// <param name="attrib">A supported window attribute.</param>
    /// <param name="value">The attribute value to set.</param>
    /// <typeparam name="TEnum32">An enum type that <b>must</b> be backed by a 32-bit integer type.</typeparam>
    [NativeMethod("glfwSetWindowAttrib")]
    public static void SetWindowAttrib<TEnum32>(Window window, WindowAttrib attrib, TEnum32 value) where TEnum32 : Enum
    {
        var size = Marshal.SizeOf(typeof(TEnum32).GetEnumUnderlyingType());
        if (size != sizeof(int))
            throw new ArgumentException("Underlying integer type of enum must be 32-bits.", nameof(TEnum32));
        glfwSetWindowAttrib(window, attrib, Unsafe.As<TEnum32, int>(ref value));
    }
    
    /// <summary>
    /// Sets the user-defined pointer of the specified window.
    /// </summary>
    /// <param name="window">The window whose pointer to set.</param>
    /// <param name="pointer">The new value.</param>
    /// <remarks>GLFW never does anything with this value, even when the window is destroyed.</remarks>
    [NativeMethod("glfwSetWindowUserPointer")]
	public static void SetWindowUserPointer(Window window, IntPtr pointer) => glfwSetWindowUserPointer(window, pointer);

    /// <summary>
    /// Returns the current value of the user-defined pointer of the specified window.
    /// </summary>
    /// <param name="window">The window whose pointer to return.</param>
    /// <returns>The current value of the user pointer.</returns>
    [NativeMethod("glfwGetWindowUserPointer")]
	public static IntPtr GetWindowUserPointer(Window window) => glfwGetWindowUserPointer(window);

    /// <summary>
    /// Processes only those events that are already in the event queue and then returns immediately. Processing events
    /// will cause the window and input callbacks associated with those events to be called.
    /// <para/>
    /// On some platforms, a window move, resize or menu operation will cause event processing to block. This is due to
    /// how event processing is designed on those platforms. You can use the window refresh callback to redraw the
    /// contents of your window when necessary during such operations.
    /// <para/>
    /// Do not assume that callbacks you set will only be called in response to event processing functions like this
    /// one. While it is necessary to poll for events, window systems that require GLFW to register callbacks of its ow
    /// can pass events to GLFW in response to many window system function calls. GLFW will pass those events on to the
    /// application callbacks before returning.
    /// <para/>
    /// Event processing is not required for joystick input to work.
    /// </summary>
    [NativeMethod("glfwPollEvents")]
	public static void PollEvents() => glfwPollEvents();

    /// <summary>
    /// Puts the calling thread to sleep until at least one event is available in the event queue. Once one or more
    /// events are available, it behaves exactly like <see cref="PollEvents"/>, i.e. the events in the queue are
    /// processed and the function then returns immediately. Processing events will cause the window and input callbacks
    /// associated with those events to be called.
    /// <para/>
    /// Since not all events are associated with callbacks, this function may return without a callback having been
    /// called even if you are monitoring all callbacks.
    /// <para/>
    /// On some platforms, a window move, resize or menu operation will cause event processing to block. This is due to
    /// how event processing is designed on those platforms. You can use the window refresh callback to redraw the
    /// contents of your window when necessary during such operations.
    /// <para/>
    /// Do not assume that callbacks you set will only be called in response to event processing functions like this
    /// one. While it is necessary to poll for events, window systems that require GLFW to register callbacks of its ow
    /// can pass events to GLFW in response to many window system function calls. GLFW will pass those events on to the
    /// application callbacks before returning.
    /// <para/>
    /// Event processing is not required for joystick input to work.
    /// </summary>
    [NativeMethod("glfwWaitEvents")]
	public static void WaitEvents() => glfwWaitEvents();

    /// <summary>
    /// Puts the calling thread to sleep until at least one event is available in the event queue, or until the
    /// specified timeout is reached. If one or more events are available, it behaves exactly like
    /// <see cref="PollEvents"/>s, i.e. the events in the queue are processed and the function then returns immediately.
    /// Processing events will cause the window and input callbacks associated with those events to be called.
    /// <para/>
    /// The timeout value must be a positive finite number.
    /// <para/>
    /// Since not all events are associated with callbacks, this function may return without a callback having been
    /// called even if you are monitoring all callbacks.
    /// <para/>
    /// On some platforms, a window move, resize or menu operation will cause event processing to block. This is due to
    /// how event processing is designed on those platforms. You can use the window refresh callback to redraw the
    /// contents of your window when necessary during such operations.
    /// <para/>
    /// Do not assume that callbacks you set will only be called in response to event processing functions like this
    /// one. While it is necessary to poll for events, window systems that require GLFW to register callbacks of its ow
    /// can pass events to GLFW in response to many window system function calls. GLFW will pass those events on to the
    /// application callbacks before returning.
    /// <para/>
    /// Event processing is not required for joystick input to work.
    /// </summary>
    /// <param name="timeout">The maximum amount of time, in seconds, to wait.</param>
    [NativeMethod("glfwWaitEventsTimeout")]
	public static void WaitEventsTimeout(double timeout) => glfwWaitEventsTimeout(timeout);

    /// <summary>
    /// Posts an empty event from the current thread to the event queue, causing <see cref="WaitEvents"/> or
    /// <see cref="WaitEventsTimeout"/> to return.
    /// </summary>
    [NativeMethod("glfwPostEmptyEvent")]
	public static void PostEmptyEvent() => glfwPostEmptyEvent();

    /// <summary>
    /// Swaps the front and back buffers of the specified window when rendering with OpenGL or OpenGL ES. If the swap
    /// interval is greater than zero, the GPU driver waits the specified number of screen updates before swapping the
    /// buffers.
    /// <para/>
    /// The specified window must have an OpenGL or OpenGL ES context. Specifying a window without a context will
    /// generate a <see cref="ErrorCode.NoWindowContext"/> error.
    /// <para/>
    /// This function does not apply to Vulkan. 
    /// </summary>
    /// <param name="window">The window whose buffers to swap.</param>
    [NativeMethod("glfwSwapBuffers")]
    public static void SwapBuffers(Window window) => glfwSwapBuffers(window);

    /// <summary>
    /// Sets the swap interval for the current OpenGL or OpenGL ES context, i.e. the number of screen updates to wait
    /// from the time <see cref="SwapBuffers"/> was called before swapping the buffers and returning. This is sometimes
    /// called vertical synchronization, vertical retrace synchronization or just <i>vsync</i>.
    /// <para/>
    /// A context that supports either of the <c>WGL_EXT_swap_control_tear</c> and <c>GLX_EXT_swap_control_tear</c>
    /// extensions also accepts negative swap intervals, which allows the driver to swap immediately even if a frame
    /// arrives a little bit late. You can check for these extensions with <see cref="ExtensionSupported"/>.
    /// <para/>
    /// A context must be current on the calling thread. Calling this function without a current context will cause a
    /// <see cref="ErrorCode.NoCurrentContext"/> error.
    /// <para/>
    /// This function does not apply to Vulkan.
    /// </summary>
    /// <param name="interval">
    /// The minimum number of screen updates to wait for until the buffers are swapped by <see cref="SwapBuffers"/>.
    /// .</param>
    [NativeMethod("glfwSwapInterval")]
    public static void SwapInterval(int interval) => glfwSwapInterval(interval);
    
    /// <summary>
    /// Returns whether the specified API extension is supported by the current OpenGL or OpenGL ES context. It searches
    /// both for client API extension and context creation API extensions
    /// <para/>
    /// A context must be current on the calling thread. Calling this function without a current context will cause a
    /// <see cref="ErrorCode.NoCurrentContext"/> error.
    /// <para/>
    /// As this functions retrieves and searches one or more extension strings each call, it is recommended that you
    /// cache its results if it is going to be used frequently. The extension strings will not change during the
    /// lifetime of a context, so there is no danger in doing this.
    /// <para/>
    /// This function does not apply to Vulkan.
    /// </summary>
    /// <param name="extension">The ASCII encoded name of the extension.</param>
    /// <returns><c>true</c> if the extension is supported, otherwise <c>false</c>.</returns>
    [Pure, ContractAnnotation("extension:null => false"), NativeMethod("glfwExtensionSupported")]
    public static bool ExtensionSupported(string? extension)
    {
        if (string.IsNullOrEmpty(extension))
            return false;

        var size = Encoding.ASCII.GetByteCount(extension);
        Span<byte> buffer = stackalloc byte[size + 1];
        Encoding.ASCII.GetBytes(extension, buffer);
        
        fixed (byte* ptr = &buffer[0])
        {
            return glfwExtensionSupported(ptr) != FALSE;   
        }
    }

    /// <summary>
    /// Returns the address of the specified OpenGL or OpenGL ES core or extension function, if it is supported by the
    /// current context.
    /// <para/>
    /// A context must be current on the calling thread. Calling this function without a current context will cause a
    /// <see cref="ErrorCode.NoCurrentContext"/> error.
    /// <para/>
    /// This function does not apply to Vulkan.
    /// </summary>
    /// <param name="procName">The ASCII encoded name of the function.</param>
    /// <returns></returns>
    [Pure, NativeMethod("glfwGetProcAddress")]
    public static IntPtr GetProcAddress(string? procName)
    {
        if (string.IsNullOrEmpty(procName))
            return IntPtr.Zero;
        
        var size = Encoding.ASCII.GetByteCount(procName);
        Span<byte> buffer = stackalloc byte[size + 1];
        Encoding.ASCII.GetBytes(procName, buffer);
        
        fixed (byte* ptr = &buffer[0])
        {
            return glfwGetProcAddress(ptr);   
        }
    }

    /// <summary>
    /// Sets the current GLFW time, in seconds. The value must be a positive finite number less than or equal to
    /// <c>18446744073.0</c>, which is approximately 584.5 years.
    /// </summary>
    /// <param name="value">The new value, in seconds.</param>
    [NativeMethod("glfwSetTime")]
    public static void SetTime(double value) => glfwSetTime(value);
    
    /// <summary>
    /// Sets the current GLFW time.
    /// </summary>
    /// <param name="value">The new value, which must not be negative.</param>
    [NativeMethod("glfwSetTime")]
    public static void SetTime(TimeSpan value) => glfwSetTime(value.TotalSeconds);

    /// <summary>
    /// Returns the current GLFW time, in seconds. Unless the time has been set using <see cref="SetTime(double)"/> it
    /// measures time elapsed since GLFW was initialized.
    /// <para/>
    /// The resolution of the timer is system dependent, but is usually on the order of a few micro- or nanoseconds.
    /// It uses the highest-resolution monotonic time source on each supported platform.
    /// </summary>
    /// <returns>The current time, in seconds, or zero if an error occurred.</returns>
    [Pure, NativeMethod("glfwGetTime")]
    public static double GetTime() => glfwGetTime();
    
    /// <summary>
    /// Returns the current value of the raw timer, measured in 1 / frequency seconds. To get the frequency, call
    /// <see cref="GetTimerFrequency"/>.
    /// </summary>
    /// <returns>The value of the timer, or zero if an error occurred.</returns>
    [Pure, CLSCompliant(false), NativeMethod("glfwGetTimerValue")]
    public static ulong GetTimerValue() => glfwGetTimerValue();
    
    /// <summary>
    /// Returns the frequency, in Hz, of the raw timer.
    /// </summary>
    /// <returns>The frequency of the timer, in Hz, or zero if an error occurred.</returns>
    [Pure, CLSCompliant(false), NativeMethod("glfwGetTimerFrequency")]
    public static ulong GetTimerFrequency() => glfwGetTimerFrequency();

    /// <summary>
    /// Makes the OpenGL or OpenGL ES context of the specified window current on the calling thread. A context must
    /// only be made current on a single thread at a time and each thread can have only a single current context at a
    /// time.
    /// <para/>
    /// When moving a context between threads, you must make it non-current on the old thread before making it current
    /// on the new one.
    /// <para/>
    /// By default, making a context non-current implicitly forces a pipeline flush. On machines that support
    /// <c>GL_KHR_context_flush_control</c>, you can control whether a context performs this flush by setting the
    /// <see cref="Anvil.GLFW3.WindowHint.ContextReleaseBehavior"/> hint.
    /// <para/>
    /// The specified window must have an OpenGL or OpenGL ES context. Specifying a window without a context will
    /// generate a <see cref="ErrorCode.NoWindowContext"/> error.
    /// </summary>
    /// <param name="window">The window whose context to make current, or <c>null</c> to detach the current context.</param>
    [NativeMethod("glfwMakeContextCurrent")]
    public static void MakeContextCurrent(Window? window) => glfwMakeContextCurrent(window ?? default);

    /// <summary>
    /// Returns the window whose OpenGL or OpenGL ES context is current on the calling thread.
    /// </summary>
    /// <returns>The window whose context is current, or <c>null</c> if no window's context is current.</returns>
    [Pure, NativeMethod("glfwGetCurrentContext")]
    public static Window? GetCurrentContext()
    {
        var ctx = glfwGetCurrentContext();
        return ctx == Window.None ? null : ctx;
    }

    /// <summary>
    /// Returns the value of an input option for the specified window. 
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="mode">The mode to query.</param>
    /// <returns>The value fo the specified input mode as an integer.</returns>
    [Pure, NativeMethod("glfwGetInputMode")]
	public static int GetInputMode(Window window, InputMode mode) => glfwGetInputMode(window, mode);

    /// <summary>
    /// Returns the value of an input option for the specified window. 
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="mode">The mode to query.</param>
    /// <returns>The value fo the specified input mode as a boolean.</returns>
    [Pure, NativeMethod("glfwGetInputMode")]
	public static bool GetInputModeBool(Window window, InputMode mode) => glfwGetInputMode(window, mode) != FALSE;

    /// <summary>
    /// Returns the value of an input option for the specified window. 
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <param name="mode">The mode to query.</param>
    /// <typeparam name="TEnum">An enumeration type that must have a 32-bit underlying integer type.</typeparam>
    /// <returns>The value fo the specified input mode as an integer.</returns>
    [Pure, NativeMethod("glfwGetInputMode")]
    public static TEnum GetInputMode<TEnum>(Window window, InputMode mode) where TEnum : Enum
    {
        var value = glfwGetInputMode(window, mode);
        return Unsafe.As<int, TEnum>(ref value);
    }

    /// <summary>
    /// Sets an input mode option for the specified window.
    /// </summary>
    /// <param name="window">The window whose input mode to set.</param>
    /// <param name="mode">The mode to set.</param>
    /// <param name="value">The new value of the specified input mode.</param>
    [NativeMethod("glfwSetInputMode")]
	public static void SetInputMode(Window window, InputMode mode, int value) => glfwSetInputMode(window, mode, value);

    /// <summary>
    /// Sets an input mode option for the specified window.
    /// </summary>
    /// <param name="window">The window whose input mode to set.</param>
    /// <param name="mode">The mode to set.</param>
    /// <param name="value">The new value of the specified input mode.</param>
    [NativeMethod("glfwSetInputMode")]
	public static void SetInputMode(Window window, InputMode mode, bool value) => glfwSetInputMode(window, mode, value ? TRUE : FALSE);

    /// <summary>
    /// Sets an input mode option for the specified window.
    /// </summary>
    /// <param name="window">The window whose input mode to set.</param>
    /// <param name="mode">The mode to set.</param>
    /// <param name="value">The new value of the specified input mode.</param>
    /// <typeparam name="TEnum">An enumeration type that must have a 32-bit underlying integer type.</typeparam>
    [NativeMethod("glfwSetInputMode")]
    public static void SetInputMode<TEnum>(Window window, InputMode mode, TEnum value) where TEnum : Enum
    {
        glfwSetInputMode(window, mode, Unsafe.As<TEnum, int>(ref value));
    }

    /// <summary>
    /// Returns whether raw mouse motion is supported on the current system. This status does not change after GLFW ha
    /// been initialized so you only need to check this once. If you attempt to enable raw motion on a system that does
    /// not support it, <see cref="ErrorCode.PlatformError"/> will be emitted.
    /// <para/>
    /// Raw mouse motion is closer to the actual motion of the mouse across a surface. It is not affected by the scaling
    /// and acceleration applied to the motion of the desktop cursor. That processing is suitable for a cursor while raw
    /// motion is better for controlling for example a 3D camera. Because of this, raw mouse motion is only provided
    /// when the cursor is disabled.
    /// </summary>
    /// <returns><c>true</c> if raw mouse motion is supported on the current machine, otherwise <c>false</c>.</returns>
    [Pure, NativeMethod("glfwRawMouseMotionSupported")]
	public static bool RawMouseMotionSupported() => glfwRawMouseMotionSupported() != FALSE;

    /// <summary>
    /// Returns the name of the specified printable key, encoded as UTF-8. This is typically the character that key
    /// would produce without any modifier keys, intended for displaying key bindings to the user. For dead keys, it is
    /// typically the diacritic it would add to a character.
    /// <para/>
    /// <b>Do not use this function for text input!</b> You will break text input for many languages even if it happens
    /// to work for yours.
    /// </summary>
    /// <param name="key">The key to query, or <see cref="Key.Unknown"/> to use the scancode.</param>
    /// <param name="scancode">The scancode of the key to query.</param>
    /// <returns>The layout-specific name of the key, or <c>null</c>.</returns>
    [Pure, NativeMethod("glfwGetKeyName")]
    public static string? GetKeyName(Key key, int scancode) => Marshal.PtrToStringUTF8(glfwGetKeyName(key, scancode));

    /// <summary>
    /// Returns the name of the specified printable key, encoded as UTF-8. This is typically the character that key
    /// would produce without any modifier keys, intended for displaying key bindings to the user. For dead keys, it is
    /// typically the diacritic it would add to a character.
    /// <para/>
    /// <b>Do not use this function for text input!</b> You will break text input for many languages even if it happens
    /// to work for yours.
    /// </summary>
    /// <param name="key">The key to query.</param>
    /// <returns>The layout-specific name of the key, or <c>null</c>.</returns>
    [Pure, NativeMethod("glfwGetKeyName")]
    public static string? GetKeyName(Key key) => Marshal.PtrToStringUTF8(glfwGetKeyName(key, -1));

    /// <summary>
    /// Returns the name of the specified printable key, encoded as UTF-8. This is typically the character that key
    /// would produce without any modifier keys, intended for displaying key bindings to the user. For dead keys, it is
    /// typically the diacritic it would add to a character.
    /// <para/>
    /// <b>Do not use this function for text input!</b> You will break text input for many languages even if it happens
    /// to work for yours.
    /// </summary>
    /// <param name="scancode">The scancode of the key to query.</param>
    /// <returns>The layout-specific name of the key, or <c>null</c>.</returns>
    [Pure, NativeMethod("glfwGetKeyName")]
    public static string? GetKeyName(int scancode) => Marshal.PtrToStringUTF8(glfwGetKeyName(Key.Unknown, scancode));

    /// <summary>
    /// Returns the platform-specific scancode of the specified key.
    /// <para/>
    /// If the key is not valid, returns <c>-1</c>.
    /// </summary>
    /// <param name="key">The key to query.</param>
    /// <returns>The platform-specific scancode for the key, or <c>-1</c> if an error occurred.</returns>
    [Pure, NativeMethod("glfwGetKeyScancode")]
	public static int GetKeyScancode(Key key) => glfwGetKeyScancode(key);

    /// <summary>
    /// Returns the last state reported for the specified key to the specified window. The returned state is one as a
    /// boolean if it is pressed or not. The higher-level action repeat querying is only reported to the key callback.
    /// <para/>
    /// If the <see cref="InputMode.StickyKeys"/> input mode is enabled, this function returns <c>true</c> the first
    /// time you call it for a key that was pressed, even if that key has already been released.
    /// <para/>
    /// The key functions deal with physical keys, with key tokens named after their use on the standard US keyboard
    /// layout. If you want to input text, use the Unicode character callback instead.
    /// </summary>
    /// <param name="window">The desired window.</param>
    /// <param name="key">The desired keyboard key.</param>
    /// <returns><c>true</c> if key is pressed, otherwise <c>false</c>.</returns>
    [Pure, NativeMethod("glfwGetKey")]
    public static bool GetKey(Window window, Key key) => key != Key.Unknown && glfwGetKey(window, key) != 0;

    /// <summary>
    /// Returns the last state reported for the specified mouse button to the specified window.
    /// <para/>
    /// If the <see cref="InputMode.StickyMouseButtons"/> input mode is enabled, this function returns <c>true</c> the
    /// first time you call it for a button that was pressed, even if that button has already been released.
    ///  </summary>
    /// <param name="window">The desired window.</param>
    /// <param name="button">The desired mouse button.</param>
    /// <returns><c>true</c> if button is pressed, otherwise <c>false</c>.</returns>
    [Pure, NativeMethod("glfwGetMouseButton")]
	public static bool GetMouseButton(Window window, MouseButton button) => glfwGetMouseButton(window, button) != 0;
    
    /// <summary>
    /// Returns the position of the cursor, in screen coordinates, relative to the upper-left corner of the content
    /// area of the specified window.
    /// <para/>
    /// If the cursor is disabled then the cursor position is unbounded and limited only by the minimum and maximum
    /// values of a <see cref="double"/>
    /// <para/>
    /// The coordinate can be converted to their integer equivalents with the floor function. Casting directly to an
    /// integer type works for positive coordinates, but fails for negative ones..
    /// </summary>
    /// <param name="window">The desired window.</param>
    /// <param name="xPos">Where to store the cursor x-coordinate.</param>
    /// <param name="yPos">Where to store the cursor y-coordinate.</param>
    [CLSCompliant(false), NativeMethod("glfwGetCursorPos")]
	public static void GetCursorPos(Window window, double* xPos, double* yPos) => glfwGetCursorPos(window, xPos, yPos);

    /// <inheritdoc cref="GetCursorPos(Anvil.GLFW3.Window,double*,double*)"/>
    [NativeMethod("glfwGetCursorPos")]
	public static void GetCursorPos(Window window, out double xPos, out double yPos)
    {
        double x, y;
        glfwGetCursorPos(window, &x, &y);
        xPos = x;
        yPos = y;
    }
    
    /// <summary>
    /// Returns the position of the cursor, in screen coordinates, relative to the upper-left corner of the content
    /// area of the specified window.
    /// <para/>
    /// If the cursor is disabled then the cursor position is unbounded and limited only by the minimum and maximum
    /// values of a <see cref="double"/>
    /// <para/>
    /// The coordinate can be converted to their integer equivalents with the floor function. Casting directly to an
    /// integer type works for positive coordinates, but fails for negative ones..
    /// </summary>
    /// <param name="window">The desired window.</param>
    /// <returns>The cursor position as a <see cref="Vector2"/>.</returns>
    [Pure, NativeMethod("glfwGetCursorPos")]
	public static Vector2 GetCursorPos(Window window)
    {
        double x, y;
        glfwGetCursorPos(window, &x, &y);
        return new Vector2((float)x, (float)y);
    }

    /// <summary>
    /// Sets the position, in screen coordinates, of the cursor relative to the upper-left corner of the content area
    /// of the specified window. The window must have input focus. If the window does not have input focus when this
    /// function is called, it fails silently.
    /// </summary>
    /// <param name="window">The desired window.</param>
    /// <param name="xPos">The desired x-coordinate, relative to the left edge of the content area.</param>
    /// <param name="yPos">The desired y-coordinate, relative to the top edge of the content area.</param>
    [NativeMethod("glfwSetCursorPos")]
	public static void SetCursorPos(Window window, double xPos, double yPos) => glfwSetCursorPos(window, xPos, yPos);

    /// <summary>
    /// Sets the position, in screen coordinates, of the cursor relative to the upper-left corner of the content area
    /// of the specified window. The window must have input focus. If the window does not have input focus when this
    /// function is called, it fails silently.
    /// </summary>
    /// <param name="window">The desired window.</param>
    /// <param name="pos">The desired cursor position, relative to the left edge of the content area.</param>
    [NativeMethod("glfwSetCursorPos")]
	public static void SetCursorPos(Window window, Vector2 pos) => glfwSetCursorPos(window, pos.X, pos.Y);

    /// <summary>
    /// Creates a new custom cursor image that can be set for a window with <see cref="SetCursor"/>r. The cursor can be
    /// destroyed with <see cref="DestroyCursor"/>. Any remaining cursors are destroyed by <see cref="Terminate"/>.
    /// <para/>
    /// The pixels are 32-bit, little-endian, non-premultiplied RGBA, i.e. eight bits per channel with the red channel
    /// first. They are arranged canonically as packed sequential rows, starting from the top-left corner.
    /// <para/>
    /// The cursor hotspot is specified in pixels, relative to the upper-left corner of the cursor image. Like all other
    /// coordinate systems in GLFW, the X-axis points to the right and the Y-axis points down.
    /// </summary>
    /// <param name="image">The desired cursor image.</param>
    /// <param name="xHot">The desired x-coordinate, in pixels, of the cursor hotspot.</param>
    /// <param name="yHot">The desired y-coordinate, in pixels, of the cursor hotspot.</param>
    /// <returns>The handle of the created cursor, or <c>null</c> if an error occurred.</returns>
    [Pure, NativeMethod("glfwCreateCursor")]
	public static Cursor CreateCursor(IBitmap image, int xHot = 0, int yHot = 0)
    {
        var bitmap = new Bitmap(image.Width, image.Height, image.Pixels); 
        return glfwCreateCursor(&bitmap, xHot, yHot);
    }

    /// <summary>
    /// Returns a cursor with a standard shape, that can be set for a window with <see cref="SetCursor"/>.
    /// </summary>
    /// <param name="shape">One of the standard shapes.</param>
    /// <returns>The handle of the created cursor, or <c>null</c> if an error occurred.</returns>
    [Pure, NativeMethod("glfwCreateStandardCursor")]
	public static Cursor CreateStandardCursor(CursorShape shape) => glfwCreateStandardCursor(shape);

    /// <summary>
    /// Destroys a cursor previously created with glfwCreateCursor. Any remaining cursors will be destroyed by
    /// <see cref="Terminate"/>.
    /// <para/>
    /// If the specified cursor is current for any window, that window will be reverted to the default cursor. This does
    /// not affect the cursor mode.
    /// </summary>
    /// <param name="cursor">The cursor object to destroy.</param>
    [NativeMethod("glfwDestroyCursor")]
	public static void DestroyCursor(Cursor cursor) => glfwDestroyCursor(cursor);

    /// <summary>
    /// Sets the cursor image to be used when the cursor is over the content area of the specified window. The set
    /// cursor will only be visible when the cursor mode of the window is <see cref="CursorMode.Normal"/>.
    /// </summary>
    /// <param name="window">The window to set the cursor for.</param>
    /// <param name="cursor">The cursor to set, or <c>null</c> to switch back to the default arrow cursor.</param>
    [NativeMethod("glfwSetCursor")]
	public static void SetCursor(Window window, Cursor? cursor) => glfwSetCursor(window, cursor ?? default);

    /// <summary>
    /// Returns whether the specified joystick is present.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns><c>true</c> if joystick is present at the specified index, otherwise <c>false</c>/</returns>
    [Pure, NativeMethod("glfwJoystickPresent")]
	public static bool JoystickPresent(int jid) => glfwJoystickPresent(jid) != FALSE;

    /// <summary>
    /// Returns the values of all axes of the specified joystick. Each element in the array is a value between
    /// <c>-1.0</c> and <c>1.0</c>.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <param name="count">Where to store the number of axis values in the returned array.</param>
    /// <returns>An array of axis values,</returns>
    [Pure, CLSCompliant(false), NativeMethod("glfwGetJoystickAxes")]
    public static float* GetJoystickAxes(int jid, int* count) => glfwGetJoystickAxes(jid, count);

    /// <summary>
    /// Returns the values of all axes of the specified joystick. Each element in the array is a value between
    /// <c>-1.0</c> and <c>1.0</c>.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns>A span containing the values.</returns>
    [Pure, NativeMethod("glfwGetJoystickAxes")]
    public static ReadOnlySpan<float> GetJoystickAxes(int jid)
    {
        int count;
        var axes = glfwGetJoystickAxes(jid, &count);
        return new ReadOnlySpan<float>(axes, count);
    }

    /// <summary>
    /// Returns the state of all buttons of the specified joystick. Each element in the array is either <c>true</c> or
    /// <c>false</c> indicating if it is pressed.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <param name="count">Where to store the number of button states in the returned array. </param>
    /// <returns>An array of button states.</returns>
    [Pure, CLSCompliant(false), NativeMethod("GetJoystickButtons")]
    public static bool* GetJoystickButtons(int jid, int* count) => glfwGetJoystickButtons(jid, count);

    /// <summary>
    /// Returns the state of all buttons of the specified joystick. Each element in the array is either <c>true</c> or
    /// <c>false</c> indicating if it is pressed.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns>A span of button states.</returns>
    [Pure, NativeMethod("GetJoystickButtons")]
    public static ReadOnlySpan<bool> GetJoystickButtons(int jid)
    {
        int count;
        var ptr = glfwGetJoystickButtons(jid, &count);
        return new ReadOnlySpan<bool>(ptr, count);
    }

    /// <summary>
    /// Returns the state of all hats of the specified joystick.
    /// <para/>
    /// The diagonal directions are bitwise combinations of the primary (up, right, down and left) directions and you
    /// can test for these individually by ANDing it with the corresponding direction.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <param name="count">Where to store the number of hat states in the returned array.</param>
    /// <returns>An array of hat states.</returns>
    [Pure, CLSCompliant(false), NativeMethod("GetJoystickHats")]
    public static bool* GetJoystickHats(int jid, int* count) => glfwGetJoystickHats(jid, count);

    /// <summary>
    /// Returns the state of all hats of the specified joystick.
    /// <para/>
    /// The diagonal directions are bitwise combinations of the primary (up, right, down and left) directions and you
    /// can test for these individually by ANDing it with the corresponding direction.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns>A span of hat states.</returns>
    [Pure, NativeMethod("GetJoystickHats")]
    public static ReadOnlySpan<bool> GetJoystickHats(int jid)
    {
        int count;
        var ptr = glfwGetJoystickHats(jid, &count);
        return new ReadOnlySpan<bool>(ptr, count);
    }

    /// <summary>
    /// Returns the name of the specified joystick.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns>The joystick name, or <c>null</c> if an error occurred.</returns>
    [Pure, NativeMethod("glfwGetJoystickName")]
    public static string? GetJoystickName(int jid) => Marshal.PtrToStringUTF8(glfwGetJoystickName(jid));

    /// <summary>
    /// Returns the SDL compatible GUID, as a UTF-8 encoded hexadecimal string, of the specified joystick.
    /// <para/>
    /// The GUID is what connects a joystick to a gamepad mapping. A connected joystick will always have a GUID even
    /// if there is no gamepad mapping assigned to it.
    /// <para/>
    /// The GUID uses the format introduced in SDL 2.0.5. This GUID tries to uniquely identify the make and model of a
    /// joystick but does not identify a specific unit, e.g. all wired Xbox 360 controllers will have the same GUID on
    /// that platform. The GUID for a unit may vary between platforms depending on what hardware information the
    /// platform specific APIs provide.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns>The GUID of the joystick.</returns>
    // ReSharper disable once InconsistentNaming
    [Pure, NativeMethod("glfwGetJoystickGUID")]
	public static Guid GetJoystickGUID(int jid)
    {
        var ptr = glfwGetJoystickGUID(jid);
        if (ptr == IntPtr.Zero)
            return default;

        var str = Marshal.PtrToStringUTF8(ptr);
        return str is null ? default : Guid.Parse(str);
    }

    /// <summary>
    /// Sets the user-defined pointer of the specified joystick.
    /// </summary>
    /// <param name="jid">The joystick to set.</param>
    /// <param name="pointer">The new value fo the user-pointer.</param>
    [NativeMethod("glfwSetJoystickUserPointer")]
	public static void SetJoystickUserPointer(int jid, IntPtr pointer) => glfwSetJoystickUserPointer(jid, pointer);

    /// <summary>
    /// Gets the user-defined pointer of the specified joystick.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns>The current value of the user-pointer.</returns>
    [Pure, NativeMethod("glfwGetJoystickUserPointer")]
	public static IntPtr GetJoystickUserPointer(int jid) => glfwGetJoystickUserPointer(jid);

    /// <summary>
    /// Returns whether the specified joystick is both present and has a gamepad mapping.
    /// <para/>
    /// If the specified joystick is present but does not have a gamepad mapping this function will return <c>false</c>
    /// but will not generate an error. Call <see cref="JoystickPresent"/> to check if a joystick is present regardless
    /// of whether it has a mapping.
    /// </summary>
    ///  <param name="jid">The joystick to query.</param>
    /// <returns><c>true</c> if the joystick is present and has a gamepad mapping, otherwise <c>false</c>.</returns>
    [Pure, NativeMethod("glfwJoystickIsGamepad")]
	public static bool JoystickIsGamepad(int jid) => glfwJoystickIsGamepad(jid) != FALSE;

    /// <summary>
    /// Parses the specified ASCII encoded string and updates the internal list with any gamepad mappings it finds.
    /// This string may contain either a single gamepad mapping or many mappings separated by newlines. The parser
    /// supports the full format of the <c>gamecontrollerdb.txt</c> source file including empty lines and comments.
    /// <para/>
    /// If there is already a gamepad mapping for a given GUID in the internal list, it will be replaced by the one
    /// passed to this function. If the library is terminated and re-initialized the internal list will revert to the
    /// built-in default.
    /// </summary>
    /// <param name="mappings">The string containing the gamepad mappings.</param>
    /// <returns><c>true</c> if successful, otherwise <c>false</c>.</returns>
    /// <seealso href="https://www.glfw.org/docs/3.3/input_guide.html#gamepad_mapping"/>
    [NativeMethod("glfwUpdateGamepadMappings")]
	public static bool UpdateGamepadMappings(string mappings)
    {
        fixed (byte* ptr = &UTF8String.Pin(mappings))
        {
            return glfwUpdateGamepadMappings(ptr) != FALSE;
        }
    }

    /// <summary>
    /// Returns the human-readable name of the gamepad from the gamepad mapping assigned to the specified joystick.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <returns>The name of the gamepad, or <c>null</c> if it is not present or not a gamepad with a mapping.</returns>
    [Pure, NativeMethod("glfwGetGamepadName")]
    public static string? GetGamepadName(int jid) => Marshal.PtrToStringUTF8(glfwGetGamepadName(jid));
    
    /// <summary>
    /// Retrieves the state of the specified joystick remapped to an Xbox-like gamepad.
    /// <para/>
    /// The Guide button may not be available for input as it is often hooked by the system or the Steam client.
    /// <para/>
    /// Not all devices have all the buttons or axes provided by <see cref="GamepadState"/>. Unavailable buttons and
    /// axes will always report <see cref="InputState.Release"/> and <c>0.0</c> respectively.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <param name="state">	The gamepad input state of the joystick.</param>
    /// <returns><c>true</c> if successful, otherwise <c>false</c>.</returns>
    [CLSCompliant(false), NativeMethod("glfwGetGamepadState")]
	public static bool GetGamepadState(int jid, GamepadState* state) => glfwGetGamepadState(jid, state) != FALSE;

    /// <summary>
    /// Retrieves the state of the specified joystick remapped to an Xbox-like gamepad.
    /// <para/>
    /// The Guide button may not be available for input as it is often hooked by the system or the Steam client.
    /// <para/>
    /// Not all devices have all the buttons or axes provided by <see cref="GamepadState"/>. Unavailable buttons and
    /// axes will always report <see cref="InputState.Release"/> and <c>0.0</c> respectively.
    /// </summary>
    /// <param name="jid">The joystick to query.</param>
    /// <param name="state">	The gamepad input state of the joystick.</param>
    /// <returns><c>true</c> if successful, otherwise <c>false</c>.</returns>
    [NativeMethod("glfwGetGamepadState")]
	public static bool GetGamepadState(int jid, ref GamepadState state)
    {
        fixed (GamepadState* ptr = &state)
        {
            return glfwGetGamepadState(jid, ptr) != FALSE;
        }
    }
    
    /// <summary>
    /// Sets the system clipboard to the specified string.
    /// </summary>
    /// <param name="str">The string to set.</param>
    [NativeMethod("glfwSetClipboardString")]
	public static void SetClipboardString(string? str)
    {
        fixed (byte* ptr = &UTF8String.Pin(str))
        {
            glfwSetClipboardString(default, ptr);
        }
    }

    /// <summary>
    /// Returns the contents of the system clipboard, if it contains or is convertible to a UTF-8 encoded string.
    /// If the clipboard is empty or if its contents cannot be converted, <c>null</c> is returned and a
    /// <see cref="ErrorCode.FormatUnavailable"/> error is generated.
    /// </summary>
    /// <returns>The contents of the clipboard string, or <c>null</c> if an error occurred.</returns>
    [Pure, NativeMethod("glfwGetClipboardString")]
    public static string? GetClipboardString() => Marshal.PtrToStringUTF8(glfwGetClipboardString(default));
    
    /// <summary>
    /// Returns whether the Vulkan loader and any minimally functional ICD have been found.
    /// <para/>
    /// The availability of a Vulkan loader and even an ICD does not by itself guarantee that surface creation or even
    /// instance creation is possible. Call <see cref="GetRequiredInstanceExtensions"/> to check whether the extensions
    /// necessary for Vulkan surface creation are available and <see cref="GetPhysicalDevicePresentationSupport(Anvil.GLFW3.Vulkan.Instance,Anvil.GLFW3.Vulkan.PhysicalDevice,int)"/>
    /// to check whether a queue family of a physical device supports image presentation.
    /// </summary>
    /// <returns><c>true</c> if Vulkan is minimally available, or <c>false</c> otherwise.</returns>
    [NativeMethod("glfwVulkanSupported")]
    public static bool VulkanSupported() => glfwVulkanSupported() != FALSE;
    
    /// <summary>
    /// Returns an array of names of Vulkan instance extensions required by GLFW for creating Vulkan surfaces for GLFW
    /// windows. If successful, the list will always contain <c>VK_KHR_surface</c>, so if you don't require any
    /// additional extensions you can pass this list directly to the <c>VkInstanceCreateInfo</c> struct.
    /// <para/>
    /// If Vulkan is not available on the machine, this function returns <see cref="IntPtr.Zero"/> and generates a
    /// <see cref="ErrorCode.ApiUnavailable"/> error. Call <see cref="VulkanSupported"/> to check whether Vulkan is at least
    /// minimally available.
    /// <para/>
    /// If Vulkan is available but no set of extensions allowing window surface creation was found, this function
    /// returns an empty array. You may still use Vulkan for off-screen rendering and compute work.
    /// </summary>
    /// <returns>An array of extension names.</returns>
    [NativeMethod("glfwGetRequiredInstanceExtensions")]
    public static string[] GetRequiredInstanceExtensions()
    {
        uint count;
        var pointers = glfwGetRequiredInstanceExtensions(&count);
        var extensions = new string[count];
        for (var i = 0u; i < count; i++)
        {
            extensions[i] = Marshal.PtrToStringUTF8(pointers[i]) ?? string.Empty;
        }
        return extensions;
    }

    /// <summary>
    /// Returns the address of the specified Vulkan core or extension function for the specified instance. If instance
    /// is set to <c>null</c> it can return any function exported from the Vulkan loader, including at least the
    /// following functions:
    /// <list type="bullet">
    /// <item><c>vkEnumerateInstanceExtensionProperties</c></item>
    /// <item><c>vkEnumerateInstanceLayerProperties</c></item>
    /// <item><c>vkCreateInstance</c></item>
    /// <item><c>vkGetInstanceProcAddr</c></item>
    /// </list>
    /// If Vulkan is not available on the machine, this function returns <see cref="IntPtr.Zero"/> and generates a
    /// <see cref="ErrorCode.ApiUnavailable"/> error. Call <see cref="VulkanSupported"/> to check whether Vulkan is at least
    /// minimally available.
    /// <para/>
    /// This function is equivalent to calling <c>vkGetInstanceProcAddr</c> with a platform-specific query of the Vulkan
    /// loader as a fallback.
    /// </summary>
    /// <param name="instance">
    /// The Vulkan instance to query, or <c>null</c> to retrieve functions related to instance creation.
    /// </param>
    /// <param name="procName">The name of the function (ASCII characters only).</param>
    /// <returns></returns>
    [NativeMethod("glfwGetInstanceProcAddress")]
    public static IntPtr GetInstanceProcAddress(Instance? instance, string procName)
    {
        fixed (byte* ptr = &UTF8String.Pin(procName))
        {
            return glfwGetInstanceProcAddress(instance ?? default, ptr);
        }
    }

    /// <summary>
    /// Creates a Vulkan surface for the specified <paramref name="window"/>.
    /// <para/>
    /// If the Vulkan loader or at least one minimally functional ICD were not found, this function returns
    /// <see cref="Result.ErrorInitializationFailed"/> and generates a <see cref="ErrorCode.ApiUnavailable"/> error. Call
    /// <see cref="VulkanSupported"/> to check whether Vulkan is at least minimally available.
    /// <para/>
    /// If the required window surface creation instance extensions are not available or if the specified instance was
    /// not created with these extensions enabled, this function returns <see cref="Result.ErrorExtensionNotPresent"/>
    /// and generates a <see cref="ErrorCode.ApiUnavailable"/> error. Call <see cref="GetRequiredInstanceExtensions"/> to
    /// check what instance extensions are required.
    /// <para/>
    /// The window surface cannot be shared with another API so the window must have been created with the client API
    /// hint set to <see cref="OpenGLApi.None"/> otherwise it generates a <see cref="ErrorCode.InvalidValue"/> error and
    /// returns <see cref="Result.ErrorNativeWindowInUseKhr"/>.
    /// <para/>
    /// The window surface must be destroyed before the specified Vulkan instance. It is the responsibility of the
    /// caller to destroy the window surface. GLFW does not destroy it for you. Call <c>vkDestroySurfaceKHR</c> to
    /// destroy the surface.
    /// </summary>
    /// <param name="instance">The Vulkan instance to create the surface in.</param>
    /// <param name="window">The window to create the surface for.</param>
    /// <param name="allocator">The allocator to use, or <c>null</c> to use the default allocator.</param>
    /// <param name="surface">
    /// Where to store the handle of the surface. This is set to a null/invalid handle if an error occurred.
    /// </param>
    /// <returns><see cref="Result.Success"/> on success, otherwise an error result.</returns>
    [NativeMethod("glfwCreateWindowSurface")]
    public static Result CreateWindowSurface(Instance instance, Window window, AllocationCallbacks? allocator, out SurfaceKHR surface)
    {
        SurfaceKHR vkSurface;
        Result vkResult;
        
        if (allocator is not null)
        {
            var callbacks = stackalloc IntPtr[6];
            callbacks[0] = allocator.UserData;
            callbacks[1] = allocator.Allocation is null
                ? IntPtr.Zero 
                : Marshal.GetFunctionPointerForDelegate(allocator.Allocation);
            callbacks[2] = allocator.Reallocation is null
                ? IntPtr.Zero
                : Marshal.GetFunctionPointerForDelegate(allocator.Reallocation);
            callbacks[3] = allocator.Free is null
                ? IntPtr.Zero
                : Marshal.GetFunctionPointerForDelegate(allocator.Free);
            callbacks[4] = allocator.InternalAllocation is null
                ? IntPtr.Zero
                : Marshal.GetFunctionPointerForDelegate(allocator.InternalAllocation);
            callbacks[5] = allocator.InternalFree is null
                ? IntPtr.Zero
                : Marshal.GetFunctionPointerForDelegate(allocator.InternalFree);
            
            vkResult = glfwCreateWindowSurface(instance, window, callbacks, &vkSurface);
        }
        else
        {
            vkResult = glfwCreateWindowSurface(instance, window, default, &vkSurface);   
        }
        
        surface = vkSurface;
        return vkResult;
    }

    /// <summary>
    /// Returns whether the specified queue family of the specified physical device supports presentation to the
    /// platform GLFW was built for.
    /// <para/>
    /// If Vulkan or the required window surface creation instance extensions are not available on the machine, or if
    /// the specified instance was not created with the required extensions, this function returns <c>false</c> and
    /// generates a <see cref="ErrorCode.ApiUnavailable"/> error. Call <see cref="VulkanSupported"/> to check whether Vulkan
    /// is at least minimally available and <see cref="GetRequiredInstanceExtensions"/> to check what instance
    /// extensions are required.
    /// </summary>
    /// <param name="instance">The instance that the physical device belongs to.</param>
    /// <param name="device">The physical device that the queue family belongs to.</param>
    /// <param name="queueFamily">The index of the queue family to query.</param>
    /// <returns><c>true</c> if the queue family supports presentation, or <c>false</c> otherwise.</returns>
    [NativeMethod("glfwGetPhysicalDevicePresentationSupport")]
    public static bool GetPhysicalDevicePresentationSupport(Instance instance, PhysicalDevice device, int queueFamily)
    {
        var queue = Unsafe.As<int, uint>(ref queueFamily);
        return glfwGetPhysicalDevicePresentationSupport(instance, device, queue) == TRUE;
    }
    
    /// <inheritdoc cref="GetPhysicalDevicePresentationSupport(Anvil.GLFW3.Vulkan.Instance,Anvil.GLFW3.Vulkan.PhysicalDevice,int)"/>
    [NativeMethod("glfwGetPhysicalDevicePresentationSupport"), CLSCompliant(false)]
    public static bool GetPhysicalDevicePresentationSupport(Instance instance, PhysicalDevice device, uint queueFamily)
    {
        return glfwGetPhysicalDevicePresentationSupport(instance, device, queueFamily) == TRUE;
    }

    /// <summary>
    /// Returns the <c>HGLRC</c> of the specified <paramref name="window"/>, or <see cref="IntPtr.Zero"/> if an error
    /// occurred, or not applicable for the current platform.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The <c>HGLRC</c> of the window, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
    public static IntPtr GetWGLContext(Window window)
    {
        return glfwGetWGLContext is null ? IntPtr.Zero : glfwGetWGLContext(window);
    }

    /// <summary>
    /// Returns the <c>GLXContext</c> of the specified <paramref name="window"/>, or <see cref="IntPtr.Zero"/> if an
    /// error occurred, or not applicable for the current platform.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The <c>GLXContext</c> of the window, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
    public static IntPtr GetGLXContext(Window window)
    {
        return glfwGetGLXContext is null ? IntPtr.Zero : glfwGetGLXContext(window);
    }

    /// <summary>
    /// Returns the <c>EGLContext</c> of the specified <paramref name="window"/>, or <see cref="IntPtr.Zero"/> if an
    /// error occurred, or not applicable for the current platform.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The <c>EGLContext</c> of the window, or <see cref="IntPtr.Zero"/> if an error occurred.</returns>
    public static IntPtr GetEGLContext(Window window)
    {
        return glfwGetEGLContext is null ? IntPtr.Zero : glfwGetEGLContext(window);
    }

    /// <summary>
    /// Returns the <c>NSOpenGLContext</c> of the specified <paramref name="window"/>, or <c>0</c> if
    /// an error occurred, or not applicable for the current platform.
    /// </summary>
    /// <param name="window">The window to query.</param>
    /// <returns>The <c>NSOpenGLContext</c> of the window, or <c>0</c> if an error occurred.</returns>
    public static int GetNSGLContext(Window window)
    {
        return glfwGetNSGLContext is null ? 0 : glfwGetNSGLContext(window);
    }
}