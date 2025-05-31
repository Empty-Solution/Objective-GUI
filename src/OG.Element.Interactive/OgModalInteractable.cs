using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgModalInteractable<TElement> : OgHoverableElement<TElement>, IOgEventCallback<IOgMouseKeyDownEvent>, IOgEventCallback<IOgMouseKeyUpEvent>,
                                             IOgModalInteractable<TElement> where TElement : IOgElement
{
    private readonly bool m_RightClickOnly;
    public OgModalInteractable(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, bool rightClickOnly) : base(name, provider,
        rectGetter)
    {
        m_RightClickOnly = rightClickOnly;
        provider.Register<IOgMouseKeyDownEvent>(this);
        provider.Register<IOgMouseKeyUpEvent>(this);
    }
    public bool Invoke(IOgMouseKeyDownEvent reason)
    {
        if(!ShouldProcess && m_RightClickOnly && (!IsHovering || reason.Key != 1)) return false;
        if(!ShouldProcess && !IsHovering) return false;
        base.Invoke(reason);
        return true;
    }
    public bool Invoke(IOgMouseKeyUpEvent reason)
    {
        if(ShouldProcess && base.Invoke(reason)) return true;
        if(!ShouldProcess && m_RightClickOnly && (!IsHovering || reason.Key != 1)) return false;
        bool oldShouldProcess = ShouldProcess;
        ShouldProcess = IsHovering && !oldShouldProcess;
        if(ShouldProcess) base.Invoke(reason);
        return oldShouldProcess || IsHovering;
    }
    public bool ShouldProcess
    {
        get;
        set
        {
            field = value;
            IsInteractingObserver?.Notify(value);
            if(m_RightClickOnly) IsRightInteractingObserver?.Notify(value);
            Resort();
            Parent?.Resort();
        }
    }
    public override bool Invoke(IOgRenderEvent reason) => ShouldProcess && base.Invoke(reason);
    public override int CompareTo(IOgElement other) => other.Order.CompareTo(Order + (ShouldProcess ? 2 : 1));
    public IDkObservable<bool>? IsInteractingObserver      { get; set; }
    public IDkObservable<bool>? IsRightInteractingObserver { get; set; }
    protected override bool HandleMouseMove(IOgMouseMoveEvent reason)
    {
        if(!ShouldProcess) return base.HandleMouseMove(reason);
        base.HandleMouseMove(reason);
        return true;
    }
    public override bool Invoke(IOgInputEvent reason) => (ShouldProcess || reason is IOgKeyBoardEvent) && base.Invoke(reason);
}