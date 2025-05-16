using DK.Getting.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using DK.Setting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgDraggableElement<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter,
    IDkSetProvider<Rect> elementRectSetter)
    : OgInteractableElement<TElement>(name, provider, rectGetter), IOgDraggableElement<TElement> where TElement : IOgElement
{
    public IDkObservableProperty<Vector2>? DragDelta { get; set; }
    public override bool Invoke(IOgMouseMoveEvent reason)
    {
        base.Invoke(reason);
        if(!IsInteracting!.Get()) return false;
        Vector2 dragDelta = DragDelta!.Get();
        dragDelta += reason.Delta;
        DragDelta!.Set(dragDelta);
        return PerformDrag(reason, dragDelta);
    }
    public override bool Invoke(IOgLayoutEvent reason)
    {
        Rect rect = ElementRect.Get();
        rect.position += DragDelta!.Get();
        elementRectSetter.Set(rect);
        DragDelta!.Set(Vector2.zero);
        return base.Invoke(reason);
    }
    protected virtual bool PerformDrag(IOgMouseMoveEvent reason, Vector2 dragDelta) => false;
}