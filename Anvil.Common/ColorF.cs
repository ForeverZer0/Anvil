using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

[assembly: CLSCompliant(true)]

namespace Anvil;

[PublicAPI, StructLayout(LayoutKind.Explicit, Size = sizeof(byte) * 4, Pack = 0)]
[DebuggerDisplay("rgba({R},{G},{B},{A})")]
public readonly struct Color : IEquatable<Color>
{
    [FieldOffset(0)]
    public readonly byte R;
    
    [FieldOffset(1)]
    public readonly byte G;
    
    [FieldOffset(2)]
    public readonly byte B;
    
    [FieldOffset(3)]
    public readonly byte A;

    [FieldOffset(0)] 
    public readonly int Value;

    [FieldOffset(0), CLSCompliant(false)]
    public readonly uint UnsignedValue;

    public Color(int r, int g, int b, int a = 255) : this()
    {
        R = (byte) Math.Clamp(r, 0, 255);
        G = (byte) Math.Clamp(g, 0, 255);
        B = (byte) Math.Clamp(b, 0, 255);
        A = (byte) Math.Clamp(a, 0, 255);
    }

    public Color(int value) : this()
    {
        Value = value;
    }
    
    [CLSCompliant(false)]
    public Color(uint value) : this()
    {
        UnsignedValue = value;
    }

    public static explicit operator int(Color color) => color.Value;
    
    [CLSCompliant(false)]
    public static explicit operator uint(Color color) => unchecked((uint) color.Value);

    /// <inheritdoc />
    public bool Equals(Color other) => Value == other.Value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Color other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => Value;

    /// <inheritdoc />
    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}{A:X2}";

    public static bool operator ==(Color left, Color right) => left.Equals(right);

    public static bool operator !=(Color left, Color right) => !left.Equals(right);
}

