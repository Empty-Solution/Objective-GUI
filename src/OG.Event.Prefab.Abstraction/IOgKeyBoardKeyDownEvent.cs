using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgKeyBoardKeyDownEvent : IOgKeyBoardEvent
{
    KeyCode KeyCode { get; }
}