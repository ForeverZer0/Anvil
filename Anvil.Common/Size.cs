using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Describes a dimension in 2D space.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 2, Pack = 0), PublicAPI, DataContract(Name = "size")]
[DebuggerDisplay("<Width = {Width}, Height = {Height}>")]
public struct Size : IEquatable<Size>
{
    /// <summary>
    /// Gets the size of a <see cref="Size"/> in bytes.
    /// </summary>
    /// <remarks>
    /// This is simply a convenience to avoid run-time calculating with <see cref="Marshal"/> or <see cref="System.Runtime.CompilerServices.Unsafe"/>.
    /// </remarks>
    public const int SizeOf = sizeof(int) * 2;
    
    /// <summary>
    /// Singleton instance of a default/empty <see cref="Point"/> object.
    /// </summary>
    public static readonly Size Empty;
    
    /// <summary>
    /// The value of the <see cref="Size"/> on the x-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 0), DataMember(Name = "width", IsRequired = true, EmitDefaultValue = true, Order = 0)]
    public int Width;

    /// <summary>
    /// The value of the <see cref="Size"/> on the y-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 1), DataMember(Name = "height", IsRequired = true, EmitDefaultValue = true, Order = 1)]
    public int Height;

    /// <summary>
    /// Gets a value indicating if the <see cref="Size"/> is empty and contains all zero values.
    /// </summary>
    public bool IsEmpty => Width == 0 && Height == 0;
    
    /// <summary>
    /// Creates a new instance of a <see cref="Size"/> from the specified dimensions.
    /// </summary>
    /// <param name="width">The value of the size on the x-axis.</param>
    /// <param name="height">The value of the size on the y-axis.</param>
    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Size"/> using the values from the specified <paramref name="point"/>.
    /// </summary>
    /// <param name="point">A <see cref="Point"/> whose x/y will be used for the width/height respectfully.</param>
    public Size(Point point)
    {
        Width = point.X;
        Height = point.Y;
    }
    
    /// <summary>
    /// Creates a new <see cref="Size"/> from the specified <see cref="Vector2"/>.
    /// </summary>
    /// <param name="vector">A <see cref="Vector2"/> containing the values to use for the new <see cref="Size"/>.</param>
    /// <remarks>
    /// Only uses the whole number portion of the components, truncating anything after the decimal point.
    /// </remarks>
    public Size(Vector2 vector)
    {
        Width = (int) vector.X;
        Height = (int) vector.Y;
    }
    
    /// <summary>
    /// Creates a new <see cref="Size"/> from the specified <see cref="Vector2"/>.
    /// </summary>
    /// <param name="vector">A <see cref="Vector2"/> containing the values to use for the new <see cref="Size"/>.</param>
    /// <param name="rounding">Determines the rounding strategy to use when setting the components.</param>
    public Size(Vector2 vector, MidpointRounding rounding)
    {
        Width = (int) MathF.Round(vector.X, rounding);
        Height = (int) MathF.Round(vector.Y, rounding);
    }
    
    /// <inheritdoc />
    public override string ToString() => string.Format(CultureInfo.CurrentCulture, "<Width = {0}, Height = {1}>", Width, Height);

    /// <inheritdoc />
    public bool Equals(Size other) => Width == other.Width && Height == other.Height;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Size other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Width, Height);
    
    /// <summary>
    /// Determines whether two specified sizes have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Size"/> to compare.</param>
    /// <param name="right">The second <see cref="Size"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Size left, Size right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified sizes have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Size"/> to compare.</param>
    /// <param name="right">The second <see cref="Size"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Size left, Size right) => !left.Equals(right);
    
    /// <summary>
    /// Performs addition of two <see cref="Size"/> objects.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Size"/> object.</returns>
    public static Point operator +(Size left, Size right) => new(left.Width + right.Width, left.Height + right.Height);
    
    /// <summary>
    /// Performs subtraction of two <see cref="Size"/> objects.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Point"/> object.</returns>
    public static Point operator -(Size left, Size right) => new(left.Width - right.Width, left.Height - right.Height);
    
    /// <summary>
    /// Performs addition of a <see cref="Size"/> and <see cref="Point"/> object.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Size"/> object.</returns>
    public static Point operator +(Size left, Point right) => new(left.Width + right.X, left.Height + right.Y);
    
    /// <summary>
    /// Performs subtraction of a <see cref="Size"/> and <see cref="Point"/> object.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Size"/> object.</returns>
    public static Point operator -(Size left, Point right) => new(left.Width - right.X, left.Height - right.Y);
    
    /// <summary>
    /// Explicitly converts a <see cref="Size"/> to a <see cref="Point"/>.
    /// </summary>
    /// <param name="size">The size to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Point(Size size) => Unsafe.As<Size, Point>(ref size);

    /// <summary>
    /// Explicitly converts a <see cref="Size"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="size">The size to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector2(Size size) => new(size.Width, size.Height);
}