using DK.Getting.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgInteractableValueElement<TElement, TValue>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgInteractableElement<TElement>(name, provider, rectGetter), IOgInteractableValueElement<TElement, TValue> where TElement : IOgElement
{
    public IDkObservableProperty<TValue>? Value { get; set; }
}