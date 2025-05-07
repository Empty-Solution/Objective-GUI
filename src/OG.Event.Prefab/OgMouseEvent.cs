using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public abstract class OgMouseEvent(UnityEngine.Event source) : OgInputEvent, IOgMouseEvent
{
    protected UnityEngine.Event Source   => source;
    public    Vector2           Position => source.mousePosition;
}