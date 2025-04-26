namespace OG.Event;

public class OgEventHandler<TEvent>(OgEventHandler<TEvent>.OgHandler handler) : OgEventHandlerBase<TEvent> where TEvent : class
{
    public delegate bool OgHandler(TEvent reason);

    public override bool Handle(TEvent reason) => handler(reason);
}