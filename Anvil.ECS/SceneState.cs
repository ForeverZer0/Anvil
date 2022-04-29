namespace Anvil.ECS;

/// <summary>
/// Describes the current state of a <see cref="Scene"/> object.
/// </summary>
public enum SceneState
{
    /// <summary>
    /// Initial state of a scene, either before or during the initialization process.
    /// </summary>
    Initial,
    
    /// <summary>
    /// The scene is actively running and receiving updates.
    /// </summary>
    Running,
    
    /// <summary>
    /// The scene has been paused, and is ready to be updated, but not currently active. 
    /// </summary>
    Suspended,
    
    /// <summary>
    /// The scene has been marked as closed, is no longer updated, and has begin/finished scene termination.
    /// </summary>
    Closing
}