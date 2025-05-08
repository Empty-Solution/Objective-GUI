using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UeEvent = UnityEngine.Event;
namespace OG.Event.Pipe;
public abstract class OgEventPipe<TEvent> : IOgEventPipe<UeEvent> where TEvent : class, IOgEvent
{
    public abstract    bool     CanHandle(UeEvent        value);
    public             IOgEvent GetEvent(UeEvent         sourceEvent) => InternalGetEvent(sourceEvent);
    protected abstract TEvent   InternalGetEvent(UeEvent sourceEvent);
}