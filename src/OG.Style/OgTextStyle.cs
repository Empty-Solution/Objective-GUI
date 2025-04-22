using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Style;

public class OgTextStyle(Vector4 offset, Color color, Font font, TextAnchor alignment, int fontSize, FontStyle fontStyle, bool wordWrap, TextClipping clipping) : OgColorizedStyle(offset, color), IOgTextStyle
{
    public void FillUnityStyle(GUIStyle ustyle)
    {
        ustyle.normal.textColor = Color;
        ustyle.wordWrap = WordWrap;
        ustyle.fontSize = FontSize;
        ustyle.alignment = Alignment;
        ustyle.clipping = Clipping;
        ustyle.font = Font;
        ustyle.fontStyle = FontStyle;
    }

    public Font Font { get; set; } = font;
    public FontStyle FontStyle { get; set; } = fontStyle;
    public bool WordWrap { get; set; } = wordWrap;
    public int FontSize { get; set; } = fontSize;
    public TextAnchor Alignment { get; set; } = alignment;
    public TextClipping Clipping { get; set; } = clipping;
}