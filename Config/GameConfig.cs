using Raylib_cs;
using System.Numerics;

namespace Main;

public static class GameConfig
{
    public static int WindowWidth = 800;
    public static int WindowHeight = 600;

    public static string WindowTitle = "OpenSourceSurvivalGame";
    public static float Scale = 3.0f;
    public static float CrosshairScale = 1.0f;

    public static float TileSize = 16.0f;
    public static float GridSize = TileSize * Scale;

    public static Vector2 GetCenterScreen()
    {
        return new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
    }
}