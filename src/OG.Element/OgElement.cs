using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Layout.Abstraction;
using UnityEngine;
namespace OG.Element;
public class OgElement : IOgElement, IOgEventCallback<IOgLayoutEvent>
{
    private readonly IOgEventHandlerProvider m_Provider;
    private          Rect                    m_LayoutRect;
    public OgElement(string name, IOgEventHandlerProvider provider)
    {
        Name       = name;
        m_Provider = provider;
        provider.Register(this);
    }
    public bool   IsActive { get; set; }
    public string Name     { get; }
    public bool ProcessEvent(IOgEvent reason) => IsActive && m_Provider.Handler(reason);
    bool IOgEventCallback<IOgLayoutEvent>.Invoke(IOgLayoutEvent reason)
    {
        m_LayoutRect = OnLayout(reason.Layout, m_LayoutRect);
        return false;
    }
    protected Rect GetLayoutRect() => m_LayoutRect;
    protected virtual Rect OnLayout(IOgLayout layout, Rect rect) => rect;
}