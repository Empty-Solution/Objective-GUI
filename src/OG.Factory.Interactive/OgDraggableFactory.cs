using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgDraggableFactory : IOgElementFactory<IOgDraggableElement<IOgElement>, OgDraggableFactoryArguments>
{
    public IOgDraggableElement<IOgElement> Create(OgDraggableFactoryArguments arguments) =>
        new OgDraggableElement<IOgElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
            arguments.RectSetProvider);
}