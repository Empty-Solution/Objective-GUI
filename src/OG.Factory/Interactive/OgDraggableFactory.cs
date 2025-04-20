using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;

namespace OG.Factory;

public class OgDraggableFactory<TScope> : OgFactory<IOgElement, IOgFactoryArguments<TScope>, TScope> where TScope : IOgTransformScope
{
    public override string TypeName { get; } = "Draggable";
    public override IOgElement Create(IOgFactoryArguments<TScope> arguments) => 
        new OgDraggable<IOgElement, TScope>(arguments.Name, arguments.Scope, arguments.Transform);
}