using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Arguments to be supplied with system-related events.
/// </summary>
[PublicAPI]
public sealed class SystemEventArgs
{
    /// <summary>
    /// The system that invoked the event.
    /// </summary>
    public ISystem System { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="SystemEventArgs"/> class.
    /// </summary>
    /// <param name="system">The system that invoked the event.</param>
    public SystemEventArgs(ISystem system)
    {
        System = system;
    }
}