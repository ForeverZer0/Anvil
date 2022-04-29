using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Arguments to be supplied with entity-related events.
/// </summary>
[PublicAPI]
public sealed class EntityEventArgs : EventArgs
{
    /// <summary>
    /// The entity that invoked the event.
    /// </summary>
    public IEntity Entity { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="EntityEventArgs"/> class.
    /// </summary>
    /// <param name="entity">The entity that invoked the event.</param>
    public EntityEventArgs(IEntity entity)
    {
        Entity = entity;
    }
}