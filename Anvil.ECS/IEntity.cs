using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Represents a generic game object that acts as a container for <see cref="Component"/> objects, which define its
/// purpose and behavior.
/// </summary>
[PublicAPI]
public interface IEntity : IReadOnlyCollection<Component>, INamed
{
    /// <summary>
    /// Occurs when a component is added to the entity.
    /// </summary>
    event EventHandler<ComponentEventArgs> ComponentAdded;

    /// <summary>
    /// Occurs when a component is removed from the entity.
    /// </summary>
    event EventHandler<ComponentEventArgs> ComponentRemoved;

    /// <summary>
    /// Gets or sets a flag indicating if this entity will be updated.
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Adds a component of the specified type to the entity.
    /// </summary>
    /// <typeparam name="TComponent">A type that implements <see cref="Component"/> and has a parameterless constructor.</typeparam>
    /// <returns>A component instance of the specified type.</returns>
    TComponent Add<TComponent>() where TComponent : Component, new();

    /// <summary>
    /// Adds a component of the specified type to the entity.
    /// </summary>
    /// <param name="componentType">A type that implements <see cref="Component"/> and has a parameterless constructor.</param>
    /// <returns>A component instance of the specified type.</returns>
    Component Add(Type componentType);
    
    /// <summary>
    /// Removes a component of the specified type from the entity.
    /// </summary>
    /// <typeparam name="TComponent">A type that implements <see cref="Component"/> and has a parameterless constructor.</typeparam>
    /// <returns><c>true</c> if component was removed, otherwise <c>false</c> if no components of the given type were found.</returns>
    bool Remove<TComponent>() where TComponent : Component, new();

    /// <summary>
    /// Removes a component of the specified type from the entity.
    /// </summary>
    /// <param name="componentType">A type that implements <see cref="Component"/> and has a parameterless constructor.</param>
    /// <returns><c>true</c> if component was removed, otherwise <c>false</c> if no components of the given type were found.</returns>
    bool Remove(Type componentType);

    /// <summary>
    /// Gets the component of the specified type from the entity.
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns>The component instance of the specified type, or <c>null</c> if no matching type could be found.</returns>
    TComponent? GetComponent<TComponent>() where TComponent : Component, new();

    /// <summary>
    /// Gets the component of the specified type from the entity.
    /// </summary>
    /// <param name="componentType">A type that implements <see cref="Component"/> and has a parameterless constructor.</param>
    /// <returns>The component instance of the specified type, or <c>null</c> if no matching type could be found.</returns>
    Component? GetComponent(Type componentType);

    /// <summary>
    /// Gets a flag indicating if the specified component type is contained within the entity.
    /// </summary>
    /// <typeparam name="TComponent">A type that implements <see cref="Component"/> and has a parameterless constructor.</typeparam>
    /// <returns></returns>
    bool Contains<TComponent>() where TComponent : Component, new();

    /// <summary>
    /// Gets a flag indicating if the specified component type is contained within the entity.
    /// </summary>
    /// <param name="componentType">A type that implements <see cref="Component"/> and has a parameterless constructor.</param>
    /// <returns><c>true</c> if entity contains the specified component type, otherwise <c>false</c>.</returns>
    bool Contains(Type componentType);
}