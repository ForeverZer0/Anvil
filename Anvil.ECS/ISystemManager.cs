using JetBrains.Annotations;

namespace Anvil.ECS;


/// <summary>
/// Represents an object that manages a collection of systems.
/// </summary>
[PublicAPI]
public interface ISystemManager
{
    /// <summary>
    /// Occurs when a system has been added to this object.
    /// </summary>
    public event EventHandler<SystemEventArgs> SystemAdded;

    /// <summary>
    /// Occurs when a system has been removed from this object.
    /// </summary>
    public event EventHandler<SystemEventArgs> SystemRemoved; 
    
    /// <summary>
    /// Gets the number of <see cref="ISystem"/> instances being managed by this object.
    /// </summary>
    int SystemCount { get; }

    /// <summary>
    /// Adds a system object to be managed by this object.
    /// </summary>
    /// <param name="system">The system to be managed.</param>
    /// <param name="priority">A value denoting the priority of the system, determining the order in which it gets
    /// updated relative to other active systems in the scene. Higher values have higher priority.</param>
    /// <returns><c>true</c> if <paramref name="system"/> was added to the manager, otherwise <c>false</c> if it
    /// is already being managed by this object or is <c>null</c>.</returns>
    [ContractAnnotation("system:null => false")]
    bool AddSystem(ISystem? system, int priority = 0);
    
    /// <summary>
    /// Removes a system object that is being managed by this object.
    /// </summary>
    /// <param name="system">The system to be removed.</param>
    /// <returns><c>true</c> if <paramref name="system"/> was removed, otherwise <c>false if it was not
    /// being managed or is <c>null</c>.</c></returns>
    [ContractAnnotation("system:null => false")]
    bool RemoveSystem(ISystem? system);

    /// <summary>
    /// Performs a frame update for all active systems being managed by this instance.
    /// </summary>
    /// <param name="delta">The elapsed time in seconds since the previous frame.</param>
    void Update(float delta);

    /// <summary>
    /// Gets a collection of all systems being managed by this object.
    /// </summary>
    /// <returns>An enumerator for the collection of systems being managed.</returns>
    IEnumerable<ISystem> GetSystems();
}