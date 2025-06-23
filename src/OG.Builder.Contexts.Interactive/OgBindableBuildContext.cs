using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgBindableBuildContext<TValue>(IOgBindableElement<IOgVisualElement, TValue> element, OgTransformerRectGetter rectGetter,
    IDkFieldProvider<TValue> property)
    : OgInteractableElementBuildContext<IOgBindableElement<IOgVisualElement, TValue>, IOgVisualElement, OgTransformerRectGetter>(element,
        rectGetter)
{
    public IDkFieldProvider<TValue> ValueProvider => property;
}