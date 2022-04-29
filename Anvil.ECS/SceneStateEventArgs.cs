using JetBrains.Annotations;

namespace Anvil.ECS;

/// <summary>
/// Arguments used for to describe the changing of state within a scene.
/// </summary>
[PublicAPI]
public sealed class SceneStateEventArgs : EventArgs
{
    /// <summary>
    /// Gets the previous state the scene just came from.
    /// </summary>
    public SceneState PreviousState { get; }
    
    /// <summary>
    /// Gets the new state that the scene just changed to.
    /// </summary>
    public SceneState NewState { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="previousState">The previous state the scene just came from.</param>
    /// <param name="newState">The new state that the scene just changed to.</param>
    public SceneStateEventArgs(SceneState previousState, SceneState newState)
    {
        PreviousState = previousState;
        NewState = newState;
    }
}