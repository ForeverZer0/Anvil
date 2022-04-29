using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Represents an object that has a human-friendly <see cref="Name"/> property for identification.
/// </summary>
[PublicAPI]
public interface INamed
{
    /// <summary>
    /// Gets the name of the object.
    /// </summary>
    string Name { get; }
}