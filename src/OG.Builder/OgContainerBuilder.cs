using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments;
using OG.Builder.Contexts;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder;
public class OgContainerBuilder(IOgElementFactory<IOgContainer<IOgElement>, OgElementFactoryArguments> factory,
    IDkProcessor<OgContainerBuildContext>? processor)
    : OgBaseBuilder<IOgElementFactory<IOgContainer<IOgElement>, OgElementFactoryArguments>, IOgContainer<IOgElement>, OgElementFactoryArguments,
        OgElementBuildArguments, OgContainerBuildContext, OgTransformerRectGetter>(factory, processor)
{
    protected override OgTransformerRectGetter
        BuildGetter(OgElementBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgElementFactoryArguments BuildFactoryArguments(OgContainerBuildContext context, OgElementBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider);
    protected override OgContainerBuildContext
        BuildContext(OgElementBuildArguments args, IOgEventHandlerProvider provider, OgTransformerRectGetter getter) =>
        new(null!, getter);
    protected override void InternalProcessContext(OgContainerBuildContext context) => context.RectGetProvider.LayoutCallback = context.Element;
}