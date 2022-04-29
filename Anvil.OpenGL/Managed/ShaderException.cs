namespace Anvil.OpenGL.Managed;

/// <summary>
/// Generic exception class for shader-related errors.
/// </summary>
public class ShaderException : Exception
{
    /// <summary>
    /// Creates a new instance of the <see cref="ShaderException"/> class.
    /// </summary>
    /// <param name="message">A brief informative message describing the nature of the exception.</param>
    /// <param name="log">The OpenGL-generated info log, if any.</param>
    public ShaderException(string message, string? log) : base($"{message}\n${log}")
    {
    }
}