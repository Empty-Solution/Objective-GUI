using OG.Element.Abstraction;
using OG.Element.Control.Abstraction;
using OG.Element.Hoverable;
using OG.Event.Abstraction;
namespace OG.Element.Control;
public abstract class OgControl<TElement> : OgHoverable<TElement>, IOgControl<TElement> where TElement : IOgElement
{
    protected OgControl(IOgEventProvider eventProvider) : base(eventProvider)
    {
        eventProvider.RegisterHandler(new OgMouseDownEventHandler(this));
        eventProvider.RegisterHandler(new OgMouseUpEventHandler(this));
    }
    public bool IsControlling { get; private set; }
    public virtual bool HandleMouseDown(IOgMouseKeyDownEvent reason) => IsControlling || !IsHovered || BeginControl(reason);
    public virtual bool HandleMouseUp(IOgMouseKeyUpEvent reason) => !IsControlling || !IsHovered || EndControl(reason);
    protected virtual bool BeginControl(IOgMouseKeyDownEvent reason) => IsControlling = true;
    protected virtual bool EndControl(IOgMouseKeyUpEvent reason)
    {
        IsControlling = false;
        return true;
    }
    public class OgMouseDownEventHandler(IOgControl<TElement> owner) : OgRecallMouseEventHandler<IOgMouseKeyDownEvent>(owner)
    {
        public override bool Handle(IOgMouseKeyDownEvent reason)
        {
            base.Handle(reason);
            return owner.HandleMouseDown(reason);
        }
    }
    public class OgMouseUpEventHandler(IOgControl<TElement> owner) : OgRecallMouseEventHandler<IOgMouseKeyUpEvent>(owner)
    {
        public override bool Handle(IOgMouseKeyUpEvent reason)
        {
            base.Handle(reason);
            return owner.HandleMouseUp(reason);
        }
    }
}