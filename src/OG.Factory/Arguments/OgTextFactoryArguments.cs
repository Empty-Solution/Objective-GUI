using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, Color color, TextAnchor alignment, Font font, int fontSize,
    FontStyle fontStyle, float pixelsPerUnit) : OgVisualFactoryArguments(name, rectGetProvider, color)
{
    public TextAnchor Alignment     { get; set; } = alignment;
    public Font       Font          { get; set; } = font;
    public int        FontSize      { get; set; } = fontSize;
    public FontStyle  FontStyle     { get; set; } = fontStyle;
    public float      PixelsPerUnit { get; set; } = pixelsPerUnit;
}