using DK.Property.Observing.Abstraction.Generic;
using OG.DataTypes.ElementState;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Element.Interactive;
public abstract class OgFocusableElement<TElement, TValue>(string name, IOgEventHandlerProvider provider)
    : OgInteractableValueElement<TElement, TValue>(name, provider), IOgFocusableElement<TElement, TValue> where TElement : IOgElement
{
    public IDkObservableProperty<bool>? IsFocusing         { get; set; }
    public IDkObservableProperty<bool>? IsFocusInteracting { get; set; }
    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        base.EndControl(reason);
        bool hovering = IsHovering!.Get();
        if(!IsInteracting!.Get() || IsFocusing!.Get() == hovering) return false;
        IsFocusing!.Set(hovering);
        State!.Set(hovering ? EOgElementState.FOCUSED : EOgElementState.IDLE);
        return hovering ? OnFocus(reason) : OnLostFocus(reason);
    }
    protected abstract bool OnFocus(IOgMouseKeyUpEvent reason);
    protected abstract bool OnLostFocus(IOgMouseKeyUpEvent reason);
}