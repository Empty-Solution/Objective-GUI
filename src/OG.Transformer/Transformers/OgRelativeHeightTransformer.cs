using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeHeightTransformer : OgTransformerBase<OgRelativeHeightOption>
{
    public override int Order { get; set; } = 0;
    public override Rect Transform(Rect rect, Rect parentRect, Rect lastRect, int remaining, OgRelativeHeightOption option) =>
        new(rect.x, rect.y, rect.width, parentRect.height * (1 + option.RelativeHeight));
}