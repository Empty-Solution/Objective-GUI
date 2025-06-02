using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Interactive;
public class OgVectorBuilder(IOgElementFactory<IOgVectorValueElement<IOgVisualElement>, OgVectorFactoryArguments> factory,
    IDkProcessor<OgVectorBuildContext>? processor)
    : OgInteractableBuilder<IOgElementFactory<IOgVectorValueElement<IOgVisualElement>, OgVectorFactoryArguments>, IOgVectorValueElement<IOgVisualElement>,
        OgVectorFactoryArguments, OgVectorBuildArguments, OgVectorBuildContext, OgTransformerRectGetter, IOgVisualElement>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(OgVectorBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgVectorFactoryArguments BuildFactoryArguments(OgVectorBuildContext context, OgVectorBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, context.ValueProvider);
    protected override OgVectorBuildContext BuildContext(OgVectorBuildArguments args, IOgEventHandlerProvider provider, OgTransformerRectGetter getter) =>
        new(null!, getter, args.Value);
}