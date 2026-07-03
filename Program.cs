using System.Net.NetworkInformation;
using System.Numerics;
using Raylib_cs;

namespace Main;

internal static class Program
{
    const int screenWidth = 1400;
    const int screenHeight = 800;

    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "Hello World");

        float scale = 3;

        Sprite playerSprite = new Sprite("Resources/player_up.png", new Vector2(screenWidth / 2, screenHeight / 2), scale);
        Sprite playerHands = new Sprite("Resources/player_hands.png", new Vector2(screenWidth / 2, screenHeight / 2), scale);

        float speed = 200;
        float playerAngle = 0;

        while (!Raylib.WindowShouldClose())
        {
            float delta = Raylib.GetFrameTime();
            Vector2 direction = Vector2.Zero;
            Vector2 mousePos = Raylib.GetMousePosition();

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

            if (direction.LengthSquared() > 0)
            {
                direction = Vector2.Normalize(direction);
            }

            Vector2 lookDir = mousePos - playerSprite.position;
            float angle = MathF.Atan2(lookDir.Y, lookDir.X) * Raylib.RAD2DEG;
            playerAngle = MathUtils.LerpAngle(playerAngle, angle, 10 * delta);

            playerSprite.position += direction * speed * delta;
            playerSprite.rotation = angle + 90;

            playerHands.position = playerSprite.position;
            playerHands.rotation = playerAngle + 90;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            playerSprite.Draw();
            playerHands.Draw();

            Raylib.EndDrawing();
        }

        playerSprite.Unload();
        playerHands.Unload();

        Raylib.CloseWindow();
    }

    static float LerpAngle(float from, float to, float amount)
    {
        float difference = WrapAngle(to - from + 100) - 100;
        return from + difference * amount;
    }

    static float WrapAngle(float angle)
    {
        angle %= 360;

        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}