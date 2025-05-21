using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextBuildArguments(string name, Color value, Font font, int fontSize, float pixelsPerUnit, FontStyle fontStyle, string text)
    : OgVisualElementBuildArguments(name, value)
{
    public Font      Font          => font;
    public int       FontSize      { get; } = fontSize;
    public float     PixelsPerUnit { get; } = pixelsPerUnit;
    public FontStyle FontStyle     { get; } = fontStyle;
    public string    Text          { get; } = text;
}