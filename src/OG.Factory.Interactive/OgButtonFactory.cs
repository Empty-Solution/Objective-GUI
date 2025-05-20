using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgButtonFactory : IOgElementFactory<IOgInteractableElement<IOgVisualElement>, OgElementFactoryArguments>
{
    public IOgInteractableElement<IOgVisualElement> Create(OgElementFactoryArguments arguments) =>
        new OgInteractableElement<IOgVisualElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider);
}