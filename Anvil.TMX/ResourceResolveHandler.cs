using System.Reflection;

namespace Anvil.TMX;

/// <summary>
/// Delegate for methods that can handle finding unresolved resource paths, and returning them as an open stream.
/// </summary>
/// <param name="assembly">
/// The assembly containing the embedded resource, or <c>null</c> when the <paramref name="path"/> is a filesystem path.
/// </param>
/// <param name="path">
/// When <paramref name="assembly"/> is <c>null</c>, indicates a filesystem name/path, otherwise it represents the
/// name/path of an embedded resource.
/// </param>
/// <param name="type">A value describing the type of resource being requested.</param>
/// <returns>
/// A <see cref="Stream"/> representing the resource that is opened for reading , or <c>null</c> if was not resolved.
/// </returns>
/// <remarks>
/// When returning a stream, it should not be created via a <c>using</c> pattern or otherwise modified in any way by
/// the subscriber once opened. The disposal of the stream will be handled by the caller.
/// </remarks>
public delegate Stream? ResourceResolveHandler(Assembly? assembly, string path, ResourceType type);