using OG.Element.Abstraction;
using OG.Element.Hoverable.Abstraction;
using OG.Event.Abstraction.Handlers;
namespace OG.Element.Control.Abstraction;
public interface IOgControl<TElement> : IOgHoverable<TElement>, IOgMouseKeyDownEventHandler, IOgMouseKeyUpEventHandler where TElement : IOgElement
{
    bool IsControlling { get; }
}