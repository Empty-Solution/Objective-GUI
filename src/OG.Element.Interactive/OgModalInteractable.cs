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
        bool shouldProcess = ShouldProcess;
        bool isHovering    = IsHovering;
        if(shouldProcess && base.Invoke(reason)) return true;
        if(m_RightClickOnly && !shouldProcess && (!isHovering || reason.Key != 1)) return false;
        bool newShouldProcess = isHovering && !shouldProcess;
        if(newShouldProcess) base.Invoke(reason);
        ShouldProcess = newShouldProcess;
        return shouldProcess || isHovering;
    }
    public bool ShouldProcess
    {
        get;
        set
        {
            field = value;
            IsInteractingObserver?.Notify(value);
            Resort();
            Parent?.Resort();
        }
    }
    public override long Order
    {
        get => ShouldProcess ? int.MinValue : base.Order;
        set => base.Order = value;
    }
    public override bool Invoke(IOgRenderEvent reason) => ShouldProcess && base.Invoke(reason);
    public IDkObservable<bool>? IsInteractingObserver { get; set; }
    protected override bool HandleMouseMove(IOgMouseMoveEvent reason)
    {
        if(!ShouldProcess) return base.HandleMouseMove(reason);
        base.HandleMouseMove(reason);
        return true;
    }
    public override bool Invoke(IOgInputEvent reason) => (ShouldProcess || reason is IOgKeyBoardEvent) && base.Invoke(reason);
}