
using OG.Event.Abstraction;

namespace OG.Event;

public abstract class OgEventHandlerBase<TEvent> : IOgEventHandler<TEvent> where TEvent : class
{
    public bool CanHandle(IOgEvent value) => value is TEvent;

    public abstract bool Handle(TEvent reason);

    public bool Handle(IOgEvent reason) => Handle((reason as TEvent)!);
}