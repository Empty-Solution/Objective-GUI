using DK.Matching.Abstraction;
namespace OG.Event.Abstraction;
public interface IOgEventPipe<TSource> : IDkMatcher<TSource>
{
    IOgEvent GetEventFromSource(TSource source);
}