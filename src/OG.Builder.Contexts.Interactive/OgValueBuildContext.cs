using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
namespace OG.Builder.Contexts.Interactive;
public class OgValueBuildContext<TElement, TChild, TGetter, TValue>(TElement element, TGetter rectGetter, IDkObservableProperty<TValue> property)
    : OgInteractableElementBuildContext<TElement, TChild, TGetter>(element, rectGetter) where TElement : IOgInteractableElement<TChild>
                                                                                        where TChild : IOgElement where TGetter : OgTransformerRectGetter
{
    public IDkObservableProperty<TValue> ValueProvider => property;
}