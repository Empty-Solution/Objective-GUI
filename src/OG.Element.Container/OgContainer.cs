using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using System.Collections.Generic;
namespace OG.Element.Container;
public class OgContainer<TElement> : OgElement, IOgContainer<TElement>, IOgEventCallback<IOgEvent>, IOgEventCallback<IOgInputEvent>
    where TElement : IOgElement
{
    private readonly List<TElement> m_Elements = [];
    public OgContainer(string name, IOgEventHandlerProvider provider) : base(name, provider)
    {
        provider.Register<IOgInputEvent>(this);
        provider.Register<IOgEvent>(this);
    }
    public IEnumerable<TElement> Elements => m_Elements;
    public bool Contains(TElement element) => m_Elements.Contains(element);
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
    bool IOgEventCallback<IOgEvent>.Invoke(IOgEvent reason)
    {
        reason.Enter(GetLayoutRect());
        bool isUsed = ProcessElementsEventForward(reason);
        reason.Exit();
        return isUsed;
    }
    bool IOgEventCallback<IOgInputEvent>.Invoke(IOgInputEvent reason)
    {
        reason.Enter(GetLayoutRect());
        bool isUsed = ProcessElementsEventBackward(reason);
        reason.Exit();
        return isUsed;
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
    protected bool ProcessElementsEventBackward(IOgEvent reason)
    {
        for(int i = m_Elements.Count - 1; i >= 0; i--)
        {
            if(!m_Elements[i].ProcessEvent(reason)) continue;
            return true;
        }
        return false;
    }
}