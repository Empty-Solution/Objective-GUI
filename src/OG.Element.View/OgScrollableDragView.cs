using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
namespace OG.Element.View;
public abstract class OgScrollableDragView<TElement, TValue> : OgDraggableValueView<TElement, TValue>, IOgElementEventHandler<IOgMouseScrollEvent>
    where TElement : IOgElement
{
    protected OgScrollableDragView(IOgEventProvider eventProvider) : base(eventProvider) =>
        eventProvider.RegisterHandler(new OgEventHandler<IOgMouseScrollEvent>(this));
    public virtual     bool OnMouseScroll(IOgMouseScrollEvent      reason) => !IsHovered || OnHoverMouseScroll(reason);
    protected abstract bool OnHoverMouseScroll(IOgMouseScrollEvent reason);
    bool IOgElementEventHandler<IOgMouseScrollEvent>.HandleEvent(IOgMouseScrollEvent reason) => !ProcElementsBackward(reason) && OnMouseScroll(reason);
}