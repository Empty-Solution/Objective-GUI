using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Event.Prefab.Abstraction;
public interface IOgRenderEvent : IOgEvent
{
    IEnumerable<IOgGraphics> Graphics { get; }
    Vector2                  Global   { get; set; }
    IOgGraphics GetGraphics(IOgGraphicsContext context);
}