using System.Net.NetworkInformation;
using System.Numerics;
using Raylib_cs;

namespace Main;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(GameConfig.WindowWidth, GameConfig.WindowHeight, GameConfig.WindowTitle);
        Raylib.HideCursor();

        Crosshair crosshair = new();
        TileMap tileMap = new(16, 16);
        Player player = new(new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y));

        AnimatedSprite testSprite = new("Assets/Textures/Entity/player.png", new Vector2(100, 100), GameConfig.Scale, 0.0f);
        testSprite.AddAnimation("test", new Animation(new Vector2(16, 16), 0, 0.1f, new Vector2(0, 3)));
        testSprite.Play("test");

        Camera2D camera = new();
        camera.Target = player.position;
        camera.Offset = new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y);
        camera.Rotation = 0.0f;
        camera.Zoom = 1.0f;

        GridIndicator gridIndicator = new(camera);

        WorldInteraction interaction = new(tileMap);

        TileDatabase.Load();
        tileMap.GenerateWorld();

        while (!Raylib.WindowShouldClose())
        {
            float delta = Raylib.GetFrameTime();

            if (Raylib.IsKeyPressed(KeyboardKey.F11)) {
                Raylib.ToggleFullscreen();
            }

            crosshair.Update();
            player.Update(camera, tileMap, delta);

            testSprite.Update(delta);

            camera.Target = player.position;
            camera.Offset = new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y);

            gridIndicator.Update(camera);
            interaction.Update(gridIndicator.mouseWorldPos);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);
            Raylib.BeginMode2D(camera);

            tileMap.Draw();
            gridIndicator.Draw();
            testSprite.Draw();

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