using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public abstract class OgEvent : IOgEvent
{
    public float Time             => UnityEngine.Time.time;
    public float DeltaTime        => UnityEngine.Time.deltaTime;
    public void  Enter(Rect rect) => GUI.BeginClip(rect);
    public void  Exit()           => GUI.EndClip();
}