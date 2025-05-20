using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgToggleFactory<TElement> : IOgElementFactory<IOgToggle<TElement>, OgToggleFactoryArguments> where TElement : IOgElement
{
    public IOgToggle<TElement> Create(OgToggleFactoryArguments arguments) =>
        new OgToggle<TElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                               arguments.ValueProvider);
}