using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.Interactable.Abstraction;
using OG.Element.View;
using OG.Event.Abstraction;

namespace OG.Element.Interactable;

public class OgScroll<TElement>(IOgEventProvider eventProvider) : OgScrollableView<TElement, OgVector2>(eventProvider), IOgScroll<TElement>
    where TElement : IOgElement
{
    protected override bool OnHoverMouseScroll(IOgMouseScrollEvent reason)
    {
        reason.Consume();
        return ChangeValue(Value!.Get() + reason.ScrollDelta);
    }
}