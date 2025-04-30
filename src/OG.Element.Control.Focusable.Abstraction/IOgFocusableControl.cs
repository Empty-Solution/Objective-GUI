#region

using OG.Element.Abstraction;
using OG.Element.View.Abstraction;

#endregion

namespace OG.Element.Control.Focusable.Abstraction;

public interface IOgFocusableControl<TElement, TValue> : IOgValueView<TElement, TValue> where TElement : IOgElement
{
    bool IsFocused          { get; }
    bool IsFocusControlling { get; }
}