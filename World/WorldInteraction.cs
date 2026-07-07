using Raylib_cs;
using System.Numerics;

namespace SharpSurv;

public class WorldInteraction(Player player, TileMap tileMap)
{
    public int currentRotation = 0;

    TileMap tileMap = tileMap;

    public void Update(Vector2 mouseWorldPos)
    {
        Vector2 gridPos = MathUtils.WorldToGrid(mouseWorldPos);
		Vector2 mouseTileWorldPos = MathUtils.GridToWorld(gridPos);

		if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (CanPlaceTile(gridPos))
            {
                int holdingSlot = player.inventory.holdingSlot;
                InventorySlot currentSlot = player.inventory.Get(holdingSlot);
                TileId holdingTile = currentSlot.tileId;

                if (holdingTile != TileId.Air && IsEmpty(gridPos, tileMap.objectTiles) && Vector2.Distance(mouseTileWorldPos, player.position) < GameConfig.MaxDistance)
                {
                    PlaceTile(holdingTile, gridPos);
                }
            }
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Right) && Vector2.Distance(mouseTileWorldPos, player.position) < GameConfig.MaxDistance)
        {
            if (tileMap.GetTileAtWorldPos(mouseTileWorldPos).currentHp <= 1)
            {
                BreakTile(gridPos);
            }
            else
            {
                tileMap.GetTileAtWorldPos(mouseTileWorldPos).currentHp -= 1;
                Console.WriteLine($"Breaking, HP: {tileMap.GetTileAtWorldPos(mouseTileWorldPos).currentHp}");
            }

		}

        if (Raylib.IsKeyPressed(KeyboardKey.R))
        {
            int holdingSlot = player.inventory.holdingSlot;
            InventorySlot currentSlot = player.inventory.Get(holdingSlot);
            TileId holdingTile = currentSlot.tileId;

            int maxRotation = TileDatabase.GetMaxRotation(holdingTile);

            currentRotation += 1;
            if (currentRotation >= maxRotation)
            {
                currentRotation = 0;
            }
        }
    }

    public void PlaceTile(TileId tileId, Vector2 gridPos)
    {
        Vector2 worldPos = MathUtils.GridToWorld(gridPos);
        int tileHp = TileDatabase.GetHP(tileId);
        Console.WriteLine($"Placed tile with hp {tileHp}");
		tileMap.objectTiles[gridPos] = new Tile(tileId, worldPos, currentRotation, tileHp);
    }

    public void BreakTile(Vector2 gridPos)
    {
        Vector2 worldPos = MathUtils.GridToWorld(gridPos);
        if (tileMap.objectTiles.ContainsKey(gridPos))
        {
			tileMap.objectTiles[gridPos] = new Tile(TileId.Air, worldPos, 0, TileDatabase.GetHP(TileId.Air));
        }
    }

    public bool CanPlaceTile(Vector2 gridPos)
    {
        Vector2 worldPos = MathUtils.GridToWorld(gridPos);

        Rectangle tileRect = new(worldPos.X, worldPos.Y, GameConfig.GridSize, GameConfig.GridSize);
        Rectangle playerRect = player.GetInteractionHitbox(player.position);

        return !Raylib.CheckCollisionRecs(tileRect, playerRect);
    }

    public static bool IsEmpty(Vector2 gridPos, Dictionary<Vector2, Tile> objectTiles)
    {
        return !objectTiles.TryGetValue(gridPos, out var tile) || tile.id == TileId.Air;
    }
}