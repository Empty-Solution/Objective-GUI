using DK.Getting.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public abstract class OgHoverableElement<TElement> : OgContainer<TElement>, IOgHoverableElement<TElement>, IOgEventCallback<IOgMouseMoveEvent>
    where TElement : IOgElement
{
    protected OgHoverableElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : base(name, provider, rectGetter) =>
        provider.Register<IOgMouseMoveEvent>(this);
    public virtual bool Invoke(IOgMouseMoveEvent reason)
    {
        bool containsMouse = ElementRect.Get().Contains(reason.LocalPosition);
        if(IsHovering!.Get() == containsMouse) return false;
        IsHovering!.Set(containsMouse);
        return false;
    }
    public IDkObservableProperty<bool>? IsHovering { get; set; }
}