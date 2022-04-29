using System.Runtime.CompilerServices;
using Anvil.Native;

namespace Anvil.OpenGL;

// ReSharper disable once InconsistentNaming
public static unsafe partial class GL
{
    #if CACHED_CAPABILITIES
    
    private static readonly Dictionary<EnableCap, bool> CapabilityCache;

    static GL()
    {
        // Initialize cache to reflect initial state of OpenGL.
        CapabilityCache = new Dictionary<EnableCap, bool>();
        foreach (var cap in Enum.GetValues<EnableCap>())
            CapabilityCache[cap] = false;

        CapabilityCache[EnableCap.Dither] = true;
        CapabilityCache[EnableCap.Multisample] = true;
    }

	/// <summary>
	/// Enable or disable server-side GL capabilities.
	/// </summary>
    [NativeMethod("glEnable")]
    public static void Enable(EnableCap cap)
    {
        if (CapabilityCache[cap])
            return;
        CapabilityCache[cap] = true;
        glEnable(cap);
    }
    
    /// <summary>
    /// Disable server-side GL capabilities.
    /// </summary>
    [NativeMethod("glDisable")]
    public static void Disable(EnableCap cap)
    {
        if (!CapabilityCache[cap])
            return;

        CapabilityCache[cap] = false;
        glDisable(cap);
    }
    
    #else
    
    /// <summary>
    /// Enable or disable server-side GL capabilities.
    /// </summary>
    [NativeMethod("glEnable")]
    public static void Enable(EnableCap cap) => glEnable(cap);
    
    /// <summary>
    /// Disable server-side GL capabilities.
    /// </summary>
    [NativeMethod("glDisable")]
    public static void Disable(EnableCap cap) => glDisable(cap);
    
    #endif

    /// <summary>
    /// Enable server-side GL capabilities.
    /// </summary>
    [NativeMethod("glEnable")]
    public static void Enable(int cap) => glEnable(Unsafe.As<int, EnableCap>(ref cap));

		
    /// <summary>
    /// Disable server-side GL capabilities.
    /// </summary>
    [NativeMethod("glDisable")]
    public static void Disable(int cap) => glDisable(Unsafe.As<int, EnableCap>(ref cap));

	/// <summary>
	/// Create a new sync object and insert it into the gl command stream.
	/// </summary>
    [NativeMethod("glFenceSync")]
    public static Sync FenceSync(SyncCondition condition, SyncBehaviorFlags flags)
    {
        ReferenceCount(DebugObjectType.Sync, 1);
        return glFenceSync(condition, flags);
    }
    
	/// <summary>
	/// Creates a program object.
	/// </summary>
    [NativeMethod("glCreateProgram")]
    public static Program CreateProgram()
    {
        ReferenceCount(DebugObjectType.Program, 1);
        return glCreateProgram();
    }

	/// <summary>
	/// Creates a shader object.
	/// </summary>
    [NativeMethod("glCreateShader")]
    public static Shader CreateShader(ShaderType type)
    {
        ReferenceCount(DebugObjectType.Shader, 1);
        return glCreateShader(type);
    }

	/// <summary>
	/// Generate buffer object names.
	/// </summary>
    [NativeMethod("glGenBuffers"), CLSCompliant(false)]
    public static void GenBuffers(int count, Buffer* buffers)
    {
        ReferenceCount(DebugObjectType.Buffer, count);
        glGenBuffers(count, buffers);
    }

	/// <summary>
	/// Generate framebuffer object names.
	/// </summary>
    [NativeMethod("glGenFramebuffers"), CLSCompliant(false)]
    public static void GenFramebuffers(int count, Framebuffer* framebuffers)
    {
        ReferenceCount(DebugObjectType.Framebuffer, count);
        glGenFramebuffers(count, framebuffers);
    }

	/// <summary>
	/// Generate query object names.
	/// </summary>
    [NativeMethod("glGenQueries"), CLSCompliant(false)]
    public static void GenQueries(int count, Query* queries)
    {
        ReferenceCount(DebugObjectType.Query, count);
        glGenQueries(count, queries);
    }

	/// <summary>
	/// Generate renderbuffer object names.
	/// </summary>
    [NativeMethod("glGenRenderbuffers"), CLSCompliant(false)]
    public static void GenRenderbuffers(int count, Renderbuffer* renderbuffers)
    {
        ReferenceCount(DebugObjectType.Renderbuffer, count);
        glGenRenderbuffers(count, renderbuffers);
    }

