using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgScrollFactory : IOgElementFactory<IOgVectorValueElement<IOgElement>, OgScrollFactoryArguments>
{
    public IOgVectorValueElement<IOgElement> Create(OgScrollFactoryArguments arguments) =>
        new OgScroll<IOgElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
            arguments.RectSetProvider, arguments.ValueProvider);
}