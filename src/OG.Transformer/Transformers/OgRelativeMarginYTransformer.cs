using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeMarginYTransformer : OgTransformerBase<OgRelativeMarginYOption>
{
    public override int Order { get; set; } = 20;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgRelativeMarginYOption option) =>
        new(rect.x, rect.y * (1 + option.RelativeMarginY), rect.width, rect.height);
}