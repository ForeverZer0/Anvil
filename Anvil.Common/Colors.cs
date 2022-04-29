// ReSharper disable UnusedMember.Global
// ReSharper disable CommentTypo
// ReSharper disable RedundantArgumentDefaultValue

using JetBrains.Annotations;

namespace Anvil;

/// <summary>
/// Static class containing pre-defined <see cref="ColorF"/> fields.
/// </summary>
[PublicAPI]
public static class Colors
{
    /// <summary>
    /// A color with an RGBA of #00000000.
    /// </summary>
    public static readonly ColorF None;
        
    /// <summary>
    /// A color with an RGBA of #FFFFFF00.
    /// </summary>
    public static readonly ColorF Transparent = new(1.00000f, 1.00000f, 1.00000f, 0.00000f);

    /// <summary>
    /// A color with an RGBA of #F0F8FFFF.
    /// </summary>
    public static readonly ColorF AliceBlue = new(0.94118f, 0.97255f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FAEBD7FF.
    /// </summary>
    public static readonly ColorF AntiqueWhite = new(0.98039f, 0.92157f, 0.84314f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00FFFFFF.
    /// </summary>
    public static readonly ColorF Aqua = new(0.00000f, 1.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #7FFFD4FF.
    /// </summary>
    public static readonly ColorF Aquamarine = new(0.49804f, 1.00000f, 0.83137f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F0FFFFFF.
    /// </summary>
    public static readonly ColorF Azure = new(0.94118f, 1.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F5F5DCFF.
    /// </summary>
    public static readonly ColorF Beige = new(0.96078f, 0.96078f, 0.86275f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFE4C4FF.
    /// </summary>
    public static readonly ColorF Bisque = new(1.00000f, 0.89412f, 0.76863f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #000000FF.
    /// </summary>
    public static readonly ColorF Black = new(0.00000f, 0.00000f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFEBCDFF.
    /// </summary>
    public static readonly ColorF BlanchedAlmond = new(1.00000f, 0.92157f, 0.80392f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #0000FFFF.
    /// </summary>
    public static readonly ColorF Blue = new(0.00000f, 0.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #8A2BE2FF.
    /// </summary>
    public static readonly ColorF BlueViolet = new(0.54118f, 0.16863f, 0.88627f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #A52A2AFF.
    /// </summary>
    public static readonly ColorF Brown = new(0.64706f, 0.16471f, 0.16471f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #DEB887FF.
    /// </summary>
    public static readonly ColorF BurlyWood = new(0.87059f, 0.72157f, 0.52941f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #5F9EA0FF.
    /// </summary>
    public static readonly ColorF CadetBlue = new(0.37255f, 0.61961f, 0.62745f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #7FFF00FF.
    /// </summary>
    public static readonly ColorF Chartreuse = new(0.49804f, 1.00000f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #D2691EFF.
    /// </summary>
    public static readonly ColorF Chocolate = new(0.82353f, 0.41176f, 0.11765f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF7F50FF.
    /// </summary>
    public static readonly ColorF Coral = new(1.00000f, 0.49804f, 0.31373f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #6495EDFF.
    /// </summary>
    public static readonly ColorF CornflowerBlue = new(0.39216f, 0.58431f, 0.92941f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFF8DCFF.
    /// </summary>
    public static readonly ColorF Cornsilk = new(1.00000f, 0.97255f, 0.86275f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #DC143CFF.
    /// </summary>
    public static readonly ColorF Crimson = new(0.86275f, 0.07843f, 0.23529f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00FFFFFF.
    /// </summary>
    public static readonly ColorF Cyan = new(0.00000f, 1.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00008BFF.
    /// </summary>
    public static readonly ColorF DarkBlue = new(0.00000f, 0.00000f, 0.54510f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #008B8BFF.
    /// </summary>
    public static readonly ColorF DarkCyan = new(0.00000f, 0.54510f, 0.54510f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #B8860BFF.
    /// </summary>
    public static readonly ColorF DarkGoldenrod = new(0.72157f, 0.52549f, 0.04314f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #A9A9A9FF.
    /// </summary>
    public static readonly ColorF DarkGray = new(0.66275f, 0.66275f, 0.66275f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #006400FF.
    /// </summary>
    public static readonly ColorF DarkGreen = new(0.00000f, 0.39216f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #BDB76BFF.
    /// </summary>
    public static readonly ColorF DarkKhaki = new(0.74118f, 0.71765f, 0.41961f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #8B008BFF.
    /// </summary>
    public static readonly ColorF DarkMagenta = new(0.54510f, 0.00000f, 0.54510f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #556B2FFF.
    /// </summary>
    public static readonly ColorF DarkOliveGreen = new(0.33333f, 0.41961f, 0.18431f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF8C00FF.
    /// </summary>
    public static readonly ColorF DarkOrange = new(1.00000f, 0.54902f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #9932CCFF.
    /// </summary>
    public static readonly ColorF DarkOrchid = new(0.60000f, 0.19608f, 0.80000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #8B0000FF.
    /// </summary>
    public static readonly ColorF DarkRed = new(0.54510f, 0.00000f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #E9967AFF.
    /// </summary>
    public static readonly ColorF DarkSalmon = new(0.91373f, 0.58824f, 0.47843f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #8FBC8BFF.
    /// </summary>
    public static readonly ColorF DarkSeaGreen = new(0.56078f, 0.73725f, 0.54510f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #483D8BFF.
    /// </summary>
    public static readonly ColorF DarkSlateBlue = new(0.28235f, 0.23922f, 0.54510f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #2F4F4FFF.
    /// </summary>
    public static readonly ColorF DarkSlateGray = new(0.18431f, 0.30980f, 0.30980f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00CED1FF.
    /// </summary>
    public static readonly ColorF DarkTurquoise = new(0.00000f, 0.80784f, 0.81961f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #9400D3FF.
    /// </summary>
    public static readonly ColorF DarkViolet = new(0.58039f, 0.00000f, 0.82745f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF1493FF.
    /// </summary>
    public static readonly ColorF DeepPink = new(1.00000f, 0.07843f, 0.57647f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00BFFFFF.
    /// </summary>
    public static readonly ColorF DeepSkyBlue = new(0.00000f, 0.74902f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #696969FF.
    /// </summary>
    public static readonly ColorF DimGray = new(0.41176f, 0.41176f, 0.41176f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #1E90FFFF.
    /// </summary>
    public static readonly ColorF DodgerBlue = new(0.11765f, 0.56471f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #B22222FF.
    /// </summary>
    public static readonly ColorF Firebrick = new(0.69804f, 0.13333f, 0.13333f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFFAF0FF.
    /// </summary>
    public static readonly ColorF FloralWhite = new(1.00000f, 0.98039f, 0.94118f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #228B22FF.
    /// </summary>
    public static readonly ColorF ForestGreen = new(0.13333f, 0.54510f, 0.13333f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF00FFFF.
    /// </summary>
    public static readonly ColorF Fuchsia = new(1.00000f, 0.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #DCDCDCFF.
    /// </summary>
    public static readonly ColorF Gainsboro = new(0.86275f, 0.86275f, 0.86275f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F8F8FFFF.
    /// </summary>
    public static readonly ColorF GhostWhite = new(0.97255f, 0.97255f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFD700FF.
    /// </summary>
    public static readonly ColorF Gold = new(1.00000f, 0.84314f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #DAA520FF.
    /// </summary>
    public static readonly ColorF Goldenrod = new(0.85490f, 0.64706f, 0.12549f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #808080FF.
    /// </summary>
    public static readonly ColorF Gray = new(0.50196f, 0.50196f, 0.50196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #008000FF.
    /// </summary>
    public static readonly ColorF Green = new(0.00000f, 0.50196f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #ADFF2FFF.
    /// </summary>
    public static readonly ColorF GreenYellow = new(0.67843f, 1.00000f, 0.18431f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F0FFF0FF.
    /// </summary>
    public static readonly ColorF Honeydew = new(0.94118f, 1.00000f, 0.94118f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF69B4FF.
    /// </summary>
    public static readonly ColorF HotPink = new(1.00000f, 0.41176f, 0.70588f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #CD5C5CFF.
    /// </summary>
    public static readonly ColorF IndianRed = new(0.80392f, 0.36078f, 0.36078f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #4B0082FF.
    /// </summary>
    public static readonly ColorF Indigo = new(0.29412f, 0.00000f, 0.50980f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFFFF0FF.
    /// </summary>
    public static readonly ColorF Ivory = new(1.00000f, 1.00000f, 0.94118f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F0E68CFF.
    /// </summary>
    public static readonly ColorF Khaki = new(0.94118f, 0.90196f, 0.54902f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #E6E6FAFF.
    /// </summary>
    public static readonly ColorF Lavender = new(0.90196f, 0.90196f, 0.98039f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFF0F5FF.
    /// </summary>
    public static readonly ColorF LavenderBlush = new(1.00000f, 0.94118f, 0.96078f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #7CFC00FF.
    /// </summary>
    public static readonly ColorF LawnGreen = new(0.48627f, 0.98824f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFFACDFF.
    /// </summary>
    public static readonly ColorF LemonChiffon = new(1.00000f, 0.98039f, 0.80392f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #ADD8E6FF.
    /// </summary>
    public static readonly ColorF LightBlue = new(0.67843f, 0.84706f, 0.90196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F08080FF.
    /// </summary>
    public static readonly ColorF LightCoral = new(0.94118f, 0.50196f, 0.50196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #E0FFFFFF.
    /// </summary>
    public static readonly ColorF LightCyan = new(0.87843f, 1.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FAFAD2FF.
    /// </summary>
    public static readonly ColorF LightGoldenrodYellow = new(0.98039f, 0.98039f, 0.82353f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #D3D3D3FF.
    /// </summary>
    public static readonly ColorF LightGray = new(0.82745f, 0.82745f, 0.82745f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #90EE90FF.
    /// </summary>
    public static readonly ColorF LightGreen = new(0.56471f, 0.93333f, 0.56471f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFB6C1FF.
    /// </summary>
    public static readonly ColorF LightPink = new(1.00000f, 0.71373f, 0.75686f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFA07AFF.
    /// </summary>
    public static readonly ColorF LightSalmon = new(1.00000f, 0.62745f, 0.47843f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #20B2AAFF.
    /// </summary>
    public static readonly ColorF LightSeaGreen = new(0.12549f, 0.69804f, 0.66667f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #87CEFAFF.
    /// </summary>
    public static readonly ColorF LightSkyBlue = new(0.52941f, 0.80784f, 0.98039f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #778899FF.
    /// </summary>
    public static readonly ColorF LightSlateGray = new(0.46667f, 0.53333f, 0.60000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #B0C4DEFF.
    /// </summary>
    public static readonly ColorF LightSteelBlue = new(0.69020f, 0.76863f, 0.87059f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFFFE0FF.
    /// </summary>
    public static readonly ColorF LightYellow = new(1.00000f, 1.00000f, 0.87843f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00FF00FF.
    /// </summary>
    public static readonly ColorF Lime = new(0.00000f, 1.00000f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #32CD32FF.
    /// </summary>
    public static readonly ColorF LimeGreen = new(0.19608f, 0.80392f, 0.19608f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FAF0E6FF.
    /// </summary>
    public static readonly ColorF Linen = new(0.98039f, 0.94118f, 0.90196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF00FFFF.
    /// </summary>
    public static readonly ColorF Magenta = new(1.00000f, 0.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #800000FF.
    /// </summary>
    public static readonly ColorF Maroon = new(0.50196f, 0.00000f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #66CDAAFF.
    /// </summary>
    public static readonly ColorF MediumAquamarine = new(0.40000f, 0.80392f, 0.66667f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #0000CDFF.
    /// </summary>
    public static readonly ColorF MediumBlue = new(0.00000f, 0.00000f, 0.80392f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #BA55D3FF.
    /// </summary>
    public static readonly ColorF MediumOrchid = new(0.72941f, 0.33333f, 0.82745f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #9370DBFF.
    /// </summary>
    public static readonly ColorF MediumPurple = new(0.57647f, 0.43922f, 0.85882f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #3CB371FF.
    /// </summary>
    public static readonly ColorF MediumSeaGreen = new(0.23529f, 0.70196f, 0.44314f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #7B68EEFF.
    /// </summary>
    public static readonly ColorF MediumSlateBlue = new(0.48235f, 0.40784f, 0.93333f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00FA9AFF.
    /// </summary>
    public static readonly ColorF MediumSpringGreen = new(0.00000f, 0.98039f, 0.60392f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #48D1CCFF.
    /// </summary>
    public static readonly ColorF MediumTurquoise = new(0.28235f, 0.81961f, 0.80000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #C71585FF.
    /// </summary>
    public static readonly ColorF MediumVioletRed = new(0.78039f, 0.08235f, 0.52157f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #191970FF.
    /// </summary>
    public static readonly ColorF MidnightBlue = new(0.09804f, 0.09804f, 0.43922f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F5FFFAFF.
    /// </summary>
    public static readonly ColorF MintCream = new(0.96078f, 1.00000f, 0.98039f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFE4E1FF.
    /// </summary>
    public static readonly ColorF MistyRose = new(1.00000f, 0.89412f, 0.88235f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFE4B5FF.
    /// </summary>
    public static readonly ColorF Moccasin = new(1.00000f, 0.89412f, 0.70980f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFDEADFF.
    /// </summary>
    public static readonly ColorF NavajoWhite = new(1.00000f, 0.87059f, 0.67843f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #000080FF.
    /// </summary>
    public static readonly ColorF Navy = new(0.00000f, 0.00000f, 0.50196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FDF5E6FF.
    /// </summary>
    public static readonly ColorF OldLace = new(0.99216f, 0.96078f, 0.90196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #808000FF.
    /// </summary>
    public static readonly ColorF Olive = new(0.50196f, 0.50196f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #6B8E23FF.
    /// </summary>
    public static readonly ColorF OliveDrab = new(0.41961f, 0.55686f, 0.13725f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFA500FF.
    /// </summary>
    public static readonly ColorF Orange = new(1.00000f, 0.64706f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF4500FF.
    /// </summary>
    public static readonly ColorF OrangeRed = new(1.00000f, 0.27059f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #DA70D6FF.
    /// </summary>
    public static readonly ColorF Orchid = new(0.85490f, 0.43922f, 0.83922f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #EEE8AAFF.
    /// </summary>
    public static readonly ColorF PaleGoldenrod = new(0.93333f, 0.90980f, 0.66667f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #98FB98FF.
    /// </summary>
    public static readonly ColorF PaleGreen = new(0.59608f, 0.98431f, 0.59608f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #AFEEEEFF.
    /// </summary>
    public static readonly ColorF PaleTurquoise = new(0.68627f, 0.93333f, 0.93333f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #DB7093FF.
    /// </summary>
    public static readonly ColorF PaleVioletRed = new(0.85882f, 0.43922f, 0.57647f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFEFD5FF.
    /// </summary>
    public static readonly ColorF PapayaWhip = new(1.00000f, 0.93725f, 0.83529f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFDAB9FF.
    /// </summary>
    public static readonly ColorF PeachPuff = new(1.00000f, 0.85490f, 0.72549f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #CD853FFF.
    /// </summary>
    public static readonly ColorF Peru = new(0.80392f, 0.52157f, 0.24706f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFC0CBFF.
    /// </summary>
    public static readonly ColorF Pink = new(1.00000f, 0.75294f, 0.79608f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #DDA0DDFF.
    /// </summary>
    public static readonly ColorF Plum = new(0.86667f, 0.62745f, 0.86667f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #B0E0E6FF.
    /// </summary>
    public static readonly ColorF PowderBlue = new(0.69020f, 0.87843f, 0.90196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #800080FF.
    /// </summary>
    public static readonly ColorF Purple = new(0.50196f, 0.00000f, 0.50196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF0000FF.
    /// </summary>
    public static readonly ColorF Red = new(1.00000f, 0.00000f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #BC8F8FFF.
    /// </summary>
    public static readonly ColorF RosyBrown = new(0.73725f, 0.56078f, 0.56078f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #4169E1FF.
    /// </summary>
    public static readonly ColorF RoyalBlue = new(0.25490f, 0.41176f, 0.88235f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #8B4513FF.
    /// </summary>
    public static readonly ColorF SaddleBrown = new(0.54510f, 0.27059f, 0.07451f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FA8072FF.
    /// </summary>
    public static readonly ColorF Salmon = new(0.98039f, 0.50196f, 0.44706f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F4A460FF.
    /// </summary>
    public static readonly ColorF SandyBrown = new(0.95686f, 0.64314f, 0.37647f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #2E8B57FF.
    /// </summary>
    public static readonly ColorF SeaGreen = new(0.18039f, 0.54510f, 0.34118f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFF5EEFF.
    /// </summary>
    public static readonly ColorF SeaShell = new(1.00000f, 0.96078f, 0.93333f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #A0522DFF.
    /// </summary>
    public static readonly ColorF Sienna = new(0.62745f, 0.32157f, 0.17647f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #C0C0C0FF.
    /// </summary>
    public static readonly ColorF Silver = new(0.75294f, 0.75294f, 0.75294f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #87CEEBFF.
    /// </summary>
    public static readonly ColorF SkyBlue = new(0.52941f, 0.80784f, 0.92157f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #6A5ACDFF.
    /// </summary>
    public static readonly ColorF SlateBlue = new(0.41569f, 0.35294f, 0.80392f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #708090FF.
    /// </summary>
    public static readonly ColorF SlateGray = new(0.43922f, 0.50196f, 0.56471f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFFAFAFF.
    /// </summary>
    public static readonly ColorF Snow = new(1.00000f, 0.98039f, 0.98039f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #00FF7FFF.
    /// </summary>
    public static readonly ColorF SpringGreen = new(0.00000f, 1.00000f, 0.49804f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #4682B4FF.
    /// </summary>
    public static readonly ColorF SteelBlue = new(0.27451f, 0.50980f, 0.70588f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #D2B48CFF.
    /// </summary>
    public static readonly ColorF Tan = new(0.82353f, 0.70588f, 0.54902f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #008080FF.
    /// </summary>
    public static readonly ColorF Teal = new(0.00000f, 0.50196f, 0.50196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #D8BFD8FF.
    /// </summary>
    public static readonly ColorF Thistle = new(0.84706f, 0.74902f, 0.84706f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FF6347FF.
    /// </summary>
    public static readonly ColorF Tomato = new(1.00000f, 0.38824f, 0.27843f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #40E0D0FF.
    /// </summary>
    public static readonly ColorF Turquoise = new(0.25098f, 0.87843f, 0.81569f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #EE82EEFF.
    /// </summary>
    public static readonly ColorF Violet = new(0.93333f, 0.50980f, 0.93333f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F5DEB3FF.
    /// </summary>
    public static readonly ColorF Wheat = new(0.96078f, 0.87059f, 0.70196f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFFFFFFF.
    /// </summary>
    public static readonly ColorF White = new(1.00000f, 1.00000f, 1.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #F5F5F5FF.
    /// </summary>
    public static readonly ColorF WhiteSmoke = new(0.96078f, 0.96078f, 0.96078f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #FFFF00FF.
    /// </summary>
    public static readonly ColorF Yellow = new(1.00000f, 1.00000f, 0.00000f, 1.00000f);

    /// <summary>
    /// A color with an RGBA of #9ACD32FF.
    /// </summary>
    public static readonly ColorF YellowGreen = new(0.60392f, 0.80392f, 0.19608f, 1.00000f);
}