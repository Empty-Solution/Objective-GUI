using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgHoverableElement<TElement> : IOgContainer<TElement> where TElement : IOgElement
{
    IDkObservable<bool>? IsHoveringObserver { get; set; }
    bool IsHovering { get; }
}