using Raylib_cs;
using System.Numerics;

namespace SharpSurv;

public class GameConfig
{
    public static int WindowWidth = 1280;
    public static int WindowHeight = 720;

    public static string WindowTitle = "SharpSurv";
    public static float Scale = 3.0f;
    public static float CrosshairScale = 1.0f;

    public static float TileSize = 16.0f;
    public static float GridSize = TileSize * Scale;

    public static int MaxDistance = 256;

    public static float BreakingSpeed = 0.4f;

    public static Vector2 GetCenterScreen()
    {
        return new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
    }
}