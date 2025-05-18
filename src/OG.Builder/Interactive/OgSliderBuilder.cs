using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using OG.Builder.Abstraction;
using OG.Builder.Arguments.Interactive;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Factory.Abstraction;
using OG.Factory.Arguments;
using OG.Transformer;
namespace OG.Builder.Interactive;
public class OgSliderBuilder<TFactory>(TFactory factory, IDkProcessor<OgSliderBuildContext> processor)
    : IOgElementBuilder<OgSliderBuildArguments> where TFactory : IOgElementFactory<IOgSlider<IOgVisualElement>, OgSliderFactoryArguments>
{
    public IOgElement Build(OgSliderBuildArguments args)
    {
        OgOptionsContainer          options    = new();
        OgEventHandlerProvider      provider   = new();
        OgTransformerRectGetter     getter     = new(provider, options);
        DkObservable<float>         observable = new([]);
        DkObservableProperty<float> property   = new(observable, args.Value);
        OgSliderFactoryArguments factoryArguments = new(args.Name, getter, property)
        {
            EventProvider = provider
        };
        IOgSlider<IOgVisualElement> element = factory.Create(factoryArguments);
        getter.LayoutCallback = element;
        processor.Process(new(element, getter, options, property, observable));
        return element;
    }
}