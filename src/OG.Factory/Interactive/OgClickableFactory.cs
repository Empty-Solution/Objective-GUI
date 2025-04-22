using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgClickableFactory(IOgTransformScope scope) : OgFactory<IOgClickable<IOgElement, IOgTransformScope>, IOgFactoryArguments>
{
    public override string TypeName { get; } = "Button";

    public override IOgClickable<IOgElement, IOgTransformScope> Create(IOgFactoryArguments arguments) =>
        new OgClickable<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}