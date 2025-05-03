using OG.Graphics;
using OG.Graphics.Abstraction.Contexts;
using OG.Unity.Extensions;
using UnityEngine;
namespace OG.Unity.Graphics;
public class OgTextRepaintHandler : OgBaseRepaintHandler<OgTextRepaintContext>
{
    private static readonly GUIStyle tempStyle = new()
    {
        normal = new()
    };
    private static readonly GUIContent tempContent = new();
    protected override bool Handle(OgTextRepaintContext reason)
    {
        // TODO: Style font to reason font.
        tempContent.text           = reason.Text;
        tempStyle.fontSize         = reason.FontSize;
        tempStyle.normal.textColor = reason.Color.ToUnity();
        tempStyle.alignment        = (TextAnchor)reason.Alignment;
        tempStyle.fontStyle        = (FontStyle)reason.FontStyle;
        tempStyle.clipping         = (TextClipping)reason.Clipping;
        tempStyle.wordWrap         = reason.WordWrap;
        tempStyle.Draw(reason.RepaintRect.ToUnity(), tempContent, 0);
        Vector2 size = tempStyle.CalcSize(tempContent);
        reason.RepaintRect = new(reason.RepaintRect.X + (int)size.x, reason.RepaintRect.Y + (int)size.y, reason.RepaintRect.Width,
                                 reason.RepaintRect.Height);
        reason.LineHeight = reason.Font.LineHeight;
        if(reason.CharsSizes.Count == reason.Text.Length) return true;
        reason.CharsSizes.Clear();
        foreach(char c in reason.Text) reason.CharsSizes.Add(reason.Font.GetCharacterAdvance(c));
        return true;
    }
}