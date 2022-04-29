using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Represents an object that manages a collection of entities.
/// </summary>
[PublicAPI]
public interface IEntityManager
{
    /// <summary>
    /// Occurs when an entity is added.
    /// </summary>
    event EventHandler<EntityEventArgs>? EntityAdded;
    
    /// <summary>
    /// Occurs when an entity is removed.
    /// </summary>
    event EventHandler<EntityEventArgs>? EntityRemoved;

    /// <summary>
    /// Adds an entity to the collection.
    /// </summary>
    /// <param name="entity">The entity instance to add.</param>
    /// <returns><c>true</c> if entity was added, otherwise <c>false</c> if it already exists in the collection.</returns>
    [ContractAnnotation("entity:null => false")]
    bool AddEntity(IEntity? entity);

    /// <summary>
    /// Removes an entity from the collection.
    /// </summary>
    /// <param name="entity">The entity instance to remove.</param>
    /// <returns><c>true</c> if entity was removed, otherwise <c>false</c> if was not found in the collection.</returns>
    [ContractAnnotation("entity:null => false")]
    bool RemoveEntity(IEntity? entity);

    /// <summary>
    /// Gets total number of entities in the collection.
    /// </summary>
    int EntityCount { get; }
}