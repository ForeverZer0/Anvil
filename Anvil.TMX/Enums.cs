using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Describes the type of an external resource.
/// </summary>
[PublicAPI]
public enum ResourceType
{
    /// <summary>
    /// An XML map file.
    /// </summary>
    /// <remarks>*.tmx</remarks>
    Map,
    
    /// <summary>
    /// An XML tileset file.
    /// </summary>
    /// <remarks>*.tsx</remarks>
    Tileset,
    
    /// <summary>
    /// An XML template file.
    /// </summary>
    /// <remarks>*.tx</remarks>
    Template,
    
    /// <summary>
    /// An image file.
    /// </summary>
    /// <remarks>*.png, *.jpg, *.bmp, etc.</remarks>
    Image
}

/// <summary>
/// Describes the general type of a <see cref="MapObject"/>.
/// </summary>
[PublicAPI]
public enum ObjectType
{
    /// <summary>
    /// An undefined/unspecified object type.
    /// </summary>
    Unspecified,
    
    /// <summary>
    /// Represents a bounding box in tile collision data.
    /// </summary>
    Rectangle,
    
    /// <summary>
    /// Represents a tile from a tileset.
    /// </summary>
    Tile,
    
    /// <summary>
    /// Describes an elliptical shape.
    /// </summary>
    Ellipse,
    
    /// <summary>
    /// Describes a single point on the map.
    /// </summary>
    Point,
    
    /// <summary>
    /// Describes a closed shape, where the first and last point are connected.
    /// </summary>
    Polygon,
    
    /// <summary>
    /// Describes a collection of connected lines.
    /// </summary>
    Polyline,
    
    /// <summary>
    /// Represents text to be rendered.
    /// </summary>
    Text
}

/// <summary>
/// Describes the encoding used for the payload of a <see cref="DataReader"/> object within an source file.
/// </summary>
[PublicAPI]
public enum DataEncoding
{
    /// <summary>
    /// No encoding, data is a collection of child elements.
    /// </summary>
    None,
    
    /// <summary>
    /// Base64 encoded string as the element contents.
    /// <para/>
    /// This is the required encoding if compression is used.
    /// </summary>
    Base64,
    
    /// <summary>
    /// The element text is a continuous string of comma-separated values.
    /// </summary>
    Csv
}

/// <summary>
/// Describes the type of compression used for the payload of a <see cref="DataReader"/> object within a source file.
/// </summary>
[PublicAPI]
public enum DataCompression
{
    /// <summary>
    /// No compression is used, valid with all encodings.
    /// <para/>
    /// Required for <see cref="DataEncoding.Csv"/> and <see cref="DataEncoding.None"/> encodings.
    /// </summary>
    None,
    
    /// <summary>
    /// Gzip compressed binary string.
    /// <para/>
    /// <see cref="DataEncoding.Base64"/> encoding is required.
    /// </summary>
    Gzip,
    
    /// <summary>
    /// Zlib compressed binary string.
    /// <para/>
    /// <see cref="DataEncoding.Base64"/> encoding is required.
    /// </summary>
    Zlib,
    
    /// <summary>
    /// Zstandard compressed binary string.
    /// <para/>
    /// <see cref="DataEncoding.Base64"/> encoding is required.
    /// </summary>
    /// <remarks>Not supported by this library.</remarks>
    [Obsolete("Not currently supported by this library.")]
    Zstd
}

/// <summary>
/// Flags describing the relative alignment to use for placing map objects.
/// </summary>
[Flags, PublicAPI]
public enum ObjectAlignment
{
    /// <summary>
    /// An undefined alignment, which will default to <see cref="BottomLeft"/> for orthogonal maps and
    /// <see cref="Bottom"/> for isometric maps.
    /// </summary>
    Unspecified = 0x00,
    
    /// <summary>Top</summary>
    Top = 0x01,
    
    /// <summary>Left</summary>
    Left = 0x02, 
    
