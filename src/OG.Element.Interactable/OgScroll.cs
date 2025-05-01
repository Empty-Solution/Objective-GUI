using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.Interactable.Abstraction;
using OG.Element.View;
using OG.Event;
using OG.Event.Abstraction;
namespace OG.Element.Interactable;
public class OgScroll<TElement> : OgScrollableView<TElement, OgVector2>, IOgScroll<TElement> where TElement : IOgElement
{
    public OgScroll(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgRepaintEventHandler(this));
    public bool HandleRepaint(IOgRepaintEvent reason)
    {
        OgRectangle rect = Rectangle!.Get();
        reason.GraphicsTool.Clip(new(rect.Position + Value!.Get(), rect.Size));
        return true;
    }
    protected override bool OnHoverMouseScroll(IOgMouseScrollEvent reason)
    {
        reason.Consume();
        return ChangeValue(Value!.Get() + reason.ScrollDelta);
    }
    public class OgRepaintEventHandler(IOgScroll<TElement> owner) : OgEventHandlerBase<IOgRepaintEvent>
    {
        public override bool Handle(IOgRepaintEvent reason)
        {
            owner.HandleRepaint(reason);
            return true;
        }
    }
}