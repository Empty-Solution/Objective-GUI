using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgEvent
{
    float Time      { get; }
    float DeltaTime { get; }
    void  Enter(Rect rect);
    void  Exit();
}