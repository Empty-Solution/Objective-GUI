using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Interactive;

public class OgVerticalSlider<TElement, TScope>(string name, TScope scope, IOgTransform transform, float value, IDkRange<float> range, float scrollStep)
    : OgSlider<TElement, TScope>(name, scope, transform, value, range, scrollStep) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected override float InverseLerp(Rect rect, Vector2 mousePosition) => Mathf.InverseLerp(rect.y, rect.yMax, mousePosition.y);
}