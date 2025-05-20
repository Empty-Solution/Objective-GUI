using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider, Color color, Font font,
    int fontSize, FontStyle fontStyle, float pixelsPerUnit, float spacing, string text) : OgVisualFactoryArguments(name, rectGetProvider, eventProvider, color)
{
    public string Text { get; } = text;
    public Font      Font          => font;
    public int       FontSize      => fontSize;
    public FontStyle FontStyle     => fontStyle;
    public float     PixelsPerUnit => pixelsPerUnit;
    public float     Spacing       => spacing;
}