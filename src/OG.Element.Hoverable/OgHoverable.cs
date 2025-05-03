using OG.Element.Abstraction;
using OG.Element.Container;
using OG.Element.Hoverable.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
namespace OG.Element.Hoverable;
public class OgHoverable<TElement> : OgContainer<TElement>, IOgHoverable<TElement>, IOgElementEventHandler<IOgMouseMoveEvent> where TElement : IOgElement
{
    public OgHoverable(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgEventHandler<IOgMouseMoveEvent>(this));
    bool IOgElementEventHandler<IOgMouseMoveEvent>.HandleEvent(IOgMouseMoveEvent reason) => !ProcElementsBackward(reason) && OnMouseMove(reason);
    public bool                                    IsHovered { get; private set; }
    public virtual bool OnMouseMove(IOgMouseMoveEvent reason)
    {
        bool containsMouse = Rectangle!.Get().Contains(reason.LocalMousePosition);
        if(IsHovered == containsMouse) return false;
        IsHovered = containsMouse;
        return true;
    }
}