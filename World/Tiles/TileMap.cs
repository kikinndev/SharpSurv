using System.Numerics;

namespace Main;

public class TileMap
{
    public Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

    int width;
    int height;

    public TileMap(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void GenerateWorld()
    {
        tiles.Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 position = new Vector2(x * GameConfig.GridSize, y * GameConfig.GridSize);
                tiles[position] = new Tile(TileId.Grass, position);
            }
        }
    }

    public void Draw()
    {
        foreach (Tile tile in tiles.Values)
        {
            tile.Draw();
        }
    }

    public void Unload()
    {
        tiles.Clear();
    }
}