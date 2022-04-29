using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Describes the state of a joystick hat.
/// </summary>
[PublicAPI, Flags]
public enum Hat
{
	/// <summary>
	/// Centered in the "default" or "none" position.
	/// </summary>
	Centered = 0,
	
	/// <summary>
	/// Up
	/// </summary>
	Up = 1,
	
	/// <summary>
	/// Right
	/// </summary>
	Right = 2,
	
	/// <summary>
	/// Down
	/// </summary>
	Down = 4,
	
	/// <summary>
	/// Left
	/// </summary>
	Left = 8,
	
	/// <summary>
	/// <see cref="Right"/> and <see cref="Up"/>
	/// </summary>
	RightUp = Right | Up,
	
	/// <summary>
	/// <see cref="Right"/> and <see cref="Down"/>
	/// </summary>
	RightDown = Right | Down,
	
	/// <summary>
	/// <see cref="Left"/> and <see cref="Up"/>
	/// </summary>
	LeftUp =   Left | Up,
	
	/// <summary>
	/// <see cref="Left"/> and <see cref="Down"/>
	/// </summary>
	LeftDown = Left | Down
}