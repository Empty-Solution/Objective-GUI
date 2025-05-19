using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgFlexibleSizeTransformer : OgBaseTransformer<OgFlexibleSizeTransformerOption>
{
    public override int Order { get; set; } = 90;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgFlexibleSizeTransformerOption option)
    {
        float occupied = option.Orientation == EOgOrientation.HORIZONTAL ? lastRect.xMax - parentRect.x : lastRect.yMax - parentRect.y;
        float free     = (option.Orientation == EOgOrientation.HORIZONTAL ? parentRect.width : parentRect.height) - occupied;
        return option.Orientation == EOgOrientation.HORIZONTAL ? new(rect.x, rect.y, Mathf.Max(free / remaining, 0), parentRect.height)
                   : new(rect.x, rect.y, parentRect.width, Mathf.Max(free / remaining, 0));
    }
}