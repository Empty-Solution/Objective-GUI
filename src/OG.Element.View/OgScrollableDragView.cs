using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
namespace OG.Element.View;
public abstract class OgScrollableDragView<TElement, TValue> : OgDraggableValueView<TElement, TValue>, IOgMouseScrollEventHandler
    where TElement : IOgElement
{
    protected OgScrollableDragView(IOgEventProvider eventProvider) : base(eventProvider) =>
        eventProvider.RegisterHandler(new OgMouseScrollEventHandler(this));
    public virtual bool HandleMouseScroll(IOgMouseScrollEvent reason) => !IsHovered || OnHoverMouseScroll(reason);
    protected abstract bool OnHoverMouseScroll(IOgMouseScrollEvent reason);
    public class OgMouseScrollEventHandler(OgScrollableDragView<TElement, TValue> owner) : OgRecallMouseEventHandler<IOgMouseScrollEvent>(owner)
    {
        public override bool Handle(IOgMouseScrollEvent reason)
        {
            base.Handle(reason);
            return owner.HandleMouseScroll(reason);
        }
    }
}