using System.Resources;

namespace SharpSurv;

public enum TileId
{
    Grass,
    Log,
    Plank,
    Table,
    Air,
}

public static class TileIdExtensions
{
    public static string GetTexturePath(this TileId tileId)
    {
        return tileId switch
        {
            TileId.Grass => "Assets/Textures/Tiles/grass.png",
            TileId.Air => "Assets/Textures/Tiles/air.png",
            TileId.Log => "Assets/Textures/Tiles/log.png",
            TileId.Plank => "Assets/Textures/Tiles/plank.png",
            TileId.Table => "Assets/Textures/Tiles/table.png",
            _ => "Assets/Textures/Tiles/missing.png"
        };
    }
}