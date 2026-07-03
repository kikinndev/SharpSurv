namespace Main;

public static class GameConfig
{
    public static int WindowWidth = 800;
    public static int WindowHeight = 600;

    public static int CenterWidth = WindowWidth / 2;
    public static int CenterHeight = WindowHeight / 2;

    public static string WindowTitle = "OpenSourceSurvivalGame";
    public static float Scale = 3.0f;
    public static float CrosshairScale = 1.0f;

    public static float TileSize = 16.0f;
    public static float GridSize = TileSize * Scale;
}