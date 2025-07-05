using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextGraphics : OgBaseGraphics<IOgTextGraphicsContext>
{
    private static          GUIStyle?   tempStyle;
    private static GUIContent? tempContent;
    public override void ProcessContext(IOgTextGraphicsContext ctx)
    {
        if(ctx.Font is null) return;
        if (!new Rect(0, 0, Screen.width, Screen.height).Overlaps(ctx.RenderRect))
            return;
        
        tempStyle ??= new()
        {
            normal = new()
        };
        tempContent ??= new();


        tempContent.text = ctx.Text;
        tempStyle!.fontSize = ctx.FontSize;
        tempStyle.alignment = ctx.Alignment;
        tempStyle.fontStyle = ctx.FontStyle;
        tempStyle.clipping = ctx.TextClipping;
        tempStyle.wordWrap = ctx.WordWrap;
        tempStyle.font = ctx.Font;
        if(ctx.OutlineSize != 0)
        {
            int roundedOutlineSize = Mathf.RoundToInt(ctx.OutlineSize);
            tempStyle.normal.textColor = ctx.OutlineColor;
            for(float x = -roundedOutlineSize; x <= roundedOutlineSize; x++)
                for(float y = -roundedOutlineSize; y <= roundedOutlineSize; y++)
                {
                    if(x == 0 && y == 0) continue;
                    var outlineRect = ctx.RenderRect;
                    outlineRect.x += x;
                    outlineRect.y += y;
                    tempStyle.Draw(outlineRect, tempContent, 0);
                }
        }
        tempStyle.normal.textColor = ctx.Color;
        tempStyle.Draw(ctx.RenderRect, tempContent, 0);
    }
}