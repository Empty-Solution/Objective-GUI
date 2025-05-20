using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgInteractableElement<TElement> : OgHoverableElement<TElement>, IOgInteractableElement<TElement>, IOgEventCallback<IOgMouseKeyDownEvent>,
                                               IOgEventCallback<IOgMouseKeyUpEvent> where TElement : IOgElement
{
    public OgInteractableElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : base(name, provider, rectGetter)
    {
        provider.Register<IOgMouseKeyDownEvent>(this);
        provider.Register<IOgMouseKeyUpEvent>(this);
    }
    protected bool IsInteracting { get; set; }
    public bool Invoke(IOgMouseKeyDownEvent reason)
    {
        if(!IsInteracting && IsHovering && BeginControl(reason)) return true;
        return base.Invoke(reason);
    }
    public bool Invoke(IOgMouseKeyUpEvent reason)
    {
        if(IsInteracting && EndControl(reason)) return true;
        return base.Invoke(reason);
    }
    public IDkObservable<bool>? IsInteractingObserver { get; set; }
    protected virtual bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        IsInteracting = true;
        IsInteractingObserver?.Notify(true);
        return false;
    }
    protected virtual bool EndControl(IOgMouseKeyUpEvent reason)
    {
        IsInteracting = false;
        IsInteractingObserver?.Notify(false);
        return false;
    }
}