using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgHorizontalSlider<TElement>(string name, IOgEventHandlerProvider provider, IOgOptionsContainer options)
    : OgSlider<TElement>(name, provider, options) where TElement : IOgElement
{
    protected override float InverseLerp(Rect rect, Vector2 mousePosition) => Mathf.InverseLerp(rect.xMin, rect.xMax, mousePosition.x);
}