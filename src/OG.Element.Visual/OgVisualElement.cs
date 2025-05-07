using OG.Element.Visual.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Graphics;
namespace OG.Element.Visual;
public abstract class OgVisualElement : OgElement, IOgVisualElement, IOgEventCallback<IOgRenderEvent>
{
    private bool               m_IsDirty;
    private OgGraphicsContext? m_RenderContext;
    protected OgVisualElement(string name, IOgEventHandlerProvider provider) : base(name, provider) => provider.Register<IOgRenderEvent>(this);
    bool IOgEventCallback<IOgRenderEvent>.Invoke(IOgRenderEvent reason)
    {
        m_RenderContext ??= new();
        if(m_IsDirty)
        {
            m_RenderContext.Clear();
            OnBuildContext(m_RenderContext);
            m_IsDirty = false;
        }
        reason.Graphics.Render(m_RenderContext);
        return false;
    }
    protected bool MarkDirty() => m_IsDirty = true;
    protected abstract void OnBuildContext(OgGraphicsContext context);
}