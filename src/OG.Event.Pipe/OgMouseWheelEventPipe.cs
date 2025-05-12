using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgMouseWheelEventPipe : OgMouseEventPipe<IOgMouseWheelEvent>
{
    public override    bool               CanHandle(UnityEngine.Event        value)       => value.type is EventType.ScrollWheel;
    protected override IOgMouseWheelEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgMouseWheelEvent(sourceEvent);
}