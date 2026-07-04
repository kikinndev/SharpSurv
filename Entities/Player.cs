using Raylib_cs;
using System.Numerics;

namespace Main;

public class Player
{
    public Vector2 position;
    public float speed = 300;

    Vector2 targetPosition;

    Sprite playerSprite;
    Sprite handsSprite;

    public Player(Vector2 position)
    {
        this.position = position;
        targetPosition = position;

        playerSprite = new Sprite("Assets/Textures/Entity/player.png", position, 3);
        handsSprite = new Sprite("Assets/Textures/Entity/player_hands.png", position, 3);
    }

    public void Update(Camera2D camera, TileMap tileMap, float delta)
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

        Vector2 next_position = targetPosition + direction * speed * delta;

        if (!tileMap.IsSolidAtRect(GetHitbox(next_position)))
        {
            targetPosition = next_position;
        }

        position = Vector2.Lerp(position, targetPosition, 12.0f * delta);

        playerSprite.position = position;
        handsSprite.position = position;

        playerSprite.LookAt(mouseWorldPos, 20, delta);
        handsSprite.LookAt(mouseWorldPos, 10, delta);
    }

    private static Rectangle GetHitbox(Vector2 position)
    {
        return new Rectangle(position.X - 16, position.Y - 16, 32, 32);
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