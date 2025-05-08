using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgKeyBoardCharacterKeyDownEventPipe : OgKeyBoardEventPipe<IOgKeyBoardCharacterKeyDownEvent>
{
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.KeyDown && (value.character != char.MinValue);
    protected override IOgKeyBoardCharacterKeyDownEvent InternalGetEvent(UnityEngine.Event sourceEvent) =>
        new OgKeyBoardCharacterKeyDownEvent(sourceEvent);
}