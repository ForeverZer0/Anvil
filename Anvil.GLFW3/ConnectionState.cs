using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the connection state of a hardware device.
/// </summary>
[PublicAPI]
public enum ConnectionState
{
    /// <summary>
    /// Device is connected.
    /// </summary>
    Connected = 0x00040001,
	
    /// <summary>
    /// Device is not detected and/or disconnected.
    /// </summary>
    Disconnected = 0x00040002
}