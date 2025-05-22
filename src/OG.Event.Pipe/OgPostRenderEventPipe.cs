using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgPostRenderEventPipe(IEnumerable<IOgGraphics> graphics) : OgEventPipe<IOgPostRenderEvent>
{
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.Repaint;
    protected override IOgPostRenderEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgPostRenderEvent(graphics);
}