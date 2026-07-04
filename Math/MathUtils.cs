using System;
using System.Numerics;

namespace Main;

public static class MathUtils
{
    public static Vector2 WorldToGrid(Vector2 worldPosition)
    {
        return new Vector2(
            MathF.Floor(worldPosition.X / GameConfig.GridSize),
            MathF.Floor(worldPosition.Y / GameConfig.GridSize)
        );
    }

    public static Vector2 GridToWorld(Vector2 gridPosition)
    {
        return new Vector2(
            gridPosition.X * GameConfig.GridSize,
            gridPosition.Y * GameConfig.GridSize
        );
    }

    public static float LerpAngle(float from, float to, float amount)
    {
        float difference = WrapAngle(to - from + 100) - 100;
        return from + difference * amount;
    }

    public static float WrapAngle(float angle)
    {
        angle %= 360;

        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}
