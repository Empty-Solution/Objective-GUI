using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Interactive;
public class OgButtonBuilder(IOgElementFactory<IOgInteractableElement<IOgVisualElement>, OgElementFactoryArguments> factory,
    IDkProcessor<OgButtonBuildContext> processor)
    : OgInteractableBuilder<IOgElementFactory<IOgInteractableElement<IOgVisualElement>, OgElementFactoryArguments>,
        IOgInteractableElement<IOgVisualElement>, OgElementFactoryArguments, OgElementBuildArguments, OgButtonBuildContext, OgTransformerRectGetter,
        IOgVisualElement>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(IOgEventHandlerProvider provider, IOgOptionsContainer container) => new(provider, container);
    protected override OgElementFactoryArguments BuildFactoryArguments(OgButtonBuildContext context, OgElementBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider);
    protected override OgButtonBuildContext BuildContext(OgElementBuildArguments args, IOgOptionsContainer container, IOgEventHandlerProvider provider,
        OgTransformerRectGetter getter) =>
        new(null!, getter, container);
}