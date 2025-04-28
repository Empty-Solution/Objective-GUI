using OG.Element.Abstraction;
using OG.Element.FocusableControl.Abstraction;
using OG.Element.View;
using OG.Event.Abstraction;

namespace OG.Element.FocusableControl;

public abstract class OgFocusableControl<TElement, TValue>(IOgEventProvider eventProvider) : OgValueView<TElement, TValue>(eventProvider), IOgFocusableControl<TElement, TValue> where TElement : IOgElement
{
    public bool IsFocused { get; private set; }
    public bool IsFocusControlling => IsFocused && IsControlling;

    protected override bool HandleMouseUp(IOgMouseKeyUpEvent reason)
    {
        base.HandleMouseUp(reason);
        if(IsFocused == IsHovered) return true;

        IsFocused = IsHovered;
        return IsFocused ? OnFocus(reason) : OnLostFocus(reason);
    }

    protected abstract bool OnFocus(IOgMouseKeyUpEvent reason);
    protected abstract bool OnLostFocus(IOgMouseKeyUpEvent reason);
}