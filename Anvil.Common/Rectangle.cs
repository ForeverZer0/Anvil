using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Describes a location and size in 2D space.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 4, Pack = 0), PublicAPI, DataContract(Name = "rect")]
[DebuggerDisplay("<X = {X}, Y = {Y}, Width = {Width}, Height = {Height}>")]
public struct Rectangle : IEquatable<Rectangle>
{
    /// <summary>
    /// Gets the size of a <see cref="Rectangle"/> in bytes.
    /// </summary>
    /// <remarks>
    /// This is simply a convenience to avoid run-time calculating with <see cref="Marshal"/> or <see cref="Unsafe"/>.
    /// </remarks>
    public const int SizeOf = sizeof(int) * 4;
    
    /// <summary>
    /// Singleton instance of a default/empty <see cref="Rectangle"/> object.
    /// </summary>
    public static readonly Rectangle Empty;
    
    /// <summary>
    /// The location of the <see cref="Rectangle"/> on the x-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 0), DataMember(Name = "x", IsRequired = true, EmitDefaultValue = true, Order = 0)]
    public int X;

    /// <summary>
    /// The location of the <see cref="Rectangle"/> on the y-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 1), DataMember(Name = "y", IsRequired = true, EmitDefaultValue = true, Order = 1)]
    public int Y;

    /// <summary>
    /// The size of the <see cref="Rectangle"/> on the x-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 2), DataMember(Name = "width", IsRequired = true, EmitDefaultValue = true, Order = 2)]
    public int Width;

    /// <summary>
    /// The size of the <see cref="Rectangle"/> on the y-axis.
    /// </summary>
    [FieldOffset(sizeof(int) * 3), DataMember(Name = "height", IsRequired = true, EmitDefaultValue = true, Order = 3)]
    public int Height;

    /// <summary>
    /// A <see cref="Point"/> describing the location of the <see cref="Rectangle"/>.
    /// </summary>
    [FieldOffset(sizeof(int) * 0)]
    public Point Location;

    /// <summary>
    /// A <see cref="Size"/> describing the size of the <see cref="Rectangle"/>.
    /// </summary>
    [FieldOffset(sizeof(int) * 2)]
    public Size Size;

    /// <summary>
    /// Gets the value of the left edge of the <see cref="Rectangle"/> on the x-axis.
    /// </summary>
    public int Left => X;

    /// <summary>
    /// Gets the value of the right edge of the <see cref="Rectangle"/> on the x-axis.
    /// </summary>
    public int Right => X + Width;

    /// <summary>
    /// Gets the value of the top edge of the <see cref="Rectangle"/> on the y-axis.
    /// </summary>
    public int Top => Y;

    /// <summary>
    /// Gets the value of the bottom edge of the <see cref="Rectangle"/> on the y-axis.
    /// </summary>
    public int Bottom => Y + Height;

    /// <summary>
    /// Gets a <see cref="Point"/> describing the top-left corner of the <see cref="Rectangle"/>.
    /// </summary>
    public Point TopLeft => Location;
    
    /// <summary>
    /// Gets a <see cref="Point"/> describing the top-right corner of the <see cref="Rectangle"/>.
    /// </summary>
    public Point TopRight => new(X + Width, Y);
    
    /// <summary>
    /// Gets a <see cref="Point"/> describing the bottom-left corner of the <see cref="Rectangle"/>.
    /// </summary>
    public Point BottomLeft => new(X, Y + Height);
    
    /// <summary>
    /// Gets a <see cref="Point"/> describing the bottom-right corner of the <see cref="Rectangle"/>.
    /// </summary>
    public Point BottomRight => new(X + Width, Y + Height);
    
    /// <summary>
    /// Gets a value indicating if the <see cref="Rectangle"/> is empty and contains all zero values.
    /// </summary>
    public bool IsEmpty => X == 0 && Y == 0 && Width == 0 && Height == 0;

    /// <summary>
    /// Gets the total area of the <see cref="Rectangle"/>.
    /// </summary>
    public int Area => Width * Height;

