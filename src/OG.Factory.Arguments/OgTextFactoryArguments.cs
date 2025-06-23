using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkGetProvider<Color> colorGetter, IDkGetProvider<Font?> font, IDkGetProvider<int> fontSize, FontStyle fontStyle, IDkGetProvider<TextAnchor> alignment,
    TextClipping textClipping, bool wordWrap, IDkGetProvider<string> text) : OgVisualFactoryArguments(name, rectGetProvider, eventProvider, colorGetter)
{
    public IDkGetProvider<string> Text => text;
    public IDkGetProvider<Font?> Font => font;
    public IDkGetProvider<int> FontSize => fontSize;
    public FontStyle FontStyle => fontStyle;
    public IDkGetProvider<TextAnchor> Alignment => alignment;
    public TextClipping TextClipping => textClipping;
    public bool WordWrap => wordWrap;
}