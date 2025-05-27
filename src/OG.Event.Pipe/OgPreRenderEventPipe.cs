using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Pipe;
public class OgPreRenderEventPipe : OgEventPipe<IOgPreRenderEvent>
{
    public override bool CanHandle(UnityEngine.Event value) => value.type is EventType.Repaint;
    protected override IOgPreRenderEvent InternalGetEvent(UnityEngine.Event sourceEvent) => new OgPreRenderEvent();
}