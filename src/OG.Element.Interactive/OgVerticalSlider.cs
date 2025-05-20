using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgVerticalSlider<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<float> value, IDkReadOnlyRange<float>? range)
    : OgSlider<TElement>(name, provider, rectGetter, value, range) where TElement : IOgElement
{
    protected override float InverseLerp(Rect rect, Vector2 mousePosition) => Mathf.InverseLerp(rect.yMin, rect.yMax, mousePosition.y);
}