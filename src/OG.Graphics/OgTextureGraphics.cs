using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextureGraphics : OgBaseGraphics<IOgTextureGraphicsContext>
{
    public override void ProcessContext(IOgTextureGraphicsContext ctx)
    {
        if(ctx.Texture is null) return;
        GUI.DrawTexture(ctx.RenderRect, ctx.Texture, ctx.ScaleMode, ctx.AlphaBlend, ctx.ImageAspect, ctx.Color, ctx.BorderWidths, ctx.BorderRadiuses);
    }
}