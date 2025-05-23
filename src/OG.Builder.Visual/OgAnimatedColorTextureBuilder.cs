using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Visual;
public class OgAnimatedColorTextureBuilder(IOgElementFactory<OgTextureElement, OgTextureFactoryArguments> factory,
    IDkProcessor<OgAnimatedColorTextureBuildContext>? processor)
    : OgBaseBuilder<IOgElementFactory<OgTextureElement, OgTextureFactoryArguments>, OgTextureElement, OgTextureFactoryArguments, OgTextureBuildArguments,
        OgAnimatedColorTextureBuildContext, OgTransformerRectGetter>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(OgTextureBuildArguments args, IOgEventHandlerProvider provider,
        IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgTextureFactoryArguments BuildFactoryArguments(OgAnimatedColorTextureBuildContext context, OgTextureBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, null, context.ColorGetter, args.Texture, args.BorderWidths, args.BorderRadiuses,
            args.ImageAspect, args.AlphaBlend);
    protected override OgAnimatedColorTextureBuildContext BuildContext(OgTextureBuildArguments args, IOgEventHandlerProvider provider,
        OgTransformerRectGetter getter) =>
        new(null!, getter, new(new(args.Value), provider));
    protected override void InternalProcessContext(OgAnimatedColorTextureBuildContext context) => context.ColorGetter.RenderCallback = context.Element;
}