using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Graphics;

public class OgTextureGraphics<TStyle> : IOgGraphics<IOgTextureGraphicsContext<TStyle>> where TStyle : IOgTextureStyle
{
    public void Draw(IOgTextureGraphicsContext<TStyle> context)
    {
        TStyle style = context.Style;
        GUI.DrawTexture(context.Rect, context.Content, style.ScaleMode, style.AlphaBlend, style.ImageAspect, style.Color, style.BorderWidths, style.BorderRadiuses);
    }
}