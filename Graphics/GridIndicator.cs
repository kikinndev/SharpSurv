using Raylib_cs;
using System.Numerics;

namespace Main;

public class GridIndicator
{
    public Vector2 mouseWorldPos;
    public Vector2 mouseGridPos;
    public Vector2 mouseTileWorldPos;

    Sprite gridIndicator = new("Assets/Textures/UI/grid.png", Vector2.Zero, GameConfig.Scale);

    public GridIndicator(Camera2D camera)
    {
        mouseWorldPos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
        mouseGridPos = MathUtils.WorldToGrid(mouseWorldPos);
        mouseTileWorldPos = MathUtils.GridToWorld(mouseGridPos);
    }

    public void Update(Camera2D camera)
    {
        mouseWorldPos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
        mouseGridPos = MathUtils.WorldToGrid(mouseWorldPos);
        mouseTileWorldPos = MathUtils.GridToWorld(mouseGridPos);
        gridIndicator.position = mouseTileWorldPos + new Vector2(GameConfig.GridSize / 2f, GameConfig.GridSize / 2f);
    }

    public void Draw()
    {
        gridIndicator.Draw();
    }
    public void Unload()
    {
        gridIndicator.Unload();
    }
}