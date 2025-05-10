using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Element;
public class OgElement : IOgElement, IOgEventCallback<IOgLayoutEvent>
{
    private readonly IOgEventHandlerProvider m_Provider;
    public OgElement(string name, IOgEventHandlerProvider provider)
    {
        m_Provider = provider;
        Name       = name;
        IsActive   = true;
    }
    public bool                IsActive    { get; set; }
    public Rect                ElementRect { get; protected set; }
    public IOgOptionsContainer Options     { get; set; }
    public string              Name        { get; set; }
    public bool ProcessEvent(IOgEvent reason) => IsActive && m_Provider.Handle(reason);
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        Rect rect = new();
        foreach(IOgTransformer transformer in reason.Transformers)
        {
            if(!Options.Provider.TryGetMatcher(transformer, out IOgTransformerOption option)) continue;
            rect = transformer.Transform(rect, reason.ParentRect, reason.LastLayoutRect, reason.RemainingLayoutItems,
                                         option);
        }
        ElementRect           = rect;
        reason.LastLayoutRect = rect;
        return false;
    }
}