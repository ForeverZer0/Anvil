using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Arguments to be supplied with component-related events.
/// </summary>
[PublicAPI]
public sealed class ComponentEventArgs : EventArgs
{
    /// <summary>
    /// The component that invoked the event.
    /// </summary>
    public Component Component { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="ComponentEventArgs"/> class.
    /// </summary>
    /// <param name="component">The component that invoked the event.</param>
    public ComponentEventArgs(Component component)
    {
        Component = component;
    }
}