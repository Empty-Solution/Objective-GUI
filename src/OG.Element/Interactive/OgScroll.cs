using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Interactive;

public class OgScroll<TElement, TScope>(string name, TScope scope, IOgTransform transform)
    : OgHoverable<TElement, TScope>(name, scope, transform) where TElement : IOgElement where TScope : IOgClipTransformScope
{
    public delegate void OgScrollHandler(OgScroll<TElement, TScope> instance, Vector2 scrollPosition);

    public Vector2 Position { get; private set; }
    public event OgScrollHandler? OnScroll;

    protected override void HandleMouseScroll(OgEvent reason)
    {
        if(!IsHovered) return;
        ChangeScrollPosition(reason.ScrollDelta);
    }

    protected virtual void ChangeScrollPosition(Vector2 scrollDelta)
    {
        Vector2 position = Position += scrollDelta;
        Scope.ScrollPosition = position;
        OnScroll?.Invoke(this, position);
    }
}