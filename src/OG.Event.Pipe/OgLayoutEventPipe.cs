using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using UnityEngine;
using UeEvent = UnityEngine.Event;
namespace OG.Event.Pipe;
public class OgLayoutEventPipe(IEnumerable<IOgTransformer> transformers) : OgEventPipe<IOgLayoutEvent>
{
    public override bool CanHandle(UeEvent value) => value.type is EventType.Layout;
    protected override IOgLayoutEvent InternalGetEvent(UeEvent sourceEvent) => new OgLayoutEvent(transformers);
}