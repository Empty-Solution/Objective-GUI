using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Interactive;
public class OgModalButtonBuilder(IOgElementFactory<IOgModalInteractable<IOgElement>, OgModalButtonFactoryArguments> factory,
    IDkProcessor<OgModalButtonBuildContext>? processor)
    : OgInteractableBuilder<IOgElementFactory<IOgModalInteractable<IOgElement>, OgModalButtonFactoryArguments>, IOgModalInteractable<IOgElement>,
        OgModalButtonFactoryArguments, OgModalButtonBuildArguments, OgModalButtonBuildContext, OgTransformerRectGetter, IOgElement>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(OgModalButtonBuildArguments args, IOgEventHandlerProvider provider,
        IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgModalButtonFactoryArguments BuildFactoryArguments(OgModalButtonBuildContext context, OgModalButtonBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.RightClickOnly);
    protected override OgModalButtonBuildContext BuildContext(OgModalButtonBuildArguments args, IOgEventHandlerProvider provider,
        OgTransformerRectGetter getter) =>
        new(null!, getter);
}