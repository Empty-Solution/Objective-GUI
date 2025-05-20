using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgFlexibleTransformer : OgBaseTransformer<OgFlexibleTransformerOption>
{
    public override int Order { get; set; } = 90;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgFlexibleTransformerOption option)
    {
        float occupied                     = option.Orientation == EOgOrientation.HORIZONTAL ? lastRect.xMax - parentRect.x : lastRect.yMax - parentRect.y;
        if(lastRect == Rect.zero) occupied = 0;
        float free                         = (option.Orientation == EOgOrientation.HORIZONTAL ? parentRect.width : parentRect.height) - occupied;
        return option.Orientation == EOgOrientation.HORIZONTAL ? new(rect.x + occupied, rect.y, Mathf.Max(free / remaining, 0), parentRect.height)
                   : new(rect.x, rect.y + occupied, parentRect.width, Mathf.Max(free / remaining, 0));
    }
}