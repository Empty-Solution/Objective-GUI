using DK.Property.Observing.Abstraction.Generic;
using OG.DataTypes.ElementState;
using OG.Element.Abstraction;
using OG.Element.Container;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
namespace OG.Element.Interactive;
public abstract class OgHoverableElement<TElement> : OgContainer<TElement>, IOgHoverableElement<TElement>, IOgEventCallback<IOgMouseMoveEvent>
    where TElement : IOgElement
{
    protected OgHoverableElement(string name, IOgEventHandlerProvider provider) : base(name, provider) => provider.Register<IOgMouseMoveEvent>(this);
    public virtual bool Invoke(IOgMouseMoveEvent reason)
    {
        bool containsMouse = ElementRect.Contains(reason.LocalPosition);
        if(IsHovering!.Get() == containsMouse) return false;
        IsHovering!.Set(containsMouse);
        State!.Set(containsMouse ? EOgElementState.HOVERED : EOgElementState.NORMAL);
        return false;
    }
    public IDkObservableProperty<bool>?            IsHovering { get; set; }
    public IDkObservableProperty<EOgElementState>? State      { get; set; }
}