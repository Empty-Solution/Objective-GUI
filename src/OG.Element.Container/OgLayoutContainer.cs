using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
using OG.Layout.Abstraction;
using System.Collections.Generic;
namespace OG.Element.Container;
public class OgLayoutContainer<TElement> : OgContainer<TElement>, IOgLayoutEventHandler
    where TElement : IOgElement
{
    public OgLayoutContainer(IOgEventProvider eventProvider, IEnumerable<IOgLayoutTool<TElement>> layoutTools) : base(eventProvider)
    {
        LayoutTools = layoutTools;
        eventProvider.RegisterHandler(new OgRecallLayoutEventHandler(this));
    }
    public IEnumerable<IOgLayoutTool<TElement>> LayoutTools { get; }
    public virtual bool HandleLayout(IOgLayoutEvent reason)
    {
        OgRectangle rect = Rectangle!.Get();
        foreach(IOgLayoutTool<TElement> tool in LayoutTools)
            foreach(TElement? element in Elements)
                if(!ShouldProcElement(element))
                    tool.ProcessElement(element, rect);
        return true;
    }
    public class OgRecallLayoutEventHandler(IOgLayoutEventHandler owner) : OgEventHandlerBase<IOgLayoutEvent>
    {
        public override bool Handle(IOgLayoutEvent reason) => owner.HandleLayout(reason);
    }
}