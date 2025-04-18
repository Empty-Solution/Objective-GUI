using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Factory;

public abstract class OgTextureFactory<TStyle, TArgumets, TGraphics, TGraphicsContext>(TStyle style, TGraphics graphics)
    : OgVisualFactory<Texture, TArgumets, TStyle, TGraphics>(style, graphics), IOgTextureFactory<TArgumets>
    where TStyle : IOgTextureStyle where TGraphics : IOgGraphics<TGraphicsContext> where TGraphicsContext : IOgTextureGraphicsContext<TStyle> where TArgumets : IOgTextureFactoryArguments
{
    public override string TypeName => "Text";
}
