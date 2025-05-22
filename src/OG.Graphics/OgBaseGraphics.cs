using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Graphics;
public abstract class OgBaseGraphics<TContext> : IOgGraphics<TContext> where TContext : class, IOgGraphicsContext
{
    private readonly List<TContext> m_Contexts = [];
    public void PushContext(TContext ctx) => m_Contexts.Add(ctx);
    public bool CanHandle(IOgGraphicsContext value) => value is TContext;
    public void PushContext(IOgGraphicsContext ctx) => PushContext((ctx as TContext)!);
    public void ProcessContexts()
    {
        foreach(TContext context in m_Contexts) ProcessContext(context);
        m_Contexts.Clear();
    }
    protected abstract void ProcessContext(TContext context);
}