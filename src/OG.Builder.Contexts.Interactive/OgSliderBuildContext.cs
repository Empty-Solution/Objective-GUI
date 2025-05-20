using DK.Observing.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgSliderBuildContext(IOgSlider<IOgVisualElement> element, OgTransformerRectGetter rectGetter,
    IDkObservableProperty<float> property, IDkObservable<float> observable)
    : OgValueBuildContext<IOgSlider<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter, float>(element, rectGetter, property,
        observable);