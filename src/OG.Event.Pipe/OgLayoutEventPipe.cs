using OG.Event.Prefab;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UeEvent = UnityEngine.Event;
namespace OG.Event.Pipe;
public class OgLayoutEventPipe : OgEventPipe<IOgLayoutEvent>
{
    private readonly IEnumerable<IOgTransformer> m_Transformers;
    public OgLayoutEventPipe(IEnumerable<IOgTransformer> transformers)
    {
        m_Transformers = transformers;
        m_Transformers = m_Transformers.OrderBy(o => o.Order);
    }
    public override bool CanHandle(UeEvent value) => value.type is EventType.Layout;
    protected override IOgLayoutEvent InternalGetEvent(UeEvent sourceEvent) => new OgLayoutEvent(m_Transformers);
}