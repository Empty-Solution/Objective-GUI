using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.Interactable.Abstraction;
using OG.Element.View;
using OG.Event;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Element.Interactable;
public class OgScroll<TElement> : OgScrollableView<TElement, OgVector2>, IOgScroll<TElement>, IOgElementEventHandler<IOgRepaintEvent>
    where TElement : IOgElement
{
    private readonly OgClipRepaintContext m_Context = new();
    public OgScroll(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgEventHandler<IOgRepaintEvent>(this));
    bool IOgElementEventHandler<IOgRepaintEvent>.HandleEvent(IOgRepaintEvent reason) => !ProcElementsForward(reason) && OnRepaint(reason);
    public bool OnRepaint(IOgRepaintEvent reason)
    {
        OgRectangle rect = Rectangle!.Get();
        m_Context.RepaintRect = new(rect.Position + Value!.Get(), rect.Size);
        reason.GraphicsTool.Repaint(m_Context);
        return true;
    }
    protected override bool OnHoverMouseScroll(IOgMouseScrollEvent reason)
    {
        reason.Consume();
        return ChangeValue(Value!.Get() + reason.ScrollDelta);
    }
}