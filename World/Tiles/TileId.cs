using System.Resources;

namespace Main;

public enum TileId
{
    Grass,
    Log,
    Table,
}

public static class TileIdExtensions
{
    public static string GetTexturePath(this TileId tileId)
    {
        return tileId switch
        {
            TileId.Grass => "Assets/Textures/Tiles/grass.png",
            TileId.Log => "Assets/Textures/Tiles/log.png",
            TileId.Table => "Assets/Textures/Tiles/table.png",
            _ => "Assets/Textures/Tiles/missing.png"
        };
    }
}