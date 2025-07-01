using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextureGraphics : OgBaseGraphics<IOgTextureGraphicsContext>
{
    public override void ProcessContext(IOgTextureGraphicsContext ctx)
    {
        if(ctx.Texture is null) return;
        if(ctx.RenderRect.position.x < 0 || ctx.RenderRect.position.y < 0 || ctx.RenderRect.position.x > Screen.width || ctx.RenderRect.position.y > Screen.height) return; 
        GUI.DrawTexture(ctx.RenderRect, ctx.Texture, ctx.ScaleMode, ctx.AlphaBlend, ctx.ImageAspect, ctx.Color, ctx.BorderWidths, ctx.BorderRadiuses);
    }
}