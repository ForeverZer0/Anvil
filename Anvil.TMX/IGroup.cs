using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Represents an object that can enumerate map layers.
/// </summary>
[PublicAPI]
public interface IGroup : IEnumerable<Layer>
{
    /// <summary>
    /// Returns an enumerator for iterating through filtered <see cref="Layer"/> instances within this object.
    /// </summary>
    /// <param name="type">A bitfield indicating the layer type(s) to yield.</param>
    /// <param name="recursive">
    /// <c>true</c> to recursively search nested instances within child groups, otherwise <c>false</c> to only yield
    /// top-level instances.
    /// </param>
    /// <returns>A <see cref="Layer"/> enumerator.</returns>
    IEnumerable<Layer> Filter(LayerType type, bool recursive = false);
}