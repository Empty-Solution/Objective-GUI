using OG.Common.Scoping.Abstraction;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;

namespace OG.Factory.Visual;

public abstract class OgTextFactory<TArguments, TGraphics, TGraphicsContext>(IOgTextStyle style, TGraphics graphics)
    : OgVisualFactory<string, TArguments, IOgTextStyle, TGraphics>(style, graphics), IOgTextFactory<TArguments>
    where TGraphics : IOgGraphics<TGraphicsContext>
    where TGraphicsContext : IOgTextGraphicsContext<IOgTextStyle>
    where TArguments : IOgTextFactoryArguments
{
    public override string TypeName => "Text";
}