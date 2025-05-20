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
    protected Rect m_Rect = Rect.zero;
    public OgTransformerRectGetter(IOgEventHandlerProvider provider, IOgOptionsContainer options)
    {
        Options = options;
        provider.Register(this);
    }
    public IOgOptionsContainer               Options        { get; }
    public IDkGetProvider<Rect>?             OriginalGetter { get; set; }
    public IOgEventCallback<IOgLayoutEvent>? LayoutCallback { get; set; }
    public Rect Get() => m_Rect;
    object IDkGetProvider.Get() => Get();
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        m_Rect = reason.Layout.ProcessLayout(OriginalGetter?.Get() ?? Rect.zero, Options);
        LayoutCallback?.Invoke(reason);
        return false;
    }
}