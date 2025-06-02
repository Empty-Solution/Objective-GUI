using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgInvertedFlexiblePositionTransformer : OgBaseTransformer<OgInvertedFlexiblePositionTransformerOption>
{
    public override int Order { get; set; } = 90;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgInvertedFlexiblePositionTransformerOption option) =>
        option.Orientation == EOgOrientation.HORIZONTAL
            ? new(lastRect.xMin - (remaining == 0 || lastRect == Rect.zero ? 0 : option.Padding), rect.y, rect.width, rect.height) : new(rect.x,
                lastRect.yMin - (remaining == 0 || lastRect == Rect.zero ? 0 : option.Padding), rect.width, rect.height);
}