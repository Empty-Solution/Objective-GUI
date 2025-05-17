using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public abstract class OgSlider<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<float> value)
    : OgDraggableValueElement<TElement, float>(name, provider, rectGetter, value), IOgSlider<TElement> where TElement : IOgElement
{
    public IDkReadOnlyRange<float>? Range { get; set; }
    protected override float CalculateValue(IOgMouseEvent reason, float value) =>
        Mathf.Lerp(Range!.Min, Range.Max, InverseLerp(ElementRect.Get(), reason.LocalPosition));
    protected abstract float InverseLerp(Rect rect, Vector2 mousePosition);
}