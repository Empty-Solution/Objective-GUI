using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgFlexiblePositionTransformer : OgBaseTransformer<OgFlexiblePositionTransformerOption>
{
    public override int Order { get; set; } = 90;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgFlexiblePositionTransformerOption option) =>
        option.Orientation == EOgOrientation.HORIZONTAL
            ? new((lastRect == Rect.zero ? rect.x : lastRect.xMax) + option.Padding, rect.y, rect.width, rect.height) : new(rect.x,
                 (lastRect == Rect.zero ? rect.y : lastRect.yMax) + option.Padding, rect.width, rect.height);
}