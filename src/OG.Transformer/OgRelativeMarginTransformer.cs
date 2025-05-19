using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer;
public class OgRelativeMarginTransformer : OgBaseTransformer<OgRelativeMarginTransformerOption>
{
    public override int Order { get; set; } = 20;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgRelativeMarginTransformerOption option) =>
        new(option.RelativeMarginX == 0 ? rect.x : parentRect.x * (1 + option.RelativeMarginX),
            option.RelativeMarginY == 0 ? rect.y : parentRect.y * (1 + option.RelativeMarginY), rect.width, rect.height);
}