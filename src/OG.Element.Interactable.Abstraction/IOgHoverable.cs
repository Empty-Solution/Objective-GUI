using OG.Element.Abstraction;

namespace OG.Element.Interactable.Abstraction;

public interface IOgHoverable<TElement> : IOgInteractable<TElement> where TElement : IOgElement
{
    bool IsHovered { get; }
}