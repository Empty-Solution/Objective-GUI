using OG.Element.Abstraction;
using OG.Element.Container;
using OG.Element.Interactable.Abstraction;
using OG.Event;
using OG.Event.Abstraction;

namespace OG.Element.Interactable;

public class OgHoverable<TElement> : OgContainer<TElement>, IOgHoverable<TElement> where TElement : IOgElement
{
    public OgHoverable(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgMouseMoveEventHandler(this));
    public bool IsHovered { get; private set; }

    protected virtual bool HandleMouseMove(IOgMouseMoveEvent reason)
    {
        bool containsMouse = Rectangle!.Get().Contains(reason.LocalMousePosition);

        if(IsHovered == containsMouse)
            return true;

        IsHovered = containsMouse;
        return true;
    }

    private class OgMouseMoveEventHandler(OgHoverable<TElement> owner) : OgEventHandlerBase<IOgMouseMoveEvent>
    {
        public override bool Handle(IOgMouseMoveEvent reason) => owner.HandleMouseMove(reason);
    }
}