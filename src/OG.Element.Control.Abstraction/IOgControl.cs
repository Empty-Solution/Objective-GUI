using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;

namespace OG.Element.Control.Abstraction;

public interface IOgControl<TElement> : IOgContainer<TElement> where TElement : IOgElement;
