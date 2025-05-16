using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.DataKit.Transformer;
public class OgTransformerRectGetter(IOgOptionsContainer options, IOgEventCallback<IOgLayoutEvent>? layoutContainer = null)
    : IDkGetProvider<Rect>, IOgEventCallback<IOgLayoutEvent>
{
    protected Rect m_Rect = Rect.zero;
    public Rect Get() => m_Rect;
    object IDkGetProvider.Get() => Get();
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        layoutContainer!.Invoke(reason);
        m_Rect = reason.Layout.ProcessLayout(options);
        return false;
    }
}