using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public abstract class OgFocusableElement<TElement, TValue>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter,
    IDkFieldProvider<TValue> value)
    : OgInteractableValueElement<TElement, TValue>(name, provider, rectGetter, value), IOgFocusableElement<TElement, TValue> where TElement : IOgElement
{
    protected bool                 IsFocusing         { get; set; }
    public    IDkObservable<bool>? IsFocusingObserver { get; set; }
    protected override bool PreEndControl(IOgMouseKeyUpEvent reason)
    {
        base.PreEndControl(reason);
        bool hovering = IsHovering;
        if(!IsInteracting || IsFocusing == hovering) return false;
        IsFocusing = hovering;
        IsFocusingObserver!.Notify(hovering);
        return hovering ? OnFocus(reason) : OnLostFocus(reason);
    }
    protected abstract bool OnFocus(IOgMouseKeyUpEvent reason);
    protected abstract bool OnLostFocus(IOgMouseKeyUpEvent reason);
}