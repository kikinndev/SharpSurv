using Raylib_cs;
using System.Numerics;

namespace Main;

public class WorldInteraction(Player player, TileMap tileMap)
{
    TileMap tileMap = tileMap;

    public void Update(Vector2 mouseWorldPos)
    {
        Vector2 gridPos = MathUtils.WorldToGrid(mouseWorldPos);

        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            // TODO: Make player place different tiles
            if (CanPlaceTile(gridPos))
            {
                PlaceTile(TileId.Log, gridPos);
            }
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

    public bool CanPlaceTile(Vector2 gridPos)
    {
        Vector2 worldPos = MathUtils.GridToWorld(gridPos);

        Rectangle tileRect = new(worldPos.X, worldPos.Y, GameConfig.GridSize, GameConfig.GridSize);
        Rectangle playerRect = player.GetInteractionHitbox(player.position);

        return !Raylib.CheckCollisionRecs(tileRect, playerRect);
    }
}