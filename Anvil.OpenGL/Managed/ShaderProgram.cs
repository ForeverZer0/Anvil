using System.Numerics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace Anvil.OpenGL.Managed;

/// <summary>
/// Object-oriented abstraction of an OpenGL shader program object.
/// <para/>
/// Provides methods for easily generating, compiling, and linking complete programs with a single invocation.
/// </summary>
[PublicAPI]
public class ShaderProgram : IDisposable
{
    private readonly Program program;
    private readonly Dictionary<string, int> uniformCache;

    private int? uniformCount;
    private int? attributeCount;
    private int? uniformBlockCount;

    /// <summary>
    /// Gets the currently active <see cref="ShaderProgram"/>, or <c>null</c> if none are active.
    /// </summary>
    public static ShaderProgram? Current
    {
        get
        {
            var handle = GL.GetInteger(GetPName.CurrentProgram);
            return handle > 0 ? new ShaderProgram(Unsafe.As<int, Program>(ref handle)) : null;
        }
    }

    /// <summary>
    /// Gets the native OpenGL name for this object.
    /// </summary>
    public Program Name { get; }
    
    /// <summary>
    /// Gets the number of active uniform variables for program.
    /// </summary>
    public int UniformCount
    {
        get
        {
            uniformCount ??= GL.GetProgram(program, ProgramProperty.ActiveUniforms);
            return uniformCount.Value;
        }
    }

    /// <summary>
    /// Gets the number of active attribute variables for program.
    /// </summary>
    public int AttributeCount
    {
        get
        {
            attributeCount ??= GL.GetProgram(program, ProgramProperty.ActiveAttributes);
            return attributeCount.Value;
        }
    }
    
    /// <summary>
    /// Gets the number of active uniform blocks in the program.
    /// </summary>
    public int UniformBlockCount
    {
        get
        {
            uniformBlockCount ??= GL.GetProgram(program, ProgramProperty.ActiveUniformBlocks);
            return uniformBlockCount.Value;
        }
    }

    /// <summary>
    /// Gets the implementation-generated info log for the shader program.
    /// </summary>
    public string? InfoLog => GL.GetProgramInfoLog(program);

    /// <summary>
    /// Gets the length in bytes of the implementation-generated info log for the shader program.
    /// </summary>
    public int InfoLogLength => GL.GetProgram(program, ProgramProperty.InfoLogLength);

    /// <summary>
    /// Copies the implementation-generated info log for the shader program into the specified <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">The buffer to receive the log.</param>
    /// <returns>The number of bytes written to the buffer.</returns>
    public int GetInfoLog(byte[] buffer)
    {
        GL.GetProgramInfoLog(program, out var length, buffer);
        return length;
    }

    /// <summary>
    /// Copies the implementation-generated info log for the shader program into the specified <paramref name="buffer"/>.
    /// </summary>
    /// <param name="buffer">The buffer to receive the log.</param>
    /// <returns>The number of bytes written to the buffer.</returns>
    public int GetInfoLog(Span<byte> buffer)
    {
        GL.GetProgramInfoLog(program, out var length, buffer);
        return length;
    }

    /// <summary>
    /// Creates a new <see cref="ShaderProgram"/> instance using the source code located in embedded resources within
    /// an <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly containing the embedded resource files.</param>
    /// <param name="vertexPath">The path to the vertex shader source code.</param>
    /// <param name="fragmentPath">The path to the fragment shader source code.</param>
    /// <param name="geometryPath">The optional path to the geometry shader source code.</param>
    /// <returns>A newly created <see cref="ShaderProgram"/> instance.</returns>
    /// <remarks>
    /// The paths follow the standard namespacing syntax used the assembly, using dots (".") to separate namespaces
    /// and/or subdirectories. For example, assume an assembly named "MyAssembly.Assets", with a root namespace of
    /// "MyAssembly", and our file named "SpriteVertex.glsl" is located within a subdirectory named "Shaders".
    /// <example>string path = "MyAssembly.Shaders.SpriteVertex.glsl";</example>
    /// The name of the assembly is ignored, the <b>root namespace</b> and directory hierarchy is how the path
    /// is defined within the assembly.
    /// </remarks>
    public static ShaderProgram Load(Assembly assembly, string vertexPath, string fragmentPath, string? geometryPath = null)
    {
        var vSrc = ReadText(assembly, vertexPath);
        var fSrc = ReadText(assembly, fragmentPath);
        var gSrc = geometryPath is null ? null : ReadText(assembly, geometryPath);
        return new ShaderProgram(vSrc, fSrc, gSrc);
    }
    
