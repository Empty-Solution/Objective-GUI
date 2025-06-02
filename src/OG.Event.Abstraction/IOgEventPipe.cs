using DK.Matching.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventPipe<TSourceEvent> : IDkMatcher<TSourceEvent> where TSourceEvent : class
{
    IOgEvent GetEvent(TSourceEvent sourceEvent);
}