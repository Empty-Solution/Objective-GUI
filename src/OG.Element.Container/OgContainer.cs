using DK.Getting.Abstraction.Generic;
using DK.Matching.Abstraction;
using OG.DataTypes.Point;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using System;
using System.Collections.Generic;
namespace OG.Element.Container;
public class OgContainer<TElement> : OgElement, IOgContainer<TElement>, IOgElementEventHandler<IOgEvent>, IOgElementEventHandler<IOgInputEvent>
    where TElement : IOgElement
{
    private readonly List<TElement> m_Elements = [];
    public OgContainer(IOgEventProvider eventProvider) : base(eventProvider)
    {
        eventProvider.RegisterHandler(new OgEventHandler<IOgInputEvent>(this));
        eventProvider.RegisterHandler(new OgEventHandler<IOgEvent>(this));
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
    protected bool ProcElementsForward(IOgEvent reason)
    {
        for(int i = 0; i < m_Elements.Count; i++) if(ProcElement(reason, m_Elements[i])) return true;
        return false;
    }
    protected bool ProcElementsBackward(IOgInputEvent reason)
    {
        for(int i = m_Elements.Count - 1; i >= 0; i--) if(ProcElement(reason, m_Elements[i])) return true;
        return false;
    }
    public bool HandleMouse(IOgMouseEvent reason)
    {
        OgRectangle rect = Rectangle!.Get();
        reason.Inline(new(rect.X, rect.Y));
        return true;
    }
    protected bool ShouldProcElement(TElement element)
    {
        IDkGetProvider<bool>? isActive = element.IsActive;
        return isActive is null || isActive.Get();
    }
    private bool ProcElement(IOgEvent reason, TElement element) => ShouldProcElement(element) && element.Proc(reason) && reason.IsConsumed;
    bool IOgElementEventHandler<IOgEvent>.HandleEvent(IOgEvent reason) => ProcElementsForward(reason);
    bool IOgElementEventHandler<IOgInputEvent>.HandleEvent(IOgInputEvent reason) => ProcElementsBackward(reason);
}