using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgInteractiveElement<TElement> : IOgHoverableElement<TElement> where TElement : IOgElement
{
    IDkObservableProperty<bool>? IsInteracting { get; }
}