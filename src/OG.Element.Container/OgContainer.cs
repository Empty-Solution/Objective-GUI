using DK.Getting.Abstraction.Generic;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using System;
using System.Collections.Generic;
namespace OG.Element.Container;
public class OgContainer<TElement> : OgElement, IOgContainer<TElement> where TElement : IOgElement
{
    private readonly List<TElement> m_Elements = [];
    public OgContainer(IOgEventProvider eventProvider) : base(eventProvider)
    {
        eventProvider.RegisterHandler(new OgRecallMouseEventHandler<IOgMouseEvent>(this));
        eventProvider.RegisterHandler(new OgRecallEventHandler(this));
    }
    public IEnumerable<TElement> Elements                   => m_Elements;
    public bool                  Contains(TElement element) => m_Elements.Contains(element);
    public void Add(TElement element)
    {
        if(Contains(element)) throw new InvalidOperationException();
        m_Elements.Add(element);
    }
    public void Remove(TElement element)
    {
        if(m_Elements.Remove(element)) return;
        throw new InvalidOperationException();
    }
    public virtual bool ProcElementsForward(IOgEvent reason)
    {
        for(int i = 0; i < m_Elements.Count; i++)
            if(ProcElement(reason, m_Elements[i]))
                break;
        return true;
    }
    public bool ProcElementsBackward(IOgInputEvent reason)
    {
        for(int i = m_Elements.Count - 1; i >= 0; i--)
            if(ProcElement(reason, m_Elements[i]))
                break;
        return true;
    }
    public bool HandleMouse(IOgMouseEvent reason)
    {
        OgRectangle rect = Rectangle!.Get();
        reason.LocalMousePosition -= new OgVector2(rect.X, rect.Y);
        return true;
    }
    protected virtual bool ShouldProcElement(TElement element)
    {
        IDkGetProvider<bool>? isActive = element.IsActive;
        return isActive is null || isActive.Get();
    }
    protected virtual bool ProcElement(IOgEvent reason, TElement element) => ShouldProcElement(element) && element.Proc(reason) && reason.IsConsumed;
    public class OgRecallMouseEventHandler<TEvent>(IOgContainer<TElement> owner)
        : OgRecallInputEventHandler<TEvent>(owner) where TEvent : class, IOgMouseEvent
    {
        public override bool Handle(TEvent reason)
        {
            owner.HandleMouse(reason);
            return base.Handle(reason);
        }
    }
    public class OgRecallInputEventHandler<TEvent>(IOgContainer<TElement> owner) : OgEventHandlerBase<TEvent> where TEvent : class, IOgInputEvent
    {
        public override bool Handle(TEvent reason) => owner.ProcElementsBackward(reason);
    }
    public class OgRecallEventHandler(IOgContainer<TElement> owner) : IOgEventHandler
    {
        public bool CanHandle(IOgEvent value)  => true;
        public bool Handle(IOgEvent    reason) => owner.ProcElementsForward(reason);
    }
}