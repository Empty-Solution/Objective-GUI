using DK.Observing.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgToggleBuildContext(IOgToggle<IOgVisualElement> element, OgTransformerRectGetter rectGetter, IDkObservableProperty<bool> property,
    IDkObservable<bool> observable)
    : OgValueBuildContext<IOgToggle<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter, bool>(element, rectGetter, property, observable);