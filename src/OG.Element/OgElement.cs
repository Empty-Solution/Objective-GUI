using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Layout.Abstraction;
using UnityEngine;
namespace OG.Element;
public class OgElement : IOgElement, IOgEventCallback<IOgLayoutEvent>
{
    private           Rect                    m_LayoutRect;
    private readonly  string                  m_Name;
    private readonly  IOgEventHandlerProvider m_Provider;
    public OgElement(string name, IOgEventHandlerProvider provider)
    {
        m_Name     = name;
        m_Provider = provider;
        provider.Register(this);
    }
    public            bool                    IsActive                              { get; set; }
    public            string                  Name                                  => m_Name;
    public            bool                    ProcessEvent(IOgEvent reason)         => IsActive && m_Provider.Handler(reason);
    protected         Rect                    GetLayoutRect()                       => m_LayoutRect;
    protected virtual Rect                    OnLayout(IOgLayout layout, Rect rect) => rect;
    bool IOgEventCallback<IOgLayoutEvent>.Invoke(IOgLayoutEvent reason)
    {
        m_LayoutRect = OnLayout(reason.Layout, m_LayoutRect);
        return false;
    }
}