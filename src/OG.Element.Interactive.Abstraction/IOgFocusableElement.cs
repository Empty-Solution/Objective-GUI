using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgFocusableElement<TElement, TValue> : IOgInteractableValueElement<TElement, TValue> where TElement : IOgElement
{
    IDkObservable<bool>? IsFocusingObserver { get; set; }
}