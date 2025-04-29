using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.View.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.View;

public class OgScroll<TElement>(IOgEventProvider eventProvider) : OgScrollableView<TElement, OgVector2>(eventProvider), IOgScroll<TElement>
    where TElement : IOgElement
{
    protected override bool OnHoverMouseScroll(IOgMouseScrollEvent reason) => ChangeValue(Value!.Get() + reason.ScrollDelta);
}