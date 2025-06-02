using OG.Event.Prefab.Abstraction;
namespace OG.Event.Prefab;
public class OgKeyBoardCharacterKeyUpEvent(UnityEngine.Event source) : OgKeyBoardKeyUpEvent(source), IOgKeyBoardCharacterKeyUpEvent
{
    public char Character => Source.character;
}