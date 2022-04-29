using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Base class for scene objects.
/// <para/>
/// A scene is where the entire entity-component-system converges into a single object, defining each system, entity,
/// and component to build a cohesive game logic that works together.
/// </summary>
[PublicAPI]
public abstract class Scene : IEntityManager, ISystemManager, IDisposable, INamed
{
    /// <inheritdoc />
    public event EventHandler<EntityEventArgs>? EntityAdded;
    
    /// <inheritdoc />
    public event EventHandler<EntityEventArgs>? EntityRemoved;
    
    /// <inheritdoc />
    public event EventHandler<SystemEventArgs>? SystemAdded;
    
    /// <inheritdoc />
    public event EventHandler<SystemEventArgs>? SystemRemoved;

    /// <summary>
    /// Occurs when the state of the scene is changed.
    /// </summary>
    public event EventHandler<SceneStateEventArgs>? StateChanged;

    /// <summary>
    /// Gets the current state of the scene.
    /// </summary>
    public SceneState State
    {
        get => sceneState;
        private set
        {
            if (sceneState == value)
                return;
            var current = value;
            sceneState = value;
            OnStateChanged(current, value);
        }
    }

    /// <summary>
    /// Gets the name of the scene.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="Scene"/> class.
    /// </summary>
    protected Scene(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or only whitespace.", nameof(name));
        
        entitySet = new HashSet<IEntity>();
        systemList = new List<ISystem>();
        sceneState = SceneState.Initial;
    }

    /// <summary>
    /// Asynchronously initializes the systems, entities, and components required by the scene.
    /// </summary>
    /// <exception cref="InvalidOperationException">When scene has already been initialized.</exception>
    /// <remarks>
    /// The scene <see cref="State"/> will transition to <see cref="SceneState.Suspended"/> once all operations have
    /// completed.
    /// </remarks>
    /// <seealso cref="Start"/>
    public async Task InitializeAsync()
    {
        var systemTask = Task.Factory.StartNew(InitializeSystems);
        var entityTask = Task.Factory.StartNew(InitializeEntities);

        await systemTask;
        await entityTask;

        systemList.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        foreach (var entity in entitySet)
        {
            foreach (var system in systemList)
                system.TryAddComponents(entity);
            entity.Active = true;
        }

        foreach (var system in systemList)
            system.Active = true;

        State = SceneState.Suspended;

    }

    /// <summary>
    /// Initializes the systems, entities, and components required by the scene.
    /// </summary>
    /// <exception cref="InvalidOperationException">When scene has already been initialized.</exception>
    /// <remarks>
    /// The scene <see cref="State"/> will transition to <see cref="SceneState.Suspended"/> once all operations have
    /// completed.
    /// </remarks>
    /// <seealso cref="Start"/>
    public void Initialize()
    {
        if (State != SceneState.Initial)
            throw new InvalidOperationException("Scene is already initialized.");
            
        InitializeSystems();
        InitializeEntities();
        
        systemList.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        foreach (var entity in entitySet)
        {
            // While iterating through each system and checking each component can be an expensive operation (depending
            // on the scene), it gets performed once during scene initialization, and the computed information is then
            // stored, allowing systems to only iterate over exactly what components they require each update, with no
            // additional overhead searching/querying for additional data. 
            
            foreach (var system in systemList)
                system.TryAddComponents(entity);
            entity.Active = true;
        }

        foreach (var system in systemList)
            system.Active = true;

        State = SceneState.Suspended;
    }

    /// <summary>
    /// Called during scene setup, creates ands adds all systems used within this scene.
    /// </summary>
    /// <remarks>
    /// This method is strictly for creating systems and adding them to the scene, and should not attempt to access
    /// entities, etc, as they may not yet exist, or even being created concurrently.
    /// </remarks>
    protected abstract void InitializeSystems();

    /// <summary>
    /// Called during initialization, creates entities that should exist at the start of the scene, adds/configures
    /// their components, and adds them to the scene.
    /// </summary>
    /// <remarks>
    /// This method is strictly for creating entities and their components and adding them to the scene, and should not
    /// attempt to access any systems, as they may not yet exist, or even being created concurrently.
    /// </remarks>
    protected abstract void InitializeEntities();

    /// <summary>
    /// Sets the scene as the active scene to receive updates.
    /// </summary>
    /// <exception cref="InvalidOperationException">When the scene is in an initial or closing state.</exception>
    public void Start()
    {
        switch (State)
        {
            case SceneState.Initial:
                throw new InvalidOperationException("Scene is not initialized.");
            case SceneState.Running:
                return;
            case SceneState.Suspended:
                State = SceneState.Running;
                break;
            case SceneState.Closing:
                throw new InvalidOperationException("Scene has been closed.");
        }
    }

    /// <summary>
    /// Suspends updating of the scene while preserving its current state, allowing it to be resumed.
    /// </summary>
    /// <exception cref="InvalidOperationException">When the scene is in an initial or closing state.</exception>
    public void Suspend()
    {
        switch (State)
        {
            case SceneState.Initial:
                throw new InvalidOperationException("Scene is not initialized.");
            case SceneState.Running:
                State = SceneState.Suspended;
                break;
            case SceneState.Suspended:
                return;
            case SceneState.Closing:
                throw new InvalidOperationException("Scene has been closed.");
        }
    }
    
    /// <summary>
    /// Permanently terminates processing of the scene. 
    /// </summary>
    /// <remarks>
    /// A closed scene cannot be resumed, and any further interaction with the scene will result in undefined behavior.
    /// </remarks>
    public void Close() => State = SceneState.Closing;
    
