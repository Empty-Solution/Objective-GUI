using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgTextFieldFactory : IOgElementFactory<IOgTextField<IOgVisualElement>, OgTextFieldFactoryArguments>
{
    public IOgTextField<IOgVisualElement> Create(OgTextFieldFactoryArguments arguments) =>
        new OgTextField<IOgVisualElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                                          arguments.ValueProvider, arguments.TextController, arguments.Font);
}