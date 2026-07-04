using Raylib_cs;

namespace Main;

public class TileData
{
    public Texture2D texture;
    public bool isSolid;

    public TileData(Texture2D texture, bool isSolid)
    {
        this.texture = texture;
        this.isSolid = isSolid;
    }
}