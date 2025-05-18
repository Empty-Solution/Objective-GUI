using DK.Observing.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgToggleBuildContext(IOgToggle<IOgVisualElement> element, OgTransformerRectGetter rectGetter, IOgOptionsContainer transformerOptions,
    IDkObservableProperty<bool> property, IDkObservable<bool> observable)
    : OgValueBuildContext<IOgToggle<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter, bool>(element, rectGetter, transformerOptions, property,
        observable);