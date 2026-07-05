using Raylib_cs;
using System.Numerics;

namespace Main;

public class Tile(TileId id, Vector2 position)
{
    public TileId id = id;
    public Vector2 position = position;

    public void Draw()
    {
        Texture2D texture = TileDatabase.GetTexture(id);

        Rectangle source = new(0, 0, texture.Width, texture.Height);
        Rectangle dest = new(position.X, position.Y, GameConfig.GridSize, GameConfig.GridSize);
        Vector2 origin = Vector2.Zero;

        Raylib.DrawTexturePro(texture, source, dest, origin, 0, Color.White);
    }
}