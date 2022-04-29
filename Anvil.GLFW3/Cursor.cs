using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Opaque cursor object handle.
/// <para/>
/// This is merely a type-safe <see cref="IntPtr"/>.
/// </summary>
/// <param name="Value">The native pointer value.</param>
[PublicAPI]
public record struct Cursor(IntPtr Value) : IHandle;