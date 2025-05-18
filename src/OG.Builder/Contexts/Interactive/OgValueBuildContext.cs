using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Interactive;
public class OgValueBuildContext<TElement, TChild, TGetter, TValue>(TElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions,
    IDkObservableProperty<TValue> property, IDkObservable<TValue> observable)
    : OgElementBuildContext<TElement, TChild, TGetter>(element, rectGetter, transformerOptions)
    where TElement : IOgInteractableElement<TChild> where TChild : IOgElement where TGetter : IDkGetProvider<Rect>
{
    public IDkObservableProperty<TValue> ValueProvider => property;
    public IDkObservable<TValue>         Observable    => observable;
}