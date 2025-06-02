using OG.Event.Prefab.Abstraction;
namespace OG.Event.Pipe;
public abstract class OgInputEventPipe<TEvent> : OgEventPipe<TEvent> where TEvent : class, IOgInputEvent;