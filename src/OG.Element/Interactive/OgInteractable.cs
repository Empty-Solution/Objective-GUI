using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Interactive;

public class OgInteractable<TElement, TScope>(string name, TScope rootScope, IOgTransform transform)
    : OgContainer<TElement, TScope>(name, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected virtual void HandleMouseDown(OgEvent reason) { }

    protected virtual void HandleMouseUp(OgEvent reason) { }

    protected virtual void HandleMouseMove(OgEvent reason) { }

    protected virtual void HandleMouseDrag(OgEvent reason) { }

    protected virtual void HandleKeyDown(OgEvent reason) { }

    protected virtual void HandleKeyUp(OgEvent reason) { }

    protected virtual void HandleMouseScroll(OgEvent reason) { }

    protected override void InternalOnGUI(OgEvent reason)
    {
        base.InternalOnGUI(reason);
        switch(reason.Type)
        {
            case EventType.MouseDown:
            HandleMouseDown(reason);
            break;
            case EventType.MouseUp:
            HandleMouseUp(reason);
            break;
            case EventType.KeyDown:
            HandleKeyDown(reason);
            break;
            case EventType.KeyUp:
            HandleKeyUp(reason);
            break;
            case EventType.ScrollWheel:
            HandleMouseScroll(reason);
            break;
            case EventType.MouseDrag:
            HandleMouseDrag(reason);
            break;
            case EventType.Layout:
            HandleMouseMoveIfNeeded(reason);
            break;
        }
    }

    private void HandleMouseMoveIfNeeded(OgEvent reason)
    {
        if(reason.MousePositionDelta == Vector2.zero) return;
        HandleMouseMove(reason);
    }
}