#region

using OG.Element.Abstraction;
using OG.Element.Control.Focusable.Abstraction;

#endregion

namespace OG.Element.Interactable.Abstraction;

public interface IOgField<TElement> : IOgFocusableControl<TElement, string> where TElement : IOgElement;