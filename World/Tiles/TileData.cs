using Raylib_cs;

namespace Main;

public class TileData(Texture2D texture, bool isSolid)
{
    public Texture2D texture = texture;
    public bool isSolid = isSolid;
}