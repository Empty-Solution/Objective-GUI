using DK.DataTypes.Abstraction;
using OG.DataTypes.Point;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.Interactable.Abstraction;
using OG.Element.View;
using OG.Event.Abstraction;
namespace OG.Element.Interactable;
public class OgVector<TElement>(IOgEventProvider eventProvider) : OgDraggableValueView<TElement, OgVector2>(eventProvider), IOgVector<TElement>
    where TElement : IOgElement
{
    public IDkReadOnlyRange<OgVector2>? Range { get; set; }
    protected override OgVector2 CalculateValue(IOgMouseEvent reason, OgVector2 value)
    {
        OgRectangle rect          = Rectangle!.Get();
        OgPoint   mousePosition = reason.LocalMousePosition;
        OgVector2   min           = Range!.Min;
        OgVector2   max           = Range.Max;
        value.X = (int)Lerp(min.X, max.X, InverseLerp(rect.X, rect.YMax, mousePosition.X));
        value.Y = (int)Lerp(min.Y, max.Y, InverseLerp(rect.Y, rect.YMax, mousePosition.Y));
        return value;
    }
}