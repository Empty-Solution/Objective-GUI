using OG.Element.Abstraction;
using OG.Element.Interactable.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.Control.Abstraction;

public interface IOgControl<TElement> : IOgHoverable<TElement> where TElement : IOgElement
{
    delegate void OgBeginInteractionHandler(IOgControl<TElement> instance, IOgMouseKeyDownEvent reason);

    delegate void OgEndInteractionHandler(IOgControl<TElement> instance, IOgMouseKeyUpEvent reason);

    bool IsInteracting { get; }
    event OgBeginInteractionHandler? OnBeginInteraction;
    event OgEndInteractionHandler? OnEndInteraction;
}