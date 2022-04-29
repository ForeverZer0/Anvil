using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.OpenGL;

/// <summary>
/// Callback handler for OpenGL debug messages.
/// </summary>
/// <param name="source">A value describing the source that emitted the message.</param>
/// <param name="type">A value describing the message type.</param>
/// <param name="id">An ID/name of the message source.</param>
/// <param name="severity">A value indicating the severity of the message.</param>
/// <param name="message">A string description of the message.</param>
[PublicAPI]
public delegate void DebugMessageHandler(DebugSource source, DebugType type, int id, DebugSeverity severity, string message);

// ReSharper disable once InconsistentNaming
public static unsafe partial class GL
{
    
    [Conditional("DEBUG"), RequiredExtension("KHR_debug")]
    private static void ReferenceCount(DebugObjectType type, int offset)
    {
        if (!references.ContainsKey(type))
            references[type] = 0;
        references[type] += offset;
    }

    /// <summary>
    /// Reports any unmanaged OpenGL objects that have not been disposed/deleted.
    /// <para/>
    /// This is typically used for debugging purposes and called at the end of program execution to ensure that all
    /// resources are being released properly.
    /// </summary>
    /// <param name="all">
    /// <c>true</c> to report the reference count of all objects regardless of their count, or <c>false</c> to only
    /// output objects that are non-zero.
    /// </param>
    /// <param name="writer">
    /// A <see cref="TextWriter"/> instance to write to, or <c>null</c> to output to the standard output stream
    /// (<see cref="Console.Out"/>).
    /// </param>
    [RequiredExtension("KHR_debug")]
    public static void ReportReferenceCount(bool all = true, TextWriter? writer = null)
    {
        var output = writer ?? Console.Out;
        foreach (var (type, count) in references)
        {
            if (!all && count == 0)
                continue;
            output.WriteLine($"{Enum.GetName(type)}: {count}");
        }
    }

    /// <summary>
	/// Specify a callback to receive debugging messages from the gl.
	/// </summary>
    [NativeMethod("glDebugMessageCallback"), ]
    private static void DebugMessageCallback(DebugProc callback, IntPtr userParam) => glDebugMessageCallback(callback, userParam);

	/// <summary>
	/// Control the reporting of debug messages in a debug context.
	/// </summary>
    [NativeMethod("glDebugMessageControl"), CLSCompliant(false), RequiredExtension("KHR_debug")]
    public static void DebugMessageControl(DebugSource source, DebugType type, DebugSeverity severity, int count, int* ids, bool enabled) => glDebugMessageControl(source, type, severity, count, ids, enabled);

	/// <summary>
	/// Control the reporting of debug messages in a debug context.
	/// </summary>
    [NativeMethod("glDebugMessageControl"), RequiredExtension("KHR_debug")]
    public static void DebugMessageControl(DebugSource source, DebugType type, DebugSeverity severity, int count, ReadOnlySpan<int> ids, bool enabled)
    {
        fixed (int* ptr = &ids.GetPinnableReference())
        {
            glDebugMessageControl(source, type, severity, count, ptr, enabled);
        }
    }

	/// <summary>
	/// Inject an application-supplied message into the debug message queue.
	/// </summary>
    [NativeMethod("glDebugMessageInsert"), RequiredExtension("KHR_debug")]
    public static void DebugMessageInsert(DebugSource source, DebugType type, int id, DebugSeverity severity, string? message)
    {
        var utf8 = new UTF8String(message);
        fixed (byte* ptr = &utf8.GetPinnableReference())
        {
            glDebugMessageInsert(source, type, id, severity, utf8.Length, ptr);	
        }
    }
        
	/// <summary>
	/// Retrieve messages from the debug message log.
	/// </summary>
    [NativeMethod("glGetDebugMessageLog"), CLSCompliant(false), RequiredExtension("KHR_debug")]
    public static int GetDebugMessageLog(int count, int bufSize, DebugSource* sources, DebugType* types, int* ids, DebugSeverity* severities, int* lengths, byte* messageLog) => glGetDebugMessageLog(count, bufSize, sources, types, ids, severities, lengths, messageLog);
        
	/// <summary>
	/// Pop the active debug group.
	/// </summary>
    [NativeMethod("glPopDebugGroup"), RequiredExtension("KHR_debug")]
    public static void PopDebugGroup() => glPopDebugGroup();

	/// <summary>
	/// Specify the primitive restart index.
	/// </summary>
    [NativeMethod("glPrimitiveRestartIndex"), RequiredExtension("KHR_debug")]
    public static void PrimitiveRestartIndex(int index) => glPrimitiveRestartIndex(index);

	/// <summary>
	/// Specify the vertex to be used as the source of data for flat shaded varyings.
	/// </summary>
    [NativeMethod("glProvokingVertex"), RequiredExtension("KHR_debug")]
    public static void ProvokingVertex(VertexProvokingMode mode) => glProvokingVertex(mode);

	/// <summary>
	/// Push a named debug group into the command stream.
	/// </summary>
    [NativeMethod("glPushDebugGroup"), RequiredExtension("KHR_debug")]
    public static void PushDebugGroup(DebugSource source, int id, string message)
    {
        var utf8 = new UTF8String(message);
        fixed (byte* ptr = &utf8.GetPinnableReference())
        {
            glPushDebugGroup(source, id, utf8.Length, ptr);
        }
    }
    
    private static void OnDebugMessage(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, byte* message, IntPtr userPara)
    {
        DebugMessageImpl?.Invoke(source, type, id, severity, Encoding.UTF8.GetString(message, length));
    }
    
    /// <summary>
    /// Occurs when OpenGL emits a debug message.
    /// </summary>
    [RequiredExtension("KHR_debug")]
    public static event DebugMessageHandler DebugMessage
    {
        add
        {
            if (debugCallback is null)
            {
                debugCallback = OnDebugMessage;
                glDebugMessageCallback(debugCallback, IntPtr.Zero);
            }
            DebugMessageImpl += value;
        }
        remove
        {
            if (DebugMessageImpl != null)
                DebugMessageImpl -= value;
            if (DebugMessageImpl is null || DebugMessageImpl.GetInvocationList().Length < 1)
            {
                debugCallback = null;
                glDebugMessageCallback(null, IntPtr.Zero);
            }
        }
    }
    
    private enum DebugObjectType
    {
        Program,
        Shader,
        Query,
        Buffer,
        Framebuffer,
        Renderbuffer,
        Sampler,
        Texture,
        VertexArray,
        Sync
    }
    
    private static DebugProc? debugCallback;
        
    private static event DebugMessageHandler? DebugMessageImpl;
    
    private static readonly Dictionary<DebugObjectType, int> references = new(); 
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private delegate void DebugProc(DebugSource source, DebugType type, int id, DebugSeverity severity, int length, byte* message, IntPtr userParam);
}
