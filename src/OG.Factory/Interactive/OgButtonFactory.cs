using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgButtonFactory<TElement> : IOgElementFactory<IOgInteractableElement<TElement>, OgElementFactoryArguments> where TElement : IOgElement
{
    public IOgInteractableElement<TElement> Create(OgElementFactoryArguments arguments) =>
        new OgInteractableElement<TElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider);
}