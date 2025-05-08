using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeMarginTransformer : OgTransformerBase<OgRelativeMarginOption>
{
    public override int Order => 1;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, OgRelativeMarginOption option) =>
        new(rect.x * (1 + option.RelativeMarginX), rect.y * (1 + option.RelativeMarginY), rect.width, rect.height);
}