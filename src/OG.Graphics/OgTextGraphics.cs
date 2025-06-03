using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgTextGraphics : OgBaseGraphics<IOgTextGraphicsContext>
{
    private static GUIStyle? tempStyle = new()
    {
        normal = new()
    };
    private static readonly GUIContent tempContent = new();
    public override void ProcessContext(IOgTextGraphicsContext ctx)
    {
        if(ctx.Font is null) return;
        tempContent.text           = ctx.Text;
        tempStyle!.fontSize        = ctx.FontSize;
        tempStyle.normal.textColor = ctx.Color;
        tempStyle.alignment        = ctx.Alignment;
        tempStyle.fontStyle        = ctx.FontStyle;
        tempStyle.clipping         = ctx.TextClipping;
        tempStyle.wordWrap         = ctx.WordWrap;
        tempStyle.Draw(ctx.RenderRect, tempContent, 0);
    }
}