    /// <summary>
    /// Creates a new <see cref="ShaderProgram"/> instance using the source code located in files at the specified
    /// paths.
    /// </summary>
    /// <param name="vertexPath">The file path to the vertex shader source code.</param>
    /// <param name="fragmentPath">The file path to the fragment shader source code.</param>
    /// <param name="geometryPath">The optional file path to the geometry shader source code.</param>
    /// <returns>A newly created <see cref="ShaderProgram"/> instance.</returns>
    public static ShaderProgram Load(string vertexPath, string fragmentPath, string? geometryPath = null)
    {
        var vSrc = File.ReadAllText(vertexPath);
        var fSrc = File.ReadAllText(fragmentPath);
        var gSrc = geometryPath is null ? null : File.ReadAllText(geometryPath);
        return new ShaderProgram(vSrc, fSrc, gSrc);
    }
    
    /// <summary>
    /// Wraps an existing OpenGL program handle.
    /// </summary>
    /// <param name="handle">A valid handle to an OpenGL program object.</param>
    /// <exception cref="ArgumentException">When <paramref name="handle"/> is not a valid program object.</exception>
    public ShaderProgram(Program handle)
    {
        if (!GL.IsProgram(handle))
            throw new ArgumentException("The specified handle does not represent a valid program object.", nameof(handle));
        
        program = handle;
        uniformCache = new Dictionary<string, int>();
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="ShaderProgram"/> class from the specified GLSL source code.
    /// </summary>
    /// <param name="vertexSrc">The GLSL source code for the vertex shader stage.</param>
    /// <param name="fragmentSrc">The GLSL source code for the fragment shader stage.</param>
    /// <param name="geometrySrc">The optional GLSL source code for the geometry shader stage.</param>
    /// <exception cref="ShaderException">When the source code fails to compile or the units fail to link to the program.</exception>
    public ShaderProgram(string vertexSrc, string fragmentSrc, string? geometrySrc = null) : this(GL.CreateProgram())
    {
        Shader vertex = default;
        Shader fragment = default;
        Shader geometry = default;
        
        try
        {
            vertex = CompileAndAttach(program, vertexSrc, ShaderType.Vertex);
            fragment = CompileAndAttach(program, fragmentSrc, ShaderType.Fragment);
            geometry = CompileAndAttach(program, geometrySrc, ShaderType.Geometry);
            
            GL.LinkProgram(program);
            if (GL.GetProgram(program, ProgramProperty.LinkStatus) == GL.TRUE) 
                return;
            
            var log = GL.GetProgramInfoLog(program);
            throw new ShaderException("Failed to link shader program.", log);
        }
        finally
        {
            DetachAndDelete(program, vertex);
            DetachAndDelete(program, fragment);
            DetachAndDelete(program, geometry);
        }
    }

    private static Shader CompileAndAttach(Program program, string? source, ShaderType type)
    {
        if (source is null)
            return default;
        
        var shader = GL.CreateShader(type);
        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);

        if (GL.GetShader(shader, ShaderParameterName.CompileStatus) == GL.TRUE)
        {
            GL.AttachShader(program, shader);
            return shader;
        }
        
        var log = GL.GetShaderInfoLog(shader);
        GL.DeleteShader(shader);
        throw new ShaderException("Failed to compile shader source.", log);
    }

    private static void DetachAndDelete(Program program, Shader shader)
    {
        if (program == default || shader == default)
            return;
        
        GL.DetachShader(program, shader);
        GL.DeleteShader(shader);
    }
    
