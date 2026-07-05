using Raylib_cs;
using System.Numerics;

namespace Main;

public class WorldInteraction(TileMap tileMap)
{
    TileMap tileMap = tileMap;

    public void Update(Vector2 mouseWorldPos)
    {
        Vector2 gridPos = MathUtils.WorldToGrid(mouseWorldPos);

        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            // TODO: Make player place different tiles
            PlaceTile(TileId.Log, gridPos);
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Right))
        {
            BreakTile(gridPos);
        }
    }

    public void PlaceTile(TileId tileId, Vector2 gridPos)
    {
        Vector2 worldPos = MathUtils.GridToWorld(gridPos);
        tileMap.objectTiles[gridPos] = new Tile(tileId, worldPos);
    }

    public void BreakTile(Vector2 gridPos)
    {
        if (tileMap.objectTiles.ContainsKey(gridPos))
        {
            tileMap.objectTiles.Remove(gridPos);
        }
    }
}