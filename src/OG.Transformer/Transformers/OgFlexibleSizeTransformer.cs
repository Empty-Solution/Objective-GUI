using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgFlexibleSizeTransformer : OgTransformerBase<OgFlexibleSizeOption>
{
    public override int Order => 0;
    public override Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        OgFlexibleSizeOption option)
    {
        float occupied = option.Orientation == EOgOrientation.HORIZONTAL ? lastRect.xMax - parentRect.x : lastRect.yMax - parentRect.y;
        float free     = (option.Orientation == EOgOrientation.HORIZONTAL ? parentRect.width : parentRect.height) - occupied;
        float size = option.Relative is { } k
                         ? Mathf.Clamp(option.Orientation == EOgOrientation.HORIZONTAL ? parentRect.width : parentRect.height * k, 0, free)
                         : Mathf.Max(free / remaining, 0);
        return option.Orientation == EOgOrientation.HORIZONTAL ? new(parentRect.x + occupied, parentRect.y, size, parentRect.height)
                   : new(parentRect.x, parentRect.y + occupied, parentRect.width, size);
    }
}