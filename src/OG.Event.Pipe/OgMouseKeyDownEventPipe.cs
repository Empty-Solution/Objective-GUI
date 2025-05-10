using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgMouseKeyDownEventPipe : OgMouseKeyEventPipe<IOgMouseKeyDownEvent>
{
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.MouseDown;
    protected override IOgMouseKeyDownEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgMouseKeyDownEvent(sourceEvent);
}