using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer.Abstraction;
namespace OG.Builder.Interactive;
public class OgSliderBuilder<TFactory>(TFactory factory, IDkProcessor<OgSliderBuildContext> processor)
    : OgInteractableBuilder<TFactory, IOgSlider<IOgVisualElement>, OgSliderFactoryArguments, OgSliderBuildArguments, OgSliderBuildContext,
        OgTransformerRectGetter, IOgVisualElement>(factory, processor)
    where TFactory : IOgElementFactory<IOgSlider<IOgVisualElement>, OgSliderFactoryArguments>
{
    protected override OgTransformerRectGetter BuildGetter(OgSliderBuildArguments args, IOgEventHandlerProvider provider, IOgOptionsContainer container) =>
        new(provider, container);
    protected override OgSliderFactoryArguments BuildFactoryArguments(OgSliderBuildContext context, OgSliderBuildArguments args,
        IOgEventHandlerProvider provider) =>
        new(args.Name, context.RectGetProvider, provider, context.ValueProvider);
    protected override OgSliderBuildContext BuildContext(OgSliderBuildArguments args, IOgOptionsContainer container, IOgEventHandlerProvider provider,
        OgTransformerRectGetter getter)
    {
        DkObservable<float>         observable = new([]);
        DkObservableProperty<float> property   = new(observable, args.Value);
        return new(null!, getter, container, property, observable);
    }
}