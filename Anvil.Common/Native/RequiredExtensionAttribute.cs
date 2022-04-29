using JetBrains.Annotations;

namespace Anvil.Native;

/// <summary>
/// Indicates that the marked symbol requires an extended feature of the library that may not be supported on all
/// platforms and should be queried before it is used.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event)]
public class RequiredExtensionAttribute : Attribute
{
    /// <summary>
    /// The name of the required extension.
    /// </summary>
    public string Name;

    /// <summary>
    /// Creates a new instance of the <see cref="RequiredExtensionAttribute"/> class.
    /// </summary>
    /// <param name="extensionName">The name of the extension that is required for this feature's support.</param>
    public RequiredExtensionAttribute(string extensionName)
    {
        Name = extensionName;
    }
}