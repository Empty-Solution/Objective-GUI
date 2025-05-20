using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgReadOnlyTextBuildArguments(string name, Color value, Font font, int fontSize, float pixelsPerUnit, FontStyle fontStyle, string text,
    Rect rect) : OgTextBuildArguments(name, value, font, fontSize, pixelsPerUnit, fontStyle,
                                      text)
{
    public Rect Rect => rect;
}