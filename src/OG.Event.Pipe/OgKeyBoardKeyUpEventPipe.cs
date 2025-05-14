using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgKeyBoardKeyUpEventPipe : OgKeyBoardEventPipe<IOgKeyBoardKeyUpEvent>
{
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.KeyUp && value.character == char.MinValue;
    protected override IOgKeyBoardKeyUpEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgKeyBoardKeyUpEvent(sourceEvent);
}