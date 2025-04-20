using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;

namespace OG.Element.Interactive;

public class OgVector<TElement, TScope>(string name, TScope scope, IOgTransform transform, Vector2 value, IDkRange<Vector2> range)
    : OgDraggableValueView<TElement, TScope, Vector2>(name, scope, transform, value) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected override Vector2 CalculateValue(OgEvent reason, Vector2 value)
    {
        Rect rect = Transform.LocalRect;
        Vector2 mousePosition = reason.MousePosition;

        Vector2 min = range.Min;
        Vector2 max = range.Max;

        float minX = min.x;
        float maxX = max.x;

        value.x = Mathf.Lerp(minX, maxX, Mathf.InverseLerp(rect.x, rect.xMax, mousePosition.x));

        float minY = min.y;
        float maxY = max.y;

        value.y = Mathf.Lerp(minY, maxY, Mathf.InverseLerp(rect.y, rect.yMax, mousePosition.y));

        return value;
    }
}