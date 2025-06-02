using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgToggle<TElement> : IOgInteractableValueElement<TElement, bool> where TElement : IOgElement;