    private static string ReadText(Assembly assembly, string path)
    {
        using var stream = assembly.GetManifestResourceStream(path);
        if (stream is null)
            throw new MissingManifestResourceException($"Unable to load \"{path}\" from requested assembly");
        
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Retrieves the index of the uniform with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">The name of the uniform to query.</param>
    /// <returns>The index of the uniform, or <c>-1</c> if the specified <paramref name="name"/> was not found.</returns>
    /// <remarks>
    /// The locations are automatically cached on the CPU within an internal dictionary, so subsequent calls of the same
    /// <paramref name="name"/> will simply return the cached value, and not need to query the GPU. 
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public int UniformLocation(string name)
    {
        if (uniformCache.TryGetValue(name, out var location)) 
            return location;
        
        location = GL.GetUniformLocation(program, name);
        if (location < 0)
            throw new ShaderException($"Unable to locate a uniform with the name \"{name}\" in the shader program.", null);

        uniformCache[name] = location;
        return location;
    }

    /// <summary>
    ///  Installs the program object as part of current rendering state.
    /// </summary>
    public void Use() => GL.UseProgram(program);
    
    /// <inheritdoc cref="Dispose()"/>
    /// <param name="disposing"><c>true</c> when called by normal managed code, otherwise <c>false</c> when
    /// called by the object finalizer.</param>
    protected virtual void Dispose(bool disposing) => GL.DeleteProgram(program);

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Object finalizer.
    /// </summary>
    ~ShaderProgram()
    {
        Dispose(false);
    }

    /// <summary>
    /// Retrieves the index of a named uniform block.
    /// </summary>
    /// <param name="uniformBlockName">The name of the uniform block whose index to retrieve.</param>
    /// <returns></returns>
    public int BlockIndex(string uniformBlockName) => GL.GetUniformBlockIndex(program, uniformBlockName);

    /// <summary>
    /// Assigns a binding point to an active uniform block.
    /// </summary>
    /// <param name="uniformBlockIndex">The index of the active uniform block within the program whose binding to assign.</param>
    /// <param name="uniformBlockBinding">Specifies the binding point to which to bind the uniform block with index <paramref name="uniformBlockIndex"/> within this program.</param>
    public void BindBlock(int uniformBlockIndex, int uniformBlockBinding)
    {
        GL.UniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding);
    }
    
    /// <summary>
    /// Assigns a binding point to an active uniform block.
    /// </summary>
    /// <param name="uniformBlockName">The name of the active uniform block within the program whose binding to assign.</param>
    /// <param name="uniformBlockBinding">Specifies the binding point to which to bind the uniform block with name <paramref name="uniformBlockName"/> within this program.</param>
    public void BindBlock(string uniformBlockName, int uniformBlockBinding)
    {
        var index = GL.GetUniformBlockIndex(program, uniformBlockName);
        GL.UniformBlockBinding(program, index, uniformBlockBinding);
    }

    /// <summary>
    /// Implicit conversion of a <see cref="ShaderProgram"/> to a <see cref="program"/>/
    /// </summary>
    /// <param name="shader">The instance being converted.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Program(ShaderProgram shader) => shader.program;
    
    /// <summary>
    /// Returns information about an active uniform variable for the program object.
    /// </summary>
    /// <param name="index">Specifies the index of the uniform variable to be queried.</param>
    /// <returns>A <see cref="ShaderUniform"/> describing the variable, or <c>null</c> if none was found at <paramref name="index"/>.</returns>
    /// <seealso cref="GetUniforms"/>
    /// <seealso cref="UniformCount"/>
    /// <remarks>
    /// This method does not cache the value and queries the GPU with each invocation, so results should be stored in a
    /// variable if repeated reference to the returned value is needed.
    /// </remarks>
    public ShaderUniform? GetUniform(int index)
    {
        var count = GL.GetProgram(program, ProgramProperty.ActiveUniforms);
        if (index < 0 || index >= count)
            return null;
        
        var maxLength = GL.GetProgram(program, ProgramProperty.ActiveUniformMaxLength);
        Span<byte> buffer = stackalloc byte[maxLength];
        GL.GetActiveUniform(program, index, maxLength, out var size, out var type, out var length, buffer);

        if (length <= 0)
            return null;
        var name = Encoding.ASCII.GetString(buffer[..length]);
        return new ShaderUniform(name, index, size, type);
    }

    /// <summary>
    /// Gets an enumerator that iterates through all active uniforms in the shader program.
    /// </summary>
    /// <returns>A uniform variable enumerator.</returns>
    /// <seealso cref="GetUniform"/>
    /// <seealso cref="UniformCount"/>
    /// <remarks>
    /// This method does not cache the value and queries the GPU with each invocation, so results should be stored in a
    /// variable if repeated reference to the returned value is needed.
    /// </remarks>
    public IEnumerable<ShaderUniform> GetUniforms()
    {
        var count = GL.GetProgram(program, ProgramProperty.ActiveUniforms);
        for (var i = 0; i < count; i++)
        {
            var uniform = GetUniform(i);
            if (uniform != null)
                yield return uniform;
        }
    }

