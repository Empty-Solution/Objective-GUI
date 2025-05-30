using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgInteractableElement<TElement> : IOgHoverableElement<TElement> where TElement : IOgElement
{
    IDkObservable<bool>? IsInteractingObserver      { get; set; }
    IDkObservable<bool>? IsRightInteractingObserver { get; set; }
    int                  SortOrder                  { get; set; }
}