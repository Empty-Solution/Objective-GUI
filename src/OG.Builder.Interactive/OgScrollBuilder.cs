using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Interactive;
public class OgScrollBuilder(IOgElementFactory<IOgVectorValueElement<IOgElement>, OgScrollFactoryArguments> factory,
    IDkProcessor<OgScrollBuildContext>? processor)
    : OgInteractableBuilder<IOgElementFactory<IOgVectorValueElement<IOgElement>, OgScrollFactoryArguments>, IOgVectorValueElement<IOgElement>,
        OgScrollFactoryArguments, OgScrollBuildArguments, OgScrollBuildContext, OgTransformerRectField, IOgElement>(factory, processor)
{
    protected override OgTransformerRectField BuildGetter(OgScrollBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgScrollFactoryArguments BuildFactoryArguments(OgScrollBuildContext context, OgScrollBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, context.ValueProvider, context.RectGetProvider);
    protected override OgScrollBuildContext BuildContext(OgScrollBuildArguments args, IOgEventHandlerProvider provider,
        OgTransformerRectField getter)
    {
        DkObservable<Vector2>         observable = new([]);
        DkObservableProperty<Vector2> property   = new(observable, args.Value);
        return new(null!, getter, property, observable);
    }
}