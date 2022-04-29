using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.OpenGL;

/// <summary>
/// Type-safe handle for an OpenGL program object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Program(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL shader object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Shader(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL query object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Query(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL buffer object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Buffer(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL framebuffer object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Framebuffer(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL renderbuffer object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Renderbuffer(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL sampler object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Sampler(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL texture object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Texture(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL vertex array object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct VertexArray(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenGL Sync object.
/// </summary>
/// <param name="Value">The handle value used by the implementation.</param>
[PublicAPI]
public record struct Sync(IntPtr Value) : IHandle;