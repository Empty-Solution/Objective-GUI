using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgFlexibleTransformer : OgTransformerBase<OgFlexibleOption>
{
    public override int Order => 0;
    public override Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        OgFlexibleOption option)
    {
        float occupied = option.Orientation == EOgOrientation.HORIZONTAL ? lastRect.xMax - parentRect.x : lastRect.yMax - parentRect.y;
        float free     = (option.Orientation == EOgOrientation.HORIZONTAL ? parentRect.width : parentRect.height) - occupied;
        float size = option.RelativeSize is { } k
                         ? Mathf.Clamp(option.Orientation == EOgOrientation.HORIZONTAL ? parentRect.width : parentRect.height * k, 0, free)
                         : Mathf.Max(free / remaining, 0);
        return option.Orientation == EOgOrientation.HORIZONTAL ? new(rect.x + occupied, rect.y, size, parentRect.height)
                   : new(rect.x, rect.y + occupied, parentRect.width, size);
    }
}