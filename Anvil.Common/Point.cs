using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Describes a location in 2D space.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 2, Pack = 0), PublicAPI, DataContract(Name = "point")]
[DebuggerDisplay("<X = {X}, Y = {Y}>")]
public struct Point : IEquatable<Point>
{
    /// <summary>
    /// Gets the size of a <see cref="Point"/> in bytes.
    /// </summary>
    /// <remarks>
    /// This is simply a convenience to avoid run-time calculating with <see cref="Marshal"/> or <see cref="Unsafe"/>.
    /// </remarks>
    public const int SizeOf = sizeof(int) * 2;
    
    /// <summary>
    /// Singleton instance of a default/empty <see cref="Point"/> object.
    /// </summary>
    public static readonly Point Empty;
    
    /// <summary>
    /// The location of the <see cref="Point"/> on the x-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 0), DataMember(Name = "x", IsRequired = true, EmitDefaultValue = true, Order = 0)]
    public int X;

    /// <summary>
    /// The location of the <see cref="Point"/> on the y-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 1), DataMember(Name = "y", IsRequired = true, EmitDefaultValue = true, Order = 1)]
    public int Y;

    /// <summary>
    /// Gets a value indicating if the <see cref="Point"/> is empty and contains all zero values.
    /// </summary>
    public bool IsEmpty => X == 0 && Y == 0;

    /// <summary>
    /// Creates a new instance of a <see cref="Point"/> from the specified coordinates.
    /// </summary>
    /// <param name="x">The location of the point on the x-axis.</param>
    /// <param name="y">The location of the point on the y-axis.</param>
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Point"/> using the values from the specified <paramref name="size"/>.
    /// </summary>
    /// <param name="size">A <see cref="Size"/> whose width/height will be used for the x/y respectfully.</param>
    public Point(Size size)
    {
        X = size.Width;
        Y = size.Height;
    }

    /// <summary>
    /// Creates a new <see cref="Point"/> from the specified <see cref="Vector2"/>.
    /// </summary>
    /// <param name="vector">A <see cref="Vector2"/> containing the values to use for the new <see cref="Point"/>.</param>
    /// <remarks>
    /// Only uses the whole number portion of the components, truncating anything after the decimal point.
    /// </remarks>
    public Point(Vector2 vector)
    {
        X = (int) vector.X;
        Y = (int) vector.Y;
    }
    
    /// <summary>
    /// Creates a new <see cref="Point"/> from the specified <see cref="Vector2"/>.
    /// </summary>
    /// <param name="vector">A <see cref="Vector2"/> containing the values to use for the new <see cref="Point"/>.</param>
    /// <param name="rounding">Determines the rounding strategy to use when setting the components.</param>
    public Point(Vector2 vector, MidpointRounding rounding)
    {
        X = (int) MathF.Round(vector.X, rounding);
        Y = (int) MathF.Round(vector.Y, rounding);
    }

    /// <summary>
    /// Offset the current <see cref="Point"/> object by the given amount on each axis.
    /// </summary>
    /// <param name="x">The amount offset on the x-axis.</param>
    /// <param name="y">The amount offset on the y-axis.</param>
    public void Offset(int x, int y)
    {
        X += x;
        Y += y;
    }

    /// <summary>
    /// Offset the current <see cref="Point"/> object by the values of another <see cref="Point"/>.
    /// </summary>
    /// <param name="other">The amount to offset.</param>
    public void Offset(Point other)
    {
        X += other.X;
        Y += other.Y;
    }

    /// <inheritdoc />
    public override string ToString() => string.Format(CultureInfo.CurrentCulture, "<X = {0}, Y = {1}>", X, Y);

    /// <inheritdoc />
    public bool Equals(Point other) => X == other.X && Y == other.Y;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Point other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <summary>
    /// Determines whether two specified points have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Point"/> to compare.</param>
    /// <param name="right">The second <see cref="Point"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Point left, Point right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified points have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Point"/> to compare.</param>
    /// <param name="right">The second <see cref="Point"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Point left, Point right) => !left.Equals(right);

    /// <summary>
    /// Performs addition of two <see cref="Point"/> objects.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Point"/> object.</returns>
    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);
    
    /// <summary>
    /// Performs subtraction of two <see cref="Point"/> objects.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Point"/> object.</returns>
    public static Point operator -(Point left, Point right) => new(left.X - right.X, left.Y - right.Y);
    
    /// <summary>
    /// Performs addition of a <see cref="Point"/> and <see cref="Size"/> object.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Point"/> object.</returns>
    public static Point operator +(Point left, Size right) => new(left.X + right.Width, left.Y + right.Height);
    
    /// <summary>
    /// Performs subtraction of a <see cref="Point"/> and <see cref="Size"/> object.
    /// </summary>
    /// <param name="left">First value to add.</param>
    /// <param name="right">Second value to add.</param>
    /// <returns>A newly created <see cref="Point"/> object.</returns>
    public static Point operator -(Point left, Size right) => new(left.X - right.Width, left.Y - right.Height);
    
    /// <summary>
    /// Explicitly converts a <see cref="Point"/> to a <see cref="Size"/>.
    /// </summary>
    /// <param name="point">The point to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Size(Point point) => Unsafe.As<Point, Size>(ref point);
    
    /// <summary>
    /// Explicitly converts a <see cref="Point"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="size">The point to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector2(Point size) => new(size.X, size.Y);
}