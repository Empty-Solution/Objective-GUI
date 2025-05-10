using OG.Element.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgClickableElement<TElement> : IOgInteractiveElement<TElement> where TElement : IOgElement
{
    delegate void OgClickHandler(IOgClickableElement<TElement> instance, IOgMouseKeyUpEvent reason);
    event OgClickHandler? OnClicked;
}