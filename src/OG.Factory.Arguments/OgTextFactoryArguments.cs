using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using OG.DataKit.Animation;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider, Color? color,
    OgAnimationColorGetter<DkReadOnlyGetter<Color>>? colorGetter, Font font, int fontSize, FontStyle fontStyle, TextAnchor alignment,
    TextClipping textClipping, bool wordWrap, string text) : OgVisualFactoryArguments(name, rectGetProvider, eventProvider, color, colorGetter)
{
    public string       Text         => text;
    public Font         Font         => font;
    public int          FontSize     => fontSize;
    public FontStyle    FontStyle    => fontStyle;
    public TextAnchor   Alignment    => alignment;
    public TextClipping TextClipping => textClipping;
    public bool         WordWrap     => wordWrap;
}