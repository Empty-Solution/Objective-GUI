using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Visual;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Factory.Visual;

public class OgImageFactory(IOgTextureStyle style, IOgTransformScope scope, IOgGraphics<IOgTextureGraphicsContext<IOgTextureStyle>> graphics) :
    OgTextureFactory<IOgTextureFactoryArguments, IOgGraphics<IOgTextureGraphicsContext<IOgTextureStyle>>,
        IOgTextureGraphicsContext<IOgTextureStyle>>(style, graphics)
{
    protected override IOgVisualContent<Texture> InternalCreate(
        string name, Texture content, IOgTransform transform,
        IOgTextureStyle style, IOgGraphics<IOgTextureGraphicsContext<IOgTextureStyle>> graphics) =>
        new OgTexture<IOgTextureStyle, IOgTransformScope>(name, scope, transform, style, content, graphics);
}