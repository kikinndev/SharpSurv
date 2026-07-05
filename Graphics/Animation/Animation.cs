using System.Numerics;

namespace Main;

public class Animation(Vector2 frameSize, int row, float speed, Vector2 position)
{
    public Vector2 frameSize = frameSize;
    public int row = row;
    public float speed = speed;
    public Vector2 position = position;
}