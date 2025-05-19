using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgFlexiblePositionTransformer : OgBaseTransformer<OgFlexiblePositionTransformerOption>
{
    public override int Order { get; set; } = 90;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgFlexiblePositionTransformerOption option) =>
        option.Orientation == EOgOrientation.HORIZONTAL ? new(rect.x + (lastRect.xMax - parentRect.x), rect.y, rect.width, rect.height)
            : new(rect.x, rect.y + (lastRect.yMax - parentRect.y), rect.width, rect.height);
}