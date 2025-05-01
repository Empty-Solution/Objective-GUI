using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event.Abstraction.Handlers;

namespace OG.Element.Hoverable.Abstraction;

public interface IOgHoverable<TElement> : IOgContainer<TElement>, IOgMouseMoveEventHandler where TElement : IOgElement
{
    bool IsHovered { get; }
}