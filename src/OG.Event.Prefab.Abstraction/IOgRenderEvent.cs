using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgRenderEvent : IOgEvent
{
    IOgGraphics Graphics { get; }
    void        Enter(Rect rect);
    void        Exit();
}