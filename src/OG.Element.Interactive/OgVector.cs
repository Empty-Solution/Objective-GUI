using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive;
public class OgVector<TElement>(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter, IDkFieldProvider<Vector2> value)
    : OgDraggableValueElement<TElement, Vector2>(name, provider, rectGetter, value), IOgVectorValueElement<TElement> where TElement : IOgElement
{
    public IDkReadOnlyRange<Vector2>? Range { get; set; }
    protected override Vector2 CalculateValue(IOgMouseEvent reason, Vector2 value)
    {
        Vector2 mousePosition = reason.LocalPosition;
        Vector2 min           = Range!.Min;
        Vector2 max           = Range.Max;
        Rect    rect          = ElementRect.Get();
        return new(Mathf.Lerp(min.x, max.x, Mathf.InverseLerp(rect.x, rect.xMax, mousePosition.x)),
                   Mathf.Lerp(min.y, max.y, Mathf.InverseLerp(rect.y, rect.yMax, mousePosition.y)));
    }
}