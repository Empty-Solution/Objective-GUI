namespace OG.Event.Abstraction;
public interface IOgEventPipe<in TSource>
{
    IOgEvent GetEventFromSource(TSource source);
}