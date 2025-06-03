using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OG.Element.Container;
public class OgContainer<TElement> : OgElement, IOgContainer<TElement>, IOgEventCallback<IOgInputEvent>, IOgEventCallback<IOgEvent>
    where TElement : IOgElement
{
    protected readonly List<TElement> m_Elements = [];
    public OgContainer(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : base(name, provider, rectGetter)
    {
        provider.Register<IOgInputEvent>(this);
        provider.Register<IOgRenderEvent>(this);
        provider.RegisterToEnd<IOgLayoutEvent>(this);
        provider.RegisterToEnd<IOgEvent>(this);
    }
    public IEnumerable<TElement> Elements => m_Elements;
    public bool                  Sort     { get; set; } = true;
    public override void Resort()
    {
        if(!Sort) return;
        m_Elements.Sort((e1, e2) => e1.CompareTo(e2));
        Parent?.Resort();
    }
    public void Clear() => m_Elements.Clear();
    public override long Order { get => m_Elements.Sum(e => e.Order); set => base.Order = value; }
    public bool Add(TElement element)
    {
        if(m_Elements.IndexOf(element) != -1) return false;
        m_Elements.Add(element);
        element.Parent = this;
        if(!Sort) return true;
        m_Elements.Sort((e1, e2) => e1.CompareTo(e2));
        return true;
    }
    public bool Remove(TElement element)
    {
        int index = m_Elements.IndexOf(element);
        if(index == -1) return false;
        m_Elements.RemoveAt(index);
        return true;
    }
    public int IndexOf(TElement element) => m_Elements.IndexOf(element);
    public virtual bool Invoke(IOgLayoutEvent reason)
    {
        reason.Layout.ParentRect     = ElementRect.Get();
        reason.Layout.LastLayoutRect = Rect.zero;
        int count = m_Elements.Count;
        for(int i = 0; i < count; i++)
        {
            TElement element = m_Elements[i];
            reason.Layout.RemainingLayoutItems = count - i - 1;
            element.ProcessEvent(reason);
            reason.Layout.LastLayoutRect = element.ElementRect.Get();
        }
        return false;
    }
    public virtual bool Invoke(IOgRenderEvent reason)
    {
        Rect rect = ElementRect.Get();
        reason.Global += rect.position;
        ProcessElementsEventForward(reason);
        reason.Global -= rect.position;
        return false;
    }
    public bool Invoke(IOgEvent reason) => ProcessElementsEventForward(reason);
    public virtual bool Invoke(IOgInputEvent reason)
    {
        Rect rect = ElementRect.Get();
        reason.LocalMousePosition -= rect.position;
        bool isUsed = ProcessElementsEventBackward(reason);
        reason.LocalMousePosition += rect.position;
        return isUsed;
    }
    protected bool ProcessElementsEventForward(IOgEvent reason)
    {
        for(int i = 0; i < m_Elements.Count; i++)
            if(m_Elements[i].ProcessEvent(reason)) return true;
        
        return false;
    }
    protected bool ProcessElementsEventBackward(IOgInputEvent reason)
    {
        for(int i = m_Elements.Count - 1; i >= 0; i--)
            if(m_Elements[i].ProcessEvent(reason)) return true;
        
        return false;
    }
}