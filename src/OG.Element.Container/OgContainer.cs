using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OG.Element.Container;
public class OgContainer<TElement> : OgElement, IOgContainer<TElement>, IOgEventCallback<IOgEvent>, IOgEventCallback<IOgInputEvent>,
                                     IOgEventCallback<IOgRenderEvent>, IOgEventCallback<IOgLayoutEvent>,
                                     IOgEventCallback<IOgMouseEvent> where TElement : IOgElement
{
    private readonly List<TElement> m_Elements = [];
    public OgContainer(string name, IOgEventHandlerProvider provider) : base(name, provider)
    {
        provider.Register<IOgLayoutEvent>(this);
        provider.Register<IOgRenderEvent>(this);
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
    public bool Invoke(IOgEvent reason) => ProcessElementsEventForward(reason);
    public bool Invoke(IOgInputEvent reason) => ProcessElementsEventBackward(reason);
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        Rect                               lastRect     = Rect.zero;
        int                                count        = m_Elements.Count;
        Rect                               parentRect   = ElementRect;
        for(int i = 0; i < count; i++)
        {
            TElement element = m_Elements[i];
            Rect     rect    = new();
            foreach(IOgTransformer transformer in reason.Transformers)
            {
                if(!element.TryGetOption(transformer, out IOgTransformerOption option)) continue;
                rect = transformer.Transform(rect, parentRect, lastRect, count - i,
                                             option);
                //for(int j = 0; j < i; j++)
                //{
                //    TElement suspect = m_Elements[j];
                //    if(!suspect.ElementRect.Overlaps(rect)) continue;
                //    element.RemoveOption(option);
                //    rect = element.ElementRect;
                //    break;
                //}
                //element.ElementRect = rect;
            }
            element.ElementRect = rect;
            lastRect            = rect;
            element.ProcessEvent(reason);
        }
        return false;
    }
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