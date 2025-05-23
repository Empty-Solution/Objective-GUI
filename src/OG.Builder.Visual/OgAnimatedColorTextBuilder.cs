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
public class OgAnimatedColorTextBuilder(IOgElementFactory<OgTextElement, OgTextFactoryArguments> factory,
    IDkProcessor<OgAnimatedColorTextBuildContext>? processor)
    : OgBaseBuilder<IOgElementFactory<OgTextElement, OgTextFactoryArguments>, OgTextElement, OgTextFactoryArguments, OgTextBuildArguments,
        OgAnimatedColorTextBuildContext, OgTransformerRectGetter>(factory, processor)
{
    protected override OgTransformerRectGetter BuildGetter(OgTextBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgTextFactoryArguments BuildFactoryArguments(OgAnimatedColorTextBuildContext context, OgTextBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.Value, null, args.Font, args.FontSize, args.FontStyle, args.Alignment, args.TextClipping,
            args.WordWrap, args.Text);
    protected override OgAnimatedColorTextBuildContext BuildContext(OgTextBuildArguments args, IOgEventHandlerProvider provider,
        OgTransformerRectGetter getter) =>
        new(null!, getter, new(new(args.Value), provider));
    protected override void InternalProcessContext(OgAnimatedColorTextBuildContext context) => context.ColorGetter.RenderCallback = context.Element;
}