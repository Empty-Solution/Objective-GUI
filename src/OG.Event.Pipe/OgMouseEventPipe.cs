using OG.Event.Prefab.Abstraction;
namespace OG.Event.Pipe;
public abstract class OgMouseEventPipe<TEvent> : OgInputEventPipe<TEvent> where TEvent : class, IOgMouseEvent;