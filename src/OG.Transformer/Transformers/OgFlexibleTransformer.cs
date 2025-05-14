using OG.DataTypes.Orientation;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgFlexibleTransformer : IOgTransformer
{
    public int Order { get; set; } = 90;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options)
    {
        if(!options.TryGetValue("FlexibleOrientation", out EOgOrientation orientation)) return rect;
        float occupied = orientation == EOgOrientation.HORIZONTAL ? lastRect.xMax - parentRect.x : lastRect.yMax - parentRect.y;
        float free     = (orientation == EOgOrientation.HORIZONTAL ? parentRect.width : parentRect.height) - occupied;
        return orientation == EOgOrientation.HORIZONTAL ? new(rect.x + occupied, rect.y, Mathf.Max(free / remaining, 0), parentRect.height)
                   : new(rect.x, rect.y + occupied, parentRect.width, Mathf.Max(free / remaining, 0));
    }
}