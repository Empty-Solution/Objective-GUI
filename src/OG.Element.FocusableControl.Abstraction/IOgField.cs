using OG.Element.Abstraction;

namespace OG.Element.FocusableControl.Abstraction;

public interface IOgField<TElement> : IOgFocusableControl<TElement, string> where TElement : IOgElement;