using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.OpenAL;

[UnmanagedFunctionPointer(CallingConvention.Cdecl), PublicAPI]
public delegate void FoldbackCallback(FoldbackEventType type, int blockIndex);

[UnmanagedFunctionPointer(CallingConvention.Cdecl), PublicAPI]
public delegate void BufferCallbackHandler(IntPtr userPtr, IntPtr samples, int size);

public delegate void BuffersCompleteHandler(Source source, int bufferCount);

public delegate void SourceStateHandler(Source source, SourceState state);

public delegate void ErrorHandler(Error error);

public delegate void ParamChangeHandler<in T>(T obj, int parameter, ParameterType type, object value) where T : unmanaged;

public static unsafe partial class AL
{
    [MethodImpl(MethodImplOptions.AggressiveInlining), Conditional("AL_ERROR_EVENTS")]
    internal static void CheckErrorState()
    {
        var error = alGetError();
        if (error != Error.None)
            ErrorEmitted?.Invoke(error);
    }
    
    /// <summary>
    /// Occurs when parameter of an <see cref="Effect"/> object is changed.
    /// </summary>
    public static event ParamChangeHandler<Effect>? EffectParamChanged;

    /// <summary>
    /// Occurs when OpenAL reports an error state.
    /// </summary>
    public static event ErrorHandler? ErrorEmitted;
    
    private enum EventType
    {
        BufferCompleted = 0x19A4,
        SourceStateChanged = 0x19A5,
        Disconnected = 0x19A6,
    }
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl), PublicAPI]
    private delegate void EventProc(EventType eventType, int obj, int param, int length, void* message, IntPtr userParam);
    
    /// <summary>
    /// Occurs when the device of the context is disconnected.
    /// </summary>
    /// <remarks>Requires <c>AL_SOFT_events</c> and <c>ALC_EXT_disconnect</c> extension support.</remarks>
    public static event Action? DeviceDisconnected
    {
        add
        {
            deviceDisconnected += value;
            EventControl(deviceDisconnected, EventType.Disconnected, true);
        }
        remove
        {
            deviceDisconnected -= value;
            EventControl(deviceDisconnected, EventType.Disconnected, false);
        }
    }
    
    /// <summary>
    /// Occurs when a streaming source's buffer queue has finished processing one or more buffers.
    /// </summary>
    /// <remarks>Requires <c>AL_SOFT_events</c> extension support.</remarks>
    public static event BuffersCompleteHandler? BufferComplete
    {
        add
        {
            bufferComplete += value;
            EventControl(bufferComplete, EventType.BufferCompleted, true);
        }
        remove
        {
            bufferComplete -= value;
            EventControl(bufferComplete, EventType.BufferCompleted, false);
        }
    }

    /// <summary>
    /// Occurs when source's playing state has changed.
    /// </summary>
    /// <remarks>Requires <c>AL_SOFT_events</c> extension support.</remarks>
    public static event SourceStateHandler? SourceStateChanged
    {
        add
        {
            sourceStateChanged += value;
            EventControl(sourceStateChanged, EventType.SourceStateChanged, true);
        }
        remove
        {
            sourceStateChanged -= value;
            EventControl(sourceStateChanged, EventType.SourceStateChanged, false);
        }
    }
    
    
    [NativeMethod("alEventControlSOFT"), NativeMethod("alEventCallbackSOFT")]
    private static void EventControl(Delegate? handler, EventType type, bool enable)
    {
        if (enable)
        {
            if (eventProc is null)
            {
                eventProc = EventProcImpl;
                alEventCallbackSOFT(eventProc, IntPtr.Zero);
            }
            alEventControlSOFT(1, &type, true);
        }
        else
        {
            alEventControlSOFT(1, &type, false);
            if (handler is null || handler.GetInvocationList().Length == 0)
                alEventControlSOFT(1, &type, false);
            
            if ((bufferComplete is null || bufferComplete.GetInvocationList().Length == 0) &&
                (sourceStateChanged is null || sourceStateChanged.GetInvocationList().Length == 0) &&
                (deviceDisconnected is null || deviceDisconnected.GetInvocationList().Length == 0))
            {
                eventProc = null;
                alEventCallbackSOFT(null, IntPtr.Zero);
            }
        }

        CheckErrorState();
    }

    private static void EventProcImpl(EventType eventType, int obj, int param, int length, void* message, IntPtr userParam)
    {
        switch (eventType)
        {
            case EventType.BufferCompleted:
                bufferComplete?.Invoke(new Source(obj), param);
                break;
            case EventType.SourceStateChanged:
                sourceStateChanged?.Invoke(new Source(obj), Unsafe.As<int, SourceState>(ref param));
                break;
            case EventType.Disconnected:
                deviceDisconnected?.Invoke();
                break;
        }
    }
    
    [NativeMethod("alRequestFoldbackStart"), CLSCompliant(false)]
    public static void RequestFoldbackStart(FoldbackMode mode, int blockCount, int blockLength, float* bufferMemory, FoldbackCallback callback)
    {
        // Store delegate reference so it is not garbage collected
        foldbackCallback = callback;
        alRequestFoldbackStart(mode, blockCount, blockLength, bufferMemory, callback);
        CheckErrorState();
    }
	
    [NativeMethod("alRequestFoldbackStart")]
    public static void RequestFoldbackStart(FoldbackMode mode, int blockCount, int blockLength, IntPtr bufferMemory, FoldbackCallback callback)
    {
        // Store delegate reference so it is not garbage collected
        foldbackCallback = callback;
        alRequestFoldbackStart(mode, blockCount, blockLength, (float*) bufferMemory.ToPointer(), callback);
        CheckErrorState();
    }

    [NativeMethod("alRequestFoldbackStop")]
    public static void RequestFoldbackStop()
    {
        alRequestFoldbackStop();
        CheckErrorState();
    }

    [NativeMethod("alDeferUpdatesSOFT")]
    public static void DeferUpdates()
    {
        alDeferUpdatesSOFT();
        CheckErrorState();
    }

    [NativeMethod("alProcessUpdatesSOFT")]
    public static void ProcessUpdates()
    {
        alProcessUpdatesSOFT();
        CheckErrorState();
    }
    
    private static FoldbackCallback? foldbackCallback;
    private static EventProc? eventProc;
    private static Action? deviceDisconnected;
    private static BuffersCompleteHandler? bufferComplete;
    private static SourceStateHandler? sourceStateChanged;
}