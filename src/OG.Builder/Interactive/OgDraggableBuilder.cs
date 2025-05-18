using DK.Processing.Abstraction.Generic;
using OG.Builder.Abstraction;
using OG.Builder.Arguments;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer;
namespace OG.Builder.Interactive;
public class OgDraggableBuilder(IOgElementFactory<IOgDraggableElement<IOgElement>, OgDraggableFactoryArguments> factory,
    IDkProcessor<OgDraggableBuildContext> processor) : IOgElementBuilder<OgElementBuildArguments>
{
    public IOgElement Build(OgElementBuildArguments args)
    {
        OgOptionsContainer     options  = new();
        OgEventHandlerProvider provider = new();
        OgTransformerRectField field    = new(provider, options);
        OgDraggableFactoryArguments factoryArguments = new(args.Name, field, field)
        {
            EventProvider = provider
        };
        IOgDraggableElement<IOgElement> element = factory.Create(factoryArguments);
        field.LayoutCallback = element;
        processor.Process(new(element, field, options));
        return element;
    }
}