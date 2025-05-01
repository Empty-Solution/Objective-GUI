using OG.Element.Abstraction;
using OG.Element.Control.Focusable.Abstraction;
using OG.Element.View;
using OG.Event.Abstraction;

namespace OG.Element.Control.Focusable;

public abstract class OgFocusableControl<TElement, TValue>(IOgEventProvider eventProvider) : OgValueView<TElement, TValue>(eventProvider), IOgFocusableControl<TElement, TValue> where TElement : IOgElement
{
    public bool IsFocused { get; private set; }

    public bool IsFocusControlling => IsFocused && IsControlling;

    public override bool HandleMouseUp(IOgMouseKeyUpEvent reason)
    {
        if(!base.HandleMouseUp(reason)) return false;
        if(IsFocused == IsHovered) return true;
        IsFocused = IsHovered;
        return IsFocused ? OnFocus(reason) : OnLostFocus(reason);
    }

    protected abstract bool OnFocus(IOgMouseKeyUpEvent reason);

    protected abstract bool OnLostFocus(IOgMouseKeyUpEvent reason);
}