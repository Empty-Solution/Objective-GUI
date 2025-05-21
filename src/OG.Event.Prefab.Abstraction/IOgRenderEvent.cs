using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgRenderEvent : IOgEvent
{
    IEnumerable<IOgGraphics> Graphics { get; }
    void Enter(Rect rect);
    void Exit();
    IOgGraphics GetGraphics(IOgGraphicsContext context);
}