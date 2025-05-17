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
    private readonly IOgOptionsContainer m_Options;
    protected        Rect                m_Rect = Rect.zero;
    public OgTransformerRectGetter(IOgEventHandlerProvider provider, IOgOptionsContainer options)
    {
        m_Options = options;
        provider.ForceRegister(this);
    }
    public IOgEventCallback<IOgLayoutEvent>? LayoutCallback { get; set; }
    public Rect Get() => m_Rect;
    object IDkGetProvider.Get() => Get();
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        m_Rect = reason.Layout.ProcessLayout(m_Options);
        LayoutCallback!.Invoke(reason);
        return false;
    }
}