	/// <summary>
	/// Generate sampler object names.
	/// </summary>
    [NativeMethod("glGenSamplers"), CLSCompliant(false)]
    public static void GenSamplers(int count, Sampler* samplers)
    {
        ReferenceCount(DebugObjectType.Sampler, count);
        glGenSamplers(count, samplers);
    }

	/// <summary>
	/// Generate texture names.
	/// </summary>
    [NativeMethod("glGenTextures"), CLSCompliant(false)]
    public static void GenTextures(int count, Texture* textures)
    {
        ReferenceCount(DebugObjectType.Texture, count);
        glGenTextures(count, textures);
    }

	/// <summary>
	/// Generate vertex array object names.
	/// </summary>
    [NativeMethod("glGenVertexArrays"), CLSCompliant(false)]
    public static void GenVertexArrays(int count, VertexArray* arrays)
    {
        ReferenceCount(DebugObjectType.VertexArray, count);
        glGenVertexArrays(count, arrays);
    }
        
	/// <summary>
	/// Generate buffer object names.
	/// </summary>
    [NativeMethod("glGenBuffers")]
    public static void GenBuffers(Span<Buffer> buffers)
    {
        fixed (Buffer* ptr = &buffers.GetPinnableReference())
        {
            glGenBuffers(buffers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Buffer, buffers.Length);
    }

	/// <summary>
	/// Generate framebuffer object names.
	/// </summary>
    [NativeMethod("glGenFramebuffers")]
    public static void GenFramebuffers(Span<Framebuffer> framebuffers)
    {
        fixed (Framebuffer* ptr = &framebuffers.GetPinnableReference())
        {
            glGenFramebuffers(framebuffers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Framebuffer, framebuffers.Length);
    }

	/// <summary>
	/// Generate query object names.
	/// </summary>
    [NativeMethod("glGenQueries")]
    public static void GenQueries(Span<Query> queries)
    {
        fixed (Query* ptr = &queries.GetPinnableReference())
        {
            glGenQueries(queries.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Query, queries.Length);
    }

	/// <summary>
	/// Generate renderbuffer object names.
	/// </summary>
    [NativeMethod("glGenRenderbuffers")]
    public static void GenRenderbuffers(Span<Renderbuffer> renderbuffers)
    {
        fixed (Renderbuffer* ptr = &renderbuffers.GetPinnableReference())
        {
            glGenRenderbuffers(renderbuffers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Renderbuffer, renderbuffers.Length);
    }

	/// <summary>
	/// Generate sampler object names.
	/// </summary>
    [NativeMethod("glGenSamplers")]
    public static void GenSamplers(Span<Sampler> samplers)
    {
        fixed (Sampler* ptr = &samplers.GetPinnableReference())
        {
            glGenSamplers(samplers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Sampler, samplers.Length);
    }

	/// <summary>
	/// Generate texture names.
	/// </summary>
    [NativeMethod("glGenTextures")]
    public static void GenTextures(Span<Texture> textures)
    {
        fixed (Texture* ptr = &textures.GetPinnableReference())
        {
            glGenTextures(textures.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Texture, textures.Length);
    }

	/// <summary>
	/// Generate vertex array object names.
	/// </summary>
    [NativeMethod("glGenVertexArrays")]
    public static void GenVertexArrays(Span<VertexArray> arrays)
    {
        fixed (VertexArray* ptr = &arrays.GetPinnableReference())
        {
            glGenVertexArrays(arrays.Length, ptr);
        }
        ReferenceCount(DebugObjectType.VertexArray, arrays.Length);
    }

    /// <summary>
    /// Generate a buffer object name.
    /// </summary>
    [NativeMethod("glGenBuffers")]
    public static Buffer GenBuffer()
    {
        Buffer value = default;
        glGenBuffers(1, &value);
        ReferenceCount(DebugObjectType.Buffer, 1);
        return value;
    }

    /// <summary>
    /// Generate a framebuffer object name.
    /// </summary>
    [NativeMethod("glGenFramebuffers")]
    public static Framebuffer GenFramebuffer()
    {
        Framebuffer value = default;
        glGenFramebuffers(1, &value);
        ReferenceCount(DebugObjectType.Framebuffer, 1);
        return value;
    }

    /// <summary>
    /// Generate a query object name.
    /// </summary>
    [NativeMethod("glGenQueries")]
    public static Query GenQuery()
    {
        Query value = default;
        glGenQueries(1, &value);
        ReferenceCount(DebugObjectType.Query, 1);
        return value;
    }

    /// <summary>
    /// Generate a renderbuffer object name.
    /// </summary>
    [NativeMethod("glGenRenderbuffers")]
    public static Renderbuffer GenRenderbuffer()
    {
        Renderbuffer value = default;
        glGenRenderbuffers(1, &value);
        ReferenceCount(DebugObjectType.Renderbuffer, 1);
        return value;
    }

    /// <summary>
    /// Generate a sampler object name.
    /// </summary>
    [NativeMethod("glGenSamplers")]
    public static Sampler GenSampler()
    {
        Sampler value = default;
        glGenSamplers(1, &value);
        ReferenceCount(DebugObjectType.Sampler, 1);
        return value;
    }

    /// <summary>
    /// Generate a texture object name.
    /// </summary>
    [NativeMethod("glGenTextures")]
    public static Texture GenTexture()
    {
        Texture value = default;
        glGenTextures(1, &value);
        ReferenceCount(DebugObjectType.Texture, 1);
        return value;
    }

    /// <summary>
    /// Generate a vertex array object name.
    /// </summary>
    [NativeMethod("glGenVertexArrays")]
    public static VertexArray GenVertexArray()
    {
        VertexArray value = default;
        glGenVertexArrays(1, &value);
        ReferenceCount(DebugObjectType.VertexArray, 1);
        return value;
    }

    /// <summary>
    /// Delete a named buffer object.
    /// </summary>
    [NativeMethod("glDeleteBuffers")]
    public static Buffer DeleteBuffer()
    {
        Buffer value = default;
        glDeleteBuffers(1, &value);
        ReferenceCount(DebugObjectType.Buffer, -1);
        return value;
    }

    /// <summary>
    /// Delete a named framebuffer object.
    /// </summary>
    [NativeMethod("glDeleteFramebuffers")]
    public static Framebuffer DeleteFramebuffer()
    {
        Framebuffer value = default;
        glDeleteFramebuffers(1, &value);
        ReferenceCount(DebugObjectType.Framebuffer, -1);
        return value;
    }

	/// <summary>
	/// Deletes a program object.
	/// </summary>
    [NativeMethod("glDeleteProgram")]
    public static void DeleteProgram(Program program)
    {
        glDeleteProgram(program);
        ReferenceCount(DebugObjectType.Program, -1);
    }

	/// <summary>
	/// Deletes a shader object.
	/// </summary>
    [NativeMethod("glDeleteShader")]
    public static void DeleteShader(Shader shader)
    {
        glDeleteShader(shader);
        ReferenceCount(DebugObjectType.Shader, -1);
    }

	/// <summary>
	/// Delete named buffer objects.
	/// </summary>
    [NativeMethod("glDeleteBuffers"), CLSCompliant(false)]
    public static void DeleteBuffers(int count, Buffer* buffers)
    {
        glDeleteBuffers(count, buffers);
        ReferenceCount(DebugObjectType.Buffer, -count);
    }

	/// <summary>
	/// Delete framebuffer objects.
	/// </summary>
    [NativeMethod("glDeleteFramebuffers"), CLSCompliant(false)]
    public static void DeleteFramebuffers(int count, Framebuffer* framebuffers)
    {
        glDeleteFramebuffers(count, framebuffers);
        ReferenceCount(DebugObjectType.Framebuffer, -count);
    }

	/// <summary>
	/// Delete named query objects.
	/// </summary>
    [NativeMethod("glDeleteQueries"), CLSCompliant(false)]
    public static void DeleteQueries(int count, Query* queries)
    {
        glDeleteQueries(count, queries);
        ReferenceCount(DebugObjectType.Query, -count);
    }

	/// <summary>
	/// Delete renderbuffer objects.
	/// </summary>
    [NativeMethod("glDeleteRenderbuffers"), CLSCompliant(false)]
    public static void DeleteRenderbuffers(int count, Renderbuffer* renderbuffers)
    {
        glDeleteRenderbuffers(count, renderbuffers);
        ReferenceCount(DebugObjectType.Renderbuffer, -count);
    }

	/// <summary>
	/// Delete named sampler objects.
	/// </summary>
    [NativeMethod("glDeleteSamplers"), CLSCompliant(false)]
    public static void DeleteSamplers(int count, Sampler* samplers)
    {
        glDeleteSamplers(count, samplers);
        ReferenceCount(DebugObjectType.Sampler, -count);
    }

	/// <summary>
	/// Delete named textures.
	/// </summary>
    [NativeMethod("glDeleteTextures"), CLSCompliant(false)]
    public static void DeleteTextures(int count, Texture* textures)
    {
        glDeleteTextures(count, textures);
        ReferenceCount(DebugObjectType.Texture, -count);
    }

	/// <summary>
	/// Delete vertex array objects.
	/// </summary>
    [NativeMethod("glDeleteVertexArrays"), CLSCompliant(false)]
    public static void DeleteVertexArrays(int count, VertexArray* arrays)
    {
        glDeleteVertexArrays(count, arrays);
        ReferenceCount(DebugObjectType.VertexArray, -count);
    }

    /// <summary>
    /// Delete a named buffer object.
    /// </summary>
    [NativeMethod("glDeleteBuffers")]
    public static void DeleteBuffer(Buffer buffer)
    {
        glDeleteBuffers(1, &buffer);
        ReferenceCount(DebugObjectType.Buffer, -1);
    }

    /// <summary>
    /// Delete a named framebuffer object.
    /// </summary>
    [NativeMethod("glDeleteFramebuffers")]
    public static void DeleteFramebuffer(Framebuffer framebuffer)
    {
        glDeleteFramebuffers(1, &framebuffer);
        ReferenceCount(DebugObjectType.Framebuffer, -1);
    }

    /// <summary>
    /// Delete a named query object.
    /// </summary>
    [NativeMethod("glDeleteQueries")]
    public static void DeleteQuery(Query query)
    {
        glDeleteQueries(1, &query);
        ReferenceCount(DebugObjectType.Query, -1);
    }

    /// <summary>
    /// Delete a named renderbuffer object.
    /// </summary>
    [NativeMethod("glDeleteRenderbuffers")]
    public static void DeleteRenderbuffer(Renderbuffer renderbuffer)
    {
        glDeleteRenderbuffers(1, &renderbuffer);
        ReferenceCount(DebugObjectType.Renderbuffer, -1);
    }

    /// <summary>
    /// Delete a named sampler object.
    /// </summary>
    [NativeMethod("glDeleteSamplers")]
    public static void DeleteSampler(Sampler sampler)
    {
        glDeleteSamplers(1, &sampler);
        ReferenceCount(DebugObjectType.Sampler, -1);
    }

    /// <summary>
    /// Delete a named texture object.
    /// </summary>
    [NativeMethod("glDeleteTextures")]
    public static void DeleteTexture(Texture texture)
    {
        glDeleteTextures(1, &texture);
        ReferenceCount(DebugObjectType.Texture, -1);
    }

    /// <summary>
    /// Delete a named vertex array object.
    /// </summary>
    [NativeMethod("glDeleteVertexArrays")]
    public static void DeleteVertexArray(VertexArray array)
    {
        glDeleteVertexArrays(1, &array);
        ReferenceCount(DebugObjectType.VertexArray, -1);
    }

	/// <summary>
	/// Delete a sync object.
	/// </summary>
    [NativeMethod("glDeleteSync")]
    public static void DeleteSync(Sync sync)
    {
        glDeleteSync(sync);
        ReferenceCount(DebugObjectType.Sync, -1);
    }
        
	/// <summary>
	/// Delete named buffer objects.
	/// </summary>
    [NativeMethod("glDeleteBuffers")]
    public static void DeleteBuffers(ReadOnlySpan<Buffer> buffers)
    {
        fixed (Buffer* ptr = &buffers.GetPinnableReference())
        {
            glDeleteBuffers(buffers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Buffer, -buffers.Length);
    }

	/// <summary>
	/// Delete framebuffer objects.
	/// </summary>
    [NativeMethod("glDeleteFramebuffers")]
    public static void DeleteFramebuffers(ReadOnlySpan<Framebuffer> framebuffers)
    {
        fixed (Framebuffer* ptr = &framebuffers.GetPinnableReference())
        {
            glDeleteFramebuffers(framebuffers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Framebuffer, -framebuffers.Length);
    }

	/// <summary>
	/// Delete named query objects.
	/// </summary>
    [NativeMethod("glDeleteQueries")]
    public static void DeleteQueries(ReadOnlySpan<Query> queries)
    {
        fixed (Query* ptr = &queries.GetPinnableReference())
        {
            glDeleteQueries(queries.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Query, -queries.Length);
    }

	/// <summary>
	/// Delete renderbuffer objects.
	/// </summary>
    [NativeMethod("glDeleteRenderbuffers")]
    public static void DeleteRenderbuffers(ReadOnlySpan<Renderbuffer> renderbuffers)
    {
        fixed (Renderbuffer* ptr = &renderbuffers.GetPinnableReference())
        {
            glDeleteRenderbuffers(renderbuffers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Renderbuffer, -renderbuffers.Length);
    }

	/// <summary>
	/// Delete named sampler objects.
	/// </summary>
    [NativeMethod("glDeleteSamplers")]
    public static void DeleteSamplers(ReadOnlySpan<Sampler> samplers)
    {
        fixed (Sampler* ptr = &samplers.GetPinnableReference())
        {
            glDeleteSamplers(samplers.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Sampler, -samplers.Length);
    }

	/// <summary>
	/// Delete named textures.
	/// </summary>
    [NativeMethod("glDeleteTextures")]
    public static void DeleteTextures(ReadOnlySpan<Texture> textures)
    {
        fixed (Texture* ptr = &textures.GetPinnableReference())
        {
            glDeleteTextures(textures.Length, ptr);
        }
        ReferenceCount(DebugObjectType.Texture, -textures.Length);
    }

	/// <summary>
	/// Delete vertex array objects.
	/// </summary>
    [NativeMethod("glDeleteVertexArrays")]
    public static void DeleteVertexArrays(ReadOnlySpan<VertexArray> arrays)
    {
        fixed (VertexArray* ptr = &arrays.GetPinnableReference())
        {
            glDeleteVertexArrays(arrays.Length, ptr);
        }
        ReferenceCount(DebugObjectType.VertexArray, -arrays.Length);
    }

	/// <summary>
	/// Determine if a name corresponds to a buffer object.
	/// </summary>
    [NativeMethod("glIsBuffer")]
    public static bool IsBuffer(Buffer? buffer) => buffer.HasValue && glIsBuffer(buffer.Value);

	/// <summary>
	/// Determine if a name corresponds to a framebuffer object.
	/// </summary>
    [NativeMethod("glIsFramebuffer")]
    public static bool IsFramebuffer(Framebuffer? framebuffer) => framebuffer.HasValue && glIsFramebuffer(framebuffer.Value);

	/// <summary>
	/// Determines if a name corresponds to a program object.
	/// </summary>
    [NativeMethod("glIsProgram")]
    public static bool IsProgram(Program? program) => program.HasValue && glIsProgram(program.Value);

	/// <summary>
	/// Determine if a name corresponds to a query object.
	/// </summary>
    [NativeMethod("glIsQuery")]
    public static bool IsQuery(Query? query) => query.HasValue && glIsQuery(query.Value);

	/// <summary>
	/// Determine if a name corresponds to a renderbuffer object.
	/// </summary>
    [NativeMethod("glIsRenderbuffer")]
    public static bool IsRenderbuffer(Renderbuffer? renderbuffer) => renderbuffer.HasValue && glIsRenderbuffer(renderbuffer.Value);

	/// <summary>
	/// Determine if a name corresponds to a sampler object.
	/// </summary>
    [NativeMethod("glIsSampler")]
    public static bool IsSampler(Sampler? sampler) => sampler.HasValue && glIsSampler(sampler.Value);

	/// <summary>
	/// Determines if a name corresponds to a shader object.
	/// </summary>
    [NativeMethod("glIsShader")]
    public static bool IsShader(Shader? shader) => shader.HasValue && glIsShader(shader.Value);

	/// <summary>
	/// Determine if a name corresponds to a sync object.
	/// </summary>
    [NativeMethod("glIsSync")]
    public static bool IsSync(Sync? sync) => sync.HasValue && glIsSync(sync.Value);

	/// <summary>
	/// Determine if a name corresponds to a texture.
	/// </summary>
    [NativeMethod("glIsTexture")]
    public static bool IsTexture(Texture? texture) => texture.HasValue && glIsTexture(texture.Value);

	/// <summary>
	/// Determine if a name corresponds to a vertex array object.
	/// </summary>
    [NativeMethod("glIsVertexArray")]
    public static bool IsVertexArray(VertexArray? array) => array.HasValue && glIsVertexArray(array.Value);
        
}
