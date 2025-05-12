using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
namespace OG.Element.Container;
public class OgContainer<TElement> : OgElement, IOgContainer<TElement>, IOgEventCallback<IOgEvent>, IOgEventCallback<IOgInputEvent>,
                                     IOgEventCallback<IOgRenderEvent>, IOgEventCallback<IOgMouseEvent> where TElement : IOgElement
{
    private readonly List<TElement> m_Elements = [];
    public OgContainer(string name, IOgEventHandlerProvider provider, IOgOptionsContainer options) : base(name, provider, options)
    {
        provider.Register<IOgLayoutEvent>(this);
        provider.Register<IOgRenderEvent>(this);
        provider.Register<IOgInputEvent>(this);
        provider.Register<IOgEvent>(this);
    }
    public IEnumerable<TElement> Elements                   => m_Elements;
    public bool                  Contains(TElement element) => m_Elements.Contains(element);
    public bool Add(TElement element)
    {
        if(m_Elements.IndexOf(element) != -1) return false;
        m_Elements.Add(element);
        return true;
    }
    public bool Remove(TElement element)
    {
        int index = m_Elements.IndexOf(element);
        if(index == -1) return false;
        m_Elements.RemoveAt(index);
        return true;
    }
    public bool Invoke(IOgEvent      reason) => ProcessElementsEventForward(reason);
    public bool Invoke(IOgInputEvent reason) => ProcessElementsEventBackward(reason);
    public bool Invoke(IOgMouseEvent reason)
    {
        reason.LocalPosition -= ElementRect.position;
        bool isUsed = ProcessElementsEventForward(reason);
        reason.LocalPosition += ElementRect.position;
        return isUsed;
    }
    public virtual bool Invoke(IOgRenderEvent reason)
    {
        reason.Enter(ElementRect);
        bool isUsed = ProcessElementsEventForward(reason);
        reason.Exit();
        return isUsed;
    }
    public override bool Invoke(IOgLayoutEvent reason)
    {
        reason.ParentRect = ElementRect;
        int count = m_Elements.Count;
        for(int i = 0; i < count; i++)
        {
            TElement element = m_Elements[i];
            reason.RemainingLayoutItems = count - i;
            element.ProcessEvent(reason);
        }
        return false;
    }
    protected bool ProcessElementsEventForward(IOgEvent reason)
    {
        for(int i = 0; i < m_Elements.Count; i++)
        {
            if(!m_Elements[i].ProcessEvent(reason)) continue;
            return true;
        }
        return false;
    }
    protected bool ProcessElementsEventBackward(IOgInputEvent reason)
    {
        for(int i = m_Elements.Count - 1; i >= 0; i--)
        {
            if(!m_Elements[i].ProcessEvent(reason)) continue;
            return true;
        }
        return false;
    }
}