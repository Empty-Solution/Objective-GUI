using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Visual;

public class OgTexture<TStyle, TScope>(
    string name,
    TScope scope,
    IOgTransform transform,
    TStyle style,
    Texture texture,
    IOgGraphics<IOgTextureGraphicsContext<TStyle>> graphics)
    : OgContentVisual<TStyle, TScope, Texture, IOgTextureGraphicsContext<TStyle>>(name, texture, style, scope, transform, graphics), IOgTexture
    where TStyle : IOgTextureStyle where TScope : IOgTransformScope
{
    private static readonly OgInternalImageGraphicsContext graphicsContext = new(default!, default!, default);

    protected override IOgTextureGraphicsContext<TStyle> GetGraphicsContext(OgEvent reason, TStyle style, Texture content, Rect rect)
    {
        graphicsContext.Fill(Style, Content, Transform.LocalRect);
        return graphicsContext;
    }

    private class OgInternalImageGraphicsContext(TStyle style, Texture content, Rect rect) : OgInternalGraphicsContext(style, content, rect), IOgTextureGraphicsContext<TStyle>;
}