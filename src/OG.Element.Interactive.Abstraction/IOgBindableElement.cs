using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgBindableElement<TElement, TValue> : IOgInteractableValueElement<TElement, TValue> where TElement : IOgElement
{
    IDkObservable<TValue>? BindObservable { get; set; }
}