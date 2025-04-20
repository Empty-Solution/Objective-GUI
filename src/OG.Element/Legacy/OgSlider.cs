using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public abstract class OgSlider<TElement, TStyle, TScope>(string name, TStyle style, float value, IDkRange<float> range, TScope scope, IOgTransform transform)
    : OgValueView<TElement, TStyle, TScope, float>(name, style, value, scope, transform)
    where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgStyle
{
    protected override float DoChangeValueElement(OgEvent reason, Rect rect, TStyle style, float original) => DoSlider(reason, rect, original, range.Min, range.Max);

    protected abstract float DoSlider(OgEvent reason, Rect rect, float original, float min, float max);
}