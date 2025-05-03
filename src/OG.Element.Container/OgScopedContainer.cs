using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;
namespace OG.Element.Container;
public abstract class OgScopedContainer<TElement> : OgContainer<TElement>, IOgElementEventHandler<IOgRepaintEvent> where TElement : IOgElement
{
    protected OgScopedContainer(IOgEventProvider eventProvider) : base(eventProvider) =>
        eventProvider.RegisterHandler(new OgEventHandler<IOgRepaintEvent>(this));
    bool IOgElementEventHandler<IOgRepaintEvent>.HandleEvent(IOgRepaintEvent reason)
    {
        using(Scope(reason, Rectangle?.Get() ?? new())) return ProcElementsForward(reason);
    }
    protected abstract DkScopeContext Scope(IOgRepaintEvent reason, OgRectangle rectangle);
}