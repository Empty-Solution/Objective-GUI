using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
using UeEvent = UnityEngine.Event;
namespace OG.Event.Pipe;
public class OgRenderEventPipe(IOgGraphics graphics) : OgEventPipe<IOgRenderEvent>
{
    public override bool CanHandle(UeEvent value) => value.type is EventType.Repaint;
    protected override IOgRenderEvent InternalGetEvent(UeEvent sourceEvent) => new OgRenderEvent(graphics);
}