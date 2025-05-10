using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgInteractableValueElement<TElement, TValue> : IOgInteractiveElement<TElement> where TElement : IOgElement
{
    IDkObservableProperty<TValue>? Value { get; }
}