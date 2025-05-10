using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgFocusableElement<TElement, TValue> : IOgInteractableValueElement<TElement, TValue> where TElement : IOgElement
{
    IDkObservableProperty<bool>? IsFocusing         { get; }
    IDkObservableProperty<bool>? IsFocusInteracting { get; }
}