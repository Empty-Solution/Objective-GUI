using OG.Event.Prefab.Abstraction;
namespace OG.Event.Pipe;
public abstract class OgKeyBoardEventPipe<TEvent> : OgEventPipe<TEvent> where TEvent : class, IOgKeyBoardEvent;