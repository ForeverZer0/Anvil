using System.Collections;
using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// A concrete implementation of <see cref="IEntity"/> suitable for most purposes.
/// </summary>
[PublicAPI]
public class Entity : IEntity, IEquatable<Entity>
{
    private static int idCounter;

    private readonly int id;
    private readonly List<Component> components;

    /// <summary>
    /// Creates a new instance of the <see cref="Entity"/> class with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public Entity(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or only whitespace.", nameof(name));
        components = new List<Component>();
        id = unchecked(++idCounter);
    }
    
    /// <inheritdoc />
    public IEnumerator<Component> GetEnumerator()
    {
        foreach (var component in components)
            yield return component;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public int Count => components.Count;

    /// <inheritdoc />
    public event EventHandler<ComponentEventArgs>? ComponentAdded;

    /// <inheritdoc />
    public event EventHandler<ComponentEventArgs>? ComponentRemoved;

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public bool Active { get; set; }

    /// <inheritdoc />
    public TComponent Add<TComponent>() where TComponent : Component, new()
    {
        var component = components.Find(c => c.GetType() == typeof(TComponent));
        if (component != null)
            return (TComponent)component;

        component = Component.Factory<TComponent>();
        components.Add(component);
        ComponentAdded?.Invoke(this, new ComponentEventArgs(component));
        return (TComponent) component;
    }

    /// <inheritdoc />
    public Component Add(Type componentType)
    {
        var component = components.Find(c => c.GetType() == componentType);
        if (component != null)
            return component;
        
        component = Component.Factory(componentType);
        components.Add(component);
        ComponentAdded?.Invoke(this, new ComponentEventArgs(component));
        return component;
    }

    /// <inheritdoc />
    public bool Remove<TComponent>() where TComponent : Component, new()
    {
        var component = components.Find(c => c.GetType() == typeof(TComponent));
        if (component is null)
            return false;

        components.Remove(component);
        ComponentRemoved?.Invoke(this, new ComponentEventArgs(component));
        return true;
    }

    /// <inheritdoc />
    public bool Remove(Type componentType)
    {
        var component = components.Find(c => c.GetType() == componentType);
        if (component is null)
            return false;

        components.Remove(component);
        ComponentRemoved?.Invoke(this, new ComponentEventArgs(component));
        return true;
    }

    /// <inheritdoc />
    public TComponent? GetComponent<TComponent>() where TComponent : Component, new()
    {
        var component = components.Find(c => c.GetType() == typeof(TComponent));
        if (component != null) 
            return (TComponent) component;
        return null;
    }

    /// <inheritdoc />
    public Component? GetComponent(Type componentType)
    {
        return components.Find(c => c.GetType() == componentType);
    }

    /// <inheritdoc />
    public bool Contains<TComponent>() where TComponent : Component, new()
    {
        return components.Any(c => c.GetType() == typeof(TComponent));
    }

    /// <inheritdoc />
    public bool Contains(Type componentType)
    {
        return components.Any(c => c.GetType() == componentType);
    }

    /// <inheritdoc />
    public bool Equals(Entity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return id == other.id && Name == other.Name;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Entity) obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(id, Name);

    public static bool operator ==(Entity? left, Entity? right) => Equals(left, right);

    public static bool operator !=(Entity? left, Entity? right) => !Equals(left, right);
}