    /// <summary>Right</summary>
    Right = 0x04, 
    
    /// <summary>Bottom</summary>
    Bottom = 0x08,
    
    /// <summary>Top-left</summary>
    TopLeft = Top | Left,
    
    /// <summary>Top-right</summary>
    TopRight = Top | Right,
    
    /// <summary>Bottom-left</summary>
    BottomLeft = Bottom | Left,
    
    /// <summary>Bottom-right</summary>
    BottomRight = Bottom | Right,
    
    /// <summary>Centered</summary>
    Center = Top | Left | Right | Bottom
}

/// <summary>
/// Describes the order in which objects are drawn relative to each other within an <see cref="ObjectGroup"/> or
/// <see cref="ObjectLayer"/>.
/// </summary>
[PublicAPI]
public enum DrawOrder
{
    /// <summary>
    /// Sorted by the y-coordinate.
    /// </summary>
    TopDown,
    
    /// <summary>
    /// Draw according to their appearance.
    /// </summary>
    Index,
}

/// <summary>
/// Strongly-typed constants representing the indices within a <see cref="WangTile"/>.
/// </summary>
[PublicAPI]
public enum WangIndex
{
    /// <summary>
    /// A null value to unset.
    /// </summary>
    Unset = 0,
    
    /// <summary>Top</summary>
    Top,
    
    /// <summary>Top-right</summary>
    TopRight,
    
    /// <summary>Right</summary>
    Right,
    
    /// <summary>Bottom-right</summary>
    BottomRight,
    
    /// <summary>Bottom</summary>
    Bottom,
    
    /// <summary>Bottom-left</summary>
    BottomLeft,
    
    /// <summary>Left</summary>
    Left,
    
    /// <summary>Top-left</summary>
    TopLeft
}

/// <summary>
/// Flags used to filter certain layer types during enumeration.
/// </summary>
[Flags, PublicAPI]
public enum LayerType : byte
{
    /// <summary>
    /// No layer types defined.
    /// </summary>
    None = 0x00,
    
    /// <summary>
    /// Tile layers.
    /// </summary>
    Tile = 0x01,
    
    /// <summary>
    /// Image layers.
    /// </summary>
    Image = 0x02,
    
    /// <summary>
    /// Object group layers.
    /// </summary>
    Object = 0x04,
    
    /// <summary>
    /// Layer groups.
    /// </summary>
    Group = 0x08,
    
    /// <summary>
    /// All layer types.
    /// </summary>
    All = 0xFF
}

/// <summary>
/// Describes the data type of a <see cref="Property"/>.
/// </summary>
[PublicAPI]
public enum PropertyType
{
    /// <summary>
    /// A <see cref="string"/>.
    /// </summary>
    String,
    
    /// <summary>
    /// A <see cref="int"/>.
    /// </summary>
    Int,
    
    /// <summary>
    /// A <see cref="float"/>.
    /// </summary>
    Float,
    
    /// <summary>
    /// A <see cref="bool"/>.
    /// </summary>
    Bool,
    
    /// <summary>
    /// A <see cref="ColorF"/>.
    /// </summary>
    Color,
    
    /// <summary>
    /// A <see cref="string"/> path for a filesystem path.
    /// </summary>
    File,
    
    /// <summary>
    /// A <see cref="int"/> ID of a <see cref="MapObject"/>.
    /// </summary>
    Object,
    
    /// <summary>
    /// A <see cref="PropertySet"/> containing child properties.
    /// </summary>
    /// <seealso cref="Property.CustomType"/>
    Class
}

/// <summary>
/// Flags describing the style of rendered text.
/// </summary>
[Flags, PublicAPI]
public enum TextStyle
{
    /// <summary>
    /// None/standard style.
    /// </summary>
    None,
    
    /// <summary>
    /// <b>Bold</b>
    /// </summary>
    Bold = 0x01,
    
    /// <summary>
    /// <i>Italic</i>
    /// </summary>
    Italic = 0x02,
    
