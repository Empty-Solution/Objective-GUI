using OG.Graphics.Abstraction;
namespace OG.Event.Prefab.Abstraction;
public interface IOgRenderEvent : IOgEvent
{
    IOgGraphics Graphics { get; }
}