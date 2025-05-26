using DK.Matching;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgRenderEvent(IEnumerable<IOgGraphics> graphics) : OgEvent, IOgRenderEvent
{
    private readonly DkTypeCacheMatcherProvider<IOgGraphicsContext, IOgGraphics> m_Provider = new(graphics);
    private readonly List<IOgGraphicsContext>                                    m_Contexts = [];
    public void PushContext(IOgGraphicsContext ctx) => m_Contexts.Add(ctx);
    public Vector2 Global { get; set; }
    public void ProcessContexts()
    {
        if(m_Contexts.Count == 0) return;
        foreach(IOgGraphicsContext context in m_Contexts.OrderBy(c => c.ZOrder))
        {
            if(!m_Provider.TryGetMatcher(context, out IOgGraphics graphics)) continue;
            graphics.ProcessContext(context);
        }
        m_Contexts.Clear();
    }
}