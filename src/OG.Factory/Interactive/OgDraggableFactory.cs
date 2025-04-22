using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgDraggableFactory(IOgTransformScope scope) : OgFactory<IOgDraggable<IOgElement, IOgTransformScope>, IOgFactoryArguments>
{
    public override string TypeName { get; } = "Draggable";

    public override IOgDraggable<IOgElement, IOgTransformScope> Create(IOgFactoryArguments arguments) =>
        new OgDraggable<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}