using System.Reflection;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Arguments to supply for events that are invoked when an external resource cannot be located automatically, and
/// needs resolved.
/// </summary>
[PublicAPI]
public sealed class ResourceResolveEventArgs : EventArgs
{
    /// <summary>
    /// Gets the assembly containing an embedded resource, or <c>null</c> when <see cref="Path"/> is a file system path.
    /// </summary>
    public Assembly? Assembly { get; }
    
    /// <summary>
    /// Gets a constant describing the type of resource required.
    /// </summary>
    public ResourceType Type { get; }
    
    /// <summary>
    /// Gets the defined path for the resource that could not be found.
    /// <para/>
    /// When <see cref="Assembly"/> is defined, this may be a namespaces path to an embedded resource, otherwise it
    /// may be assumed to be a file system path.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Gets a flag indicating if the resource has been found.
    /// <para/>
    /// For multicast delegates, previous subscribers to the same event may have already resolved the path, so this
    /// value should be first queried before taking any action.
    /// </summary>
    public bool Found => Stream is not null;
    
    /// <summary>
    /// Gets or sets an open <see cref="Stream"/> containing the resource.
    /// <para/>
    /// This stream will be automatically disposed by the caller, and should not be disposed by subscribers.
    /// </summary>
    public Stream? Stream { get; set; }

    internal ResourceResolveEventArgs(Assembly? assembly, string path, ResourceType type)
    {
        Assembly = assembly;
        Path = path;
        Stream = null;
        Type = type;
    }
}