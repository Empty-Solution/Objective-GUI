using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Layout.Abstraction;
using System.Collections.Generic;
namespace OG.Element.Container;
public class OgLayoutContainer<TElement> : OgContainer<TElement>, IOgElementEventHandler<IOgLayoutEvent> where TElement : IOgElement
{
    private readonly IEnumerable<IOgLayoutTool<TElement>> m_LayoutTools;
    public OgLayoutContainer(IOgEventProvider eventProvider, IEnumerable<IOgLayoutTool<TElement>> layoutTools) : base(eventProvider)
    {
        m_LayoutTools = layoutTools;
        eventProvider.RegisterHandler(new OgEventHandler<IOgLayoutEvent>(this));
    }
    bool IOgElementEventHandler<IOgLayoutEvent>.HandleEvent(IOgLayoutEvent reason)
    {
        OgRectangle rect = Rectangle!.Get();
        foreach(IOgLayoutTool<TElement> tool in m_LayoutTools)
        foreach(TElement element in Elements)
            if(!ShouldProcElement(element))
                tool.ProcessElement(element, rect);
        return true;
    }
}