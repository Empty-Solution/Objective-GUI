using OG.Common.Scoping.Abstraction;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Factory.Visual;

public abstract class OgTextureFactory<TArguments, TGraphics, TGraphicsContext>(IOgTextureStyle style, IOgTransformScope scope, TGraphics graphics)
    : OgVisualFactory<Texture, TArguments, IOgTextureStyle, TGraphics>(style, scope, graphics), IOgTextureFactory<TArguments>
    where TGraphics : IOgGraphics<TGraphicsContext>
    where TGraphicsContext : IOgTextureGraphicsContext<IOgTextureStyle>
    where TArguments : IOgTextureFactoryArguments
{
    public override string TypeName => "Texture";
}