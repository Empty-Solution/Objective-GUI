using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgToggleFactory : IOgElementFactory<IOgToggle<IOgVisualElement>, OgToggleFactoryArguments>
{
    public IOgToggle<IOgVisualElement> Create(OgToggleFactoryArguments arguments) =>
        new OgToggle<IOgVisualElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                                       arguments.ValueProvider);
}