using OG.Common.Scoping.Abstraction;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;

namespace OG.Factory.Visual;

public abstract class OgTextFactory<TStyle, TArguments, TGraphics, TGraphicsContext, TScope>(TStyle style, TGraphics graphics)
    : OgVisualFactory<string, TArguments, TStyle, TGraphics, TScope>(style, graphics), IOgTextFactory<TArguments, TScope>
    where TStyle : IOgTextStyle where TGraphics : IOgGraphics<TGraphicsContext> where TGraphicsContext : IOgTextGraphicsContext<TStyle>
    where TScope : IOgTransformScope where TArguments : IOgTextFactoryArguments<TScope>
{
    public override string TypeName => "Text";
}