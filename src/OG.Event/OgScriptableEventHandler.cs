using OG.Event.Abstraction;
namespace OG.Event;
public class OgScriptableEventHandler<TEvent>(OgScriptableEventHandler<TEvent>.OgHandler handler)
    : OgEventHandlerBase<TEvent> where TEvent : class, IOgEvent
{
    public delegate bool OgHandler(TEvent reason);
    public override bool Handle(TEvent    reason) => handler(reason);
}