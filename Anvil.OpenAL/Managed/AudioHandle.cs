using Anvil.Native;
using JetBrains.Annotations;

namespace Anvil.OpenAL.Managed;

/// <summary>
/// Abstract base class for managed wrappers of native OpenAL handles.
/// </summary>
[PublicAPI]
public abstract class AudioHandle : IDisposable
{
    
    /// <inheritdoc cref="IDisposable.Dispose"/>
    /// <param name="disposing">
    /// <c>true</c> when this method is being called explicitly or via a <c>using</c> patter, otherwise <c>false</c>
    /// if being invoked by the object finalizer during garbage collection.
    /// </param>
    protected abstract void Dispose(bool disposing);
    
    /// <summary>
    /// Gets a flag indicating if the underlying handle is a valid by querying the native API.
    /// </summary>
    public abstract bool IsValid { get; }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Object finalizer.
    /// </summary>
    ~AudioHandle()
    {
        Dispose(false);
    }
}

/// <summary>
/// Abstract base class for managed wrappers of native OpenAL handles.
/// </summary>
/// <typeparam name="T">A native OpenAL handle type.</typeparam>
[PublicAPI]
public abstract class AudioHandle<T> : AudioHandle, IEquatable<AudioHandle<T>> where T : unmanaged, IHandle32
{
    /// <summary>
    /// Gets the native handle this object wraps.
    /// </summary>
    public T Handle { get; }

    /// <summary>
    /// Creates a new instance, wrapping an existing <paramref name="handle"/> to an OpenAL object.
    /// </summary>
    /// <param name="handle">The handle to wrap.</param>
    private protected AudioHandle(T handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Implicit conversion of this wrapper to the integral value of the native handle.
    /// </summary>
    /// <param name="obj">The wrapper object to convert.</param>
    /// <returns>The native handle value.</returns>
    public static implicit operator int(AudioHandle<T> obj) => obj.Handle.Value;

    /// <summary>
    /// Implicit conversion of this wrapper to the underlying handle type.
    /// </summary>
    /// <param name="obj">The wrapper object to convert.</param>
    /// <returns>The native handle value.</returns>
    public static implicit operator T(AudioHandle<T> obj) => obj.Handle;

    /// <inheritdoc />
    public bool Equals(AudioHandle<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || Handle.Equals(other.Handle);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((AudioHandle<T>) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => Handle.GetHashCode();

    /// <summary>
    /// Determines whether two specified handles have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="AudioHandle{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="AudioHandle{T}"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(AudioHandle<T>? left, AudioHandle<T>? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified handles have different values.
    /// </summary>
    /// <param name="left">The first <see cref="AudioHandle{T}"/> to compare.</param>
    /// <param name="right">The second <see cref="AudioHandle{T}"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(AudioHandle<T>? left, AudioHandle<T>? right) => !Equals(left, right);
}