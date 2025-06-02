using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgCharacterKeyBoardKeyUpEventPipe : OgKeyBoardEventPipe<IOgKeyBoardCharacterKeyUpEvent>
{
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.KeyUp && value.character != char.MinValue;
    protected override IOgKeyBoardCharacterKeyUpEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgKeyBoardCharacterKeyUpEvent(sourceEvent);
}