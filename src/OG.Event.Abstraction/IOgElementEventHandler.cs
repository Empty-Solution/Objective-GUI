namespace OG.Event.Abstraction;
// TODO: ChangeName
public interface IOgElementEventHandler<in TEvent> where TEvent : IOgEvent
{
    bool HandleEvent(TEvent reason);
}