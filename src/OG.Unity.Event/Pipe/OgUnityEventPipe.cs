using OG.Event.Abstraction;
namespace OG.Unity.Event.Pipe;
public abstract class OgUnityEventPipe<TEvent> : IOgEventPipe<UnityEngine.Event> where TEvent : class, IOgEvent
{
    private           TEvent?  m_Event;
    public abstract   bool     CanHandle(UnityEngine.Event          value);
    public            IOgEvent GetEventFromSource(UnityEngine.Event source) => InternalGetEventFromSource(source);
    protected virtual TEvent   InternalGetEventFromSource(UnityEngine.Event source) => m_Event ??= Create();
    protected abstract TEvent   Create();
}