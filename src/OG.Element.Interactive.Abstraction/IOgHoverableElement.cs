using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgHoverableElement<TElement> : IOgContainer<TElement>, IOgInteractable where TElement : IOgElement
{
    IDkObservableProperty<bool>? IsHovering { get; }
}