using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgDeltaEvent : IOgEvent
{
    Vector2 Delta { get; }
}