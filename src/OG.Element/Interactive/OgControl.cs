using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public class OgControl<TElement, TScope>(string name, TScope rootScope, IOgTransform transform)
    : OgHoverable<TElement, TScope>(name, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgBeginInteractionHandler(OgControl<TElement, TScope> instance, OgEvent reason);

    public delegate void OgEndInteractionHandler(OgControl<TElement, TScope> instance, OgEvent reason);

    public bool IsInteracting { get; private set; }
    public event OgEndInteractionHandler? OnBeginInteraction;
    public event OgBeginInteractionHandler? OnEndInteraction;

    protected override void HandleMouseDown(OgEvent reason)
    {
        base.HandleMouseDown(reason);
        if(IsInteracting) return;
        if(!IsHovered) return;
        BeginInteract(reason);
        reason.Use();
    }

    protected override void HandleMouseUp(OgEvent reason)
    {
        base.HandleMouseUp(reason);
        if(!IsInteracting) return;
        EndInteract(reason);
        reason.Use();
    }

    protected virtual void BeginInteract(OgEvent reason)
    {
        IsInteracting = true;
        OnBeginInteraction?.Invoke(this, reason);
    }

    protected virtual void EndInteract(OgEvent reason)
    {
        IsInteracting = false;
        OnEndInteraction?.Invoke(this, reason);
    }
}