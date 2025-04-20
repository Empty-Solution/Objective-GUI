using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Interactive;

public abstract class OgSlider<TElement, TScope>(string name, TScope scope, IOgTransform transform, float value, IDkRange<float> range, float scrollStep)
    : OgDraggableValueView<TElement, TScope, float>(name, scope, transform, value) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected override float CalculateValue(OgEvent reason, float value)
    {
        float min = range.Min;
        float max = range.Max;
        return Mathf.Lerp(min, max, InverseLerp(Transform.LocalRect, reason.MousePosition));
    }

    protected override void HandleMouseScroll(OgEvent reason)
    {
        base.HandleMouseScroll(reason);
        if(!IsHovered) return;
        ChangeValue(Value + scrollStep, reason);
    }

    protected abstract float InverseLerp(Rect rect, Vector2 mousePosition);
}