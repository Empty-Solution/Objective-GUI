using OG.Event.Abstraction;
namespace OG.Unity.Event;
public abstract class OgUnityFillEventPipe<TEvent> : OgUnityEventPipe<TEvent> where TEvent : class, IOgEvent
{
    protected override TEvent InternalGetEventFromSource(UnityEngine.Event source) => FillBySource(base.InternalGetEventFromSource(source), source);
    protected abstract TEvent FillBySource(TEvent                          target, UnityEngine.Event source);
}