using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
namespace OG.Factory.Interactive;
public class OgBindableFactory<TValue> : IOgElementFactory<IOgBindableElement<IOgVisualElement, TValue>, OgBindableFactoryArguments<TValue>>
{
    public IOgBindableElement<IOgVisualElement, TValue> Create(OgBindableFactoryArguments<TValue> arguments) =>
        new OgBindableElement<IOgVisualElement, TValue>(arguments.Name, arguments.EventProvider ?? new OgEventHandlerProvider(), arguments.RectGetProvider,
            arguments.Value, arguments.ValueOverride, arguments.Bind, arguments.BindTypeGetProvider);
}