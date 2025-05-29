using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgBindableBuildContext<TValue>(IOgInteractableValueElement<IOgVisualElement, TValue> element, OgTransformerRectGetter rectGetter,
    IDkObservableProperty<TValue> property)
    : OgValueBuildContext<IOgInteractableValueElement<IOgVisualElement, TValue>, IOgVisualElement, OgTransformerRectGetter, TValue>(element, rectGetter,
        property);