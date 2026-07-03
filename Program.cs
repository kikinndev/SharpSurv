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

        Texture2D playerTex = Raylib.LoadTexture("Resources/player_up.png");
        Texture2D playerHandsTex = Raylib.LoadTexture("Resources/player_hands.png");

        float scale = 3;
        Vector2 playerPos = new Vector2(screenWidth / 2 - playerTex.Width * 3 / 2, screenHeight / 2 - playerTex.Height * 3 / 2);
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

            playerPos += direction * speed * delta;

            Vector2 lookDir = mousePos - playerPos;
            float angle = MathF.Atan2(lookDir.Y, lookDir.X) * Raylib.RAD2DEG;
            playerAngle = LerpAngle(playerAngle, angle, 10 * delta);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Rectangle playerSource = new Rectangle(0, 0, playerTex.Width, playerTex.Height);
            Rectangle playerDest = new Rectangle(playerPos.X, playerPos.Y, playerTex.Width * scale, playerTex.Height * scale);
            Vector2 playerOrigin = new Vector2(playerTex.Width * scale / 2, playerTex.Height * scale / 2);

            Rectangle handsSource = new Rectangle(0, 0, playerHandsTex.Width, playerHandsTex.Height);
            Rectangle handsDest = new Rectangle(playerPos.X, playerPos.Y, playerHandsTex.Width * scale, playerHandsTex.Height * scale);
            Vector2 handsOrigin = new Vector2(playerHandsTex.Width * scale / 2, playerHandsTex.Height * scale / 2);

            Raylib.DrawTexturePro(playerTex, playerSource, playerDest, playerOrigin, angle + 90, Color.White);
            Raylib.DrawTexturePro(playerHandsTex, handsSource, handsDest, handsOrigin, playerAngle + 90, Color.White);

            Raylib.DrawText("X: " + Math.Floor(playerPos.X) + " Y: " + Math.Floor(playerPos.Y), 12, 12, 20, Color.Black);

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(playerTex);

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