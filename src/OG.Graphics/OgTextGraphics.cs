using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextGraphics : OgBaseGraphics<IOgTextGraphicsContext>
{
    public /*private*/ static GUIStyle? tempStyle; /*= new()*/ // TODO: CHANGE TO PRIVATE + CCTOR (В Эдиторе нельзя создавать гуистайл вне онгуи)
    //{
    //    normal = new()
    //};
    private static readonly GUIContent tempContent = new();
    public override void Render(IOgTextGraphicsContext ctx)
    {
        if(ctx.Font is null) return;
        Rect rect = ctx.RenderRect;
        rect.position              += Global;
        tempContent.text           =  ctx.Text;
        tempStyle!.fontSize        =  ctx.FontSize;
        tempStyle.normal.textColor =  ctx.Color;
        tempStyle.alignment        =  ctx.Alignment;
        tempStyle.fontStyle        =  ctx.FontStyle;
        tempStyle.clipping         =  ctx.TextClipping;
        tempStyle.wordWrap         =  ctx.WordWrap;
        tempStyle.Draw(rect, tempContent, 0);
        Vector2 size = tempStyle.CalcSize(tempContent);
        ctx.RenderRect = new(ctx.RenderRect.x + (int)size.x, ctx.RenderRect.y + (int)size.y, ctx.RenderRect.width, ctx.RenderRect.height);
    }
}