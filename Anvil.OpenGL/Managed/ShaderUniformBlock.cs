using JetBrains.Annotations;

namespace Anvil.OpenGL.Managed;

/// <summary>
/// Describes a uniform block within a <see cref="ShaderProgram"/>.
/// </summary>
/// <param name="Name">The name of the uniform block.</param>
/// <param name="Index">The index of the uniform block within the program.</param>
/// <param name="Binding">The binding point within the index.</param>
/// <param name="Size">The total size of the uniform block in basic machine units (i.e. bytes).</param>
/// <param name="Uniforms">An array of the uniforms contained within this block.</param>
/// <param name="UsedByVertex">Flag indicating if the uniform block is referenced by the vertex shader stage of the program.</param>
/// <param name="UsedByFragment">Flag indicating if the uniform block is referenced by the fragment shader stage of the program.</param>
/// <param name="UsedByGeometry">Flag indicating if the uniform block is referenced by the geometry shader stage of the program.</param>
[PublicAPI]
public record ShaderUniformBlock(string Name, int Index, int Binding, int Size, ShaderUniform[] Uniforms, bool UsedByVertex, bool UsedByFragment, bool UsedByGeometry);