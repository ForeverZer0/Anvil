using System.Runtime.InteropServices;
using System.Text;

namespace Anvil.Native;

/// <summary>
/// Simple stack-allocated structure to be used as a helper for passing UTF-8 encoded strings into native code and
/// automatically ensuring that that they are null-terminated.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
public readonly ref struct UTF8String
{
    /// <summary>
    /// Returns a reference to the the first byte in the UTF-8 bytes of the given string, which can be pinned in a
    /// <c>fixed</c> statement.
    /// </summary>
    /// <param name="str">The string to convert to UTF-8 and pass into native code.</param>
    /// <returns>A reference to the first byte in the string contents.</returns>
    public static ref readonly byte Pin(string? str)
    {
        var utf8 = new UTF8String(str);
        return ref utf8.GetPinnableReference();
    }
    
    /// <summary>
    /// Gets the number of bytes in the UTF-8 string, not including the null-terminator.
    /// </summary>
    public int Length => buffer.Length - 1;
        
    private readonly Span<byte> buffer;
        
    /// <summary>
    /// Initializes a new instance of the <see cref="UTF8String"/> from the specified string. 
    /// </summary>
    /// <param name="str">A string to pass into native code, or <c>null</c> to pass a <c>NULL</c> value.</param>
    public UTF8String(string? str)
    {
        if (string.IsNullOrEmpty(str))
        {
            buffer = new byte[1];
        }
        else
        {
            buffer = new byte[Encoding.UTF8.GetByteCount(str) + 1];
            Encoding.UTF8.GetBytes(str, buffer);
        }
    }

    /// <summary>
    /// Returns a reference to the first byte in the UTF-8 encoded string.
    /// </summary>
    /// <returns>A reference to the beginning of the string.</returns>
    public ref readonly byte GetPinnableReference() => ref buffer.GetPinnableReference();

    /// <inheritdoc />
    public override string ToString()
    {
        if (buffer.Length <= 1)
            return string.Empty;
        return Encoding.UTF8.GetString(buffer);
    }
}