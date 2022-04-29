using JetBrains.Annotations;

namespace Anvil.Native;

/// <summary>
/// Indicates a native/unmanaged function that the annotated method or property makes a direct call to.
/// </summary>
[PublicAPI, AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public class NativeMethodAttribute : Attribute
{
    /// <summary>
    /// The name of the native function.
    /// </summary>
    public readonly string Name;
        
    /// <summary>
    /// Creates a new instance of the <see cref="NativeMethodAttribute"/> class.
    /// </summary>
    /// <param name="name">The name of a native/unmanaged function that is invoked.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is <see langword="null"/>.</exception>
    public NativeMethodAttribute(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}