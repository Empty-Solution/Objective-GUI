using OG.Transformer.Options;
using UnityEngine;
namespace OG.Transformer.Transformers;
public class OgRelativeSizeTransformer : OgTransformerBase<OgRelativeSizeOption>
{
    public override int Order { get; set; } = 0;
    public override Rect Transform(
        Rect rect, Rect parentRect, Rect lastRect, int remaining,
        OgRelativeSizeOption option) =>
        new(rect.x, rect.y, parentRect.width * (1 + option.RelativeWidth), parentRect.height * (1 + option.RelativeHeight));
}