using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgTextField<TElement> : IOgFocusableElement<TElement, string> where TElement : IOgElement;