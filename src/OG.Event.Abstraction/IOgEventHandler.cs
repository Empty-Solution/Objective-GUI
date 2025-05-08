using DK.Matching.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventHandler<in TEvent> : IOgEventHandler where TEvent : IOgEvent
{
    bool HandleEvent(TEvent reason);
}
public interface IOgEventHandler : IDkMatcher<IOgEvent>
{
    bool HandleEvent(IOgEvent reason);
}