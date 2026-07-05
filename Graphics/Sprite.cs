using Raylib_cs;
using System.Numerics;

namespace Main;

public class Sprite(string texturePath, Vector2 position, float scale = 1.0f, float rotation = 0.0f)
{
    Texture2D texture = Raylib.LoadTexture(texturePath);

    public Vector2 position = position;
    public float scale = scale;
    public float rotation = rotation;

    public void Draw()
    {
        Rectangle source = new(0, 0, texture.Width, texture.Height);
        Rectangle dest = new(position.X, position.Y, texture.Width * scale, texture.Height * scale);
        Vector2 origin = new(texture.Width * scale / 2, texture.Height * scale / 2);

        Raylib.DrawTexturePro(texture, source, dest, origin, rotation, Color.White);
    }

    public void LookAt(Vector2 target, float lerpSpeed, float delta)
    {
        Vector2 lookDir = target - position;
        float angle = MathF.Atan2(lookDir.Y, lookDir.X) * Raylib.RAD2DEG;
        float lerpedAngle = MathUtils.LerpAngle(rotation, angle, lerpSpeed * delta);

        rotation = lerpedAngle;
    }

    public void Unload()
    {
        Raylib.UnloadTexture(texture);
    }
}
