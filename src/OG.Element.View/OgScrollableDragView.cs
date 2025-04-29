using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;

namespace OG.Element.View;

public abstract class OgScrollableDragView<TElement, TValue> : OgDraggableValueView<TElement, TValue> where TElement : IOgElement
{
    protected OgScrollableDragView(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgMouseScrollEventHandler(this));
    protected virtual bool HandleMouseScroll(IOgMouseScrollEvent reason) => !IsHovered || OnHoverMouseScroll(reason);
    protected abstract bool OnHoverMouseScroll(IOgMouseScrollEvent reason);

    private class OgMouseScrollEventHandler(OgScrollableDragView<TElement, TValue> owner) : OgEventHandlerBase<IOgMouseScrollEvent>
    {
        public override bool Handle(IOgMouseScrollEvent reason) => owner.HandleMouseScroll(reason);
    }
}