    /// <summary>
    /// Returns information about an active attribute variable for the program object.
    /// </summary>
    /// <param name="index">Specifies the index of the attribute variable to be queried.</param>
    /// <returns>A <see cref="ShaderAttribute"/> describing the variable, or <c>null</c> if none was found at <paramref name="index"/>.</returns>
    /// <seealso cref="GetAttributes"/>
    /// <seealso cref="AttributeCount"/>
    /// <remarks>
    /// This method does not cache the value and queries the GPU with each invocation, so results should be stored in a
    /// variable if repeated reference to the returned value is needed.
    /// </remarks>
    public ShaderAttribute? GetAttribute(int index = 0)
    {
        var count = GL.GetProgram(program, ProgramProperty.ActiveAttributes);
        if (index < 0 || index >= count)
            return null;
        
        var maxLength = GL.GetProgram(program, ProgramProperty.ActiveAttributeMaxLength);
        Span<byte> buffer = stackalloc byte[maxLength];
        GL.GetActiveAttrib(program, index, out var size, out var type, out var length, buffer);

        if (length <= 0)
            return null;
        var name = Encoding.ASCII.GetString(buffer[..length]);
        return new ShaderAttribute(name, index, size, type);
    }
    
    /// <summary>
    /// Gets an enumerator that iterates through all active attributes in the shader program.
    /// </summary>
    /// <returns>An attribute variable enumerator.</returns>
    /// <seealso cref="GetAttribute"/>
    /// <seealso cref="AttributeCount"/>
    /// <remarks>
    /// This method does not cache the value and queries the GPU with each invocation, so results should be stored in a
    /// variable if repeated reference to the returned value is needed.
    /// </remarks>
    public IEnumerable<ShaderAttribute> GetAttributes()
    {
        var count = GL.GetProgram(program, ProgramProperty.ActiveAttributes);
        for (var i = 0; i < count; i++)
        {
            var attribute = GetAttribute(i);
            if (attribute != null)
                yield return attribute;
        }
    }

    /// <summary>
    /// Returns information about an active uniform block in the program object.
    /// </summary>
    /// <param name="index">Specifies the uniform block index to be queried.</param>
    /// <returns>An <see cref="ShaderUniformBlock"/> describing the uniform block, or <c>null</c> if none was found at <paramref name="index"/>.</returns>
    /// <remarks>
    /// This method does not cache the value and queries the GPU with each invocation, so results should be stored in a
    /// variable if repeated reference to the returned value is needed.
    /// </remarks>
    public ShaderUniformBlock? GetUniformBlock(int index)
    {
        if (index < 0)
            return null;
        
        // Get uniforms
        var length = GL.GetActiveUniformBlock(program, index, UniformBlockPName.ActiveUniforms);
        Span<int> indices = stackalloc int[length];
        GL.GetActiveUniformBlock(program, index, UniformBlockPName.ActiveUniformIndices, indices);
        var uniforms = new ShaderUniform[length];
        for (var i = 0; i < length; i++)
        {
            var uniform = GetUniform(indices[i]);
            uniforms[i] = uniform ?? throw new ShaderException("An error occurred fetching uniform data from shader program.", InfoLog);
        }

        // Get name
        length = GL.GetActiveUniformBlock(program, index, UniformBlockPName.NameLength);
        Span<byte> nameBuffer = stackalloc byte[length];
        GL.GetActiveUniformBlockName(program, index, out length, nameBuffer);
        var name = Encoding.ASCII.GetString(nameBuffer[..length]);

        // Misc. information
        var binding = GL.GetActiveUniformBlock(program, index, UniformBlockPName.Binding);
        var size = GL.GetActiveUniformBlock(program, index, UniformBlockPName.DataSize);
        var v = GL.GetActiveUniformBlock(program, index, UniformBlockPName.ReferencedByVertexShader) == GL.TRUE;
        var f = GL.GetActiveUniformBlock(program, index, UniformBlockPName.ReferencedByFragmentShader) == GL.TRUE;
        var g = GL.GetActiveUniformBlock(program, index, UniformBlockPName.ReferencedByGeometryShader) == GL.TRUE;

        return new ShaderUniformBlock(name, index, binding, size, uniforms, v, f, g);
    }

