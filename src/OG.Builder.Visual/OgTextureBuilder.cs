using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Visual;
public class OgTextureBuilder(IOgElementFactory<OgTextureElement, OgTextureFactoryArguments> factory, IDkProcessor<OgTextureBuildContext>? processor)
    : OgBaseBuilder<IOgElementFactory<OgTextureElement, OgTextureFactoryArguments>, OgTextureElement, OgTextureFactoryArguments, OgTextureBuildArguments,
        OgTextureBuildContext, OgAnimationRectGetter<OgTransformerRectGetter>>(factory, processor)
{
    protected override OgAnimationRectGetter<OgTransformerRectGetter> BuildGetter(OgTextureBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(new(provider, container), provider);
    protected override OgTextureFactoryArguments BuildFactoryArguments(OgTextureBuildContext context, OgTextureBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.Value, args.Material, args.Borders);
    protected override OgTextureBuildContext BuildContext(OgTextureBuildArguments args, IOgEventHandlerProvider provider,
        OgAnimationRectGetter<OgTransformerRectGetter> getter) =>
        new(null!, getter);
    protected override void InternalProcessContext(OgTextureBuildContext context) => context.RectGetProvider.RenderCallback = context.Element;
}