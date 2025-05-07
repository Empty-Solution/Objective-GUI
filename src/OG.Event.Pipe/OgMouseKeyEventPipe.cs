using OG.Event.Prefab.Abstraction;
namespace OG.Event.Pipe;
public abstract class OgMouseKeyEventPipe<TEvent> : OgMouseEventPipe<TEvent> where TEvent : class, IOgMouseKeyEvent;