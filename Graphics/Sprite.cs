using Raylib_cs;
using System;
using System.Numerics;
using System.Reflection.Metadata;

namespace Main;

public class Sprite
{
    Texture2D texture;

    public Vector2 position;
    public float scale;
    public float rotation;

    public Sprite(string texturePath, Vector2 position, float scale = 1.0f, float rotation = 0.0f)
    {
        texture = Raylib.LoadTexture(texturePath);
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
    }

    public void Draw()
    {
        Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
        Rectangle dest = new Rectangle(position.X, position.Y, texture.Width * scale, texture.Height * scale);
        Vector2 origin = new Vector2(texture.Width * scale / 2, texture.Height * scale / 2);

        Raylib.DrawTexturePro(texture, source, dest, origin, rotation, Color.White);
    }

    public void Unload()
    {
        Raylib.UnloadTexture(texture);
    }
}
