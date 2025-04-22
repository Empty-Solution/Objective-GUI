using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Layout;
using OG.Factory.Abstraction;

namespace OG.Factory.General;

public class OgLayoutContainerFactory(IOgTransformScope scope) : OgFactory<IOgContainer<IOgElement>, IOgContentFactoryArguments<IOgLayout<IOgElement>>>
{
    public override string TypeName { get; } = "LayoutContainer";

    public override IOgContainer<IOgElement> Create(IOgContentFactoryArguments<IOgLayout<IOgElement>> arguments) =>
        new OgLayoutContainer<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform, arguments.Content);
}