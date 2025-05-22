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
public class OgTextBuilder(IOgElementFactory<OgTextElement, OgTextFactoryArguments> factory, IDkProcessor<OgTextBuildContext>? processor)
    : OgBaseBuilder<IOgElementFactory<OgTextElement, OgTextFactoryArguments>, OgTextElement, OgTextFactoryArguments, OgTextBuildArguments,
        OgTextBuildContext, OgAnimationRectGetter<OgTransformerRectGetter>>(factory, processor)
{
    protected override OgAnimationRectGetter<OgTransformerRectGetter> BuildGetter(OgTextBuildArguments args, IOgEventHandlerProvider provider,
        IOgOptionsContainer container)
    {
        OgAnimationRectGetter<OgTransformerRectGetter> getter = new(new(provider, container), provider);
        getter.SetTime(1);
        return getter;
    }
    protected override OgTextFactoryArguments BuildFactoryArguments(OgTextBuildContext context, OgTextBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.Value, args.Font, args.FontSize, args.FontStyle, args.Alignment, args.TextClipping,
            args.WordWrap, args.Text);
    protected override OgTextBuildContext BuildContext(OgTextBuildArguments args, IOgEventHandlerProvider provider,
        OgAnimationRectGetter<OgTransformerRectGetter> getter) =>
        new(null!, getter);
    protected override void InternalProcessContext(OgTextBuildContext context) => context.RectGetProvider.RenderCallback = context.Element;
}