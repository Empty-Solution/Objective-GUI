using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
using OG.Layout.Abstraction;

namespace OG.Element.Container;

public class OgLayoutContainer<TElement> : OgContainer<TElement>, IOgLayoutEventHandler
    where TElement : IOgElement
{
    public OgLayoutContainer(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgLayoutEventHandler(this));
    public IOgLayoutTool<TElement>? LayoutTool { get; set; }

    public virtual bool HandleLayout(IOgLayoutEvent reason)
    {
        foreach(TElement? element in m_Element)
        {
            LayoutTool!.ProcessElement(element);
            OgRectangle elementRect = element.Rectangle!.Get();
            OgRectangle rect        = Rectangle!.Get();
            if(elementRect.X > rect.XMax || elementRect.Y > rect.YMax || ProcElement(reason, element))
                break;
        }

        return true;
    }

    public class OgLayoutEventHandler(IOgLayoutEventHandler owner) : OgEventHandlerBase<IOgLayoutEvent>
    {
        public override bool Handle(IOgLayoutEvent reason) => owner.HandleLayout(reason);
    }
}