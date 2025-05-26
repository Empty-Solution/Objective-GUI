using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgModalInteractable<TElement> : OgHoverableElement<TElement>, IOgModalInteractable<TElement>,
                                             IOgEventCallback<IOgMouseKeyUpEvent>, IOgEventCallback<IOgMouseKeyDownEvent> where TElement : IOgElement
{
    private readonly bool m_RightClickOnly;
    public OgModalInteractable(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, bool rightClickOnly) : base(name, provider,
        rectGetter)
    {
        m_RightClickOnly = rightClickOnly;
        provider.Register<IOgMouseKeyUpEvent>(this);
        provider.Register<IOgMouseKeyDownEvent>(this);
    }
    protected bool ShouldProcess { get; set; }
    public bool Invoke(IOgMouseKeyDownEvent reason)
    {
        if(!ShouldProcess && !IsHovering) return false;
        base.Invoke(reason);
        return true;
    }
    public bool Invoke(IOgMouseKeyUpEvent reason)
    {
        if(ShouldProcess && base.Invoke(reason)) return true;
        if(m_RightClickOnly && !IsHovering && reason.Key == 1) return false;
        bool oldShouldProcess = ShouldProcess;
        ShouldProcess = IsHovering && !oldShouldProcess;
        IsModalInteractObserver?.Notify(ShouldProcess);
        if(m_RightClickOnly) IsRightInteractingObserver?.Notify(ShouldProcess);
        return oldShouldProcess || IsHovering;
    }
    public IDkObservable<bool>? IsInteractingObserver      { get; set; }
    public IDkObservable<bool>? IsRightInteractingObserver { get; set; }
    public IDkObservable<bool>? IsModalInteractObserver    { get; set; }
    public override bool Invoke(IOgRenderEvent reason) => ShouldProcess && base.Invoke(reason);
    public override bool Invoke(IOgInputEvent reason) => ShouldProcess && base.Invoke(reason);
}