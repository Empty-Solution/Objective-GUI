using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgModalInteractable<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, bool rightClickOnly)
    : OgInteractableElement<TElement>(name, provider, rectGetter), IOgModalInteractable<TElement>, IOgEventCallback<IOgPostRenderEvent> where TElement : IOgElement 
{
    protected bool                 ShouldRender          { get; set; }
    public    IDkObservable<bool>? IsRenderModalObserver { get; set; }
    protected override bool PreEndControl(IOgMouseKeyUpEvent reason)
    {
        base.PreEndControl(reason);
        if(!rightClickOnly && IsHovering && reason.Key == 1) return false;
        ShouldRender = IsHovering;
        IsRenderModalObserver?.Notify(ShouldRender);
        return false;
    }
    public bool Invoke(IOgPostRenderEvent reason)
    {
        if(!ShouldRender) return false;
        foreach(IOgGraphics graphics in reason.Graphics)
            graphics.ProcessContexts();
        return true;
    }
    public override bool Invoke(IOgRenderEvent reason)
    {
        if(!ShouldRender) return false;
        Rect rect = ElementRect.Get();
        reason.Global += rect.position;
        ProcessElementsEventForward(reason);
        reason.Global -= rect.position;
        return false;
    }
}