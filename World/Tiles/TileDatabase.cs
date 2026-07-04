using Raylib_cs;
using System.Numerics;

namespace Main;

public static class TileDatabase
{
    private static Dictionary<TileId, TileData> tileData = new Dictionary<TileId, TileData>();

    public static void Load()
    {
        Add(TileId.Grass, "Assets/Textures/Tiles/grass.png", false);
        Add(TileId.Log, "Assets/Textures/Tiles/log.png", true);
        Add(TileId.Table, "Assets/Textures/Tiles/table.png", true);
    }

    public static void Add(TileId id, string texturePath, bool isSolid)
    {
        Texture2D texture = Raylib.LoadTexture(texturePath);
        tileData[id] = new TileData(texture, isSolid);
    }

    public static Texture2D GetTexture(TileId tileId)
    {
        return tileData[tileId].texture;
    }

    public static void Unload()
    {
        foreach (TileData data in tileData.Values)
        {
            Raylib.UnloadTexture(data.texture);
        }

        tileData.Clear();
    }
}