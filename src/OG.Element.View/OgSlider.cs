using DK.Common.DataTypes.Abstraction;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.View.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.View;

public abstract class OgSlider<TElement>(IOgEventProvider eventProvider) : OgScrollableDragView<TElement, float>(eventProvider), IOgSlider<TElement> where TElement : IOgElement
{
    public IDkRange<float>? Range { get; set; }
    public float ScrollStep { get; set; }

    protected override float CalculateValue(IOgMouseEvent reason, float value) =>
        Lerp(Range!.Min, Range.Max, InverseLerp(Rectangle!.Get(), reason.LocalMousePosition));
    protected abstract float InverseLerp(OgRectangle rect, OgVector2 mousePosition);

    protected override bool OnHoverMouseScroll(IOgMouseScrollEvent reason) =>
        ChangeValue(Clamp(Value!.Get() + (Sign(reason.ScrollDelta.Y) * ScrollStep), Range!.Min, Range.Max));

    protected static float Sign(float value) => value >= 0.0 ? 1f : -1f;
}