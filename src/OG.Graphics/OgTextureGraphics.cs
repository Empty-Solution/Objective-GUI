using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextureGraphics : OgBaseGraphics<IOgTextureGraphicsContext>
{
    public override void ProcessContext(IOgTextureGraphicsContext ctx)
    {
        if(ctx.Texture is null) return;
        if (!new Rect(0, 0, Screen.width, Screen.height).Overlaps(ctx.RenderRect))
            return;
        GUI.DrawTexture(ctx.RenderRect, ctx.Texture, ctx.ScaleMode, ctx.AlphaBlend, ctx.ImageAspect, ctx.Color, ctx.BorderWidths, ctx.BorderRadiuses);
    }
}