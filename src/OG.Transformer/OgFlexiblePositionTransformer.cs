using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgFlexiblePositionTransformer : OgBaseTransformer<OgFlexiblePositionTransformerOption>
{
    public override int Order { get; set; } = 1;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgFlexiblePositionTransformerOption option) =>
        option.Orientation == EOgOrientation.HORIZONTAL ? new(lastRect.xMax + option.Padding, rect.y, rect.width, rect.height)
            : new(rect.x, lastRect.yMax + option.Padding, rect.width, rect.height);
}