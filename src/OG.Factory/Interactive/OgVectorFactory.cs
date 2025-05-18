using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgVectorFactory<TElement> : IOgElementFactory<IOgVectorValueElement<TElement>, OgVectorFactoryArguments> where TElement : IOgElement
{
    public IOgVectorValueElement<TElement> Create(OgVectorFactoryArguments arguments) =>
        new OgVector<TElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                               arguments.ValueProvider);
}