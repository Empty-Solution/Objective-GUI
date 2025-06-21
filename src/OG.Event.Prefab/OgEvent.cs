using OG.Event.Prefab.Abstraction;
namespace OG.Event.Prefab;
public abstract class OgEvent : IOgEvent
{
    public float Time => UnityEngine.Time.time;
    public float DeltaTime => UnityEngine.Time.deltaTime;
}