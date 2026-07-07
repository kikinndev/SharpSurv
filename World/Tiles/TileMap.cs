using Raylib_cs;
using System.Numerics;

namespace SharpSurv;

public class TileMap(int width, int height)
{
    public Dictionary<Vector2, Tile> worldTiles = new();
    public Dictionary<Vector2, Tile> objectTiles = new();

    int width = width;
    int height = height;

    public void GenerateWorld()
    {
        worldTiles.Clear();
        objectTiles.Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 gridPos = new(x, y);
                Vector2 worldPos = MathUtils.GridToWorld(gridPos);

                worldTiles[gridPos] = new Tile(TileId.Grass, worldPos, 0);
                objectTiles[gridPos] = new Tile(TileId.Air, worldPos, 0);
            }
        }
    }

    public bool IsSolidAtRect(Rectangle playerRect)
    {
        foreach (Tile tile in objectTiles.Values)
        {
            if (!TileDatabase.tileData[tile.id].isSolid)
            {
                continue;
            }

            Rectangle tileRect = new(tile.position.X, tile.position.Y, GameConfig.GridSize, GameConfig.GridSize);

            if (Raylib.CheckCollisionRecs(playerRect, tileRect))
            {
                return true;
            }
        }

        return false;
    }

    public void Draw()
    {
        foreach (Tile tile in worldTiles.Values)
        {
            tile.Draw();
        }

        foreach (Tile tile in objectTiles.Values)
        {
            tile.Draw();
        }
    }

    public void Unload()
    {
        worldTiles.Clear();
        objectTiles.Clear();
    }
}