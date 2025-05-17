using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgDraggableElement<TElement> : IOgInteractableElement<TElement> where TElement : IOgElement;