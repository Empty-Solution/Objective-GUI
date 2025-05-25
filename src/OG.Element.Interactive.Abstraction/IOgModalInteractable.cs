using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgModalInteractable<TElement> : IOgInteractableElement<TElement> where TElement : IOgElement
{
    public IDkObservable<bool>? IsModalInteractObserver { get; set; }
}