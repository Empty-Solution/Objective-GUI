using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgModalButtonFactory : IOgElementFactory<IOgModalInteractable<IOgElement>, OgModalButtonFactoryArguments>
{
    public IOgModalInteractable<IOgElement> Create(OgModalButtonFactoryArguments arguments) =>
        new OgModalInteractable<IOgElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                                            arguments.RightClickOnly);
}