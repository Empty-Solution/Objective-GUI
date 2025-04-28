using OG.Element.Abstraction;
using OG.Element.Control.Abstraction;

namespace OG.Element.Draggable.Abstraction;

public interface IOgDraggable<TElement> : IOgControl<TElement> where TElement : IOgElement
{
    bool IsDragging { get; }
}