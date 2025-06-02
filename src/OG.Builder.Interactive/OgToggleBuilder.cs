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
public class OgToggleBuilder(IOgElementFactory<IOgToggle<IOgVisualElement>, OgToggleFactoryArguments> factory,
    IDkProcessor<OgToggleBuildContext>? processor)
    : OgInteractableBuilder<IOgElementFactory<IOgToggle<IOgVisualElement>, OgToggleFactoryArguments>, IOgToggle<IOgVisualElement>, OgToggleFactoryArguments
      , OgToggleBuildArguments, OgToggleBuildContext, OgTransformerRectGetter, IOgVisualElement>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(OgToggleBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgToggleFactoryArguments BuildFactoryArguments(OgToggleBuildContext context, OgToggleBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, context.ValueProvider);
    protected override OgToggleBuildContext BuildContext(OgToggleBuildArguments args, IOgEventHandlerProvider provider, OgTransformerRectGetter getter) =>
        new(null!, getter, args.Value);
}