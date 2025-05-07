using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgDeltaEvent
{
    Vector2 Delta { get; }
}