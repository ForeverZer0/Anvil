using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Represents a class that provides logic and updates to <see cref="Component"/> objects.
/// </summary>
[PublicAPI]
public interface ISystem
{
    /// <summary>
    /// Gets or sets the priority in which this system will be updated within a scene.
    /// </summary>
    int Priority { get; set; }
    
    /// <summary>
    /// Gets the number of components/archetypes being managed by this system.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets or sets a flag indicating if this system will be updated.
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Performs a regular frame update, updating all components this system manages.
    /// </summary>
    /// <param name="delta">The elapsed time in seconds since the previous frame.</param>
    void Update(float delta);
    
    /// <summary>
    /// Removes all components/archetypes that where the given <paramref name="predicate"/> returns <c>true</c>.
    /// </summary>
    /// <param name="predicate">A predicate that evaluates for every component within the system.</param>
    /// <returns>The number of components that were removed.</returns>
    int RemoveAll(Predicate<Component> predicate);

    /// <summary>
    /// Gets an array of component type(s) this system manages. 
    /// </summary>
    Type[] ComponentTypes { get; }

    /// <summary>
    /// Checks the given <paramref name="entity"/> for the presence of components managed by this system, and adds
    /// them when found.
    /// </summary>
    /// <param name="entity">The entity instance to query.</param>
    /// <returns><c>true</c> if entity contained components to be managed, otherwise <c>false</c>.</returns>
    bool TryAddComponents(IEntity entity);
}

public abstract class SystemBase<T> : ISystem, IEnumerable<T>
{
    private protected readonly List<T> Components = new();

    /// <inheritdoc />
    public int Priority { get; set; }

    /// <inheritdoc />
    public int Count => Components.Count;

    /// <inheritdoc />
    public bool Active { get; set; }

    /// <inheritdoc />
    public abstract void Update(float delta);

    /// <inheritdoc />
    public abstract int RemoveAll(Predicate<Component> predicate);

    /// <summary>
    /// Adds a component/archetype to the system to be managed.
    /// </summary>
    /// <param name="component">The <see cref="Component"/> or <see cref="Tuple"/> of components to add.</param>
    public void Add(T component)
    {
        if (Components.Contains(component))
            return;
        Components.Add(component);
    }

    /// <summary>
    /// Removes the component/archetype from the system.
    /// </summary>
    /// <param name="component">The component to remove.</param>
    /// <returns><c>true</c> if the component was removed, otherwise <c>false</c> if were not found or <c>null</c>.</returns>
    public bool Remove(T? component) => component is not null && Components.Remove(component);

    /// <inheritdoc />
    public abstract Type[] ComponentTypes { get; }

    /// <inheritdoc />
    public abstract bool TryAddComponents(IEntity entity);

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in Components)
            yield return item;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public abstract class ArchetypeSystem<TTuple> : SystemBase<TTuple> where TTuple : struct, ITuple
{
    protected ArchetypeSystem()
    {
        ComponentTypes = typeof(TTuple).GetGenericArguments();
        if (!ComponentTypes.All(c => c.IsAssignableTo(typeof(Component))))
            throw new ConstraintException("Archetype tuple must contain only component types.");
    }

    /// <inheritdoc />
    public sealed override Type[] ComponentTypes { get; }

    /// <inheritdoc />
    public override int RemoveAll(Predicate<Component> predicate)
    {
        return Components.RemoveAll(tuple =>
        {
            for (var i = 0; i < tuple.Length; i++)
            {
                if (tuple[i] is Component component && predicate.Invoke(component))
                    return true;
            }
            return false;
        });
    }
    
    /// <inheritdoc />
    public override bool TryAddComponents(IEntity entity)
    {
        var length = ComponentTypes.Length;
        var c = new Component[length];
        for (var i = 0; i < length; i++)
        {
            var comp = entity.GetComponent(ComponentTypes[i]);
            if (comp is null)
                return false;
            c[i] = comp;
        }
        
        ITuple tuple = length switch
        {
            2 => Tuple.Create(c[0], c[1]),
            3 => Tuple.Create(c[0], c[1], c[2]),
            4 => Tuple.Create(c[0], c[1], c[2], c[3]),
            5 => Tuple.Create(c[0], c[1], c[2], c[3], c[4]),
            6 => Tuple.Create(c[0], c[1], c[2], c[3], c[4], c[5]),
            7 => Tuple.Create(c[0], c[1], c[2], c[3], c[4], c[5], c[6]),
            8 => Tuple.Create(c[0], c[1], c[2], c[3], c[4], c[5], c[6], c[7]),
            1 => Tuple.Create(c[0]),
            _ => throw new ArgumentOutOfRangeException(nameof(entity), "Archetypes may only contain 1-7 types.")
        };
        
        Add((TTuple) tuple);
        return true;
    }
}

public abstract class System<T> : SystemBase<T> where T : Component, new()
{
    protected System()
    {
        ComponentTypes = new[] {typeof(T)};
    }
    
    /// <inheritdoc />
    public sealed override Type[] ComponentTypes { get; }

    /// <inheritdoc />
    public override int RemoveAll(Predicate<Component> predicate) => Components.RemoveAll(predicate);
    
    /// <inheritdoc />
    public override bool TryAddComponents(IEntity entity)
    {
        var component = entity.GetComponent<T>();
        if (component is null)
            return false;
        Add(component);
        return true;
    }
}