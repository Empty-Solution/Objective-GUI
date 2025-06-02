using OG.Event.Prefab.Abstraction;
namespace OG.Event.Prefab;
public class OgKeyBoardCharacterKeyDownEvent(UnityEngine.Event source) : OgKeyBoardKeyDownEvent(source), IOgKeyBoardCharacterKeyDownEvent
{
    public char Character => Source.character;
}