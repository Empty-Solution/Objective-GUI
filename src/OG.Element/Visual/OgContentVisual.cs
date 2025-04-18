using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Visual;

public abstract class OgContentVisual<TStyle, TScope, TContent, TGraphicsContext>(
    string name,
    TContent content,
    TStyle style,
    TScope scope,
    IOgTransform transform,
    IOgGraphics<TGraphicsContext> graphics)
    : OgVisual<TStyle, TScope, TGraphicsContext>(name, style, scope, transform, graphics), IOgVisualContent<TContent>
    where TStyle : IOgStyle where TScope : IOgTransformScope where TGraphicsContext : IOgStyledGraphicsContext<TContent, TStyle>
{
    public TContent Content { get; set; } = content;

    protected override TGraphicsContext GetGraphicsContext(OgEvent reason) => GetGraphicsContext(reason, Style, Content, GetTransformRectWithOffset());

    protected abstract TGraphicsContext GetGraphicsContext(OgEvent reason, TStyle style, TContent content, Rect rect);

    protected class OgInternalGraphicsContext(TStyle style, TContent content, Rect rect)
        : IOgStyledGraphicsContext<TContent, TStyle>
    {
        public TStyle Style => style;

        public TContent Content => content;
        public Rect Rect => rect;

        public void Fill(TStyle nstyle, TContent ncontent, Rect nrect)
        {
            style = nstyle;
            content = ncontent;
            rect = nrect;
        }
    }
}