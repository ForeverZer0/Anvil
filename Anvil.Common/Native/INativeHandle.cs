namespace Anvil.Native;

/// <summary>
/// Represents an object that encapsulates a handle to a native unmanaged type.
/// </summary>
/// <typeparam name="T">An unmanaged type.</typeparam>
public interface INativeHandle<out T> where T : unmanaged
{
    /// <summary>
    /// Gets the value of the native handle.
    /// </summary>
    T Value { get; }
}

/// <summary>
/// Represents an object that encapsulates a 32-bit handle to a native unmanaged type.
/// </summary>
public interface IHandle32 : INativeHandle<int>, IEquatable<IHandle32>
{
    /// <inheritdoc />
    bool IEquatable<IHandle32>.Equals(IHandle32? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Value.Equals(other.Value);
    }
}

/// <summary>
/// Represents an object that encapsulates a handle to a native pointer-sized type.
/// </summary>
public interface IHandle : INativeHandle<IntPtr>, IEquatable<IHandle>
{
    /// <inheritdoc />
    bool IEquatable<IHandle>.Equals(IHandle? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Value.Equals(other.Value);
    }
}