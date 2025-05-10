using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgKeyBoardKeyDownEventPipe : OgKeyBoardEventPipe<IOgKeyBoardKeyDownEvent>
{
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.KeyDown && value.character == char.MinValue;
    protected override IOgKeyBoardKeyDownEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgKeyBoardKeyDownEvent(sourceEvent);
}