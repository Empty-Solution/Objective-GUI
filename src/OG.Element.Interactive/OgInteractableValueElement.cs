using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
namespace OG.Element.Interactive;
public class OgInteractableValueElement<TElement, TValue>(string name, IOgEventHandlerProvider provider)
    : OgInteractableElement<TElement>(name, provider), IOgInteractableValueElement<TElement, TValue> where TElement : IOgElement
{
    public IDkObservableProperty<TValue>? Value { get; set; }
}