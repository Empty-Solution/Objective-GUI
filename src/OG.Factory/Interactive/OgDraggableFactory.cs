using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgDraggableFactory(IOgTransformScope scope) : OgElementFactory<IOgDraggable<IOgElement, IOgTransformScope>, IOgElementFactoryArguments>
{
    public override string TypeName { get; } = "Draggable";

    public override IOgDraggable<IOgElement, IOgTransformScope> Create(IOgElementFactoryArguments arguments) =>
        new OgDraggable<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}