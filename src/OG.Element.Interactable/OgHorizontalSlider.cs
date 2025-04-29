using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.Interactable;

public class OgHorizontalSlider<TElement>(IOgEventProvider eventProvider) : OgSlider<TElement>(eventProvider) where TElement : IOgElement
{
    protected override float InverseLerp(OgRectangle rect, OgVector2 mousePosition) => InverseLerp(rect.X, rect.XMax, mousePosition.X);
}