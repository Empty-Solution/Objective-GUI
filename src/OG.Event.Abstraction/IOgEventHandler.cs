using DK.Matching.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventHandler<in TEvent> : IOgEventHandler where TEvent : class, IOgEvent
{
    bool Handle(TEvent reason);
}
public interface IOgEventHandler : IDkMatcher<IOgEvent>
{
    bool Handle(IOgEvent reason);
}