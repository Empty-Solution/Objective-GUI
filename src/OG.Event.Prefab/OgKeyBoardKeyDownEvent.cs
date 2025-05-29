using OG.Event.Prefab.Abstraction;
namespace OG.Event.Prefab;
public class OgKeyBoardKeyDownEvent(UnityEngine.Event source) : OgKeyBoardEvent(source), IOgKeyBoardKeyDownEvent;