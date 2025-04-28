using OG.Element.Abstraction;
using OG.Element.Interactable.Abstraction;

namespace OG.Element.Control.Abstraction;

public interface IOgControl<TElement> : IOgHoverable<TElement> where TElement : IOgElement
{
    bool IsControlling { get; }
}