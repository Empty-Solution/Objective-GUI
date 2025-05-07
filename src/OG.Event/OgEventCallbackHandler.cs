using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Event;
public class OgEventCallbackHandler<TEvent>(IOgEventCallback<TEvent> callback) : OgEventHandler<TEvent> where TEvent : IOgEvent
{
    public override bool HandleEvent(TEvent reason) => callback.Invoke(reason);
}