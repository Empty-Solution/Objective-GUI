using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public abstract class OgDraggableValueElement<TElement, TValue>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgInteractableValueElement<TElement, TValue>(name, provider, rectGetter) where TElement : IOgElement
{
    public override bool Invoke(IOgMouseMoveEvent reason) => base.Invoke(reason) || UpdateValue(reason);
    protected override bool BeginControl(IOgMouseKeyDownEvent reason) => base.BeginControl(reason) || UpdateValue(reason);
    protected override bool EndControl(IOgMouseKeyUpEvent reason) => base.EndControl(reason) || UpdateValue(reason);
    protected abstract TValue CalculateValue(IOgMouseEvent reason, TValue value);
    private bool UpdateValue(IOgMouseEvent reason)
    {
        TValue value    = Value!.Get();
        TValue newValue = CalculateValue(reason, value);
        if(Equals(value, newValue)) return false;
        Value!.Set(newValue);
        return true;
    }
}