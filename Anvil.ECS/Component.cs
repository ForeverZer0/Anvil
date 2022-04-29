using System.Collections.Concurrent;
using System.Data;
using System.Reflection.Emit;
using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Represents a generic data class containing information for a <see cref="ISystem"/> to process.
/// </summary>
/// <remarks>
/// Ideally any given component class will contain no logic, and just be a record class consisting of POD types. Due to
/// the archetype system, components must be implemented as reference types (classes) instead of value types (structs),
/// hence the inclusion of this as an abstract class over an interface.
/// </remarks>
[PublicAPI]
public abstract class Component : IEquatable<Component>
{
    /// <summary>
    /// Gets the parent <see cref="IEntity"/> this component is contained within.
    /// </summary>
    public IEntity? Parent { get; internal set; }
    
    /// <summary>
    /// Gets or sets a flag indicating if the data has changed or is invalid.
    /// </summary>
    public bool IsInvalid { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Component"/> class.
    /// </summary>
    protected Component()
    {
        id = unchecked(++idIncrementer);
    }

    /// <summary>
    /// Creates a new instance of the given component type.
    /// </summary>
    /// <typeparam name="TComponent">A subclass of <see cref="Component"/> with a parameterless constructor.</typeparam>
    /// <returns>A new instance of the given component type.</returns>
    public static TComponent Factory<TComponent>() where TComponent : Component, new()
    {
        return (TComponent) Factory(typeof(TComponent));
    }
    
    /// <summary>
    /// Creates a new instance of the given component type.
    /// </summary>
    /// <param name="type">A subclass of <see cref="Component"/> with a parameterless constructor.</param>
    /// <returns>A new instance of the given component type.</returns>
    public static Component Factory(Type type)
    {
        if (activators.TryGetValue(type, out var activator))
            return activator.Invoke();

        activator = Emit.Ctor<Func<Component>>(type);
        activators[type] = activator;
        return activator.Invoke();
    }

    /// <inheritdoc />
    public bool Equals(Component? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return id == other.id && Equals(Parent, other.Parent);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Component) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => id;
    
    public static bool operator ==(Component? left, Component? right) => Equals(left, right);

    public static bool operator !=(Component? left, Component? right) => !Equals(left, right);
    
    private readonly int id;
    private static int idIncrementer;
    private static ConcurrentDictionary<Type, Func<Component>> activators = new();
}