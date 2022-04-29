using System.Diagnostics;
using JetBrains.Annotations;

namespace Anvil.TMX;

/// <summary>
/// Stores a <see cref="Tileset"/> with its respective first GID for a specific map.
/// </summary>
/// <param name="FirstGid">The first global tile ID for the tileset.</param>
/// <param name="Tileset">The <see cref="Tileset"/> instance used by the map.</param>
[PublicAPI, DebuggerDisplay("Name = {Name}, First ID = {FirstGid}")]
public record MapTileset(int FirstGid, Tileset Tileset) : INamed
{
    /// <summary>
    /// Gets the name of the tileset.
    /// </summary>
    public string Name => Tileset.Name;
}