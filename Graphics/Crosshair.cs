using Raylib_cs;
using System.Numerics;

namespace Main;

public class Crosshair
{
    const int GL_ONE_MINUS_DST_COLOR = 0x0307;
    const int GL_ONE_MINUS_SRC_ALPHA = 0x0303;
    const int GL_FUNC_ADD = 0x8006;

    Sprite crosshair = new Sprite("Assets/Textures/UI/crosshair.png", new Vector2(0, 0), GameConfig.CrosshairScale);

    public void Update()
    {
        crosshair.position = Raylib.GetMousePosition();
    }

    public void Draw()
    {
        Rlgl.SetBlendFactors(GL_ONE_MINUS_DST_COLOR, GL_ONE_MINUS_SRC_ALPHA, GL_FUNC_ADD);
        Raylib.BeginBlendMode(BlendMode.Custom);
        crosshair.Draw();
        Raylib.EndBlendMode();
    }

    public void Unload()
    {
        crosshair.Unload();
    }
}