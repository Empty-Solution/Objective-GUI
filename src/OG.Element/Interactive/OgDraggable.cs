using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Interactive;

public class OgDraggable<TElement, TScope>(string name, TScope rootScope, IOgTransform transform)
    : OgControl<TElement, TScope>(name, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgDragEnterHandler(OgDraggable<TElement, TScope> instance, Rect rect, OgEvent reason);

    public delegate void OgDragExitHandler(OgDraggable<TElement, TScope> instance, Rect rect, OgEvent reason);

    public delegate void OgDragPerformHandler(OgDraggable<TElement, TScope> instance, Rect rect, Vector2 delta, OgEvent reason);

    public event OgDragEnterHandler? OnBeginDrag;
    public event OgDragPerformHandler? OnPerformDrag;
    public event OgDragExitHandler? OnEndDrag;

    protected override void BeginInteract(OgEvent reason)
    {
        base.BeginInteract(reason);
        BeginDrag(reason, Transform.LocalRect);
    }

    protected override void EndInteract(OgEvent reason)
    {
        base.EndInteract(reason);
        EndDrag(reason, Transform.LocalRect);
    }

    protected override void HandleMouseMove(OgEvent reason)
    {
        base.HandleMouseMove(reason);
        if(!IsInteracting) return;
        PerformDrag(reason);
    }

    protected virtual void PerformDrag(OgEvent reason)
    {
        IOgTransform transform = Transform;
        Vector2 delta = reason.MousePositionDelta;
        transform.LocalRect = Move(transform.LocalRect, delta);
        OnPerformDrag?.Invoke(this, transform.LocalRect, delta, reason);
    }

    protected virtual Rect Move(Rect rect, Vector2 delta) => new(rect.x + delta.x, rect.y + delta.y, rect.width, rect.height);

    protected virtual void BeginDrag(OgEvent reason, Rect rect) => OnBeginDrag?.Invoke(this, Transform.LocalRect, reason);

    protected virtual void EndDrag(OgEvent reason, Rect rect) => OnEndDrag?.Invoke(this, Transform.LocalRect, reason);
}