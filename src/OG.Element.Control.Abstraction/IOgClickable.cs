using OG.Element.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.Control.Abstraction;

public interface IOgClickable<TElement> : IOgControl<TElement> where TElement : IOgElement
{
    delegate void OgClickHandler(IOgClickable<TElement> instance, IOgMouseKeyUpEvent reason);

    event OgClickHandler? OnClicked;
}