using OG.DataTypes.Orientation;
using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgFlexibleSizeTransformer : OgBaseTransformer<OgFlexibleSizeTransformerOption>
{
    public override int Order { get; set; } = 90;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgFlexibleSizeTransformerOption option)
    {
        float freeHorizontal = parentRect.width - (lastRect.xMax - parentRect.x);
        float freeVertical = parentRect.height - (lastRect.yMax - parentRect.y);
        float width = option.Orientation is EOgOrientation.HORIZONTAL or EOgOrientation.ALL ? Mathf.Max(freeHorizontal / remaining, 0) : parentRect.width;
        float height = option.Orientation is EOgOrientation.VERTICAL or EOgOrientation.ALL ? Mathf.Max(freeVertical / remaining, 0) : parentRect.height;
        return new(rect.x, rect.y, width, height);
    }
}