using DK.Processing.Abstraction.Generic;
using OG.Builder.Abstraction;
using OG.Builder.Arguments;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer;
namespace OG.Builder.Interactive;
public class OgButtonBuilder(IOgElementFactory<IOgInteractableElement<IOgVisualElement>, OgElementFactoryArguments> factory,
    IDkProcessor<OgButtonBuildContext> processor) : IOgElementBuilder<OgElementBuildArguments>
{
    public IOgElement Build(OgElementBuildArguments args)
    {
        OgOptionsContainer      options  = new();
        OgEventHandlerProvider  provider = new();
        OgTransformerRectGetter getter   = new(provider, options);
        OgElementFactoryArguments factoryArguments = new(args.Name, getter)
        {
            EventProvider = provider
        };
        IOgInteractableElement<IOgVisualElement> element = factory.Create(factoryArguments);
        getter.LayoutCallback = element;
        processor.Process(new(element, getter, options));
        return element;
    }
}