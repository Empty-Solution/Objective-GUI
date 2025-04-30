#region

using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;

#endregion

namespace OG.Element.View;

public abstract class OgScrollableView<TElement, TValue> : OgValueView<TElement, TValue> where TElement : IOgElement
{
    protected OgScrollableView(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgMouseScrollEventHandler(this));
    protected virtual bool HandleMouseScroll(IOgMouseScrollEvent reason) => !IsHovered || OnHoverMouseScroll(reason);
    protected abstract bool OnHoverMouseScroll(IOgMouseScrollEvent reason);

    private class OgMouseScrollEventHandler(OgScrollableView<TElement, TValue> owner) : OgEventHandlerBase<IOgMouseScrollEvent>
    {
        public override bool Handle(IOgMouseScrollEvent reason) => owner.HandleMouseScroll(reason);
    }
}