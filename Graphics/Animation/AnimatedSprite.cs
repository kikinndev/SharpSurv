using Raylib_cs;
using System.Numerics;

namespace Main;

public class AnimatedSprite(string texturePath, Vector2 position, float scale = 1.0f, float rotation = 0.0f)
{
    public Dictionary<string, Animation> animations = new();

    public string texturePath = texturePath;
    public Vector2 position = position;
    public float scale = scale;
    public float rotation = rotation;

    public string currentAnimation = "";
    public int currentFrame = 0;
    public float elapsedTime = 0.0f;

    Texture2D texture = Raylib.LoadTexture(texturePath);

    public void AddAnimation(string name, Animation animation)
    {
        animations.Add(name, animation);
    }

    public void Update(float delta)
    {
        if (currentAnimation != "")
        {
            Animation animation = animations[currentAnimation];
            //currentFrame += 1;
            elapsedTime += delta;
            
            if (elapsedTime >= animation.speed)
            {
                currentFrame += 1;
                if (currentFrame >= (int)animation.position.Y)
                {
                    currentFrame = (int)animation.position.X;
                }
                elapsedTime = 0.0f;
            }
        }
    }

    public void Play(string animationName)
    {
        currentAnimation = animationName;
        currentFrame = 0;
    }

    public void Stop()
    {
        currentAnimation = "";
        currentFrame = 0;
    }

    public void Draw()
    {
        if (currentAnimation != "")
        {
            Animation animation = animations[currentAnimation];

            Rectangle source = new(animation.frameSize.X * currentFrame, animation.frameSize.Y * animation.row, animation.frameSize.X, animation.frameSize.Y);
            Rectangle dest = new(position.X, position.Y, animation.frameSize.X * scale, animation.frameSize.Y * scale);
            Vector2 origin = new(animation.frameSize.X * scale / 2, animation.frameSize.Y * scale / 2);

            Raylib.DrawTexturePro(texture, source, dest, origin, rotation, Color.White);
        }
    }

    public void Unload()
    {
        Raylib.UnloadTexture(texture);
    }
}
