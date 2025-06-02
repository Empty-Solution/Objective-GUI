using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgMouseWheelEvent(UnityEngine.Event source) : OgMouseEvent(source), IOgMouseWheelEvent
{
    public Vector2 Delta => Source.delta;
}