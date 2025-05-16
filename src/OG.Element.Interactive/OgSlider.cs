using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public abstract class OgSlider<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgDraggableValueElement<TElement, float>(name, provider, rectGetter), IOgSlider<TElement> where TElement : IOgElement
{
    public IDkReadOnlyRange<float>? Range { get; set; }
    protected override float CalculateValue(IOgMouseEvent reason, float value) =>
        Mathf.Lerp(Range!.Min, Range.Max, InverseLerp(ElementRect.Get(), reason.LocalPosition));
    protected abstract float InverseLerp(Rect rect, Vector2 mousePosition);
}