using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgMouseEvent : IOgInputEvent
{
    Vector2 Position { get; }
}