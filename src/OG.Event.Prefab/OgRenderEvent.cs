using DK.Matching;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgRenderEvent(IEnumerable<IOgGraphics> graphics) : OgEvent, IOgRenderEvent
{
    private readonly List<Rect>                                                  m_Clip     = [];
    private readonly List<IOgGraphicsContext>                                    m_Contexts = [];
    private readonly DkTypeCacheMatcherProvider<IOgGraphicsContext, IOgGraphics> m_Provider = new(graphics);
    public void Enter(Rect rect) => m_Clip.Add(rect);
    public void Exit() => m_Clip.RemoveAt(m_Clip.Count - 1);
    public void PushContext(IOgGraphicsContext ctx)
    {
        if(m_Clip.Count > 0)
        {
            Rect clipRect = m_Clip.LastOrDefault();
            clipRect.position += Global;
            ctx.ClipRect      =  clipRect;
        }
        m_Contexts.Add(ctx);
    }
    public Vector2 Global { get; set; }
    public void ProcessContexts()
    {
        if(m_Contexts.Count == 0) return;
        foreach(IOgGraphicsContext context in m_Contexts.OrderBy(c => c.ZOrder))
        {
            if(!m_Provider.TryGetMatcher(context, out IOgGraphics graphics)) continue;
            if(context.ClipRect != Rect.zero) GUI.BeginClip(context.ClipRect);
            graphics.ProcessContext(context);
            GUI.EndClip();
        }
        m_Contexts.Clear();
    }
}