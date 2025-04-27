using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event;
using OG.Event.Abstraction;

namespace OG.Element.Container;

public abstract class OgScopedContainer<TElement> : OgContainer<TElement> where TElement : IOgElement
{
    public OgScopedContainer(IOgEventProvider eventProvider) : base(eventProvider) => eventProvider.RegisterHandler(new OgRepaintEventHandler(this));

    protected virtual bool OnRepaint(IOgRepaintEvent reason, OgRectangle rectangle)
    {
        using(reason.GraphicsTool.Clip(rectangle)) return ProcElementsForward(reason);
    }

    protected abstract DkScopeContext Scope(IOgRepaintEvent reason, OgRectangle rectangle);

    private class OgRepaintEventHandler(OgScopedContainer<TElement> owner) : OgEventHandlerBase<IOgRepaintEvent>
    {
        public override bool Handle(IOgRepaintEvent reason) => owner.OnRepaint(reason, owner.Rectangle?.Get() ?? new());
    }
}
