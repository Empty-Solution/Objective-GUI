using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
namespace OG.Event.Prefab;
public class OgRenderEvent(IOgGraphics graphics) : OgEvent, IOgRenderEvent
{
    public IOgGraphics Graphics => graphics;
}