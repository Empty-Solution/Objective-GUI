using OG.DataTypes.Orientation;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Transformer;
public class OgFlexiblePositionTransformer : IOgTransformer
{
    public int Order { get; set; } = 90;
    public Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, IOgOptionsContainer options)
    {
        if(!options.TryGetValue("FlexiblePositionOrientation", out EOgOrientation orientation)) return rect;
        return orientation == EOgOrientation.HORIZONTAL ? new(rect.x + (lastRect.xMax - parentRect.x), rect.y, rect.width, rect.height)
                   : new(rect.x, rect.y + (lastRect.yMax - parentRect.y), rect.width, rect.height);
    }
}