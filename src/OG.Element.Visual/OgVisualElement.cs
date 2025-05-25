using DK.Getting.Abstraction.Generic;
using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Element.Visual;
public abstract class OgVisualElement<TContext> : OgElement, IOgVisualElement, IOgEventCallback<IOgRenderEvent> where TContext : IOgGraphicsContext
{
    protected TContext? m_RenderContext;
    protected OgVisualElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : base(name, provider, rectGetter) =>
        provider.RegisterToEnd(this);
    public bool Invoke(IOgRenderEvent reason)
    {
        FillContext();
        if(m_RenderContext == null) return false;
        IOgGraphics graphics = reason.GetGraphics(m_RenderContext);
        Rect        rect     = ElementRect.Get();
        rect.position              += reason.Global;
        m_RenderContext.RenderRect =  rect;
        graphics.PushContext(m_RenderContext);
        return false;
    }
    protected abstract void FillContext();
}