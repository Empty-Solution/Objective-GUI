using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgDraggableFactory(IOgTransformScope scope) : OgFactory<IOgElement, IOgFactoryArguments>
{
    public override string TypeName { get; } = "Draggable";

    public override IOgElement Create(IOgFactoryArguments arguments) =>
        new OgDraggable<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform);
}