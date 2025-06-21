using DK.Matching;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace OG.Event.Prefab;
public class OgRenderEvent(IEnumerable<IOgGraphics> graphics) : OgEvent, IOgRenderEvent
{
    private readonly List<IOgGraphicsContext> m_Contexts = new(256);
    private readonly DkTypeCacheMatcherProvider<IOgGraphicsContext, IOgGraphics> m_Provider = new(graphics);
    private int m_ClipContextIndex = -1;
    private OgClipContext?[] m_ClipContexts = [];
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Enter(Rect rect, Vector2 scrollOffset)
    {
        if(m_ClipContextIndex + 1 >= m_ClipContexts.Length) Array.Resize(ref m_ClipContexts, m_ClipContexts.Length == 0 ? 4 : m_ClipContexts.Length * 2);
        m_ClipContextIndex++;
        m_ClipContexts[m_ClipContextIndex] = new(rect, Global, scrollOffset);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Exit() => m_ClipContextIndex--;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PushContext(IOgGraphicsContext ctx)
    {
        if(m_ClipContextIndex >= 0)
        {
            var context = m_ClipContexts[m_ClipContextIndex];
            var rect = ctx.RenderRect;
            rect.position = ctx.RenderRect.position + (Global - context!.Value.Global) + context.Value.OriginalClipRect.position -
                            context.Value.ScrollOffset;
            ctx.RenderRect = rect;
            context.Value.Contexts.Add(ctx);
            return;
        }
        ctx.RenderRect = new(ctx.RenderRect.position + Global, ctx.RenderRect.size);
        m_Contexts.Add(ctx);
    }
    public Vector2 Global { get; set; }
    public void ProcessContexts()
    {
        if(m_Contexts.Count == 0) return;
        foreach(var context in m_Contexts.OrderBy(c => c.ZOrder))
        {
            if(!m_Provider.TryGetMatcher(context, out var graphics)) continue;
            graphics.ProcessContext(context);
        }
        m_Contexts.Clear();
        for(int i = 0; i < m_ClipContexts.Length; i++)
        {
            var clip = m_ClipContexts[i];
            if(clip is null) continue;
            GUI.BeginClip(clip.Value.GetRectToClip());
            foreach(var context in clip.Value.Contexts.OrderBy(c => c.ZOrder))
            {
                if(!m_Provider.TryGetMatcher(context, out var graphics)) continue;
                graphics.ProcessContext(context);
            }
            GUI.EndClip();
            m_ClipContexts[i] = null;
        }
        m_ClipContextIndex = -1;
    }
    private struct OgClipContext(Rect clipRect, Vector2 global, Vector2 scrollOffset)
    {
        public Rect OriginalClipRect => clipRect;
        public Vector2 Global { get; set; } = global;
        public Vector2 ScrollOffset => scrollOffset;
        public Rect GetRectToClip() => new(clipRect.position + Global, clipRect.size);
        public List<IOgGraphicsContext> Contexts { get; } = [];
    }
}