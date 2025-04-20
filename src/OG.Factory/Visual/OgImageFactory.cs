using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Visual;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Factory.Visual;

public class OgImageFactory<TScope>(IOgTextureStyle style, IOgGraphics<IOgTextureGraphicsContext<IOgTextureStyle>> graphics) :
    OgTextureFactory<IOgTextureStyle, IOgTextureFactoryArguments<TScope>, IOgGraphics<IOgTextureGraphicsContext<IOgTextureStyle>>,
        IOgTextureGraphicsContext<IOgTextureStyle>, TScope>(style, graphics) where TScope : IOgTransformScope
{
    protected override IOgVisualContent<Texture> InternalCreate(string name, Texture content, IOgTransform transform,
        IOgTextureStyle style, TScope scope, IOgGraphics<IOgTextureGraphicsContext<IOgTextureStyle>> graphics) =>
        new OgTexture<IOgTextureStyle, TScope>(name, scope, transform, style, content, graphics);
}