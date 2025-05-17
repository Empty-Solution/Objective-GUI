using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgDraggableFactory<TElement> : IOgElementFactory<IOgDraggableElement<TElement>, OgDraggableFactoryArguments> where TElement : IOgElement
{
    public IOgDraggableElement<TElement> Create(OgDraggableFactoryArguments arguments) =>
        new OgDraggableElement<TElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider, arguments.RectSetProvider);
}