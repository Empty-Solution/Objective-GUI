using OG.Element.Abstraction;
using OG.Element.Control.Abstraction;
using OG.Element.Hoverable;
using OG.Event;
using OG.Event.Abstraction;
namespace OG.Element.Control;
public abstract class OgControl<TElement> : OgHoverable<TElement>, IOgControl<TElement>, IOgElementEventHandler<IOgMouseKeyUpEvent>,
                                            IOgElementEventHandler<IOgMouseKeyDownEvent> where TElement : IOgElement
{
    protected OgControl(IOgEventProvider eventProvider) : base(eventProvider)
    {
        eventProvider.RegisterHandler(new OgEventHandler<IOgMouseKeyUpEvent>(this));
        eventProvider.RegisterHandler(new OgEventHandler<IOgMouseKeyDownEvent>(this));
    }
    public bool                                       IsControlling                             { get; private set; }
    bool IOgElementEventHandler<IOgMouseKeyDownEvent>.HandleEvent(IOgMouseKeyDownEvent  reason) => !ProcElementsBackward(reason) && OnMouseDown(reason);
    bool IOgElementEventHandler<IOgMouseKeyUpEvent>.  HandleEvent(IOgMouseKeyUpEvent    reason) => !ProcElementsBackward(reason) && OnMouseUp(reason);
    public virtual    bool                            OnMouseDown(IOgMouseKeyDownEvent  reason) => IsControlling || !IsHovered || BeginControl(reason);
    public virtual    bool                            OnMouseUp(IOgMouseKeyUpEvent      reason) => !IsControlling || !IsHovered || EndControl(reason);
    protected virtual bool                            BeginControl(IOgMouseKeyDownEvent reason) => IsControlling = true;
    protected virtual bool EndControl(IOgMouseKeyUpEvent reason)
    {
        IsControlling = false;
        return true;
    }
}