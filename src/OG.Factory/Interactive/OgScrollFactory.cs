using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgScrollFactory(IOgClipTransformScope scope) : OgFactory<IOgElement, IOgFactoryArguments>
{
    public override string TypeName { get; } = "Scroll";

    public override IOgElement Create(IOgFactoryArguments arguments) =>
        new OgScroll<IOgElement, IOgClipTransformScope>(arguments.Name, scope, arguments.Transform);
}