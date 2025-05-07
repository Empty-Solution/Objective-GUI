using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using System;
namespace OG.Event;
public abstract class OgEventHandler<TEvent> : IOgEventHandler<TEvent> where TEvent : IOgEvent
{
    public bool CanHandle(Type value) => value.IsAssignableFrom(typeof(TEvent));
    public abstract bool HandleEvent(TEvent reason);
    bool IOgEventHandler.HandleEvent(IOgEvent reason) => reason is TEvent castedReason && HandleEvent(castedReason);
}