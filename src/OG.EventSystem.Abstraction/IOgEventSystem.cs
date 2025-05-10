using OG.Event.Abstraction;
namespace OG.EventSystem.Abstraction;
public interface IOgEventSystem<TEvent> where TEvent : class
{
    void Register(IOgEventPipe<TEvent> pipe);
    void Unregister(IOgEventPipe<TEvent> pipe);
    void Handle();
}