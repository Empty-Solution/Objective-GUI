
using DK.Common.DataTypes.Abstraction;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.View;
using OG.Element.View.Abstraction;
using OG.Event.Abstraction;
using System;

namespace OG.Element.InteractableElements;

public class OgVector<TElement>(IOgEventProvider eventProvider) : OgDraggableValueView<TElement, OgVector2>(eventProvider), IOgVector<TElement>
    where TElement : IOgElement
{
    public IDkRange<OgVector2>? Range { get; set; }
    protected override OgVector2 CalculateValue(IOgMouseEvent reason, OgVector2 value)
    {
        var rect = Rectangle!.Get();
        var mousePosition = reason.LocalMousePosition;
        var min = Range!.Min;
        var max = Range.Max;

        value.X = (int)Lerp(min.X, max.X, InverseLerp(rect.X, rect.YMax, mousePosition.X));
        value.Y = (int)Lerp(min.Y, max.Y, InverseLerp(rect.Y, rect.YMax, mousePosition.Y));

        return value;
    }
}