using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextBuildArguments(string name, IDkGetProvider<Color> value, IOgEventHandlerProvider? provider, Font font, int fontSize,
    TextAnchor alignment, FontStyle fontStyle, TextClipping textClipping, bool wordWrap, string text)
    : OgVisualElementBuildArguments(name, value, provider)
{
    public Font         Font         => font;
    public int          FontSize     => fontSize;
    public FontStyle    FontStyle    => fontStyle;
    public TextAnchor   Alignment    => alignment;
    public string       Text         => text;
    public TextClipping TextClipping => textClipping;
    public bool         WordWrap     => wordWrap;
}