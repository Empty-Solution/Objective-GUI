using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Interactive;
public class OgDraggableBuilder(IOgElementFactory<IOgDraggableElement<IOgElement>, OgDraggableFactoryArguments> factory,
    IDkProcessor<OgDraggableBuildContext> processor)
    : OgInteractableBuilder<IOgElementFactory<IOgDraggableElement<IOgElement>, OgDraggableFactoryArguments>, IOgDraggableElement<IOgElement>,
        OgDraggableFactoryArguments, OgElementBuildArguments, OgDraggableBuildContext, OgTransformerRectField, IOgElement>(factory, processor)
{
    protected override OgTransformerRectField BuildGetter(IOgEventHandlerProvider provider, IOgOptionsContainer container) => new(provider, container);
    protected override OgDraggableFactoryArguments BuildFactoryArguments(OgDraggableBuildContext context, OgElementBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, context.RectGetProvider, provider);
    protected override OgDraggableBuildContext BuildContext(OgElementBuildArguments args, IOgOptionsContainer container, IOgEventHandlerProvider provider,
        OgTransformerRectField getter) =>
        new(null!, getter, container);
}