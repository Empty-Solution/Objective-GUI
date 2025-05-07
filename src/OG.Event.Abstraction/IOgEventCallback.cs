using OG.Event.Prefab.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventCallback<in TEvent> where TEvent : IOgEvent
{
    bool Invoke(TEvent reason);
}