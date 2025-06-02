using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Event;
public abstract class OgEventHandler<TEvent> : IOgEventHandler<TEvent> where TEvent : IOgEvent
{
    public bool CanHandle(IOgEvent value) => value is TEvent;
    public abstract bool HandleEvent(TEvent reason);
    bool IOgEventHandler.HandleEvent(IOgEvent reason) => reason is TEvent castedReason && HandleEvent(castedReason);
}