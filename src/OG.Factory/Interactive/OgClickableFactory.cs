using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgClickableFactory(IOgTransformScope scope) : OgElementFactory<IOgClickable<IOgElement, IOgTransformScope>, IOgElementFactoryArguments>
{
    public override string TypeName { get; } = "Button";

    public override IOgClickable<IOgElement, IOgTransformScope> Create(IOgElementFactoryArguments arguments) =>
        new OgClickable<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}