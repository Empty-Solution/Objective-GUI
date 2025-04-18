using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public class OgHoverable<TElement, TScope>(string name, TScope rootScope, IOgTransform transform)
    : OgInteractable<TElement, TScope>(name, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgMouseEnterHandler(OgHoverable<TElement, TScope> instance, OgEvent reason);

    public delegate void OgMouseExitHandler(OgHoverable<TElement, TScope> instance, OgEvent reason);

    public bool IsHovered { get; private set; }

    public event OgMouseEnterHandler? OnMouseEnter;
    public event OgMouseExitHandler? OnMouseExit;

    protected override void HandleMouseMove(OgEvent reason)
    {
        base.HandleMouseMove(reason);
        UpdateHoverState(reason);
    }

    protected virtual void MouseEnter(OgEvent reason) => OnMouseEnter?.Invoke(this, reason);

    protected virtual void MouseExit(OgEvent reason) => OnMouseExit?.Invoke(this, reason);

    private void UpdateHoverState(OgEvent reason)
    {
        bool containsMouse = Transform.LocalRect.Contains(reason.MousePosition);

        if(IsHovered == containsMouse) return;

        IsHovered = containsMouse;
        InternalInvokeHoverEvent(containsMouse, reason);
    }

    private void InternalInvokeHoverEvent(bool containsMouse, OgEvent reason)
    {
        if(containsMouse)
        {
            MouseEnter(reason);
            return;
        }

        MouseExit(reason);
    }
}