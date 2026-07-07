using Raylib_cs;
using System.Numerics;

namespace SharpSurv;

public class Tile(TileId id, Vector2 position, int rotation)
{
    public TileId id = id;
    public Vector2 position = position;

    public void Draw()
    {
        Texture2D texture = TileDatabase.GetTexture(id);

        Rectangle source = new(rotation * GameConfig.TileSize, rotation * GameConfig.TileSize, GameConfig.TileSize, GameConfig.TileSize);
        Rectangle dest = new(position.X, position.Y, GameConfig.GridSize, GameConfig.GridSize);
        Vector2 origin = Vector2.Zero;

        Raylib.DrawTexturePro(texture, source, dest, origin, 0, Color.White);
    }
}