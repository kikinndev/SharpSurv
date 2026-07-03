using Raylib_cs;
using System.Numerics;

namespace Main;

public static class TileDatabase
{
    private static Dictionary<TileId, Texture2D> textures = new Dictionary<TileId, Texture2D>();

    public static void Load()
    {
        textures[TileId.Grass] = Raylib.LoadTexture(TileId.Grass.GetTexturePath());
    }

    public static Texture2D GetTexture(TileId tileId)
    {
        return textures[tileId];
    }

    public static void Unload()
    {
        foreach (Texture2D texture in textures.Values)
        {
            Raylib.UnloadTexture(texture);
        }

        textures.Clear();
    }
}