using JetBrains.Annotations;

namespace Anvil.OpenGL.Managed;

/// <summary>
/// Describes an attribute variable within a <see cref="ShaderProgram"/>.
/// </summary>
/// <param name="Name">The name of the attribute variable.</param>
/// <param name="Location">The index of the attribute within the shader program.</param>
/// <param name="Size">The size of the attribute variable.</param>
/// <param name="Type">The data type of the attribute variable.</param>
[PublicAPI]
public record ShaderAttribute(string Name, int Location, int Size, AttributeType Type);