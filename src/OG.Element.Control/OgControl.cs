using OG.Element.Abstraction;
using OG.Element.Control.Abstraction;
using OG.Element.Interactable;
using OG.Event;
using OG.Event.Abstraction;

namespace OG.Element.Control;

public class OgControl<TElement> : OgHoverable<TElement>, IOgControl<TElement> where TElement : IOgElement
{
    public OgControl(IOgEventProvider eventProvider) : base(eventProvider)
    {
        eventProvider.RegisterHandler(new OgMouseDownEventHandler(this));
        eventProvider.RegisterHandler(new OgMouseUpEventHandler(this));
    }

    public bool IsControlling { get; private set; }

    protected virtual bool HandleMouseDown(IOgMouseKeyDownEvent reason)
    {
        if(IsControlling || !IsHovered) return true;
        return BeginControl(reason);
    }

    protected virtual bool HandleMouseUp(IOgMouseKeyUpEvent reason) =>
        !IsControlling || EndControl(reason);

    protected virtual bool BeginControl(IOgMouseKeyDownEvent reason) =>
        IsControlling = true;

    protected virtual bool EndControl(IOgMouseKeyUpEvent reason)
    {
        IsControlling = false;
        return true;
    }

    private class OgMouseDownEventHandler(OgControl<TElement> owner) : OgEventHandlerBase<IOgMouseKeyDownEvent>
    {
        public override bool Handle(IOgMouseKeyDownEvent reason) => owner.HandleMouseDown(reason);
    }

    private class OgMouseUpEventHandler(OgControl<TElement> owner) : OgEventHandlerBase<IOgMouseKeyUpEvent>
    {
        public override bool Handle(IOgMouseKeyUpEvent reason) => owner.HandleMouseUp(reason);
    }
}