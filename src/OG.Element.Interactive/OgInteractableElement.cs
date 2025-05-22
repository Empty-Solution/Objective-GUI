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
        if(IsHovering && PreBeginControl(reason)) return true;
        return base.Invoke(reason);
    }
    public bool Invoke(IOgMouseKeyUpEvent reason) => PreEndControl(reason) || base.Invoke(reason);
    public IDkObservable<bool>? IsInteractingObserver      { get; set; }
    public IDkObservable<bool>? IsRightInteractingObserver { get; set; }
    protected virtual bool PreBeginControl(IOgMouseKeyDownEvent reason) => !IsInteracting && BeginControl(reason); 
    protected virtual bool PreEndControl(IOgMouseKeyUpEvent reason) => IsInteracting && EndControl(reason);
    protected virtual bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        IsInteracting = true;
        if(reason.Key == 1) IsRightInteractingObserver?.Notify(true);
        IsInteractingObserver?.Notify(true);
        return false;
    }
    protected virtual bool EndControl(IOgMouseKeyUpEvent reason)
    {
        IsInteracting = false;
        if(reason.Key == 1) IsRightInteractingObserver?.Notify(false);
        IsInteractingObserver?.Notify(false);
        return false;
    }
}