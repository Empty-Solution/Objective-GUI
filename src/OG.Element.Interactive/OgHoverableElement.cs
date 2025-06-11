using DK.Getting.Abstraction.Generic;
using DK.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgHoverableElement<TElement> : OgContainer<TElement>, IOgHoverableElement<TElement>, IOgEventCallback<IOgMouseMoveEvent>
    where TElement : IOgElement
{
    public OgHoverableElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : base(name, provider, rectGetter) =>
        provider.Register<IOgMouseMoveEvent>(this);
    public bool Invoke(IOgMouseMoveEvent reason) => HandleMouseMove(reason);
    public bool                 IsHovering         { get; private set; }
    public IDkObservable<bool>? IsHoveringObserver { get; set; }
    protected virtual bool HandleMouseMove(IOgMouseMoveEvent reason) => base.Invoke(reason) || HandleHover(reason);
    protected bool HandleHover(IOgMouseMoveEvent reason)
    {
        bool containsMouse = ElementRect.Get().Contains(reason.LocalMousePosition);
        if(IsHovering == containsMouse) return false;
        IsHovering = containsMouse;
        IsHoveringObserver?.Notify(IsHovering);
        return containsMouse;
    }
}