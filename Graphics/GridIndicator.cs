using Raylib_cs;
using System.Numerics;

namespace Main;

public class GridIndicator
{
    Vector2 mouseWorldPos;
    Vector2 mouseGridPos;
    Vector2 mouseTileWorldPos;

    Sprite gridIndicator = new Sprite("Resources/grid.png", Vector2.Zero, GameConfig.Scale);

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