using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgClickableFactory(IOgTransformScope scope) : OgFactory<IOgElement, IOgFactoryArguments>
{
    public override string TypeName { get; } = "Button";

    public override IOgElement Create(IOgFactoryArguments arguments) =>
        new OgClickable<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}