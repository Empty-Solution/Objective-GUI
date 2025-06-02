using DK.Getting.Abstraction.Generic;
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
    protected override bool HandleMouseMove(IOgMouseMoveEvent reason)
    {
        base.HandleMouseMove(reason);
        return IsInteracting && elementRectSetter.Set(PerformDrag(reason.Delta, ElementRect.Get()));
    }
    protected virtual Rect PerformDrag(Vector2 delta, Rect elementRect)
    {
        elementRect.position -= delta;
        return elementRect;
    }
}