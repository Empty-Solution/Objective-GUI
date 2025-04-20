using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Factory.Abstraction;
using OG.Graphics.Abstraction;
using OG.Style.Abstraction;

namespace OG.Factory;

public abstract class OgVisualFactory<TContent, TArguments, TStyle, TGraphics, TScope>(TStyle style, TGraphics graphics)
    : OgFactory<IOgVisualContent<TContent>, TArguments, TScope>, IOgVisualFactory<TContent, TArguments, TScope>
    where TStyle : IOgStyle where TGraphics : IOgGraphics where TScope : IOgTransformScope where TArguments : IOgVisualFactoryArguments<TContent, TScope>
{
    public sealed override IOgVisualContent<TContent> Create(TArguments arguments) => InternalCreate(arguments.Name, arguments.Content, arguments.Transform, style, arguments.Scope, graphics);
    protected abstract IOgVisualContent<TContent> InternalCreate(string name, TContent content, IOgTransform transform, TStyle style, TScope scope, TGraphics graphics);
}