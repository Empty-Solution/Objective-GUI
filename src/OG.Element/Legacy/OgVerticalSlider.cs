using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public class OgVerticalSlider<TElement, TStyle, TScope>(string name, TStyle style, float value, IDkRange<float> range, TScope scope, IOgTransform transform)
    : OgSlider<TElement, TStyle, TScope>(name, style, value, range, scope, transform)
    where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgStyle
{
    protected override float DoSlider(OgEvent reason, Rect rect, float original, float min, float max) =>
        GUI.VerticalSlider(rect, original, min, max, GUIStyle.none, GUIStyle.none, GUIStyle.none);
}