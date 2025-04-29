using OG.Element.Abstraction;

namespace OG.Element.Focusable.Abstraction;

public interface IOgField<TElement> : IOgFocusableControl<TElement, string> where TElement : IOgElement;