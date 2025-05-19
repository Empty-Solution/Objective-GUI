using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgKeyBoardKeyDownEvent(UnityEngine.Event source) : OgKeyBoardEvent(source), IOgKeyBoardKeyDownEvent
{
    public KeyCode KeyCode => Source.keyCode;
}