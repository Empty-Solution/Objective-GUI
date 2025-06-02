using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgKeyBoardEvent : IOgInputEvent, IOgKeyEvent
{
    EventModifiers Modifiers { get; }
    KeyCode        KeyCode   { get; }
}