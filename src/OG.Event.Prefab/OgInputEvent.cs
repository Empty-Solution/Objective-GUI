using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public abstract class OgInputEvent(UnityEngine.Event source) : OgEvent, IOgInputEvent
{
    protected UnityEngine.Event Source => source;
    public Vector2 LocalMousePosition { get; set; } = source.mousePosition;
    public Vector2 GlobalMousePosition { get; } = source.mousePosition;
}