using Raylib_cs;
using System.Numerics;

namespace SharpSurv;

public static class TileDatabase
{
    public static Dictionary<TileId, TileData> tileData = new();

    public static void Load()
    {
        Add(TileId.Air, "Assets/Textures/Tiles/air.png", false, -1);
        Add(TileId.Grass, "Assets/Textures/Tiles/grass.png", false, 1);
        Add(TileId.Log, "Assets/Textures/Tiles/log.png", true, 3);
        Add(TileId.Plank, "Assets/Textures/Tiles/plank.png", true, 3);
        Add(TileId.Table, "Assets/Textures/Tiles/table.png", true, 2);
    }

    public static void Add(TileId id, string texturePath, bool isSolid, int hp)
    {
        Console.WriteLine($"Creating tile data with HP {hp}");
        Texture2D texture = Raylib.LoadTexture(texturePath);
        tileData[id] = new TileData(texture, isSolid, 4, hp);
    }

    public static Texture2D GetTexture(TileId tileId)
    {
        return tileData[tileId].texture;
    }

    public static int GetMaxRotation(TileId tileId)
    {
        return tileData[tileId].maxRotation;
    }

    public static int GetHP(TileId tileId)
	{
		return tileData[tileId].hp;
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