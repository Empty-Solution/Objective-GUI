using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.DataKit.Transformer;
public class OgTransformerRectGetter : IDkGetProvider<Rect>, IOgEventCallback<IOgLayoutEvent>
{
    protected        Rect                              m_Rect = Rect.zero;
    private readonly IOgOptionsContainer               m_Options;
    private readonly IOgEventCallback<IOgLayoutEvent>? m_LayoutContainer;
    public OgTransformerRectGetter(IOgEventHandlerProvider provider, IOgOptionsContainer options, IOgEventCallback<IOgLayoutEvent>? layoutContainer = null)
    {
        m_Options         = options;
        m_LayoutContainer = layoutContainer;
        provider.Register(this);
    }
    public Rect Get() => m_Rect;
    object IDkGetProvider.Get() => Get();
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        m_LayoutContainer!.Invoke(reason);
        m_Rect = reason.Layout.ProcessLayout(m_Options);
        return false;
    }
}