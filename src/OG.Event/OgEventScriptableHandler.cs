using OG.Event.Prefab.Abstraction;
namespace OG.Event;
public class OgEventScriptableHandler<TEvent>(OgEventScriptableHandler<TEvent>.EventHandler handler) : OgEventHandler<TEvent> where TEvent : IOgEvent
{
    public delegate bool EventHandler(TEvent reason);
    public override bool HandleEvent(TEvent  reason) => handler(reason);
}