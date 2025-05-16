using DK.Getting.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public abstract class OgFocusableElement<TElement, TValue>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgInteractableValueElement<TElement, TValue>(name, provider, rectGetter), IOgFocusableElement<TElement, TValue> where TElement : IOgElement
{
    public IDkObservableProperty<bool>? IsFocusing         { get; set; }
    public IDkObservableProperty<bool>? IsFocusInteracting { get; set; }
    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        base.EndControl(reason);
        bool hovering = IsHovering!.Get();
        if(!IsInteracting!.Get() || IsFocusing!.Get() == hovering) return false;
        IsFocusing!.Set(hovering);
        return hovering ? OnFocus(reason) : OnLostFocus(reason);
    }
    protected abstract bool OnFocus(IOgMouseKeyUpEvent reason);
    protected abstract bool OnLostFocus(IOgMouseKeyUpEvent reason);
}