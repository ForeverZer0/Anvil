using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Represents an object that contains child properties.
/// </summary>
[PublicAPI]
public interface IPropertyContainer
{
    /// <summary>
    /// Gets a dictionary containing the <see cref="Property"/> instances.
    /// </summary>
    PropertySet Properties { get; }
}