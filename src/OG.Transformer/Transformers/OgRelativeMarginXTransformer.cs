using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeMarginXTransformer : OgTransformerBase<OgRelativeMarginXOption>
{
    public override int Order { get; set; } = 20;
    public override Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        OgRelativeMarginXOption option) =>
        new(rect.x * (1 + option.RelativeMarginX), rect.y, rect.width, rect.height);
}