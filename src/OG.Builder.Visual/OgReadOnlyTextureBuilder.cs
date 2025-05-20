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
public class OgReadOnlyTextureBuilder(IOgElementFactory<OgTextureElement, OgTextureFactoryArguments> factory,
    IDkProcessor<OgReadOnlyTextureBuildContext>? processor)
    : OgBaseBuilder<IOgElementFactory<OgTextureElement, OgTextureFactoryArguments>, OgTextureElement, OgTextureFactoryArguments,
        OgReadOnlyTextureBuildArguments, OgReadOnlyTextureBuildContext, DkReadOnlyGetter<Rect>>(factory, processor)
{
    protected override DkReadOnlyGetter<Rect> BuildGetter(OgReadOnlyTextureBuildArguments args, IOgEventHandlerProvider provider,
        IOgOptionsContainer container) =>
        new(args.Rect);
    protected override OgTextureFactoryArguments BuildFactoryArguments(OgReadOnlyTextureBuildContext context, OgReadOnlyTextureBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, args.Value, args.Material, args.Borders);
    protected override OgReadOnlyTextureBuildContext BuildContext(OgReadOnlyTextureBuildArguments args,
        IOgEventHandlerProvider provider, DkReadOnlyGetter<Rect> getter) =>
        new(null!, getter);
    protected override void InternalProcessContext(OgReadOnlyTextureBuildContext context)
    {
    }
}