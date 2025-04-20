using OG.Common.Scoping.Abstraction;
using OG.Element.Legacy;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Factory;

public abstract class OgTextureFactory<TStyle, TArguments, TGraphics, TGraphicsContext, TScope>(TStyle style, TGraphics graphics)
    : OgVisualFactory<Texture, TArguments, TStyle, TGraphics, TScope>(style, graphics), IOgTextureFactory<TArguments, TScope>
    where TStyle : IOgTextureStyle where TGraphics : IOgGraphics<TGraphicsContext> where TGraphicsContext : IOgTextureGraphicsContext<TStyle>
    where TScope : IOgTransformScope where TArguments : IOgTextureFactoryArguments<TScope>
{
    public override string TypeName => "Texture";
}