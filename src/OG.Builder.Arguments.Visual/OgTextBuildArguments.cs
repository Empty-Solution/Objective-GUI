using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextBuildArguments(string name, IDkGetProvider<Color> value, IOgEventHandlerProvider? provider, IDkGetProvider<Font?> font, IDkGetProvider<int> fontSize,
    IDkGetProvider<TextAnchor> alignment, FontStyle fontStyle, TextClipping textClipping, bool wordWrap, IDkGetProvider<string> text)
    : OgVisualElementBuildArguments(name, value, provider)
{
    public IDkGetProvider<Font?>                       Font         => font;
    public IDkGetProvider<int>        FontSize     => fontSize;
    public FontStyle                  FontStyle    => fontStyle;
    public IDkGetProvider<TextAnchor> Alignment    => alignment;
    public IDkGetProvider<string>     Text         => text;
    public TextClipping               TextClipping => textClipping;
    public bool                       WordWrap     => wordWrap;
}