using JetBrains.Annotations;

namespace Anvil.OpenGL.Managed;

/// <summary>
/// Describes a uniform variable within a <see cref="ShaderProgram"/>.
/// </summary>
/// <param name="Name">The name of the uniform variable.</param>
/// <param name="Location">The index of the uniform within the shader program.</param>
/// <param name="Size">The size of the uniform variable.</param>
/// <param name="Type">The data type of the uniform variable.</param>
[PublicAPI]
public record ShaderUniform(string Name, int Location, int Size, UniformType Type);