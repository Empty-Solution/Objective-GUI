#region

using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using System;
using System.Collections.Generic;

#endregion

namespace OG.Element.Container;

public class OgContainer<TElement> : OgElement, IOgContainer<TElement> where TElement : IOgElement
{
    protected readonly List<TElement> m_Element = [];

    public OgContainer(IOgEventProvider eventProvider) : base(eventProvider)
    {
        eventProvider.RegisterHandler(new OgRecallInputEventHandler(this));
        eventProvider.RegisterHandler(new OgRecallEventHandler(this));
    }

    public IEnumerable<TElement> Elements => m_Element;

    public bool Contains(TElement element) => m_Element.Contains(element);

    public void Add(TElement element)
    {
        if(Contains(element)) throw new InvalidOperationException();
        m_Element.Add(element);
    }

    public void Remove(TElement element)
    {
        if(m_Element.Remove(element)) return;
        throw new InvalidOperationException();
    }

    protected virtual bool ProcElementsForward(IOgEvent reason)
    {
        for(int i = 0; i < m_Element.Count; i++)
            if(ProcElement(reason, m_Element[i]))
                break;
        return true;
    }

    protected bool ProcElementsBackward(IOgEvent reason)
    {
        for(int i = m_Element.Count - 1; i >= 0; i--)
            if(ProcElement(reason, m_Element[i]))
                break;
        return true;
    }

    protected virtual bool ShouldProcElement(IOgEvent reason, TElement element)
    {
        IDkGetProvider<bool>? isActive = element.IsActive;
        return isActive is null || isActive.Get();
    }

    protected virtual bool ProcElement(IOgEvent reason, TElement element) => ShouldProcElement(reason, element) && element.Proc(reason) && reason.IsConsumed;

    private class OgRecallEventHandler(OgContainer<TElement> owner) : IOgEventHandler
    {
        public bool CanHandle(IOgEvent value) => true;
        public bool Handle(IOgEvent reason) => owner.ProcElementsForward(reason);
    }

    private class OgRecallInputEventHandler(OgContainer<TElement> owner) : OgEventHandlerBase<IOgInputEvent>
    {
        public override bool Handle(IOgInputEvent reason) => owner.ProcElementsBackward(reason);
    }
}