    /// <summary>
    /// Returns information about an active uniform block in the program object.
    /// </summary>
    /// <param name="name">Specifies the name of the uniform block to be queried.</param>
    /// <returns>An <see cref="ShaderUniformBlock"/> describing the uniform block, or <c>null</c> if none was found at <paramref name="name"/>.</returns>
    /// <remarks>
    /// This method does not cache the value and queries the GPU with each invocation, so results should be stored in a
    /// variable if repeated reference to the returned value is needed.
    /// </remarks>
    public ShaderUniformBlock? GetUniformBlock(string name)
    {
        var index = GL.GetUniformBlockIndex(program, name);
        return index < 0 ? null : GetUniformBlock(index);
    }

    /// <summary>
    /// Sets a 4x4 matrix uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    /// <param name="transpose"><c>true</c> to have matrix transposed, otherwise <c>false</c>.</param>
    /// <exception cref="ArgumentException">When there is less than 16 components in the value.</exception>
    public void UniformMat4(string name, ReadOnlySpan<float> value, bool transpose = false)
    {
        var count = value.Length / 16;
        if (value.Length < 1)
            throw new ArgumentException("Span must contain at least 16 components.", nameof(value));
        GL.UniformMatrix4(UniformLocation(name), count, transpose, value);
    }
    
    /// <inheritdoc cref="UniformMat4(string,System.ReadOnlySpan{float},bool)"/>
    public void UniformMat4(string name, float[] value, bool transpose = false)
    {
        var count = value.Length / 16;
        if (value.Length < 1)
            throw new ArgumentException("Array must contain at least 16 components.", nameof(value));
        GL.UniformMatrix4(UniformLocation(name), count, transpose, value);
    }
    
    /// <summary>
    /// Sets a 4x4 matrix uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    /// <param name="count">The number of 4x4 matrices the pointer represents (not the number of float values).</param>
    /// <param name="transpose"><c>true</c> to have matrix transposed, otherwise <c>false</c>.</param>
    [CLSCompliant(false)]
    public unsafe void UniformMat4(string name, float *value, int count = 1, bool transpose = false)
    {
        GL.UniformMatrix4(UniformLocation(name), count, transpose, value);
    }
    
    /// <summary>
    /// Sets a 4x4 matrix uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    /// <param name="transpose"><c>true</c> to have matrix transposed, otherwise <c>false</c>.</param>
    [CLSCompliant(false)]
    public unsafe void UniformMat4(string name, ref Matrix4x4 value, bool transpose = false)
    {
        fixed (Matrix4x4* ptr = &value)
        {
            GL.UniformMatrix4(UniformLocation(name), 1, transpose, ptr);
        }
    }

    /// <summary>
    /// Sets a 4x4 matrix uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    /// <param name="transpose"><c>true</c> to have matrix transposed, otherwise <c>false</c>.</param>
    public unsafe void UniformMat4(string name, Matrix4x4 value, bool transpose = false)
    {
        GL.UniformMatrix4(UniformLocation(name), 1, transpose, &value);
    }

    /// <summary>
    /// Sets a 4x4 matrix uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="values">The uniform value(s) to set.</param>
    /// <param name="transpose"><c>true</c> to have matrix transposed, otherwise <c>false</c>.</param>
    public void UniformMat4(string name, ReadOnlySpan<Matrix4x4> values, bool transpose = false)
    {
        GL.UniformMatrix4(UniformLocation(name), transpose, values);
    }
    
    /// <summary>
    /// Sets a 4x4 matrix uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    /// <param name="transpose"><c>true</c> to have matrix transposed, otherwise <c>false</c>.</param>
    [CLSCompliant(false)]
    public unsafe void UniformMat4(string name, Matrix4x4 *value, bool transpose = false)
    {
        GL.UniformMatrix4(UniformLocation(name), 1, transpose, value);
    }
    
    /// <summary>
    /// Sets a 4x4 matrix uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value(s) to set.</param>
    /// <param name="count">The number of 4x4 matrices the pointer represents.</param>
    /// <param name="transpose"><c>true</c> to have matrix transposed, otherwise <c>false</c>.</param>
    [CLSCompliant(false)]
    public unsafe void UniformMat4(string name, Matrix4x4 *value, int count, bool transpose = false)
    {
        GL.UniformMatrix4(UniformLocation(name), count, transpose, value);
    }
    
    /// <summary>
    /// Sets a 1-component uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    public void Uniform1(string name, byte value) => GL.Uniform1(UniformLocation(name), value);
    
