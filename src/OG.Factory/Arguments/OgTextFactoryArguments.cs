using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, Color color, Font font, int fontSize, FontStyle fontStyle,
    float pixelsPerUnit, float spacing) : OgVisualFactoryArguments(name, rectGetProvider, color)
{
    public Font      Font          => font;
    public int       FontSize      => fontSize;
    public FontStyle FontStyle     => fontStyle;
    public float     PixelsPerUnit => pixelsPerUnit;
    public float     Spacing       => spacing;
}