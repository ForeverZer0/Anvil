using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Describes a global tile ID.
/// </summary>
/// <remarks>
/// Internally this structure is backed by a single 32-bit integer, with bit-masking to indicate flip/rotate parameters. 
/// </remarks>
[PublicAPI, StructLayout(LayoutKind.Explicit, Size = sizeof(int)), DebuggerDisplay("Id = {Id}")]
public readonly struct Gid : IEquatable<Gid>, IComparable<Gid>, IComparable
{
    [FieldOffset(0)]
    private readonly int value;
    
    private const int FLIPPED_HORIZONTALLY_FLAG  = unchecked((int) 0x80000000);
    private const int FLIPPED_VERTICALLY_FLAG    = 0x40000000;
    private const int FLIPPED_DIAGONALLY_FLAG    = 0x20000000;
    private const int ROTATED_HEXAGONAL_120_FLAG = 0x10000000;

    private const int ID_MASK = ~(FLIPPED_HORIZONTALLY_FLAG |
                                  FLIPPED_VERTICALLY_FLAG |
                                  FLIPPED_DIAGONALLY_FLAG |
                                  ROTATED_HEXAGONAL_120_FLAG);

    /// <summary>
    /// Gets the numeric tile ID with flip/rotate flags masked.
    /// </summary>
    public int Id => value & ID_MASK;
    
    /// <summary>
    /// Gets a flag indicating if the tile should be flipped horizontally when rendered.
    /// </summary>
    public bool FlipHorizontal => (value & FLIPPED_HORIZONTALLY_FLAG) != 0;

    /// <summary>
    /// Gets a flag indicating if the tile should be flipped vertically when rendered.
    /// </summary>
    public bool FlipVertical => (value & FLIPPED_VERTICALLY_FLAG) != 0;
    
    /// <summary>
    /// Gets a flag indicating if the tile should be flipped diagonally when rendered.
    /// </summary>
    public bool FlipDiagonal => (value & FLIPPED_DIAGONALLY_FLAG) != 0;
    
    /// <summary>
    /// Gets a flag indicating whether the tile is rotated 60 degrees clockwise.
    /// </summary>
    /// <remarks>Only valid for hexagonal maps.</remarks>
    public bool Rotate60 => (value & FLIPPED_DIAGONALLY_FLAG) != 0;
    
    /// <summary>
    /// Gets a flag indicating whether the tile is rotated 120 degrees clockwise.
    /// </summary>
    /// <remarks>Only valid for hexagonal maps.</remarks>
    public bool Rotate120 => (value & ROTATED_HEXAGONAL_120_FLAG) != 0;
    
    /// <summary>
    /// Initializes a new instance of a <see cref="Gid"/>.
    /// </summary>
    /// <param name="id">The tile ID.</param>
    /// <param name="flipHorizontal">Flag indicating if tile should be flipped horizontally.</param>
    /// <param name="flipVertical">Flag indicating if tile should be flipped vertically.</param>
    /// <param name="flipDiagonal">
    /// Flag indicating if tile should be flipped diagonally. Only valid for orthogonal and isometric maps.
    /// </param>
    public Gid(int id, bool flipHorizontal, bool flipVertical, bool flipDiagonal = false)
    {
        unchecked
        {
            value = id;
            if (flipHorizontal)
                value |= FLIPPED_HORIZONTALLY_FLAG;
            if (flipVertical)
                value |= FLIPPED_VERTICALLY_FLAG;
            if (flipDiagonal)
                value |= FLIPPED_DIAGONALLY_FLAG;
        }
    }
    
    /// <summary>
    /// Initializes a new instance of a <see cref="Gid"/>.
    /// </summary>
    /// <param name="id">The tile ID.</param>
    /// <param name="flipHorizontal">Flag indicating if tile should be flipped horizontally.</param>
    /// <param name="flipVertical">Flag indicating if tile should be flipped vertically.</param>
    /// <param name="rotate60">For hexagonal maps, indicates if tile will be rotated 60 degrees clockwise.</param>
    /// <param name="rotate120">For hexagonal maps, indicates if tile will be rotated 120 degrees clockwise.</param>
    public Gid(int id, bool flipHorizontal, bool flipVertical, bool rotate60, bool rotate120)
    {
        unchecked
        {
            value = id;
            if (flipHorizontal)
                value |= FLIPPED_HORIZONTALLY_FLAG;
            if (flipVertical)
                value |= FLIPPED_VERTICALLY_FLAG;
            if (rotate60)
                value |= FLIPPED_DIAGONALLY_FLAG;
            if (rotate120)
                value |= ROTATED_HEXAGONAL_120_FLAG;
        }
    }

    /// <summary>
    /// Initializes a new <see cref="Gid"/> with the specified value.
    /// </summary>
    /// <param name="gid">A global tile ID, complete with bit-fields set to indicate flipping/rotating.</param>
    public Gid(int gid)
    {
        value = gid;
    }

    /// <summary>
    /// Initializes a new <see cref="Gid"/> with the specified value.
    /// </summary>
    /// <param name="gid">A global tile ID, complete with bit-fields set to indicate flipping/rotating.</param>
    [CLSCompliant(false)]
    public Gid(uint gid)
    {
        value = Unsafe.As<uint, int>(ref gid);
    }

    /// <inheritdoc />
    public bool Equals(Gid other) => value == other.value;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Gid other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => value;

    /// <inheritdoc />
    public int CompareTo(Gid other) => Id != other.Id ? value.CompareTo(other.value) : Id.CompareTo(other.Id);

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        return obj is Gid other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Gid)}");
    }
    
    /// <summary>
    /// Implicit conversion of a <see cref="Gid"/> to a <see cref="int"/>.
    /// </summary>
    /// <param name="gid">The <see cref="Gid"/> to convert.</param>
    /// <returns>The integral value of the <see cref="Gid"/> with flip/rotate flags masked.</returns>
    public static implicit operator int(Gid gid) => gid.value & ID_MASK;
    
    /// <summary>
    /// Determines whether two specified global tile IDs have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="Gid"/> to compare.</param>
    /// <param name="right">The second <see cref="Gid"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is the same value as <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator ==(Gid left, Gid right) => left.Equals(right);

    /// <summary>
    /// Determines whether two specified global tile IDs have different values.
    /// </summary>
    /// <param name="left">The first <see cref="Gid"/> to compare.</param>
    /// <param name="right">The second <see cref="Gid"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="left"/> is a different than <paramref name="right"/>, otherwise <c>false</c>.
    /// </returns>
    public static bool operator !=(Gid left, Gid right) => !left.Equals(right);
    
    /// <summary>
    /// Indicates whether a specified <see cref="Gid"/> is less than another specified <see cref="Gid"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Gid"/> to compare.</param>
    /// <param name="right">The second <see cref="Gid"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is less than the value of <paramref name="right"/>,
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <(Gid left, Gid right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether a specified <see cref="Gid"/> is greater than another specified <see cref="Gid"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Gid"/> to compare.</param>
    /// <param name="right">The second <see cref="Gid"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is greater than the value of <paramref name="right"/>,
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >(Gid left, Gid right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether a specified <see cref="Gid"/> is less than or equal to another specified <see cref="Gid"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Gid"/> to compare.</param>
    /// <param name="right">The second <see cref="Gid"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is less than or equal to the value of
    /// <paramref name="right"/>, otherwise, <c>false</c>.
    /// </returns>
    public static bool operator <=(Gid left, Gid right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether a specified <see cref="Gid"/> is greater than or equal to another specified <see cref="Gid"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Gid"/> to compare.</param>
    /// <param name="right">The second <see cref="Gid"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is greater than or equal to the value of
    /// <paramref name="right"/>, otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >=(Gid left, Gid right) => left.CompareTo(right) >= 0;
}