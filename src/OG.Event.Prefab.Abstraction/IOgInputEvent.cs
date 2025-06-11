using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgInputEvent : IOgEvent
{
    Vector2 LocalMousePosition  { get; set; }
    Vector2 GlobalMousePosition { get; }
}