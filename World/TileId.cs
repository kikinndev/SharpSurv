using System.Resources;

namespace Main;

public enum TileId
{
    Grass,
}

public static class TileIdExtensions
{
    public static string GetTexturePath(this TileId tileId)
    {
        return tileId switch
        {
            TileId.Grass => "Resources/Tiles/grass.png",
            _ => "Resources/Tiles/missing.png"
        };
    }
}