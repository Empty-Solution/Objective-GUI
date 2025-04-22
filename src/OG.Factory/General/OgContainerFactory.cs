using OG.Common.Scoping.Abstraction;
using OG.Element;
using OG.Element.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.General;

public class OgContainerFactory(IOgTransformScope scope) : OgElementFactory<IOgContainer<IOgElement>, IOgElementFactoryArguments>
{
    public override string TypeName { get; } = "Container";

    public override IOgContainer<IOgElement> Create(IOgElementFactoryArguments arguments) =>
        new OgContainer<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}