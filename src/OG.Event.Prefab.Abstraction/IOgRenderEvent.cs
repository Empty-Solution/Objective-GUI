using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgRenderEvent : IOgEvent
{
    Vector2                  Global   { get; set; }
    void PushContext(IOgGraphicsContext ctx);
    void ProcessContexts();
}