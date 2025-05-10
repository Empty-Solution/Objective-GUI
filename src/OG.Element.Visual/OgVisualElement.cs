using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Graphics;
using UnityEngine;
namespace OG.Element.Visual;
public abstract class OgVisualElement : OgElement, IOgVisualElement, IOgEventCallback<IOgRenderEvent>
{
    private bool               m_IsDirty = true;
    private OgGraphicsContext? m_RenderContext;
    protected OgVisualElement(string name, IOgEventHandlerProvider provider) : base(name, provider) => provider.Register(this);
    bool IOgEventCallback<IOgRenderEvent>.Invoke(IOgRenderEvent reason)
    {
        m_RenderContext ??= new();
        if(m_IsDirty)
        {
            m_RenderContext.Clear();
            BuildContext(m_RenderContext);
            m_IsDirty = false;
        }
        reason.Graphics.Render(m_RenderContext);
        return false;
    }
    public Color Color
    {
        get;
        set
        {
            if(field == value) return;
            field = value;
            MarkDirty();
        }
    } = Color.white;
    protected void MarkDirty() => m_IsDirty = true;
    protected abstract void BuildContext(OgGraphicsContext context);
}