    /// <summary>
    /// Sets a 2-component uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="v1">The value of the first component.</param>
    /// <param name="v2">The value of the second component.</param>
    public void Uniform2(string name, byte v1, byte v2) => GL.Uniform2(UniformLocation(name), v1, v2);
    
    /// <summary>
    /// Sets a 3-component uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="v1">The value of the first component.</param>
    /// <param name="v2">The value of the second component.</param>
    /// <param name="v3">The value of the third component.</param>
    public void Uniform3(string name, byte v1, byte v2, byte v3) => GL.Uniform3(UniformLocation(name), v1, v2, v3);
    
    /// <summary>
    /// Sets a 4-component uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="v1">The value of the first component.</param>
    /// <param name="v2">The value of the second component.</param>
    /// <param name="v3">The value of the third component.</param>
    /// <param name="v4">The value of the fourth component.</param>
    public void Uniform4(string name, byte v1, byte v2, byte v3, byte v4) => GL.Uniform4(UniformLocation(name), v1, v2, v3, v4);
    
    /// <inheritdoc cref="Uniform1(string,byte)"/>
    public void Uniform1(string name, short value) => GL.Uniform1(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform2(string,byte,byte)"/>
    public void Uniform2(string name, short v1, short v2) => GL.Uniform2(UniformLocation(name), v1, v2);
    
    /// <inheritdoc cref="Uniform3(string,byte,byte,byte)"/>
    public void Uniform3(string name, short v1, short v2, short v3) => GL.Uniform3(UniformLocation(name), v1, v2, v3);
    
    /// <inheritdoc cref="Uniform4(string,byte,byte,byte,byte)"/>
    public void Uniform4(string name, short v1, short v2, short v3, short v4) => GL.Uniform4(UniformLocation(name), v1, v2, v3, v4);
    
    /// <inheritdoc cref="Uniform1(string,byte)"/>
    [CLSCompliant(false)]
    public void Uniform1(string name, uint value) => GL.Uniform1(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform2(string,byte,byte)"/>
    [CLSCompliant(false)]
    public void Uniform2(string name, uint v1, uint v2) => GL.Uniform2(UniformLocation(name), v1, v2);
    
    /// <inheritdoc cref="Uniform3(string,byte,byte,byte)"/>
    [CLSCompliant(false)]
    public void Uniform3(string name, uint v1, uint v2, uint v3) => GL.Uniform3(UniformLocation(name), v1, v2, v3);
    
    /// <inheritdoc cref="Uniform4(string,byte,byte,byte,byte)"/>
    [CLSCompliant(false)]
    public void Uniform4(string name, uint v1, uint v2, uint v3, uint v4) => GL.Uniform4(UniformLocation(name), v1, v2, v3, v4);
    
    /// <inheritdoc cref="Uniform1(string,byte)"/>
    public void Uniform1(string name, int value) => GL.Uniform1(UniformLocation(name), value);

    /// <inheritdoc cref="Uniform2(string,byte,byte)"/>
    public void Uniform2(string name, int v1, int v2) => GL.Uniform2(UniformLocation(name), v1, v2);
    
    /// <inheritdoc cref="Uniform3(string,byte,byte,byte)"/>
    public void Uniform3(string name, int v1, int v2, int v3) => GL.Uniform3(UniformLocation(name), v1, v2, v3);
    
    /// <inheritdoc cref="Uniform4(string,byte,byte,byte,byte)"/>
    public void Uniform4(string name, int v1, int v2, int v3, int v4) => GL.Uniform4(UniformLocation(name), v1, v2, v3, v4);

    /// <inheritdoc cref="Uniform1(string,byte)"/>
    public void Uniform1(string name, float value) => GL.Uniform1(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform2(string,byte,byte)"/>
    public void Uniform2(string name, float v1, float v2) => GL.Uniform2(UniformLocation(name), v1, v2);
    
    /// <inheritdoc cref="Uniform3(string,byte,byte,byte)"/>
    public void Uniform3(string name, float v1, float v2, float v3) => GL.Uniform3(UniformLocation(name), v1, v2, v3);
    
    /// <inheritdoc cref="Uniform4(string,byte,byte,byte,byte)"/>
    public void Uniform4(string name, float v1, float v2, float v3, float v4) => GL.Uniform4(UniformLocation(name), v1, v2, v3, v4);
    
    /// <summary>
    /// Sets a 2-component uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    public void Uniform2(string name, Vector2 value) => GL.Uniform2(UniformLocation(name), value);
    
    /// <summary>
    /// Sets a 3-component uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    public void Uniform3(string name, Vector3 value) => GL.Uniform3(UniformLocation(name), value);
    
    /// <summary>
    /// Sets a 4-component uniform value.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="value">The uniform value to set.</param>
    public void Uniform4(string name, Vector4 value) => GL.Uniform4(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform4(string,Vector4)"/>
    public void Uniform4(string name, ColorF value) => GL.Uniform4(UniformLocation(name), value);

    /// <inheritdoc cref="Uniform2(string,Vector2)"/>
    [CLSCompliant(false)]
    public void Uniform2(string name, ref Vector2 value) => GL.Uniform2(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform3(string,Vector3)"/>
    [CLSCompliant(false)]
    public void Uniform3(string name, ref Vector3 value) => GL.Uniform3(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform4(string,Vector4)"/>
    [CLSCompliant(false)]
    public void Uniform4(string name, ref Vector4 value) => GL.Uniform4(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform4(string,Vector4)"/>
    [CLSCompliant(false)]
    public void Uniform4(string name, ref ColorF value) => GL.Uniform4(UniformLocation(name), value);
    
    /// <inheritdoc cref="Uniform2(string,Vector2)"/>
    [CLSCompliant(false)]
    public unsafe void Uniform2(string name, Vector2 *value) => GL.Uniform2(UniformLocation(name), 1, value);
    
    /// <inheritdoc cref="Uniform3(string,Vector3)"/>
    [CLSCompliant(false)]
    public unsafe void Uniform3(string name, Vector3 *value) => GL.Uniform3(UniformLocation(name), 1, value);
    
    /// <inheritdoc cref="Uniform4(string,Vector4)"/>
    [CLSCompliant(false)]
    public unsafe void Uniform4(string name, Vector4 *value) => GL.Uniform4(UniformLocation(name), 1, value);
    
    /// <inheritdoc cref="Uniform4(string,Vector4)"/>
    [CLSCompliant(false)]
    public unsafe void Uniform4(string name, ColorF *value) => GL.Uniform4(UniformLocation(name), 1, value);
    
    /// <summary>
    /// Sets multiple 1-component uniform values.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="values">The uniform values to set.</param>
    public void Uniform1(string name, ReadOnlySpan<int> values) => GL.Uniform1(UniformLocation(name), values);
    
    /// <summary>
    /// Sets multiple 2-component uniform values.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="values">The uniform values to set.</param>
    public void Uniform2(string name, ReadOnlySpan<int> values) => GL.Uniform2(UniformLocation(name), values);
    
    /// <summary>
    /// Sets multiple 3-component uniform values.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="values">The uniform values to set.</param>
    public void Uniform3(string name, ReadOnlySpan<int> values) => GL.Uniform3(UniformLocation(name), values);
    
    /// <summary>
    /// Sets multiple 4-component uniform values.
    /// </summary>
    /// <param name="name">The name of the uniform to change.</param>
    /// <param name="values">The uniform values to set.</param>
    public void Uniform4(string name, ReadOnlySpan<int> values) => GL.Uniform4(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform1(string,ReadOnlySpan{int})"/>
    public void Uniform1(string name, ReadOnlySpan<float> values) => GL.Uniform1(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform2(string,ReadOnlySpan{int})"/>
    public void Uniform2(string name, ReadOnlySpan<float> values) => GL.Uniform2(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform3(string,ReadOnlySpan{int})"/>
    public void Uniform3(string name, ReadOnlySpan<float> values) => GL.Uniform3(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform4(string,ReadOnlySpan{int})"/>
    public void Uniform4(string name, ReadOnlySpan<float> values) => GL.Uniform4(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform2(string,ReadOnlySpan{int})"/>
    public void Uniform2(string name, ReadOnlySpan<Vector2> values) => GL.Uniform2(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform3(string,ReadOnlySpan{int})"/>
    public void Uniform3(string name, ReadOnlySpan<Vector3> values) => GL.Uniform3(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform4(string,ReadOnlySpan{int})"/>
    public void Uniform4(string name, ReadOnlySpan<Vector4> values) => GL.Uniform4(UniformLocation(name), values);
    
    /// <inheritdoc cref="Uniform4(string,ReadOnlySpan{int})"/>
    public void Uniform4(string name, ReadOnlySpan<ColorF> values) => GL.Uniform4(UniformLocation(name), values);
}