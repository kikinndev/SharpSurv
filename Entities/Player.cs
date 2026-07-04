using Raylib_cs;
using System.Numerics;

namespace Main;

public class Player
{
    public Vector2 position;
    public float speed = 250;

    Sprite playerSprite;
    Sprite handsSprite;

    public Player(Vector2 position)
    {
        this.position = position;
        playerSprite = new Sprite("Assets/Textures/Entity/player.png", position, 3);
        handsSprite = new Sprite("Assets/Textures/Entity/player_hands.png", position, 3);
    }

    public void Update(Camera2D camera, float delta)
    {
        Vector2 mouseScreenPos = Raylib.GetMousePosition();
        Vector2 mouseWorldPos = Raylib.GetScreenToWorld2D(mouseScreenPos, camera);
        Vector2 direction = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            direction.Y -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            direction.Y += 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            direction.X -= 1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            direction.X += 1;
        }

        position += direction * speed * delta;
        
        playerSprite.position = position;
        handsSprite.position = position;

        playerSprite.LookAt(mouseWorldPos, 20, delta);
        handsSprite.LookAt(mouseWorldPos, 10, delta);
    }

    public void Draw()
    {
        playerSprite.Draw();
        handsSprite.Draw();
    }

    public void Unload()
    {
        playerSprite.Unload();
        handsSprite.Unload();
    }
}