    /// <summary>
    /// Underlined
    /// </summary>
    Underline = 0x04,
    
    /// <summary>
    /// Strikeout
    /// </summary>
    Strikeout = 0x08
}

/// <summary>
/// Flags describing the alignment of rendered text.
/// </summary>
[Flags, PublicAPI]
public enum TextAlign
{
    /// <summary>
    /// Undefined, will default to <see cref="Top"/> and <see cref="Left"/>.
    /// </summary>
    Unspecified = 0x00,
    
    /// <summary>Left</summary>
    Left = 0x01,
    
    /// <summary>Right</summary>
    Right = 0x02,
    
    /// <summary>Centered horizontally</summary>
    CenterH = Left | Right,
    
    /// <summary>Top</summary>
    Top = 0x04,
    
    /// <summary>Bottom</summary>
    Bottom = 0x08,
    
    /// <summary>Centered vertically.</summary>
    CenterV = Top | Bottom,
    
    /// <summary>Centered horizontally and vertically.</summary>
    Center = CenterH | CenterV,
    
    /// <summary>Justified</summary>
    Justify = 0x10,
    
    /// <summary>Default (<see cref="Left"/> and <see cref="Top"/>)</summary>
    Default = Left | Top
}

/// <summary>
/// Describes which axis gets staggered for staggered and hexagonal maps.
/// </summary>
[PublicAPI]
public enum StaggerAxis
{
    /// <summary>
    /// Staggered along the x-axis.
    /// </summary>
    X,
    
    /// <summary>
    /// Staggered along the y-axis.
    /// </summary>
    Y
}

/// <summary>
/// Describes which indices get staggered along the axis for staggered and hexagonal maps.
/// </summary>
[PublicAPI]
public enum StaggerIndex
{
    /// <summary>
    /// Even (i.e. 0, 2, 4...) indices get staggered.
    /// </summary>
    Even,
    
    /// <summary>
    /// Odd (i.e. 1, 3, 5...) indices get staggered.
    /// </summary>
    Odd
}

/// <summary>
/// Describes the order in which tile layers rows are rendered on orthogonal maps.
/// </summary>
[PublicAPI]
public enum RenderOrder
{
    /// <summary>
    /// Draw rows from left to right, top to bottom. (default)
    /// </summary>
    RightDown,
    
    /// <summary>
    /// Draw rows from left to right, bottom to top.
    /// </summary>
    RightUp,
    
    /// <summary>
    /// Draw rows from right to left, top to bottom.
    /// </summary>
    LeftDown,
    
    /// <summary>
    /// Draw rows from right to left, bottom to top.
    /// </summary>
    LeftUp
}

/// <summary>
/// Describes the orientation of the map, which determines how it is rendered and how its data is interpreted.
/// </summary>
[PublicAPI]
public enum Orientation
{
    /// <summary>
    /// Default top-down "square" map.
    /// <para/>
    /// Examples:
    /// <list type="bullet">
    /// <item>Zelda: A Link to the Past</item>
    /// <item>Final Fantasy VI</item>
    /// </list>
    /// </summary>
    Orthogonal, 
    
    /// <summary>
    /// Projected isometric perspective.
    /// <para/>
    /// Examples:
    /// <list type="bullet">
    /// <item>Super Mario RPG</item>
    /// <item>Final Fantasy Tactics</item>
    /// <item>Starcraft</item>
    /// </list>
    /// </summary>
    Isometric, 
    
    /// <summary>
    /// A form of isometric perspective, but differs in that it is still rectangular, and not "diamond" shaped.
    /// <list type="bullet">
    /// <item>Civilization 2</item>
    /// </list>
    /// </summary>
    Staggered,
    
    /// <summary>
    /// Hexagonal tiling, where instead of a square, each tile is a hexagon.
    /// <list type="bullet">
    /// <item>The Battle for Wesnoth</item>
    /// </list>
    /// </summary>
    Hexagonal
}

