using DK.Property.Observing.Abstraction.Generic;
using OG.DataTypes.ElementState;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgDraggableElement<TElement>(string name, IOgEventHandlerProvider provider)
    : OgInteractableElement<TElement>(name, provider), IOgDraggableElement<TElement> where TElement : IOgElement
{
    public IDkObservableProperty<Vector2>? DragDelta { get; set; }
    public override bool Invoke(IOgMouseMoveEvent reason)
    {
        base.Invoke(reason);
        if(!IsInteracting!.Get()) return false;
        Vector2 dragDelta = DragDelta!.Get();
        dragDelta += reason.Delta;
        DragDelta!.Set(dragDelta);
        State!.Set(EOgElementState.ACTIVE);
        return PerformDrag(reason, dragDelta);
    }
    public override bool Invoke(IOgLayoutEvent reason)
    {
        base.Invoke(reason);
        Rect rect = ElementRect;
        rect.position += DragDelta!.Get();
        ElementRect   =  rect;
        DragDelta!.Set(Vector2.zero);
        return false;
    }
    protected virtual bool PerformDrag(IOgMouseMoveEvent reason, Vector2 dragDelta) => false;
}