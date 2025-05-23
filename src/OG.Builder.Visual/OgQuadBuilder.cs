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
public class OgQuadBuilder(IOgElementFactory<OgQuadElement, OgQuadFactoryArguments> factory, IDkProcessor<OgQuadBuildContext>? processor)
    : OgBaseBuilder<IOgElementFactory<OgQuadElement, OgQuadFactoryArguments>, OgQuadElement, OgQuadFactoryArguments, OgQuadBuildArguments,
        OgQuadBuildContext, OgAnimationRectGetter<OgTransformerRectGetter>>(factory, processor)
{
    protected override OgAnimationRectGetter<OgTransformerRectGetter> BuildGetter(OgQuadBuildArguments args, IOgEventHandlerProvider provider,
        IOgOptionsContainer container) =>
        new(new(provider, container), provider);
    protected override OgQuadFactoryArguments BuildFactoryArguments(OgQuadBuildContext context, OgQuadBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.TopLeft, args.TopRight, args.BottomLeft, args.BottomRight);
    protected override OgQuadBuildContext BuildContext(OgQuadBuildArguments args, IOgEventHandlerProvider provider,
        OgAnimationRectGetter<OgTransformerRectGetter> getter) =>
        new(null!, getter);
    protected override void InternalProcessContext(OgQuadBuildContext context) => context.RectGetProvider.RenderCallback = context.Element;
}