using DK.Getting.Generic;
using DK.Processing.Abstraction.Generic;
using OG.Builder.Arguments.Visual;
using OG.Builder.Contexts.Visual;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Visual;
public class OgReadOnlyTextBuilder(IOgElementFactory<OgTextElement, OgTextFactoryArguments> factory, IDkProcessor<OgReadOnlyTextBuildContext> processor)
    : OgBaseBuilder<IOgElementFactory<OgTextElement, OgTextFactoryArguments>, OgTextElement, OgTextFactoryArguments, OgReadOnlyTextBuildArguments,
        OgReadOnlyTextBuildContext, DkReadOnlyGetter<Rect>>(factory, processor)
{
    protected override DkReadOnlyGetter<Rect> BuildGetter(OgReadOnlyTextBuildArguments args, IOgEventHandlerProvider provider,
        IOgOptionsContainer container) =>
        new(args.Rect);
    protected override OgTextFactoryArguments BuildFactoryArguments(OgReadOnlyTextBuildContext context, OgReadOnlyTextBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.Value, args.Font, args.FontSize,
            args.FontStyle, args.PixelsPerUnit, 1f);
    protected override OgReadOnlyTextBuildContext BuildContext(OgReadOnlyTextBuildArguments args, IOgOptionsContainer container,
        IOgEventHandlerProvider provider, DkReadOnlyGetter<Rect> getter) =>
        new(null!, getter, container);
    protected override void InternalProcessContext(OgReadOnlyTextBuildContext context)
    {
        return;
    }
}