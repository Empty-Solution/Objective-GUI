using OG.Element.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.View;

public abstract class OgScrollableView<TElement, TValue> : OgValueView<TElement, TValue> where TElement : IOgElement
{
    protected OgScrollableView(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgMouseScrollEventHandler(this));
    protected virtual bool HandleMouseScroll(IOgMouseScrollEvent reason) => !IsHovered || OnHoverMouseScroll(reason);
    protected abstract bool OnHoverMouseScroll(IOgMouseScrollEvent reason);

    public class OgMouseScrollEventHandler(OgScrollableView<TElement, TValue> owner) : OgRecallMouseEventHandler<IOgMouseScrollEvent>(owner)
    {
        public override bool Handle(IOgMouseScrollEvent reason)
        {
            base.Handle(reason);
            return owner.HandleMouseScroll(reason);
        }
    }
}