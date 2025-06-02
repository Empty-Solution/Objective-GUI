using OG.Element.Abstraction;
using OG.Element.Container;
using OG.Element.Container.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory;
public class OgContainerFactory<TElement> : IOgElementFactory<IOgContainer<TElement>, OgElementFactoryArguments> where TElement : IOgElement
{
    public IOgContainer<TElement> Create(OgElementFactoryArguments arguments) =>
        new OgContainer<TElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider);
}