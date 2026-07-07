using Raylib_cs;
using System.Numerics;

namespace SharpSurv;

public class GridIndicator
{
    public Vector2 mouseWorldPos;
    public Vector2 mouseGridPos;
    public Vector2 mouseTileWorldPos;

    public bool inRange = true;
    public bool canPlace = true;

	Sprite gridIndicator = new("Assets/Textures/UI/grid.png", Vector2.Zero, GameConfig.Scale);

    public GridIndicator(Camera2D camera)
    {
        mouseWorldPos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
        mouseGridPos = MathUtils.WorldToGrid(mouseWorldPos);
        mouseTileWorldPos = MathUtils.GridToWorld(mouseGridPos);
    }

    public void Update(Camera2D camera, Player player)
    {
        mouseWorldPos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
        mouseGridPos = MathUtils.WorldToGrid(mouseWorldPos);
        mouseTileWorldPos = MathUtils.GridToWorld(mouseGridPos);
        gridIndicator.position = mouseTileWorldPos + new Vector2(GameConfig.GridSize / 2f, GameConfig.GridSize / 2f);

        inRange = Vector2.Distance(mouseTileWorldPos, player.position) < GameConfig.MaxDistance;
	}

    public void Draw()
    {
        if (inRange && canPlace)
		{
			gridIndicator.Draw(Color.White);
		}
		else
		{
			gridIndicator.Draw(new Color(255, 0, 0));
		}
    }
    public void Unload()
    {
        gridIndicator.Unload();
    }
}