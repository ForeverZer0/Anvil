using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.OpenAL;

/// <summary>
/// Type-safe handle for an OpenAL source object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Source(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenAL buffer object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Buffer(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenAL filter object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Filter(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenAL effect object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct Effect(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenAL auxiliary effect object.
/// </summary>
/// <param name="Value">The integer handle value used by the implementation.</param>
[PublicAPI]
public record struct EffectSlot(int Value) : IHandle32;

/// <summary>
/// Type-safe handle for an OpenAL device object.
/// </summary>
/// <param name="Value">The handle value used by the implementation.</param>
[PublicAPI]
public record struct Device(IntPtr Value) : IHandle;

/// <summary>
/// Type-safe handle for an OpenAL context object.
/// </summary>
/// <param name="Value">The pointer handle value used by the implementation.</param>
[PublicAPI]
public record struct Context(IntPtr Value) : IHandle;