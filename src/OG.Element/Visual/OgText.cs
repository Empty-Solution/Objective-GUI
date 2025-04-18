using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Visual;

public class OgText<TStyle, TScope>(
    string name,
    string text,
    TStyle style,
    TScope scope,
    IOgTransform transform,
    IOgGraphics<IOgTextGraphicsContext<TStyle>> graphics)
    : OgContentVisual<TStyle, TScope, string, IOgTextGraphicsContext<TStyle>>(name, text, style, scope, transform, graphics), IOgText
    where TStyle : IOgTextStyle where TScope : IOgTransformScope
{
    private static readonly OgInternalTextGraphicsContext graphicsContext = new(default!, default!, default);

    protected override IOgTextGraphicsContext<TStyle> GetGraphicsContext(OgEvent reason, TStyle style, string content, Rect rect)
    {
        graphicsContext.Fill(style, content, rect);
        return graphicsContext;
    }

    private class OgInternalTextGraphicsContext(TStyle style, string content, Rect rect) : OgInternalGraphicsContext(style, content, rect), IOgTextGraphicsContext<TStyle>;
}