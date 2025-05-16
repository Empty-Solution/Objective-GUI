using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgHorizontalSlider<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgSlider<TElement>(name, provider, rectGetter) where TElement : IOgElement
{
    protected override float InverseLerp(Rect rect, Vector2 mousePosition) => Mathf.InverseLerp(rect.xMin, rect.xMax, mousePosition.x);
}