using System.Net.NetworkInformation;
using System.Numerics;
using Raylib_cs;

namespace Main;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(GameConfig.WindowWidth, GameConfig.WindowHeight, GameConfig.WindowTitle);
        Raylib.HideCursor();

        Crosshair crosshair = new Crosshair();
        TileMap tileMap = new TileMap(128, 128);
        Player player = new Player(new Vector2(GameConfig.CenterWidth, GameConfig.CenterHeight));

        Camera2D camera = new Camera2D();
        camera.Target = player.position;
        camera.Offset = new Vector2(GameConfig.CenterWidth, GameConfig.CenterHeight);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        TileDatabase.Load();
        tileMap.GenerateWorld();

        while (!Raylib.WindowShouldClose())
        {
            float delta = Raylib.GetFrameTime();

            crosshair.Update();
            player.Update(camera, delta);

            camera.Target = player.position;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.BeginMode2D(camera);

            Raylib.DrawCircle(100, 100, 50, Color.Red);
            tileMap.Draw();
            player.Draw();

            Raylib.EndMode2D();

            crosshair.Draw();

            Raylib.EndDrawing();
        }

        TileDatabase.Unload();

        crosshair.Unload();
        tileMap.Unload();
        player.Unload();
        

        Raylib.CloseWindow();
    }
}