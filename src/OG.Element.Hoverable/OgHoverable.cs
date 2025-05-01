using OG.Element.Abstraction;
using OG.Element.Container;
using OG.Element.Hoverable.Abstraction;
using OG.Event.Abstraction;
namespace OG.Element.Hoverable;
public class OgHoverable<TElement> : OgContainer<TElement>, IOgHoverable<TElement> where TElement : IOgElement
{
    public OgHoverable(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgMouseMoveEventHandler(this));
    public bool IsHovered { get; private set; }
    public virtual bool HandleMouseMove(IOgMouseMoveEvent reason)
    {
        bool containsMouse = Rectangle!.Get().Contains(reason.LocalMousePosition);
        if(IsHovered == containsMouse) return false;
        IsHovered = containsMouse;
        return true;
    }
    public class OgMouseMoveEventHandler(IOgHoverable<TElement> owner) : OgRecallMouseEventHandler<IOgMouseMoveEvent>(owner)
    {
        public override bool Handle(IOgMouseMoveEvent reason)
        {
            base.Handle(reason);
            return owner.HandleMouseMove(reason);
        }
    }
}