    private void OnEntityComponentChanged(object? sender, ComponentEventArgs e)
    {
        if (sender is not IEntity entity) 
            return;
        
        foreach (var system in systemList)
        {
            // Skip systems that do not interact with the component type in any way.
            if (!system.ComponentTypes.Contains(e.Component.GetType()))
                continue;
            system.RemoveAll(c => c.Parent == entity);
            system.TryAddComponents(entity);
        }
    }

    /// <inheritdoc />
    [ContractAnnotation("entity:null => false")]
    public bool AddEntity(IEntity? entity)
    {
        if (entity is null || !entitySet.Add(entity))
            return false;
        entity.ComponentAdded += OnEntityComponentChanged;
        entity.ComponentRemoved += OnEntityComponentChanged;
        if (State != SceneState.Initial)
            OnEntityAdded(entity);
        return true;
    }

    /// <inheritdoc />
    [ContractAnnotation("entity:null => false")]
    public bool RemoveEntity(IEntity? entity)
    {
        if (entity is null || !entitySet.Remove(entity))
            return false;
        entity.ComponentAdded -= OnEntityComponentChanged;
        entity.ComponentRemoved -= OnEntityComponentChanged;
        
        foreach (var system in systemList)
            system.RemoveAll(c => c.Parent == entity);
        if (State != SceneState.Initial)
            OnEntityRemoved(entity);
        return true;
    }

    /// <inheritdoc />
    public int EntityCount => entitySet.Count;

    /// <inheritdoc />
    public int SystemCount => systemList.Count;

    /// <inheritdoc />
    [ContractAnnotation("system:null => false")]
    public bool AddSystem(ISystem? system, int priority)
    {
        if (system is null || systemList.Contains(system))
            return false;
        
        systemList.Add(system);
        if (State != SceneState.Initial)
            OnSystemAdded(system);
        return true;
    }

    /// <inheritdoc />
    [ContractAnnotation("system:null => false")]
    public virtual bool RemoveSystem(ISystem? system)
    {
        if (system is null || !systemList.Remove(system))
            return false;
        
        if (State != SceneState.Initial)
            OnSystemRemoved(system);
        return true;
    }

    /// <inheritdoc />
    public void Update(float delta)
    {
        if (State != SceneState.Running)
            return;
        
        foreach (var system in systemList)
        {
            if (!system.Active)
                continue;
            system.Update(delta);
        }
    }

    /// <inheritdoc />
    public IEnumerable<ISystem> GetSystems()
    {
        foreach (var system in systemList)
            yield return system;
    }

    /// <summary>
    /// Invokes the <see cref="EntityAdded"/> even when an entity has been added to the scene.
    /// </summary>
    /// <param name="entity">The entity that was added.</param>
    protected virtual void OnEntityAdded(IEntity entity)
    {
        EntityAdded?.Invoke(this, new EntityEventArgs(entity));
    }

    /// <summary>
    /// Invokes the <see cref="EntityRemoved"/> event when an entity has been removed from the scene.
    /// </summary>
    /// <param name="entity">The entity that was removed.</param>
    protected virtual void OnEntityRemoved(IEntity entity)
    {
        EntityRemoved?.Invoke(this, new EntityEventArgs(entity));
    }

    /// <summary>
    /// Invokes the <see cref="SystemAdded"/> event when a system has been added to the scene.
    /// </summary>
    /// <param name="system">The system that was added.</param>
    protected virtual void OnSystemAdded(ISystem system)
    {
        SystemAdded?.Invoke(this, new SystemEventArgs(system));
    }

    /// <summary>
    /// Invokes the <see cref="SystemRemoved"/> event when a system has been added to the scene.
    /// </summary>
    /// <param name="system">The system that was added.</param>
    protected virtual void OnSystemRemoved(ISystem system)
    {
        SystemRemoved?.Invoke(this, new SystemEventArgs(system));
    }
    
    /// <summary>
    /// Invokes the <see cref="StateChanged"/> event.
    /// </summary>
    /// <param name="previousState">The previous state the scene just came from.</param>
    /// <param name="newState">The new state that the scene just changed to.</param>
    protected virtual void OnStateChanged(SceneState previousState, SceneState newState)
    {
        StateChanged?.Invoke(this, new SceneStateEventArgs(previousState, newState));
    }

    /// <summary>
    /// Cleans up unmanaged resources present in the scene, including systems, entities, and components that implement
    /// the <see cref="IDisposable"/> interface.
    /// </summary>
    /// <param name="disposing"><c>true</c> when being called explicitly, otherwise <c>false</c> if being invoked
    /// by the finalizer.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) 
            return;
        
        // ReSharper disable SuspiciousTypeConversion.Global
        foreach (var system in systemList)
        {
            if (system is IDisposable disposableSystem)
                disposableSystem.Dispose();
        }

        foreach (var entity in entitySet)
        {
            foreach (var component in entity)
            {
                if (component is IDisposable disposableComponent)
                    disposableComponent.Dispose();
            }
            if (entity is IDisposable disposableEntity)
                disposableEntity.Dispose();
        }
        // ReSharper restore SuspiciousTypeConversion.Global
    }

    /// <inheritdoc />
    /// <remarks>
    /// Any systems, entity, or component that implements <see cref="IDisposable"/> and is present in
    /// the scene will also be disposed automatically.</remarks>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private readonly HashSet<IEntity> entitySet;
    private readonly List<ISystem> systemList;
    private SceneState sceneState;
}