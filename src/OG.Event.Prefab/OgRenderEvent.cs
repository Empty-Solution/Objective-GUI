using DK.Matching;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgRenderEvent(IEnumerable<IOgGraphics> graphics) : OgEvent, IOgRenderEvent
{
    private          int                                                         m_ClipContextIndex = -1;
    private readonly List<OgClipContext>                                         m_ClipContexts     = [];
    private readonly List<IOgGraphicsContext>                                    m_Contexts         = [];
    private readonly DkTypeCacheMatcherProvider<IOgGraphicsContext, IOgGraphics> m_Provider         = new(graphics);
    public void Enter(Rect rect, Vector2 scrollOffset)
    {
        m_ClipContexts.Add(new(rect, Global, scrollOffset));
        m_ClipContextIndex++;
    }
    public void Exit()
    {
        m_ClipContextIndex--;
    }
    public void PushContext(IOgGraphicsContext ctx)
    {
        if(m_ClipContextIndex >= 0)
        {
            OgClipContext context = m_ClipContexts[m_ClipContextIndex];
            ctx.RenderRect = new(ctx.RenderRect.position + (Global - context.Global) + context.OriginalClipRect.position - context.ScrollOffset, ctx.RenderRect.size);
            context.Contexts.Add(ctx);
            return;
        }
        ctx.RenderRect = new(ctx.RenderRect.position + Global, ctx.RenderRect.size);
        m_Contexts.Add(ctx);
    }
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
        if(m_ClipContexts.Count == 0) return;
        foreach(OgClipContext clip in m_ClipContexts)
        {
            GUI.BeginClip(clip.GetRectToClip());
            foreach(IOgGraphicsContext context in clip.Contexts.OrderBy(c => c.ZOrder))
            {
                if(!m_Provider.TryGetMatcher(context, out IOgGraphics graphics)) continue;
                graphics.ProcessContext(context);
            }
            GUI.EndClip();
        }
        m_ClipContexts.Clear();
        m_ClipContextIndex = -1;
    }
} 