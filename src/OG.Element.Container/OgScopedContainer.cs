using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
using OG.Event.Abstraction.Handlers;
namespace OG.Element.Container;
public abstract class OgScopedContainer<TElement> : OgContainer<TElement>, IOgRepaintEventHandler<IOgRepaintEvent, bool> where TElement : IOgElement
{
    public OgScopedContainer(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgRepaintEventHandler(this));
    public virtual bool HandleRepaint(IOgRepaintEvent reason)
    {
        using(Scope(reason, Rectangle?.Get() ?? new())) return ProcElementsForward(reason);
    }
    protected abstract DkScopeContext Scope(IOgRepaintEvent reason, OgRectangle rectangle);
    public class OgRepaintEventHandler(IOgRepaintEventHandler<IOgRepaintEvent, bool> owner) : OgEventHandlerBase<IOgRepaintEvent>
    {
        public override bool Handle(IOgRepaintEvent reason) => owner.HandleRepaint(reason);
    }
}