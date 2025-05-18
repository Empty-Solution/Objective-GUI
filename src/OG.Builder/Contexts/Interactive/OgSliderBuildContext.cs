using DK.Observing.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgSliderBuildContext(IOgSlider<IOgVisualElement> element, OgTransformerRectGetter rectGetter, IOgOptionsContainer transformerOptions,
    IDkObservableProperty<float> property, IDkObservable<float> observable)
    : OgValueBuildContext<IOgSlider<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter, float>(element, rectGetter, transformerOptions, property,
        observable);