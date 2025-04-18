using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;

namespace OG.Factory;

public abstract class OgTextFactory<TStyle, TArguments, TGraphics, TGraphicsContext>(TStyle style, TGraphics graphics)
    : OgVisualFactory<string, TArguments, TStyle, TGraphics>(style, graphics), IOgTextFactory<TArguments>
    where TStyle : IOgTextStyle where TGraphics : IOgGraphics<TGraphicsContext> where TGraphicsContext : IOgTextGraphicsContext<TStyle> where TArguments : IOgTextFactoryArguments
{
    public override string TypeName => "Text";
}
