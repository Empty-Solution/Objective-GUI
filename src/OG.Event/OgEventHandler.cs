using OG.Event.Abstraction;
namespace OG.Event;
public class OgEventHandler<TEvent>(IOgElementEventHandler<TEvent> handler) : OgEventHandlerBase<TEvent> where TEvent : class, IOgEvent
{
    public override bool Handle(TEvent reason) => handler.HandleEvent(reason);
}