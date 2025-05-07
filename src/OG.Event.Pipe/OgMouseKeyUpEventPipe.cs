using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgMouseKeyUpEventPipe : OgMouseKeyEventPipe<IOgMouseKeyUpEvent>
{
    public override    bool               CanHandle(UnityEngine.Event        value)       => value.type is EventType.MouseUp;
    protected override IOgMouseKeyUpEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgMouseKeyUpEvent(sourceEvent);
}