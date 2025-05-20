using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgVectorFactory : IOgElementFactory<IOgVectorValueElement<IOgVisualElement>, OgVectorFactoryArguments>
{
    public IOgVectorValueElement<IOgVisualElement> Create(OgVectorFactoryArguments arguments) =>
        new OgVector<IOgVisualElement>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
                                       arguments.ValueProvider);
}