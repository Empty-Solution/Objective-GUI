using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public abstract class OgDraggableValueElement<TElement, TValue>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter,
    IDkFieldProvider<TValue> value) : OgInteractableValueElement<TElement, TValue>(name, provider, rectGetter, value) where TElement : IOgElement
{
    protected override bool HandleMouseMove(IOgMouseMoveEvent reason) => base.HandleMouseMove(reason) || UpdateValue(reason);
    protected override bool BeginControl(IOgMouseKeyDownEvent reason) => base.BeginControl(reason) || UpdateValue(reason);
    protected override bool EndControl(IOgMouseKeyUpEvent reason) => base.EndControl(reason) || UpdateValue(reason);
    protected abstract TValue CalculateValue(IOgMouseEvent reason, TValue value);
    private bool UpdateValue(IOgMouseEvent reason)
    {
        if(!IsInteracting) return false;
        TValue value = Value.Get();
        value = CalculateValue(reason, value);
        Value.Set(value);
        return true;
    }
}