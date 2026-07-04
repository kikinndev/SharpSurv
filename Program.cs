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
        TileMap tileMap = new TileMap(16, 16);
        Player player = new Player(new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y));

        Camera2D camera = new Camera2D();
        camera.Target = player.position;
        camera.Offset = new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        GridIndicator gridIndicator = new GridIndicator(camera);

        WorldInteraction interaction = new WorldInteraction(tileMap);

        TileDatabase.Load();
        tileMap.GenerateWorld();

        while (!Raylib.WindowShouldClose())
        {
            float delta = Raylib.GetFrameTime();

            if (Raylib.IsKeyPressed(KeyboardKey.F11)) {
                Raylib.ToggleFullscreen();
            }

            crosshair.Update();
            player.Update(camera, delta);

            camera.Target = player.position;
            camera.Offset = new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y);

            gridIndicator.Update(camera);
            interaction.Update(gridIndicator.mouseWorldPos);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.BeginMode2D(camera);

            Raylib.DrawCircle(100, 100, 50, Color.Red);
            tileMap.Draw();
            gridIndicator.Draw();
            player.Draw();

            Raylib.EndMode2D();

            crosshair.Draw();

            Raylib.EndDrawing();
        }

        TileDatabase.Unload();

        crosshair.Unload();
        tileMap.Unload();
        gridIndicator.Unload();
        player.Unload();
        
        Raylib.CloseWindow();
    }
}