/// <summary>
/// Describes a color in the RGB color space, with normalized single-precision floating point numbers for components
/// in the range of <c>0.0f</c> to <c>1.0f</c>.
/// </summary>
[PublicAPI, StructLayout(LayoutKind.Explicit, Size = sizeof(float) * 4)]
public readonly struct ColorF : IEquatable<ColorF>, IFormattable
{
    /// <summary>
    /// The value of the red component as a normalized value between <c>0.0f</c> and <c>1.0f</c>.
    /// </summary>
    [FieldOffset(sizeof(float) * 0)]
    public readonly float R;
    
    /// <summary>
    /// The value of the green component as a normalized value between <c>0.0f</c> and <c>1.0f</c>.
    /// </summary>
    [FieldOffset(sizeof(float) * 1)]
    public readonly float G;
    
    /// <summary>
    /// The value of the blue component as a normalized value between <c>0.0f</c> and <c>1.0f</c>.
    /// </summary>
    [FieldOffset(sizeof(float) * 2)]
    public readonly float B;
    
    /// <summary>
    /// The value of the alpha component as a normalized value between <c>0.0f</c> and <c>1.0f</c>.
    /// </summary>
    [FieldOffset(sizeof(float) * 3)]
    public readonly float A;

    /// <summary>
    /// Gets the value of the color as a <see cref="Vector4"/>.
    /// </summary>
    [FieldOffset(sizeof(float) * 0)]
    public readonly Vector4 Rgba;

    /// <summary>
    /// Gets the value of the color as a <see cref="Vector3"/>, with alpha value omitted.
    /// </summary>
    [FieldOffset(sizeof(float) * 0)] 
    public readonly Vector3 Rgb;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorF"/> struct from the specified vector.
    /// </summary>
    /// <param name="rgba">A vector representing the RGBA color components.</param>
    /// <remarks>Values will be automatically clamped to the range of <c>0.0f</c> and <c>1.0f</c>.</remarks>
    public ColorF(Vector4 rgba) : this()
    {
        Rgba = Vector4.Clamp(rgba, Vector4.Zero, Vector4.One);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorF"/> struct from the specified vector.
    /// </summary>
    /// <param name="rgb">A vector representing the RGB color components.</param>
    /// <param name="a">The value of the alpha component.</param>
    /// <remarks>Values will be automatically clamped to the range of <c>0.0f</c> and <c>1.0f</c>.</remarks>
    public ColorF(Vector3 rgb, float a = 1.0f) : this()
    {
        Rgba = Vector4.Clamp(new Vector4(rgb, a), Vector4.Zero, Vector4.One);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorF"/> struct from the specified values.
    /// </summary>
    /// <param name="r">The value of the red component.</param>
    /// <param name="g">The value of the green component.</param>
    /// <param name="b">The value of the blue component.</param>
    /// <param name="a">The value of the alpha component.</param>
    /// <remarks>Values will be automatically clamped to the range of <c>0.0f</c> and <c>1.0f</c>.</remarks>
    public ColorF(float r, float g, float b, float a = 1.0f) : this()
    {
        Rgba = Vector4.Clamp(new Vector4(r, g, b, a), Vector4.Zero, Vector4.One);
    }

    public ColorF(int r, int g, int b, int a = 255) : this()
    {
        Rgba = Vector4.Clamp(new Vector4(r, g, b, a) / 255.0f, Vector4.Zero, Vector4.One);
    }

    public ColorF(Color color) : this()
    {
        Rgba = new Vector4(color.R, color.G, color.B, color.A) / 255.0f;
    }

    public static implicit operator Vector4(ColorF color) => color.Rgba;

    public static explicit operator ColorF(Vector4 vec4)
    {
        vec4 = Vector4.Clamp(Vector4.Zero, Vector4.One, vec4);
        return Unsafe.As<Vector4, ColorF>(ref vec4);
    }
    
    public static explicit operator ColorF(Vector3 vec3)
    {
        return new ColorF(Vector3.Clamp(Vector3.Zero, Vector3.One, vec3));
    }
    
    public static explicit operator Color(ColorF color)
    {
        var vec = color.Rgba * 255.0f;
        return new Color((int) vec.X, (int) vec.Y, (int) vec.Z, (int) vec.W);
    }

    public static explicit operator ColorF(Color color)
    {
        return new ColorF(color.R * 255.0f, color.G * 255.0f, color.B * 255.0f, color.A * 255.0f);
    }

    public bool Equals(ColorF other, float epsilon)
    {
        return MathF.Abs(R - other.R) < epsilon &&
               MathF.Abs(G - other.G) < epsilon &&
               MathF.Abs(B - other.B) < epsilon &&
               MathF.Abs(A - other.A) < epsilon;
    }
    
    /// <inheritdoc />
    public bool Equals(ColorF other) => Rgba.Equals(other.Rgba);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is ColorF other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => Rgba.GetHashCode();

    public static bool operator ==(ColorF left, ColorF right) => left.Equals(right);

    public static bool operator !=(ColorF left, ColorF right) => !left.Equals(right);
    
    /// <inheritdoc />
    public override string ToString() => "#" + ToString(null, null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        const string DEFAULT_FORMAT = "rgba";
        
        format ??= DEFAULT_FORMAT;
        formatProvider ??= CultureInfo.CurrentCulture;
        var buffer = new StringBuilder(8);
        foreach (var c in format)
        {
            switch (c)
            {
                case 'R': 
                    buffer.Append(formatProvider, $"{(int) (R * 255.0f):X2}");
                    break;
                case 'G': 
                    buffer.Append(formatProvider, $"{(int) (G * 255.0f):X2}");
                    break;
                case 'B': 
                    buffer.Append(formatProvider, $"{(int) (B * 255.0f):X2}");
                    break;
                case 'A': 
                    buffer.Append(formatProvider, $"{(int) (A * 255.0f):X2}");
                    break;
                case 'r': 
                    buffer.Append(formatProvider, $"{(int) (R * 255.0f):x2}");
                    break;
                case 'g': 
                    buffer.Append(formatProvider, $"{(int) (G * 255.0f):x2}");
                    break;
                case 'b': 
                    buffer.Append(formatProvider, $"{(int) (B * 255.0f):x2}");
                    break;
                case 'a': 
                    buffer.Append(formatProvider, $"{(int) (A * 255.0f):x2}");
                    break;
                case 'X':
                    buffer.Append(formatProvider, $"{R:F5}");
                    break;
                case 'Y':
                    buffer.Append(formatProvider, $"{G:F5}");
                    break;
                case 'Z':
                    buffer.Append(formatProvider, $"{B:F5}");
                    break;
                case 'W':
                    buffer.Append(formatProvider, $"{A:F5}");
                    break;
                case 'x':
                    buffer.Append(formatProvider, $"{(int)(R * 255.0f)}");
                    break;
                case 'y':
                    buffer.Append(formatProvider, $"{(int)(G * 255.0f)}");;
                    break;
                case 'z':
                    buffer.Append(formatProvider, $"{(int)(B * 255.0f)}");
                    break;
                case 'w':
                    buffer.Append(formatProvider, $"{(int)(A * 255.0f)}");
                    break;
                default:
                    throw new FormatException($"The '{format}' format string is not supported.");
            }
        }

        return buffer.ToString();
    }

    public static bool TryParse(string value, out ColorF result)
    {
        try
        {
            result = Parse(value);
            return true;
        }
        catch (FormatException)
        {
            result = default;
            return false;
        }
    }

    public static ColorF Parse(string value)
    {
        var span = value.AsSpan(value.StartsWith('#') ? 1 : 0);
        
        byte r, g, b, a;
        switch (span.Length)
        {
            case 6:
                a = byte.MaxValue;
                r = byte.Parse(span[0..2], NumberStyles.HexNumber);
                g = byte.Parse(span[2..4], NumberStyles.HexNumber);
                b = byte.Parse(span[4..6], NumberStyles.HexNumber);
                break;
            case 8:
                a = byte.Parse(span[0..2], NumberStyles.HexNumber);
                r = byte.Parse(span[2..4], NumberStyles.HexNumber);
                g = byte.Parse(span[4..6], NumberStyles.HexNumber);
                b = byte.Parse(span[6..8], NumberStyles.HexNumber);
                break;
            default:
                throw new FormatException($"Cannot parse color from \"{value}\"");
        }

        return new ColorF(r, g, b, a);
    }
}