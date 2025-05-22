using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextureGraphics : OgBaseGraphics<IOgTextureGraphicsContext>
{
    protected override void ProcessContext(IOgTextureGraphicsContext ctx)
    {
        if(ctx.Texture is null) return;
        GUI.DrawTexture(ctx.RenderRect, ctx.Texture, ScaleMode.StretchToFill, ctx.AlphaBlend, ctx.ImageAspect, ctx.Color, ctx.BorderWidths, ctx.BorderRadiuses);
    }
}