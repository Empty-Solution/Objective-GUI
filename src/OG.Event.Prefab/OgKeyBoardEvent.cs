using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public abstract class OgKeyBoardEvent(UnityEngine.Event source) : OgInputEvent, IOgKeyBoardEvent
{
    protected UnityEngine.Event Source    => source;
    public    int               Key       => (int)source.keyCode;
    public    EventModifiers    Modifiers => source.modifiers;
}