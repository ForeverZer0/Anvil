using JetBrains.Annotations;

namespace Anvil.GLFW3;

/// <summary>
/// Strongly-typed constants describing the keys on a keyboard.
/// <para/>
/// Any key input not in this enumeration will default to <see cref="Unknown"/>.
/// </summary>
[PublicAPI]
public enum Key
{
    /// <summary>
    /// The "unknown" key, which is the default value for any unrecognized key not found in this enumeration.
    /// </summary>
    Unknown = -1,
    
    /// <summary>Space</summary>
    Space = 32,
    
    /// <summary>' or "</summary>
    Apostrophe = 39,
    
    /// <summary>, or &lt;</summary>
    Comma = 44,
    
    /// <summary>- or _</summary>
    Minus = 45,
    
    /// <summary>. or &gt;</summary>
    Period = 46,
    
    /// <summary>/ or ?</summary>
    Slash = 47,
    
    /// <summary>0 or ) (main keyboard)</summary>
    Kb0 = 48,
    
    /// <summary>1 or ! (main keyboard)</summary>
    Kb1 = 49,
    
    /// <summary>2 or @ (main keyboard)</summary>
    Kb2 = 50,
    
    /// <summary>3 or # (main keyboard)</summary>
    Kb3 = 51,
    
    /// <summary>4 or $ (main keyboard)</summary>
    Kb4 = 52,
    
    /// <summary>5 or % (main keyboard)</summary>
    Kb5 = 53,
    
    /// <summary>6 or ^ (main keyboard)</summary>
    Kb6 = 54,
    
    /// <summary>7 or & (main keyboard)</summary>
    Kb7 = 55,
    
    /// <summary>8 or * (main keyboard)</summary>
    Kb8 = 56,
    
    /// <summary>9 or ( (main keyboard)</summary>
    Kb9 = 57,
    
    /// <summary>; or :</summary>
    Semicolon = 59,
    
    /// <summary>= or +</summary>
    Equal = 61,
    
    /// <summary>A or a</summary>
    A = 65,
    
    /// <summary>B or b</summary>
    B = 66,
    
    /// <summary>C or c</summary>
    C = 67,
    
    /// <summary>D or d</summary>
    D = 68,
    
    /// <summary>E or e</summary>
    E = 69,
    
    /// <summary>F or f</summary>
    F = 70,
    
    /// <summary>G or g</summary>
    G = 71,
    
    /// <summary>H or h</summary>
    H = 72,
    
    /// <summary>I or i</summary>
    I = 73,
    
    /// <summary>J or j</summary>
    J = 74,
    
    /// <summary>K or k</summary>
    K = 75,
    
    /// <summary>L or l</summary>
    L = 76,
    
    /// <summary>M or m</summary>
    M = 77,
    
    /// <summary>N or n</summary>
    N = 78,
    
    /// <summary>O or o</summary>
    O = 79,
    
    /// <summary>P or p</summary>
    P = 80,
    
    /// <summary>Q or q</summary>
    Q = 81,
    
    /// <summary>R or r</summary>
    R = 82,
    
    /// <summary>S or s</summary>
    S = 83,
    
    /// <summary>T or t</summary>
    T = 84,
    
    /// <summary>U or u</summary>
    U = 85,
    
    /// <summary>V or v</summary>
    V = 86,
    
    /// <summary>W or w</summary>
    W = 87,
    
    /// <summary>X or x</summary>
    X = 88,
    
    /// <summary>Y or y</summary>
    Y = 89,
    
    /// <summary>Z or z</summary>
    Z = 90,
    
    /// <summary>[ or {</summary>
    LeftBracket = 91,
    
    /// <summary>\ or |</summary>
    Backslash = 92,
    
    /// <summary>] or }</summary>
    RightBracket = 93,
    
    /// <summary>~ or `</summary>
    GraveAccent = 96,
    
    /// <summary>World 1</summary>
    World1 = 161,
    
    /// <summary>World 2</summary>
    World2 = 162,
    
    /// <summary>Escape</summary>
    Escape = 256,
    
    /// <summary>Enter</summary>
    Enter = 257,
    
    /// <summary>Tab</summary>
    Tab = 258,
    
    /// <summary>Backspace</summary>
    Backspace = 259,
    
    /// <summary>Insert</summary>
    Insert = 260,
    
    /// <summary>Delete</summary>
    Delete = 261,
    
    /// <summary>Right arrow</summary>
    Right = 262,
    
    /// <summary>Left arrow</summary>
    Left = 263,
    
