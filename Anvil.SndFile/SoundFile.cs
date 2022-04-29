using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.SndFile;

/// <summary>
/// Opaque handle for a native sound file.
/// </summary>
/// <param name="Value">The handle value used by the implementation.</param>
[PublicAPI]
public record struct SoundFile(IntPtr Value) : IHandle;