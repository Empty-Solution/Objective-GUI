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
        if(m_RenderContext is null) return false;
        reason.GetGraphics(m_RenderContext!).Render(m_RenderContext!);
        return false;
    }
    protected abstract void FillContext();
}