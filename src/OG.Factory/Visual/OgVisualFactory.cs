using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.General;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;

namespace OG.Factory.Visual;

public abstract class OgVisualFactory<TContent, TArguments, TStyle, TGraphics>(TStyle style, TGraphics graphics)
    : OgFactory<IOgVisualContent<TContent>, TArguments>, IOgVisualFactory<TContent, TArguments>
    where TStyle : IOgStyle where TGraphics : IOgGraphics where TArguments : IOgContentFactoryArguments<TContent>
{
    public sealed override IOgVisualContent<TContent> Create(TArguments arguments) =>
        InternalCreate(arguments.Name, arguments.Content, arguments.Transform, style, graphics);

    protected abstract IOgVisualContent<TContent> InternalCreate(string name, TContent content, IOgTransform transform, TStyle style, TGraphics graphics);
}