    /// <summary>Down arrow</summary>
    Down = 264,
    
    /// <summary>Up arrow</summary>
    Up = 265,
    
    /// <summary>Page Up</summary>
    PageUp = 266,
    
    /// <summary>Page Down</summary>
    PageDown = 267,
    
    /// <summary>Home</summary>
    Home = 268,
    
    /// <summary>End</summary>
    End = 269,
    
    /// <summary>Caps Lock</summary>
    CapsLock = 280,
    
    /// <summary>Scroll Lock</summary>
    ScrollLock = 281,
    
    /// <summary>Num Lock</summary>
    NumLock = 282,
    
    /// <summary>Print Screen</summary>
    PrintScreen = 283,
    
    /// <summary>Pause</summary>
    Pause = 284,
    
    /// <summary>F1</summary>
    F1 = 290,
    
    /// <summary>F2</summary>
    F2 = 291,
    
    /// <summary>F3</summary>
    F3 = 292,
    
    /// <summary>F4</summary>
    F4 = 293,
    
    /// <summary>F5</summary>
    F5 = 294,
    
    /// <summary>F6</summary>
    F6 = 295,
    
    /// <summary>F7</summary>
    F7 = 296,
    
    /// <summary>F8</summary>
    F8 = 297,
    
    /// <summary>F9</summary>
    F9 = 298,
    
    /// <summary>F10</summary>
    F10 = 299,
    
    /// <summary>F11</summary>
    F11 = 300,
    
    /// <summary>F12</summary>
    F12 = 301,
    
    /// <summary>F13</summary>
    F13 = 302,
    
    /// <summary>F14</summary>
    F14 = 303,
    
    /// <summary>F15</summary>
    F15 = 304,
    
    /// <summary>F16</summary>
    F16 = 305,
    
    /// <summary>F17</summary>
    F17 = 306,
    
    /// <summary>F18</summary>
    F18 = 307,
    
    /// <summary>F19</summary>
    F19 = 308,
    
    /// <summary>F20</summary>
    F20 = 309,
    
    /// <summary>F21</summary>
    F21 = 310,
    
    /// <summary>F22</summary>
    F22 = 311,
    
    /// <summary>F23</summary>
    F23 = 312,
    
    /// <summary>F24</summary>
    F24 = 313,
    
    /// <summary>F25</summary>
    F25 = 314,
    
    /// <summary>0 (number pad)</summary>
    Kp0 = 320,
    
    /// <summary>1 (number pad)</summary>
    Kp1 = 321,
    
    /// <summary>2 (number pad)</summary>
    Kp2 = 322,
    
    /// <summary>3 (number pad)</summary>
    Kp3 = 323,
    
    /// <summary>4 (number pad)</summary>
    Kp4 = 324,
    
    /// <summary>5 (number pad)</summary>
    Kp5 = 325,
    
    /// <summary>6 (number pad)</summary>
    Kp6 = 326,
    
    /// <summary>7 (number pad)</summary>
    Kp7 = 327,
    
    /// <summary>8 (number pad)</summary>
    Kp8 = 328,
    
    /// <summary>9 (number pad)</summary>
    Kp9 = 329,
    
    /// <summary>. (number pad)</summary>
    KpDecimal = 330,
    
    /// <summary>/ (number pad)</summary>
    KpDivide = 331,
    
    /// <summary>* (number pad)</summary>
    KpMultiply = 332,
    
    /// <summary>- (number pad)</summary>
    KpSubtract = 333,
    
    /// <summary>+ (number pad)</summary>
    KpAdd = 334,
    
    /// <summary>Ender (number pad)</summary>
    KpEnter = 335,
    
    /// <summary>= (number pad)</summary>
    KpEqual = 336,
    
    /// <summary>Left Shift</summary>
    LeftShift = 340,
    
    /// <summary>Left Control</summary>
    LeftControl = 341,
    
    /// <summary>Left Alt</summary>
    LeftAlt = 342,
    
    /// <summary>Left Super</summary>
    LeftSuper = 343,
    
    /// <summary>Right Shift</summary>
    RightShift = 344,
    
    /// <summary>Right Control</summary>
    RightControl = 345,
    
    /// <summary>Right Alt</summary>
    RightAlt = 346,
    
    /// <summary>Right Super</summary>
    RightSuper = 347,
    
    /// <summary>Menu</summary>
    Menu = 348,
    
    /// <summary>The first valid key.</summary>
    First = Space,
    
    /// <summary>The last valid key.</summary>
    Last = Menu,
}