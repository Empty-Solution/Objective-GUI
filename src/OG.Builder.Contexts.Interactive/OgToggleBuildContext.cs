using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgToggleBuildContext(IOgToggle<IOgVisualElement> element, OgTransformerRectGetter rectGetter, IDkObservableProperty<bool> property)
    : OgValueBuildContext<IOgToggle<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter, bool>(element, rectGetter, property);