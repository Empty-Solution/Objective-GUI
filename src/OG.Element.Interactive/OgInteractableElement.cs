using DK.Getting.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
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
    protected OgInteractableElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : base(name, provider, rectGetter)
    {
        provider.Register<IOgMouseKeyDownEvent>(this);
        provider.Register<IOgMouseMoveEvent>(this);
    }
    public bool Invoke(IOgMouseKeyDownEvent reason) => !IsInteracting!.Get() && IsHovering!.Get() && BeginControl(reason);
    public bool Invoke(IOgMouseKeyUpEvent reason) => IsInteracting!.Get() && EndControl(reason);
    public IDkObservableProperty<bool>? IsInteracting { get; set; }
    protected virtual bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        IsInteracting!.Set(true);
        return false;
    }
    protected virtual bool EndControl(IOgMouseKeyUpEvent reason)
    {
        IsInteracting!.Set(false);
        return false;
    }
}