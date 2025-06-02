using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public abstract class OgKeyBoardEvent(UnityEngine.Event source) : OgInputEvent(source), IOgKeyBoardEvent
{
    public int            Key       => (int)Source.keyCode;
    public EventModifiers Modifiers => Source.modifiers;
    public KeyCode        KeyCode   => Source.keyCode;
}