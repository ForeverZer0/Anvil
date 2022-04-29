using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Opaque monitor handle.
/// <para/>
/// This is merely a type-safe <see cref="IntPtr"/>.
/// </summary>
/// <param name="Value">The native pointer value.</param>
[PublicAPI]
public record struct Monitor(IntPtr Value) : IHandle;