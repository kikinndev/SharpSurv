using Raylib_cs;

namespace SharpSurv;

public class TileData(Texture2D texture, bool isSolid, int maxRotation = 4, int hp = 1)
{
    public Texture2D texture = texture;
    public bool isSolid = isSolid;
    public int hp = hp;

    public int maxRotation = maxRotation;
}