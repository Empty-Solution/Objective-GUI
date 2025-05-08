using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgRenderEvent(IOgGraphics graphics) : OgEvent, IOgRenderEvent
{
    public IOgGraphics Graphics         => graphics;
    public void        Enter(Rect rect) => GUI.BeginClip(rect);
    public void        Exit()           => GUI.EndClip();
}