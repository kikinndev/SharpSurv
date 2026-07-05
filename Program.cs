using System.Net.NetworkInformation;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;

namespace Main;

internal static class Program
{
    [System.STAThread]
    public static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(GameConfig.WindowWidth, GameConfig.WindowHeight, GameConfig.WindowTitle);

        TileMap tileMap = new(128, 128);
        Player player = new(new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y));

        Camera2D camera = new();
        camera.Target = player.position;
        camera.Offset = new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y);
        camera.Rotation = 0.0f;
        camera.Zoom = 1f;

        GridIndicator gridIndicator = new(camera);

        WorldInteraction interaction = new(player, tileMap);

        TileDatabase.Load();
        tileMap.GenerateWorld();

        rlImGui.Setup(true);

        while (!Raylib.WindowShouldClose())
        {
            float delta = Raylib.GetFrameTime();

            if (Raylib.IsKeyPressed(KeyboardKey.F11)) {
                Raylib.ToggleFullscreen();
            }

            player.Update(camera, tileMap, delta);

            camera.Target = player.position;
            camera.Offset = new Vector2(GameConfig.GetCenterScreen().X, GameConfig.GetCenterScreen().Y);

            int holdingSlot = player.inventory.holdingSlot;
            InventorySlot currentSlot = player.inventory.Get(holdingSlot);
            TileId currentTile = currentSlot.tileId;
            Texture2D currentTileTexture = TileDatabase.GetTexture(currentTile);

            ImGuiIOPtr io = ImGui.GetIO();
            if (!io.WantCaptureMouse)
            {
                gridIndicator.Update(camera);
                interaction.Update(gridIndicator.mouseWorldPos);
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.BeginMode2D(camera);

            tileMap.Draw();
            player.Draw();
            gridIndicator.Draw();

            Raylib.EndMode2D();

            Rectangle source = new(interaction.currentRotation * GameConfig.TileSize, interaction.currentRotation * GameConfig.TileSize, GameConfig.TileSize, GameConfig.TileSize);
            Rectangle dest = new(Raylib.GetMouseX() + 13, Raylib.GetMouseY() + 13, 16, 16);
            Vector2 origin = Vector2.Zero;

            Raylib.DrawTexturePro(currentTileTexture, source, dest, origin, 0, Color.White);

            rlImGui.Begin();
            for (int i = 0; i < player.inventory.slots.Length; i++)
            {
                InventorySlot slot = player.inventory.Get(i);
                ImGui.Text($"Slot: {i + 1} | {slot.tileId}");
            }
            ImGui.NewLine();
            ImGui.Text($"Current Slot: {player.inventory.holdingSlot + 1}");
            rlImGui.End();

            Raylib.EndDrawing();
        }

        TileDatabase.Unload();

        tileMap.Unload();
        gridIndicator.Unload();
        player.Unload();

        rlImGui.Shutdown();

        Raylib.CloseWindow();
    }
}