using DK.Processing.Abstraction.Generic;
using OG.Animator;
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
public class OgTextureBuilder(IOgElementFactory<OgTextureElement, OgTextureFactoryArguments> factory, IDkProcessor<OgTextureBuildContext> processor)
    : OgBaseBuilder<IOgElementFactory<OgTextureElement, OgTextureFactoryArguments>, OgTextureElement, OgTextureFactoryArguments, OgTextureBuildArguments,
        OgTextureBuildContext, OgAnimationRectGetter>(factory, processor)
{
    protected override OgAnimationRectGetter BuildGetter(IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(new OgTransformerRectGetter(provider, container), provider, new OgRectAnimator());
    protected override OgTextureFactoryArguments BuildFactoryArguments(OgTextureBuildContext context, OgTextureBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.Value, args.Material, args.Borders);
    protected override OgTextureBuildContext BuildContext(OgTextureBuildArguments args, IOgOptionsContainer container, IOgEventHandlerProvider provider,
        OgAnimationRectGetter getter) =>
        new(null!, getter, container);
    protected override void InternalProcessContext(OgTextureBuildContext context) => context.RectGetProvider.RenderCallback = context.Element;
}