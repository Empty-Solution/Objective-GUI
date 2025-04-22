using OG.Common.Scoping.Abstraction;
using OG.Element;
using OG.Element.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.General;

public class OgContainerFactory(IOgTransformScope scope) : OgFactory<IOgContainer<IOgElement>, IOgFactoryArguments>
{
    public override string TypeName { get; } = "Container";

    public override IOgContainer<IOgElement> Create(IOgFactoryArguments arguments) =>
        new OgContainer<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}