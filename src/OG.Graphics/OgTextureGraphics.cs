using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextureGraphics : OgBaseGraphics<IOgTextureGraphicsContext>
{
    public override void Render(IOgTextureGraphicsContext ctx)
    {
        if(ctx.Texture is null) return;
        Rect rect = ctx.RenderRect;
        rect.position += Global;
        GUI.DrawTexture(rect, ctx.Texture, ScaleMode.StretchToFill, ctx.AlphaBlend, ctx.ImageAspect, ctx.Color, ctx.BorderWidths, ctx.BorderRadiuses);
    }
}