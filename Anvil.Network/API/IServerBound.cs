using JetBrains.Annotations;

namespace Anvil.Network.API;

/// <summary>
/// Represent an object that is sent to a server with a unique identifier.
/// </summary>
/// <typeparam name="T">An enumeration/integer type suitable for identification.</typeparam>
[PublicAPI]
public interface IServerBound<out T> where T : unmanaged, IComparable<T>, IComparable, IEquatable<T>
{
    /// <summary>
    /// Gets a unique identifier to indicate what this object is.
    /// </summary>
    T ServerId { get; }
}