using Raylib_cs;
using System.Numerics;

namespace Main;

public class Tile
{
    public TileId id;
    public Vector2 position;

    public Tile(TileId id, Vector2 position)
    {
        this.id = id;
        this.position = position;
    }

    public void Draw()
    {
        Texture2D texture = TileDatabase.GetTexture(id);

        Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
        Rectangle dest = new Rectangle(position.X, position.Y, GameConfig.GridSize, GameConfig.GridSize);
        Vector2 origin = Vector2.Zero;

        Raylib.DrawTexturePro(texture, source, dest, origin, 0, Color.White);
    }
}