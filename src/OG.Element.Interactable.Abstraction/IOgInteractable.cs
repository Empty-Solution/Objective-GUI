using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;

namespace OG.Element.Interactable.Abstraction;

public interface IOgInteractable<TElement> : IOgContainer<TElement> where TElement : IOgElement;