    /// <summary>
    /// Gets the circumference of the <see cref="Rectangle"/>.
    /// </summary>
    public int Circumference => (Width * 2) + (Height * 2);

    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instances from the specified location of each side.
    /// </summary>
    /// <param name="left">The left edge of the rectangle on the x-axis.</param>
    /// <param name="top">The top edge of the rectangle on the y-axis.</param>
    /// <param name="right">The right edge of the rectangle on the x-axis.</param>
    /// <param name="bottom">The bottom edge of the rectangle on the y-axis.</param>
    /// <returns>The newly created <see cref="Rectangle"/>.</returns>
    public static Rectangle FromSides(int left, int top, int right, int bottom)
    {
        return new Rectangle(left, top, right - left, bottom - top);
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="x">The location of the <see cref="Rectangle"/> on the x-axis.</param>
    /// <param name="y">The location of the <see cref="Rectangle"/> on the y-axis.</param>
    /// <param name="width">The size of the <see cref="Rectangle"/> on the x-axis.</param>
    /// <param name="height">The size of the <see cref="Rectangle"/> on the y-axis.</param>
    public Rectangle(int x, int y, int width, int height) : this()
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="location">The location of the <see cref="Rectangle"/>.</param>
    /// <param name="size">The size of the <see cref="Rectangle"/>.</param>
    public Rectangle(Point location, Size size) : this()
    {
        X = location.X;
        Y = location.Y;
        Width = size.Width;
        Height = size.Height;
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="x">The location of the <see cref="Rectangle"/> on the x-axis.</param>
    /// <param name="y">The location of the <see cref="Rectangle"/> on the y-axis.</param>
    /// <param name="size">The size of the <see cref="Rectangle"/>.</param>
    public Rectangle(int x, int y, Size size) : this()
    {
        X = x;
        Y = y;
        Width = size.Width;
        Height = size.Height;
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="location">The location of the <see cref="Rectangle"/>.</param>
    /// <param name="width">The size of the <see cref="Rectangle"/> on the x-axis.</param>
    /// <param name="height">The size of the <see cref="Rectangle"/> on the y-axis.</param>
    public Rectangle(Point location, int width, int height) : this()
    {
        X = location.X;
        Y = location.Y;
        Width = width;
        Height = height;
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="location">The location of the <see cref="Rectangle"/>.</param>
    /// <param name="size">The size of the <see cref="Rectangle"/>.</param>
    public Rectangle(Vector2 location, Vector2 size) : this()
    {
        X = (int) location.X;
        Y = (int) location.Y;
        Width = (int) size.X;
        Height = (int) size.Y;
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="location">The location of the <see cref="Rectangle"/>.</param>
    /// <param name="size">The size of the <see cref="Rectangle"/>.</param>
    /// <param name="rounding">Determines the rounding strategy to use when setting the components.</param>
    public Rectangle(Vector2 location, Vector2 size, MidpointRounding rounding) : this()
    {
        X = (int) MathF.Round(location.X, rounding);
        Y = (int) MathF.Round(location.Y, rounding);
        Width = (int) MathF.Round(size.X, rounding);
        Height = (int) MathF.Round(size.Y, rounding);
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="bounds">A <see cref="Vector4"/> describing the bounds of the <see cref="Rectangle"/>.</param>
    public Rectangle(Vector4 bounds) : this()
    {
        X = (int) bounds.X;
        Y = (int) bounds.Y;
        Width = (int) bounds.Z;
        Height = (int) bounds.W;
    }

    /// <summary>
    /// Creates a new <see cref="Rectangle"/> instance from the specified location and size.
    /// </summary>
    /// <param name="bounds">A <see cref="Vector4"/> describing the bounds of the <see cref="Rectangle"/>.</param>
    /// <param name="rounding">Determines the rounding strategy to use when setting the components.</param>
    public Rectangle(Vector4 bounds, MidpointRounding rounding) : this()
    {
        X = (int) MathF.Round(bounds.X, rounding);
        Y = (int) MathF.Round(bounds.Y, rounding);
        Width = (int) MathF.Round(bounds.Z, rounding);
        Height = (int) MathF.Round(bounds.W, rounding);
    }

    /// <summary>
    /// Determines if the specified coordinates are contained within this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="x">The location to query on the x-axis.</param>
    /// <param name="y">The location to query on the y-axis.</param>
    /// <returns>
    /// <c>true</c> if the coordinates are within this <see cref="Rectangle"/>, otherwise <c>false</c>.
    /// </returns>
    public bool Contains(int x, int y) => X <= x && x < X + Width && Y <= y && y < Y + Height;

    /// <summary>
    /// Determines if the specified <paramref name="point"/> is contained within this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="point">The location to query.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="point"/> is within this <see cref="Rectangle"/>, otherwise <c>false</c>.
    /// </returns>
    public bool Contains(Point point) => Contains(point.X, point.Y);

    /// <summary>
    /// Determines if the specified <paramref name="rectangle"/> is fully contained within the bounds of this
    /// <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> instance to query.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="rectangle"/> is fully within this <see cref="Rectangle"/>, otherwise <c>false</c>.
    /// </returns>
    public bool Contains(Rectangle rectangle)
    {
        return(X <= rectangle.X) &&
              ((rectangle.X + rectangle.Width) <= (X + Width)) &&
              (Y <= rectangle.Y) &&
              ((rectangle.Y + rectangle.Height) <= (Y + Height));
    }

    /// <summary>
    /// Inflates this <see cref="Rectangle"/> by the specified amounts on each axis.
    /// <para/>
    /// This "pushes" the edges on each axis outward from from the center.
    /// </summary>
    /// <param name="width">The amount to inflate on the x-axis.</param>
    /// <param name="height">The amount to inflate on the y-axis.</param>
    public void Inflate(int width, int height)
    {
        X -= width;
        Y -= height;
        Width += 2 * width;
        Height += 2 * height;
    }

    /// <summary>
    /// Inflates this <see cref="Rectangle"/> by the specified amounts on each axis.
    /// <para/>
    /// This "pushes" the edges on each axis outward from from the center.
    /// </summary>
    /// <param name="size">The amount to inflate the rectangle.</param>
    public void Inflate(Size size) => Inflate(size.Width, size.Height);

    /// <summary>
    /// Creates a new <see cref="Rectangle"/> by inflating the specified rectangle by the given amount.
    /// </summary>
    /// <param name="rect">The source rectangle to create an inflated rectangle from.</param>
    /// <param name="x">The amount to inflate on the x-axis.</param>
    /// <param name="y">The amount to inflate on the y-axis.</param>
    /// <returns>A newly created <see cref="Rectangle"/>.</returns>
    /// <remarks>The source rectangle is not modified.</remarks>
    [Pure]
    public static Rectangle Inflate(Rectangle rect, int x, int y)
    {
        return new Rectangle(rect.X - x, rect.Y - y, rect.Width + (x * 2), rect.Height + (y * 2));
    }

    /// <summary>
    /// Creates a new <see cref="Rectangle"/> that represents the between two other rectangles, which is the shared
    /// shared area the both contain.
    /// <para/>
    /// An empty rectangle is returned if they do not intersect.
    /// </summary>
    /// <param name="a">The first rectangle.</param>
    /// <param name="b">The second rectangle.</param>
    /// <returns>The intersection of the two rectangles.</returns>
    [Pure]
    public static Rectangle Intersect(Rectangle a, Rectangle b)
    {
        var x1 = Math.Max(a.X, b.X);
        var x2 = Math.Min(a.X + a.Width, b.X + b.Width);
        var y1 = Math.Max(a.Y, b.Y);
        var y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
 
        if (x2 >= x1 && y2 >= y1) 
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        return Empty;
    }

    /// <summary>
    /// Determines if the specified rectangle intersects with this <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="other">The other rectangle to query for intersection.</param>
    /// <returns><c>true</c> if this rectangle shares any area with <paramref name="other"/>, otherwise <c>false</c>.</returns>
    [Pure]
    public bool IntersectsWith(Rectangle other)
    {
        return (other.X < X + Width) &&
               (X < (other.X + other.Width)) &&
               (other.Y < Y + Height) &&
               (Y < other.Y + other.Height);
    }
    
    /// <summary>
    /// Creates a new <see cref="Rectangle"/> that is the minimum location and size to completely encompass the two
    /// given rectangles.
    /// </summary>
    /// <param name="a">The first rectangle.</param>
    /// <param name="b">The second rectangle.</param>
    /// <returns>
    /// A new <see cref="Rectangle"/> large enough to contain both <paramref name="a"/> and <paramref name="b"/>.
    /// </returns>
    [Pure]
    public static Rectangle Union(Rectangle a, Rectangle b) 
    {
        var x1 = Math.Min(a.X, b.X);
        var x2 = Math.Max(a.X + a.Width, b.X + b.Width);
        var y1 = Math.Min(a.Y, b.Y);
        var y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
 
        return new Rectangle(x1, y1, x2 - x1, y2 - y1);
    }

    /// <summary>
    /// Offset the current <see cref="Rectangle"/> object by the given amount on each axis.
    /// </summary>
    /// <param name="x">The amount offset on the x-axis.</param>
    /// <param name="y">The amount offset on the y-axis.</param>
    public void Offset(int x, int y)
    {
        X += x;
        Y += y;
    }

    /// <summary>
    /// Offset the current <see cref="Rectangle"/> object by the values of the given <paramref name="point"/>.
    /// </summary>
    /// <param name="point">The amount to offset.</param>
    public void Offset(Point point)
    {
        X += point.X;
        Y += point.Y;
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return string.Format(CultureInfo.CurrentCulture, "<X = {0}, Y = {1}, Width = {2}, Height = {3}>", X, Y, Width, Height);
    }
    
    /// <inheritdoc />
    public bool Equals(Rectangle other) => X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Rectangle other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);
    
    /// <summary>
    /// Determines whether two specified rectangles have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Rectangle"/> to compare.</param>
    /// <param name="right">The second <see cref="Rectangle"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Rectangle left, Rectangle right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified rectangles have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Rectangle"/> to compare.</param>
    /// <param name="right">The second <see cref="Rectangle"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Rectangle left, Rectangle right) => !left.Equals(right);
    
    /// <summary>
    /// Explicitly converts a <see cref="Rectangle"/> to a <see cref="Vector4"/>.
    /// </summary>
    /// <param name="rect">The rectangle to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector4(Rectangle rect) => new(rect.X, rect.Y, rect.Width, rect.Height);
}