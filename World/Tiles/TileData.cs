using Raylib_cs;

namespace SharpSurv;

public class TileData(Texture2D texture, bool isSolid, int maxRotation = 4)
{
    public Texture2D texture = texture;
    public bool isSolid = isSolid;

    public int maxRotation = maxRotation;
}