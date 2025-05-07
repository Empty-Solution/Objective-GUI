using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgMouseMoveEvent(UnityEngine.Event source, Vector2 mouseMoveDelta) : OgMouseEvent(source), IOgMouseMoveEvent
{
    public Vector2 Delta => mouseMoveDelta;
}