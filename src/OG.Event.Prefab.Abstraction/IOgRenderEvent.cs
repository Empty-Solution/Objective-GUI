using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgRenderEvent : IOgEvent
{
    Vector2 Global { get; set; }
    void Enter(Rect rect);
    void Exit();
    void PushContext(IOgGraphicsContext ctx);
    void ProcessContexts();
}